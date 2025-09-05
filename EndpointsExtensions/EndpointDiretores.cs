using System;
using FuscaFilmes.API.EndpointHandler;

namespace FuscaFilmes.API.EndpointsExtensions;

public static class EndpointDiretores
{
    public static void DiretoresEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/diretores", DiretoresHandlers.GetDiretores).WithOpenApi();

        app.MapGet("/diretores/agregacao{name}", DiretoresHandlers.GetDiretorByName).WithOpenApi();

        app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretorById).WithOpenApi();

        app.MapPost("/diretores", DiretoresHandlers.AddDiretor).WithOpenApi();

        app.MapPut("/diretores", DiretoresHandlers.UpdateDiretor).WithOpenApi();

        app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretor).WithOpenApi();
    }

}
