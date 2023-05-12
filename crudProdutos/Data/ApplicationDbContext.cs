using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crudProdutos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace crudProdutos.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Indicação de chave(primária) Composta pelo EF
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemPedido>()
                .HasKey(e => new { e.IdPedido, e.IdProduto });

            modelBuilder.Entity<Favorito>()
                .HasKey(e => new { e.IdUsuario, e.IdProduto });

            modelBuilder.Entity<Visitado>()
                .HasKey(e => new { e.IdUsuario, e.IdProduto });

        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Visitado> Visitados { get; set; }
    }
}
