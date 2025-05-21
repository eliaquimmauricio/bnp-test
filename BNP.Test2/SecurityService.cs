using BNP.Test2.Interfaces;
using Microsoft.Extensions.Logging;

namespace BNP.Test2
{
    public class SecurityService
    {
		protected IDataBase DataBase { get; }
		protected ISecurityIntegration SecurityIntegration { get; }
		public ILogger<SecurityService>? Logger { get; }

		public SecurityService(IDataBase dataBase, ISecurityIntegration securityIntegration, ILogger<SecurityService>? logger)
		{
			DataBase = dataBase;
			SecurityIntegration = securityIntegration;
			Logger = logger;
		}

		public void RetriveStorePrice(List<string>? isins)
        {
			if (isins == null || isins.Count == 0)
				throw new ArgumentNullException("The ISINS cannot be empty!");

			foreach (string isin in isins)
			{
				if (isin.Length != 12)
				{
					Logger?.LogError($"This ISIN cannot be processed: {isin}");
					continue;
				}

				decimal price = SecurityIntegration.RetrievePrice(isin);

				DataBase.StorePrice(isin, price);
			}
        }
    }
}
