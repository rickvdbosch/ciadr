using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azure.Configuration.Demos
{
    public class Keyvault01Model : PageModel
	{
		public string Secret1 { get; set; }

		public void OnGet()
		{
			try
			{
                var sc = new SecretClient(
                    new Uri("https://ciadr-kv2.vault.azure.net/"),
                    new DefaultAzureCredential());
                var keyVaultSecret = sc.GetSecret(nameof(Secret1)).Value;
                Secret1 = keyVaultSecret.Value;
            }
			catch (Exception e)
			{
				Secret1 = e.Message;
			}
		}
	}
}