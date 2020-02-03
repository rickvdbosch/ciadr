namespace Azure.Configuration.Demos.Services
{
    public interface ICustomerConfigurationService
    {
        string GetSetting(string settingName);
    }
}