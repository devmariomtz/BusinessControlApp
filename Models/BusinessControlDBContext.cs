using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class BusinessControlDBContext : DbContext
    {
        public BusinessControlDBContext(DbContextOptions<BusinessControlDBContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ItemMenu> ItemsMenu { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Negocio>().ToTable("negocios");
            modelBuilder.Entity<Categoria>().ToTable("categoria");
            modelBuilder.Entity<ItemMenu>().ToTable("items_menu");
            modelBuilder.Entity<TipoUsuario>().ToTable("tipos_usuarios");
        }
    }
}