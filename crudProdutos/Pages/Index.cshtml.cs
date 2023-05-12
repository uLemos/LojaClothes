using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crudProdutos.Pages
{
    public class IndexModel : PageModel
    {
        private const int tamanhoPagina = 12;

        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _context; //-> Injeção de Dependência que está vindo da Startup.
        public int PaginaAtual { get; set; }
        public int QuantidadePaginas { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IList<Produto> Produtos { get; set; }

        //OnGetAsync dividido por Threads.
        public async Task OnGetAsync([FromQuery(Name = "q")]string termoBusca,
            [FromQuery(Name = "o")]int? ordem = 1, [FromQuery(Name = "p")] int? pagina = 1)
        {
            this.PaginaAtual = pagina.Value;

            var query = _context.Produto.AsQueryable();

            if (!string.IsNullOrEmpty(termoBusca))
            {
                query = query.Where(p => p.Nome.ToUpper().Contains(termoBusca.ToUpper()));
            }


            if (ordem.HasValue)
            {
                switch (ordem.Value)
                {
                    case 1:
                        //Produtos = Produtos.OrderBy(p => p.Nome).ToList();
                        query = query.OrderBy(p => p.Nome);
                        break;

                    case 2:
                        //Produtos = Produtos.OrderBy(p => p.Preco).ToList();
                        query = query.OrderBy(p => p.Preco);
                        break;

                    case 3:
                        //Produtos = Produtos.OrderByDescending(p => p.Preco).ToList();
                        query = query.OrderByDescending(p => p.Preco);
                        break;
                }
            }

            var queryCount = query;
            int quantidadeProdutos = queryCount.Count();
            this.QuantidadePaginas = Convert.ToInt32(Math.Ceiling(quantidadeProdutos * 1M / tamanhoPagina));
            query = query.Skip(tamanhoPagina * (this.PaginaAtual - 1)).Take(tamanhoPagina);

            Produtos = await query.ToListAsync();
        }
    }
}
