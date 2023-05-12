using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace crudProdutos.Pages.UsuarioTest
{
    public class AdicionarModel : PageModel
    {

        [BindProperty] //Os dados preenchidos já são enviados para esta classe por conta desta notação.
        public Usuario Usuario { get; set; }

        private readonly ApplicationDbContext _context;

        public AdicionarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet() //Quando houver alguma resposta, ele irá retornar a própria página
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() //Colocando "OnPostAsync", ele já intercepta o retorno de um forms.
        {
            var usuario = new Usuario();
            Usuario.Endereco = new Endereco();
            //Novos Usuários sem iniciam com essa situação!
            usuario.Situacao = Usuario.SituacaoUsuario.Cadastrado;

            if(await TryUpdateModelAsync(usuario, Usuario.GetType(), nameof(Usuario)))
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Listar");
            }
            return Page();
        }
    }
}
