using crudProdutos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace crudProdutos.Pages
{
    public class LoginModel : PageModel
    {

        public class DadosLogin
        {
            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [EmailAddress]
            [Display(Name = "E-mail")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Senha { get; set; }
            [Display(Name = "Lembrar de mim")]
            public bool Lembrar { get; set; }
        }

        private readonly SignInManager<AppUser> _signInManager;
        public LoginModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public DadosLogin Dados { get; set; }
        public string ReturnUrl { get; set; }

        //este decorator permite manter um valor entre duas requisicoes
        //neste caso, esta propriedade temporaria esta aqui para capturar
        //o valor vindo de outra pagina, caso tenha.
        //https:([www.learnrazorgages.comlrazor—Qagesltemgdata
        [TempData]
        public string MensagemDeErro { get; set; }
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(MensagemDeErro))
            {
                ModelState.AddModelError(string.Empty, MensagemDeErro);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // elimina o cookie anterior para garantir um processo de login novo
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {                                                                                               //não quero bloquear o usuário mesmo que ele erre seu login.
                var result = await _signInManager.PasswordSignInAsync(Dados.Email, Dados.Senha, Dados.Lembrar, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Reveja seus dados de acesso e tente novamente!");
                    return Page();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}