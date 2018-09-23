using AuthorizeNetSample.DAL.Data.Protection;
using NUnit.Framework;

namespace AuthorizeNetSample.Tests.DAL
{
	[TestFixture]
	public class DataEncryptorTest
	{
		[Test]
		public void EncryptionTest()
		{
			//Declare
			string data = "testString123";

			//Action
			string protectedData = DataEncryptor.Encrypt(data);
			string unprotectedData = DataEncryptor.Decrypt(protectedData);

			//Assert
			Assert.AreEqual(data, unprotectedData);
			Assert.AreNotEqual(data, protectedData);
		}
	}
}
