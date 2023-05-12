using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace crudProdutos.Pages.UsuarioTest
{
    public class AlterarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AlterarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (Usuario == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Para garantir que o CPF e o E-mail não serão atualizados
            var usuario = await _context.Usuarios.Select(m => new { m.IdUsuario, m.Email, m.CPF }).FirstOrDefaultAsync();
            Usuario.Email = usuario.Email;
            Usuario.CPF = usuario.CPF;

            //ModelState.ClearValidationState("Usuario.Email");
            //ModelState.ClearValidationState("Usuario.CPF");

            //Evita erros.
            if (ModelState.Keys.Contains("Usuario.Email")) 
            {
                ModelState["Usuario.Email"].Errors.Clear();
                ModelState.Remove("Usuario.Email");
            }
            if (ModelState.Keys.Contains("Usuario.CPF"))
            {
                ModelState["Usuario.CPF"].Errors.Clear();
                ModelState.Remove("Usuario.CPF");
            }

            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Usuario).State = EntityState.Modified;
            _context.Attach(Usuario.Endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioAindaExiste(Usuario.IdUsuario))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToPage("./Listar");
        }

        private bool UsuarioAindaExiste(int id)
        {
            return _context.Usuarios.Any(m => m.IdUsuario == id);
        }
    }
}
