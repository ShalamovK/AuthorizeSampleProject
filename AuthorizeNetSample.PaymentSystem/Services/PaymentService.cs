using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNetSample.Common.Contracts;
using AuthorizeNetSample.Common.Models.Payment;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.PaymentSystem.Services
{
	public class PaymentService : IPaymentService
	{
		public PaymentResponse ProcessCreditCardPayment(CreditCardPaymentRequest creditCardPayment)
		{
			//TODO: Make errors more informative
			if (creditCardPayment.Card == null || creditCardPayment.LineItems == null)
			{
				return new PaymentResponse
				{
					Success = false,
					Message = "Bad request"
				};
			}

			creditCardType creditCard = new creditCardType
			{
				cardCode = creditCardPayment.Card.CVC,
				cardNumber = creditCardPayment.Card.CardNumber,
				expirationDate = creditCardPayment.Card.ExpDate,
			};

			customerAddressType shipAddress = null;
			customerAddressType billAddress = null;

			if (creditCardPayment.BillAddress != null)
			{
				billAddress = new customerAddressType
				{
					firstName = creditCardPayment.BillAddress.FirstName,
					lastName = creditCardPayment.BillAddress.LastName,
					city = creditCardPayment.BillAddress.City,
					country = creditCardPayment.BillAddress.Country,
					address = creditCardPayment.BillAddress.Address,
					zip = creditCardPayment.BillAddress.Zip
				};
			}

			if (creditCardPayment.ShipAddress != null)
			{
				shipAddress = new customerAddressType
				{
					firstName = creditCardPayment.ShipAddress.FirstName,
					lastName = creditCardPayment.ShipAddress.LastName,
					city = creditCardPayment.ShipAddress.City,
					country = creditCardPayment.ShipAddress.Country,
					address = creditCardPayment.ShipAddress.Address,
					zip = creditCardPayment.ShipAddress.Zip
				};
			}

			List<lineItemType> lines = new List<lineItemType>();
			List<InvoiceLine> requestLines = creditCardPayment.LineItems;

			for (int i = 0; i < creditCardPayment.LineItems.Count; i++)
			{
				lines.Add(new lineItemType
				{
					itemId = requestLines[i].Id,
					name = requestLines[i].Name,
					unitPrice = requestLines[i].Price,
					quantity = requestLines[i].Quantity
				});
			}

			paymentType paymentType = new paymentType
			{
				Item = creditCard
			};

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType
			{
				Item = creditCardPayment.Authentication.ApiTransactionKey,
				name = creditCardPayment.Authentication.ApiLoginId,
				ItemElementName = ItemChoiceType.transactionKey
			};

			transactionRequestType requestType = new transactionRequestType
			{
				amount = creditCardPayment.Amount,
				billTo = billAddress,
				shipTo = shipAddress,
				payment = paymentType,
				lineItems = lines.ToArray()
			};

			createTransactionRequest request = new createTransactionRequest
			{
				transactionRequest = requestType
			};

			createTransactionController controller = new createTransactionController(request);
		}
	}
}
