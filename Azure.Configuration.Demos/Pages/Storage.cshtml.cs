using System;
using System.IO;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Azure.Configuration.Demos.Pages
{
	public class StorageModel : PageModel
    {
		#region Fields

		private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public StorageModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		#endregion

		#region Properties

		public string NextSession { get; set; }

        #endregion

        public async Task OnGet()
        {
            var containerClient = new BlobContainerClient(
                new Uri(_configuration.GetValue<string>("sessioncontainer")),
                new DefaultAzureCredential());
            var blob = containerClient.GetBlobClient("next-session.txt");

            using var ms = new MemoryStream();
			await blob.DownloadToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(ms);

            NextSession = await reader.ReadToEndAsync();
        }
    }
}
