using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crudProdutos.Pages.ProdutoTestCompleto
{
    public class AlterarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Produto Produtos { get; set; }

        public string CaminhoImagem { get; set; }

        [BindProperty]
        [Display(Name = "Imagem do Produto")]
        public IFormFile ImagemProduto { get; set; } //Não é obrigatório para que o usuário ao alterar alguma informação, queira manter a imagem.

        public AlterarModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment; 
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produtos = await _context.Produto.FirstOrDefaultAsync(p => p.IdProduto == id && p.Estoque > 0);

            if (Produtos == null)
            {
                return NotFound();
            }

            CaminhoImagem = $"~/img/produto/{Produtos.IdProduto:D6}.png";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Produtos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //Se há uma imagem de produto submetida
                if (ImagemProduto != null)
                {
                    await AppUtils.ProcessarArquivoDeImagem(Produtos.IdProduto, ImagemProduto, _webHostEnvironment);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(Produtos.IdProduto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Listar");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.IdProduto == id);
        }
    }
}



