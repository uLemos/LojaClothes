using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace crudProdutos.Pages.ProdutoTestCompleto
{
    public class AdicionarModel : PageModel
    {

        [BindProperty]
        public Produto Produtos { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string CaminhoImagem { get; set; }

        [BindProperty]
        [Display(Name = "Imagem do Produto")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public IFormFile ImagemProduto { get; set; }

        public AdicionarModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            CaminhoImagem = "~/img/produto/sem_imagem.png";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImagemProduto == null)
            {
                return Page();
            }

            var produto = new Produto();

            if (await TryUpdateModelAsync(produto, Produtos.GetType(), nameof(Produto)))
            {
                _context.Produto.Add(produto);
                await _context.SaveChangesAsync();
                await AppUtils.ProcessarArquivoDeImagem(produto.IdProduto,
                    ImagemProduto, _webHostEnvironment);

                return RedirectToPage("./Listar");
            }

            return Page();
        }
    }
}
