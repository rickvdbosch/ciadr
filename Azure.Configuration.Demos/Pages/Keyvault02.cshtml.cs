using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azure.Configuration.Demos
{
    public class Keyvault02Model : PageModel
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        public string Secret1 { get; set; }

        #endregion

        #region Constructors

        public Keyvault02Model(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        public void OnGet()
        {
            Secret1 = _configuration.GetValue<string>(nameof(Secret1));
        }
    }
}