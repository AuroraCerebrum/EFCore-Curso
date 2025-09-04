using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using FuscaFilmes.API.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
                      .LogTo(Console.WriteLine, LogLevel.Information)
);

// using (var context = new Context())
// {
//     context.Database.EnsureCreated();
// }

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/diretores", (Context context) =>
{
    return context.Diretores.Include(diretor => diretor.Filmes).ToList();
})
.WithOpenApi();

app.MapGet("/diretores/agregacao{name}", (string name,Context context) =>
{
    // return context.Diretores
    //         .Include(diretor => diretor.Filmes)
    //         //.Select(diretor => diretor.Name)
    //         .FirstOrDefault(diretor => diretor.Name.Contains(name))
    //         ?? new Diretor { Id = 999999, Name = "Nenhum diretor encontrado" };
            
    return context.Diretores
            .Include(diretor => diretor.Filmes)
            .Select(diretor => diretor.Name)
            .FirstOrDefault()
            ?? "Nenhum diretor encontrado";
})
.WithOpenApi();

app.MapGet("/diretores/where/{id}", (int id,
    Context context) =>
{
    return context.Diretores
            .Include(diretor => diretor.Filmes)
            .Where(diretor => diretor.Id == id)
            .ToList();
})
.WithOpenApi();

app.MapGet("/filmes/{id}", (int id,
    Context context) =>
{
    return context.Filmes
    .Where(filme => filme.Id == id)
    .Include(filme => filme.Diretor).ToList();
})
.WithOpenApi();

app.MapGet("/filmes", (Context context) =>
{
    return context.Filmes
            .Include(filme => filme.Diretor)
            //.OrderBy(filme => filme.Ano)
            .OrderByDescending(filme => filme.Ano)
            .ThenBy(filme => filme.Titulo)
            //.ThenByDescending(filme => filme.Titulo)
            .ToList();
})
.WithOpenApi();

app.MapGet("/filmesEFFunction/byName/{titulo}", (string titulo,
    Context context) =>
{
    return context.Filmes
            .Where(filmes => 
                EF.Functions.Like(filmes.Titulo, $"%{titulo}%"))
            .Include(filmes => filmes.Diretor).ToList();
})
.WithOpenApi();

app.MapGet("/filmesLinq/byName/{titulo}", (string titulo,
    Context context) =>
{
    return context.Filmes
            .Where(filme => filme.Titulo.Contains(titulo))
            .Include(filme => filme.Diretor).ToList();

})
.WithOpenApi();

app.MapPost("/diretores", (Context context, Diretor diretor) =>
{
    context.Diretores.Add(diretor);
    context.SaveChanges();
})
.WithOpenApi();

app.MapPut("/diretores/{diretorId}", (Context context, int diretorId, Diretor diretorNovo) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
    {
        diretor.Name = diretorNovo.Name;
        if (diretorNovo.Filmes.Count > 0)
        {
            diretor.Filmes.Clear();
            foreach (var filme in diretorNovo.Filmes)
            {
                diretor.Filmes.Add(filme);
            }
        }

    }
    
    context.SaveChanges();
})
.WithOpenApi();

app.MapDelete("/filmes/{filmeid}", (Context context, int filmeid) =>
{
    context.Filmes
        .Where(filme => filme.Id == filmeid)
        .ExecuteDelete<Filme>();
})
.WithOpenApi();

app.MapPatch("/filmesUpdate", (Context context, FilmeUpdate filmeUpdate) =>
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
 
})
.WithOpenApi();


app.MapPatch("/filmesExecUpdate", (Context context, FilmeUpdate filmeUpdate) =>
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
})
.WithOpenApi();



app.MapDelete("/diretores/{diretorId}", (Context context, int diretorId) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
        context.Diretores.Remove(diretor);

    context.SaveChanges();
})
.WithOpenApi();

app.Run();

