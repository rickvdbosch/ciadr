using Azure.Data.AppConfiguration;

namespace Azure.Configuration.Demos.Services
{
    public class CustomerConfigurationService : ICustomerConfigurationService
    {
        #region Fields

        private readonly string _clientId;
        private readonly ConfigurationClient _configurationClient;

        #endregion

        #region Constructors

        public CustomerConfigurationService(IConfiguration configuration, IAuthorizationContext authorizationContext)
        {
            _clientId = authorizationContext.ClientId;
            _configurationClient = new ConfigurationClient(configuration.GetValue<string>("AppConfigurationConnectionString"));
        }

        #endregion

        public string GetSetting(string settingName)
        {
            var setting = _configurationClient.GetConfigurationSetting($"{_clientId}:{settingName}").Value;

            return setting.Value;
        }
    }
}