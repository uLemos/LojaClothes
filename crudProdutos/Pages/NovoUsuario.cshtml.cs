using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static ServiceStack.Diagnostics.Events;
using System.Threading.Tasks;

namespace crudProdutos.Pages
{
    public class NovoUsuarioModel : PageModel
    {
        public class Senhas
        {
            [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
            [StringLength(16, ErrorMessage = "O campo {0} deve ter pelo menos {2} e no máximo {1} caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Senha { get; set; }

            [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            [Compare("Senha", ErrorMessage = "A confirmação da senha confere com a senha informada.")]
            public string ConfirmacaoSenha { get; set; }

        }

        private ApplicationDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public NovoUsuarioModel(ApplicationDbContext context,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager; //Gerenciamento de perfis
            _roleManager = roleManager; 
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        [BindProperty]
        public Senhas SenhasUsuario { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //cria um novo objeto Usuario
            var usuario = new Usuario();
            usuario.Endereco = new Endereco();
            //novos usuarios sempre iniciam com essa situacao
            usuario.Situacao = Usuario.SituacaoUsuario.Cadastrado;
            var senhasUsuario = new Senhas();
            
            if (!await TryUpdateModelAsync(senhasUsuario, senhasUsuario.GetType(), nameof(senhasUsuario)))
                return Page();

            //tenta atualizar o novo objeto com os dados oriundos do formulario
            if (await TryUpdateModelAsync(usuario, Usuario.GetType(), nameof(Usuario)))
            {
                //verifica se o perfil de usuério "usuario" existe
                if (!await _roleManager.RoleExistsAsync("usuario"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("usuario"));
                }

                //verifica se ja existe um usuario com o e-mail informado
                var usuarioExistente = await _userManager.FindByEmailAsync(usuario.Email);

                if (usuarioExistente != null)
                {
                    //adiciona um erro na propriedade Email do cliente para que o campo apresente o erro no formularic
                    ModelState.AddModelError("Usuario.Email", "Já existe um usuario cadastrado com este e—mail.");
                    return Page();

                }
                //cria o objeto usuario Identity e adiciona ao perfil "cliente"
                var cliente = new AppUser()
                {
                    UserName = usuario.Email,
                    Email = usuario.Email,
                    PhoneNumber = usuario.Telefone,
                    Nome = usuario.Nome
                };

                //cria usuario no banco de dados
                var result = await _userManager.CreateAsync(cliente, senhasUsuario.Senha);


                //se a criacao do usuario Identity foi bem sucedida
                if (result.Succeeded)
                {
                    //associa o usuario ao perfil "cliente"
                    await _userManager.AddToRoleAsync(cliente, "usuario");
                    //adiciona o novo objeto cliente ao contexto de banco de dados atual e salva no banco
                    _context.Usuarios.Add(usuario);
                    int afetados = await _context.SaveChangesAsync();
                    //se salvou o cliente no banco de dados I
                    if (afetados > 0)
                    {
                        //Para enviar e-mail de confirmação e de ativação, verificar implementacão origem
                        //https://kenhaggerty.com/articles/article/aspnet-core-31-smtp-emailsender
                        //Tentar usar TempData
                        return RedirectToPage("/CadastroRealizado");
                    }
                    else
                    {
                        //exclui o usuário Identity criado
                        await _userManager.DeleteAsync(cliente);
                        ModelState.AddModelError("Usuario", "Não foi possível efetuar o cadastr. Verifique os e tente novamente. Se o problema persistir, entre em contato conosco.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError("Usuario.Email", "Não foi possível criar um usuário com este endereço de email! Use outro endereço de e-mail ou tente recuperar a senha do mesmo.");
                }
            }

            return Page();
        }
    }
}
