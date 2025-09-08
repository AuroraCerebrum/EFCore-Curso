using System.Threading.Tasks;
using FuscaFilmes.API.Domain.Entities;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.API.EndpointHandler;

public static class DiretoresHandlers
{
    public static async Task<List<Diretor>> GetDiretoresAsync(IDiretorRepository diretorRepository)
    {
        return await diretorRepository.GetDiretoresAsync();
    }

    public static async Task<Diretor> GetDiretorByNameAsync(string name, IDiretorRepository diretorRepository)
    {
        return await diretorRepository.GetDiretorByNameAsync(name);
    }

    public static async Task<List<Diretor>> GetDiretorByIdAsync(int id, IDiretorRepository diretorRepository)
    {
        return await diretorRepository.GetDiretorByIdAsync(id);
    }

    public static async Task AddDiretorAsync(IDiretorRepository diretorRepository, Diretor diretor)
    {
        await diretorRepository.AddAsync(diretor);
        await diretorRepository.SaveChangesAsync();
    }

    public static async Task UpdateDiretorAsync(IDiretorRepository diretorRepository, Diretor diretorNovo)
    {
        await diretorRepository.UpdateAsync(diretorNovo);
        await diretorRepository.SaveChangesAsync();
    }
    
    public static async Task DeleteDiretorAsync(IDiretorRepository diretorRepository, int diretorId)
    {
        await diretorRepository.DeleteAsync(diretorId);
        await diretorRepository.SaveChangesAsync();
    }

}


