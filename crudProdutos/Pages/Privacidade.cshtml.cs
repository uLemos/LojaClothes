using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace crudProdutos.Pages
{
    public class PrivacidadeModel : PageModel
    {
        private readonly ILogger<PrivacidadeModel> _logger;

        public PrivacidadeModel(ILogger<PrivacidadeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
