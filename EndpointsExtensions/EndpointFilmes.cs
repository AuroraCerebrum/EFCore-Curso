using System;
using FuscaFilmes.API.EndpointHandler;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointFilmes
{

    public static void FilmesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/filmes", FilmesHandlers.GetFilmes).WithOpenApi();

        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmeById).WithOpenApi();

        app.MapGet("/filmesEFFunction/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionByTitulo).WithOpenApi();

        app.MapGet("/filmesContains/byName/{titulo}", FilmesHandlers.GetFilmeContainsByTitulo).WithOpenApi();

        app.MapDelete("/filmes/{filmeid}", FilmesHandlers.DeleteFilme).WithOpenApi();

        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme).WithOpenApi();

        app.MapPatch("/filmesExecUpdate", FilmesHandlers.ExecuteUpdateFilme).WithOpenApi();
    }
}
