using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace crudProdutos.Pages.ProdutoTestCompleto
{
    public class ListarModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment ;

        public ListarModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IList<Produto> Produtos { get; set; }

        public async Task OnGetAsync()
        {
            Produtos = await _context.Produto.OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produto.FindAsync(id);

            if (produtos != null)
            {
                _context.Produto.Remove(produtos);
                if (await _context.SaveChangesAsync() > 0)
                {
                    var caminhoArquivoImagem = Path.Combine(
                        _webHostEnvironment.WebRootPath, "img\\produto", 
                        produtos.IdProduto.ToString("D6") + ".png");
                    if (System.IO.File.Exists(caminhoArquivoImagem))
                    {
                        System.IO.File.Delete(caminhoArquivoImagem);
                    }
                }
            }
            return RedirectToPage("./Listar");
        }
    }
}
