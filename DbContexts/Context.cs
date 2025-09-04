using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.DbContexts;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Diretor> Diretores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diretor>()
            .HasMany(e => e.Filmes)
            .WithOne(e => e.Diretor)
            .HasForeignKey(e => e.DiretorId)
            .IsRequired();

        modelBuilder.Entity<Diretor>().HasData(
            new Diretor { Id = 1, Name = "Christopher Nolan" },
            new Diretor { Id = 2, Name = "Steven Spielberg" },
            new Diretor { Id = 3, Name = "Martin Scorsese" },
            new Diretor { Id = 4, Name = "Quentin Tarantino" },
            new Diretor { Id = 5, Name = "Greta Gerwig" }
        );

        modelBuilder.Entity<Filme>().HasData(
            // Nolan
            new Filme { Id = 1, Titulo = "Inception", Ano = 2010, DiretorId = 1 },
            new Filme { Id = 2, Titulo = "Interstellar", Ano = 2014, DiretorId = 1 },
            new Filme { Id = 3, Titulo = "Dunkirk", Ano = 2017, DiretorId = 1 },

            // Spielberg
            new Filme { Id = 4, Titulo = "Jurassic Park", Ano = 1993, DiretorId = 2 },
            new Filme { Id = 5, Titulo = "E.T. the Extra-Terrestrial", Ano = 1982, DiretorId = 2 },

            // Scorsese
            new Filme { Id = 6, Titulo = "The Wolf of Wall Street", Ano = 2013, DiretorId = 3 },
            new Filme { Id = 7, Titulo = "Goodfellas", Ano = 1990, DiretorId = 3 },

            // Tarantino
            new Filme { Id = 8, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 4 },
            new Filme { Id = 9, Titulo = "Django Unchained", Ano = 2012, DiretorId = 4 },

            // Greta Gerwig
            new Filme { Id = 10, Titulo = "Lady Bird", Ano = 2017, DiretorId = 5 },
            new Filme { Id = 11, Titulo = "Barbie", Ano = 2023, DiretorId = 5 }
        );
    }
}
