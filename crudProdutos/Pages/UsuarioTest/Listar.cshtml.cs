using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crudProdutos.Pages.UsuarioTest
{
    public class ListarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<Usuario> Usuarios { get; set; }

        public ListarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync() 
        {
            Usuarios = await _context.Usuarios.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {                                                   
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Listar");
        }
    }
}
