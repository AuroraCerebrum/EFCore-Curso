using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Domain.Entities;
using FuscaFilmes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointHandler;


public static class FilmesHandlers
{
    public static List<Filme> GetFilmeById(int id, Context context)
    {
        return context.Filmes
            .Where(filme => filme.Id == id)
            .Include(filme => filme.Diretores).ToList();
    }

    public static List<Filme> GetFilmes(Context context)
    {
        return context.Filmes
                .Include(filme => filme.Diretores)
                //.OrderBy(filme => filme.Ano)
                .OrderByDescending(filme => filme.Ano)
                .ThenBy(filme => filme.Titulo)
                //.ThenByDescending(filme => filme.Titulo)
                .ToList();
    }

    public static List<Filme> GetFilmeEFFunctionByTitulo(string titulo, Context context)
    {
        return context.Filmes
                .Where(filmes =>
                    EF.Functions.Like(filmes.Titulo, $"%{titulo}%"))
                .Include(filmes => filmes.Diretores).ToList();
    }

    public static List<Filme> GetFilmeContainsByTitulo(string titulo, Context context)
    {
        return context.Filmes
            .Where(filme => filme.Titulo.Contains(titulo))
            .Include(filme => filme.Diretores).ToList();

    }

    public static void DeleteFilme(int filmeid, Context context)
    {
        context.Filmes
            .Where(filme => filme.Id == filmeid)
            .ExecuteDelete<Filme>();
    }

    public static IResult UpdateFilme(Context context, FilmeUpdate filmeUpdate)
    {
        var filme = context.Filmes.Find(filmeUpdate.Id);

        if (filme == null)
        {
            return Results.NotFound("Filme não encontrado");
        }

        filme.Titulo = filmeUpdate.Titulo;
        filme.Ano = filmeUpdate.Ano;

        context.Filmes.Update(filme);
        context.SaveChanges();

        return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso");

    }
    
    public static IResult ExecuteUpdateFilme(Context context, FilmeUpdate filmeUpdate)
    {
        var linhasAfetadas = context.Filmes
            .Where(filme => filme.Id == filmeUpdate.Id)
            .ExecuteUpdate(setter => setter
                .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
                .SetProperty(f => f.Ano, filmeUpdate.Ano)
            );

        if (linhasAfetadas > 0)
        {
            return Results.Ok($"Você teve um total de {linhasAfetadas} linha(s) afetadas");
        }
        else
        {
            return Results.NoContent();
        }
    }
}