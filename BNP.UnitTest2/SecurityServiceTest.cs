using BNP.Test2;
using BNP.Test2.Interfaces;
using Moq;

namespace BNP.UnitTest2
{
	[TestClass]
	public sealed class SecurityServiceTest
	{
		private readonly SecurityService _service;
		private readonly Mock<IDataBase> _dataBase;
		private readonly Mock<ISecurityIntegration> _securityIntegration;

		public SecurityServiceTest()
		{
			_dataBase = new Mock<IDataBase>();
			_securityIntegration = new Mock<ISecurityIntegration>();

			_service = new SecurityService(_dataBase.Object, _securityIntegration.Object, null);
		}

		[TestMethod]
		public void RetriveStorePrice_Sucess_Test()
		{
			//Arrange
			_securityIntegration.Setup(s => s.RetrievePrice(It.IsAny<string>())).Returns(200.00M);

			List<string> isins = new List<string>
			{
				"123456789012"
			};

			//Act
			_service.RetriveStorePrice(isins);
		}

		[TestMethod]
		public void RetriveStorePrice_SucessWithInvalidISINS_Test()
		{
			//Arrange
			_securityIntegration.Setup(s => s.RetrievePrice(It.IsAny<string>())).Returns(200.00M);

			List<string> isins =
			[
				"123456789012",
				"123"
			];

			//Act
			_service.RetriveStorePrice(isins);

			//Assert
			_securityIntegration.Verify(s => s.RetrievePrice(It.IsAny<string>()), Times.Once);
			_dataBase.Verify(s => s.StorePrice(It.IsAny<string>(), It.IsAny<decimal>()), Times.Once);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void RetriveStorePrice_ListEmpty_Fail_Test()
		{
			//Act
			_service.RetriveStorePrice(null);
		}
	}
}
