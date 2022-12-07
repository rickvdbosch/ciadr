using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azure.Configuration.Demos.Pages
{
    public class IndexModel : PageModel
    {
        #region Field

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        public string Setting1 { get; set; }

        #endregion

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Setting1 = _configuration.GetValue<string>(nameof(Setting1));
        }
    }
}