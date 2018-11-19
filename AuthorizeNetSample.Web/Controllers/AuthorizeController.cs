using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Authorize;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Web.Controllers.Base;
using AuthorizeNetSample.Web.Models;
using AuthorizeNetSample.Web.Models.Authorize;
using AutoMapper;
using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Contracts;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;
using EmbroideryOrderes.AuthorizePaymentSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Controllers {
    public class AuthorizeController : BaseController
    {
        public readonly ICustomerProfileService _customerProfileService;

        public AuthorizeController(IServiceHost serviceHost)
            : base(serviceHost) {
            _customerProfileService = new CustomerProfileService();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region [ Connection ]
        [HttpGet]
        public ActionResult GetRequestToken() {
            AuthorizeConfigDto authorizeConfig = _serviceHost.GetService<IAuthorizeService>().GetConfig();
            if (authorizeConfig == null) return null;

            OauthClient client = _CreateAuthorizeOauthClient(authorizeConfig);
            if (client == null) return Redirect(nameof(Index));

            GetRequestTokenPageViewModel model = Mapper.Map<GetRequestTokenPageViewModel>(authorizeConfig);
            model.OAuthUrl = client.OauthUrl();
            model.Scope = client.GetScope();
            model.State = client.GetState();
            model.Sub = client.GetSub();

            return View(model);
        }

        [HttpPost]
        public RedirectResult GetRequestToken(GetRequestTokenPageViewModel model) {
            //string clientId = "4dp5b7gRqk";
            
            return Redirect(model.OAuthUrl);
        }

        [HttpGet]
        public ActionResult GetAccessToken(string clientId, string clientSecret) {
            GetAccessTokenPageViewModel model = new GetAccessTokenPageViewModel {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetAccessToken(GetAccessTokenPageViewModel model) {
            Dictionary<string, string> keyValues = new Dictionary<string, string> {
                { "grant_type", "authorization_code" },
                { "code", model.Code },
                { "client_id", model.ClientId },
                { "client_secret", model.ClientSecret }
            };

            AccessTokenResponse responeData = await _AccessTokenResponseHelper(keyValues);

            return PartialView("Partials/_AccessTokenResponsePartial", responeData);
        }

        public async Task<PartialViewResult> RefreshAccessToken() {
            AuthorizeConfigDto configDto = _serviceHost.GetService<IAuthorizeService>().GetConfig();

            Dictionary<string, string> keyValues = new Dictionary<string, string> {
                { "grant_type", "refresh_token" },
                { "refresh_token", configDto.RefreshToken },
                { "client_id", configDto.ClientId },
                { "client_secret", configDto.ClientSecret }
            };

            AccessTokenResponse responeData = await _AccessTokenResponseHelper(keyValues);

            return PartialView("Partials/_AccessTokenResponsePartial", responeData);
        }

        private OauthClient _CreateAuthorizeOauthClient(AuthorizeConfigDto authorizeConfig) {
            string requestTokenUrl = WebConfigurationManager.AppSettings["AuthorizeRequestTokenUrl"];

            OauthClient client = new OauthClient(requestTokenUrl, authorizeConfig.ClientId, authorizeConfig.RedirectUri, "test1023");
            return client;
        }

        private async Task<AccessTokenResponse> _AccessTokenResponseHelper(Dictionary<string, string> keyValues) {
            HttpClient httpClient = new HttpClient();
            string accessTokenUrl = WebConfigurationManager.AppSettings["AuthorizeAccessTokenUrl"];

            FormUrlEncodedContent data = new FormUrlEncodedContent(keyValues);

            var response = await httpClient.PostAsync(accessTokenUrl, data);

            AccessTokenResponse responeData;
            if (response.StatusCode == HttpStatusCode.OK) {
                responeData = await response.Content.ReadAsAsync<AccessTokenResponse>(new[] { new JsonMediaTypeFormatter() });
            } else {
                responeData = null;
            }

            if (responeData != null) {
                AuthorizeConfigDto configDto = new AuthorizeConfigDto {
                    AccessToken = responeData.AccessToken,
                    RefreshToken = responeData.RefreshToken,
                    AccesssTokenExpiresIn = DateTime.Now.AddSeconds(responeData.ExpiresIn),
                    RefreshTokenExpiresIn = DateTime.Now.AddSeconds(responeData.RefreshTokenExpiresIn)
                };

                _serviceHost.GetService<IAuthorizeService>().StoreTokens(configDto);
            }

            return responeData;
        }
        #endregion
        #region [ Transactions ]
        [HttpGet]
        public ActionResult Customers() {
            List<CustomerDto> customers = _serviceHost.GetService<ICustomerService>().GetCustomersList();

            return View(Mapper.Map<List<CustomerViewModel>>(customers));
        }

        [HttpGet]
        public ActionResult ChargeCustomer(Guid customerId) {
            CustomerDto customer = _serviceHost.GetService<ICustomerService>().GetCustomerWithCardsAndAddresses(customerId);

            ChargeCustomerViewModel chargeCustomerViewModel = new ChargeCustomerViewModel {
                Customer = Mapper.Map<CustomerViewModel>(customer),
                CreditCardId = customer.CreditCards?.First().Id ?? Guid.Empty
            };
            return View(chargeCustomerViewModel);
        }

        [HttpPost]
        public PartialViewResult ChargeCustomer(ChargeCustomerViewModel viewModel) {
            ChargeCustomerDto charge = Mapper.Map<ChargeCustomerDto>(viewModel);
            AuthorizePaymentResponse paymentResponse = _serviceHost.GetService<IAuthorizeService>().ChargeCreditCard(charge);

            PaymentViewModel paymentViewModel = new PaymentViewModel {
                Amount = viewModel.Amount,
                TransactionId = paymentResponse.TransactionId,
                AuthKey = paymentResponse.AuthCode
            };

            return PartialView("Partials/_ChargeResponsePartial", paymentViewModel);
        }

        [HttpGet]
        public ActionResult VisaCheckout() {
            string VisaCheckoutApiKey = WebConfigurationManager.AppSettings["VisaCheckoutApiKey"];
            VisaCheckoutPaymentViewModel model = new VisaCheckoutPaymentViewModel {
                ApiKey = VisaCheckoutApiKey
            };

            return View(model);
        }

        [HttpPost]
        public PartialViewResult EncryptedVisaCheckoutData(EncryptVisaCheckoutDataViewModel model) {
            return PartialView("Partials/_EncryptedVisaCheckoutDataPartial", model);
        }

        [HttpPost]
        public PartialViewResult EncryptVisaCheckoutData(EncryptVisaCheckoutDataViewModel model) {
            EncryptVisaCheckoutDataDto request = Mapper.Map<EncryptVisaCheckoutDataDto>(model);

            DecryptedVisaCheckoutDataDto response = _serviceHost.GetService<IAuthorizeService>().DecryptVisaCheckoutPaymentData(request);

            return PartialView("Partials/_DecryptedVisaCheckoutDataPartial", Mapper.Map<DecryptedVisaCheckoutDataViewModel>(response));
        }

        [HttpGet]
        public ActionResult CreateCustomerProfile() {
            CreateCustomerAccountViewModel model = new CreateCustomerAccountViewModel {
                Id = Guid.NewGuid()
            };

            return View(model);
        }

        [HttpPost]
        public PartialViewResult CreateCustomerProfile(CreateCustomerAccountViewModel model) {
            ANetCustomerProfileModel profileModel = new ANetCustomerProfileModel {
                Email = model.Email,
                Id = model.Id.ToString(),
                Name = model.Description,
                PaymentProfiles = new List<PaymentProfileModel> {
                    new PaymentProfileModel {
                        BillTo = new ANetAddressModel {
                            Address = model.BillingAddress.Address,
                            City = model.BillingAddress.City,
                            Country = model.BillingAddress.Country,
                            Email = model.BillingAddress.Email,
                            State = model.BillingAddress.State,
                            Zip = model.BillingAddress.Zip,
                            FirstName = model.BillingAddress.FirstName,
                            LastName = model.BillingAddress.LastName
                        },
                        CreditCard = new ANetCardModel {
                            ExpirationDate = model.CreditCard.ExpDate,
                            Number = model.CreditCard.CardNum
                        },
                        Default = true,
                    }
                },
                Shippings = new List<ANetAddressModel> {
                    new ANetAddressModel {
                        Address = model.ShippingAddress.Address,
                        City = model.ShippingAddress.City,
                        Country = model.ShippingAddress.Country,
                        State = model.ShippingAddress.State,
                        Zip = model.ShippingAddress.Zip
                    }
                }
            };

            string ApiLoginId = WebConfigurationManager.AppSettings["AuthorizeApiLoginId"];
            string TransactionKey = WebConfigurationManager.AppSettings["AuthorizeTransactionKey"];

            ANetResponse<CustomerProfileResponse> response = _customerProfileService.CreateCustomerProfile(profileModel, ApiLoginId, TransactionKey, AuthorizeEnviromentsEnum.Sandbox);

            if (!response.IsSuccessful) return PartialView("Partials/_CreateCustomerProfileResponse", new CustomerAccountResponseViewModel());

            CustomerAccountResponseViewModel responseModel = new CustomerAccountResponseViewModel {
                CustomerId = response.ResponseObject.CustomerProfileId,
                PaymentProfiles = response.ResponseObject.CustomerPaymentProfileIds,
                ShippingProfiles = response.ResponseObject.CustomerShippingProfileIds
            };

            _serviceHost.GetService<ICustomerService>().CreateCustomer(Mapper.Map<ANetCustomerProfileModelDto>(profileModel), Mapper.Map<CustomerAccountResponseDto>(responseModel));

            return PartialView("Partials/_CreateCustomerProfileResponse", responseModel);
        }

        [HttpGet]
        public ActionResult GetAuthorizeCustomers() {
            //var response = _serviceHost.GetService<ICustomerService>().GetAuthorizeCustomersList();

            //List<CustomerPaymentProfileInfoViewModel> model = new List<CustomerPaymentProfileInfoViewModel>();
            //if (response != null) {
            //    foreach (CustomerDto customer in response) {
            //        model.Add(new CustomerPaymentProfileInfoViewModel {
            //            Company = $"{customer.FirstName} {customer.LastName} Company",
            //            FirstName = customer.FirstName,
            //            LastName = customer.LastName,
            //            CardNum = $"XXXX{customer.CreditCards.First().LastFourDigits}",
            //            PaymentProfileId = customer.CreditCards.First().AuthorizeId,
            //            ProfileId = customer.AuthorizeId
            //        });
            //    }
            //}

            string ApiLoginId = WebConfigurationManager.AppSettings["AuthorizeApiLoginId"];
            string TransactionKey = WebConfigurationManager.AppSettings["AuthorizeTransactionKey"];

            var response = _customerProfileService.GetPaymentProfiles(ApiLoginId, TransactionKey, AuthorizeEnviromentsEnum.Sandbox);

            List<CustomerPaymentProfileInfoViewModel> model = new List<CustomerPaymentProfileInfoViewModel>();
            if (response.IsSuccessful) {
                model = Mapper.Map<List<CustomerPaymentProfileInfoViewModel>>(response.ResponseObject);
            }

            return View(model);
        }

        #endregion
    }
}