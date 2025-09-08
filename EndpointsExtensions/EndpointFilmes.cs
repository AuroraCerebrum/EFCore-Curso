using System;
using FuscaFilmes.API.EndpointHandler;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointFilmes
{

    public static void FilmesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/filmes", FilmesHandlers.GetFilmesAsync).WithOpenApi();

        app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmeByIdAsync).WithOpenApi();

        app.MapGet("/filmesEFFunction/byName/{titulo}", FilmesHandlers.GetFilmeEFFunctionByTituloAsync).WithOpenApi();

        app.MapGet("/filmesContains/byName/{titulo}", FilmesHandlers.GetFilmeContainsByTituloAsync).WithOpenApi();

        app.MapDelete("/filmes/{filmeid}", FilmesHandlers.DeleteFilmeAsync).WithOpenApi();

        app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilmeAsync).WithOpenApi();

        app.MapPatch("/filmesExecUpdate", FilmesHandlers.ExecuteUpdateFilmeAsync).WithOpenApi();
    }
}
