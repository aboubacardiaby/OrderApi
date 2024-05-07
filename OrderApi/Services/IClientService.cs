using OrderApi.Entities;
using System.Runtime.CompilerServices;

namespace OrderApi.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int clientId);

        Task<Client> CreateClientAsync(Client client);
        Task<List<Client>> Client();


    }
}
