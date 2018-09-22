using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNetSample.PaymentSystem.Contracts;
using AuthorizeNetSample.PaymentSystem.Common;
using AuthorizeNetSample.PaymentSystem.Helpers;
using AuthorizeNetSample.PaymentSystem.Models.Payment;
using System.Collections.Generic;
using AuthorizeNetSample.PaymentSystem.Services.Base;
using AuthorizeNetSample.PaymentSystem.Requests;

namespace AuthorizeNetSample.PaymentSystem.Services
{
	public class PaymentService : BaseService, IPaymentService
	{
		public PaymentResponse ProcessCreditCardPayment(CreditCardPaymentRequest request)
		{
			if (request.Card == null)
			{
				return new PaymentResponse
				{
					Success = false,
					Message = PaymentErrorsEnum.CreditCardNotFound.GetDescription()
				};
			}

			creditCardType creditCard = new creditCardType
			{
				cardCode = request.Card.CVC,
				cardNumber = request.Card.CardNumber,
				expirationDate = request.Card.ExpDate,
			};

			customerAddressType shipAddress = null;
			customerAddressType billAddress = null;

			if (request.BillAddress != null)
			{
				billAddress = new customerAddressType
				{
					firstName = request.BillAddress.FirstName,
					lastName = request.BillAddress.LastName,
					city = request.BillAddress.City,
					country = request.BillAddress.Country,
					address = request.BillAddress.Address,
					zip = request.BillAddress.Zip
				};
			}

			if (request.ShipAddress != null)
			{
				shipAddress = new customerAddressType
				{
					firstName = request.ShipAddress.FirstName,
					lastName = request.ShipAddress.LastName,
					city = request.ShipAddress.City,
					country = request.ShipAddress.Country,
					address = request.ShipAddress.Address,
					zip = request.ShipAddress.Zip
				};
			}

			List<lineItemType> lines = new List<lineItemType>();

			if (request.LineItems?.Count > 0)
			{
				List<InvoiceLine> requestLines = request.LineItems;

				for (int i = 0; i < request.LineItems.Count; i++)
				{
					lines.Add(new lineItemType
					{
						itemId = requestLines[i].Id,
						name = requestLines[i].Name,
						unitPrice = requestLines[i].Price,
						quantity = requestLines[i].Quantity
					});
				}
			}

			paymentType paymentType = new paymentType
			{
				Item = creditCard
			};

			Init(request);

			transactionRequestType requestType = new transactionRequestType
			{
				amount = request.Amount,
				billTo = billAddress,
				shipTo = shipAddress,
				payment = paymentType,
				lineItems = lines.ToArray()
			};

			createTransactionRequest transactionRequest = new createTransactionRequest
			{
				transactionRequest = requestType
			};

			createTransactionController controller = new createTransactionController(transactionRequest);
			controller.Execute();

			var response = controller.GetApiResponse();

			if (response == null)
				return new PaymentResponse
				{
					Success = false,
					Message = PaymentErrorsEnum.NullResponse.GetDescription()
				};

			if (response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transactionResponse.messages != null)
				{
					string message = response.transactionResponse.messages[0].description;
					string transactionId = response.transactionResponse.transId;
					string authCode = response.transactionResponse.authCode;

					return new PaymentResponse
					{
						Success = true,
						TransactionId = transactionId,
						AuthKey = authCode,
						Message = message
					};
				}
				else
				{
					string message;

					if (response.transactionResponse.errors != null)
						message = response.transactionResponse.errors[0].errorText;
					else
						message = PaymentErrorsEnum.TransactionFailed.GetDescription();

					return new PaymentResponse
					{
						Success = false,
						Message = message
					};
				}
			}
			else
			{
				string message;

				if(response.transactionResponse?.errors != null)
				{
					message = response.transactionResponse.errors[0].errorText;
				}
				else
				{
					message = response.messages.message[0].text;
				}

				return new PaymentResponse
				{
					Success = false,
					Message = message
				};
			}
		}
	}
}
