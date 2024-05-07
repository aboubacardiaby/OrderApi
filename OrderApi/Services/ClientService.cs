using OrderApi.Data;
using OrderApi.Entities;
using System.Threading.Tasks;

namespace OrderApi.Services
{
    public class ClientService : IClientService
    {
        private readonly OrderDbContext dbContext;
        private readonly List<Client> _clients;

        public ClientService(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
            _clients = new List<Client>()
            {
                new Client
                {
                    ClientId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 1,
                            Orderdate = DateTime.Now,
                            ClientId = "1"
                        }
                    }
                },
                new Client
                {
                    ClientId = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 2,
                            Orderdate = DateTime.Now,
                            ClientId = "2"
                        }
                    }
                },
                new Client
                {
                    ClientId = 3,
                    FirstName = "John",
                    LastName = "Smith",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 3,
                            Orderdate = DateTime.Now,
                            ClientId = "3"
                        }
                    }
                },
                new Client
                {
                    ClientId = 4,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 4,
                            Orderdate = DateTime.Now,
                            ClientId = "4"
                        }
                    }
                },
                new Client
                {
                    ClientId = 5,
                    FirstName = "John",
                    LastName = "Jones",
                    Orders = new List<Order>()
                    {
                        new Order
                        {
                            OrderId = 5,
                            Orderdate = DateTime.Now,
                            ClientId = "5"
                        }
                    }
                },
                new Client
                {
                    ClientId = 6,
                    FirstName = "Jane",
                    LastName = "Jones",
                    Orders = new List<Order>
                    {
                        new Order()
                        {
                            OrderId = 6,
                            Orderdate = DateTime.Now,
                            ClientId = "6"
                        }
                    }
                }
            };
            if (dbContext.Clients.Count() == 0)
            {
                initialpopuylation();
            }
        }

        public void initialpopuylation()
        {
            if (_clients != null && _clients.Any())
            {
                foreach (var client in _clients)
                {
                    dbContext.Clients.Add(client);
                    dbContext.SaveChanges();
                }
            }
        }

        public Task<Client> CreateClientAsync(Client client)
        {
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            return Task.FromResult(client);
        }

        public Task<Client> GetClientByIdAsync(int clientId)
        {
            var client = dbContext.Clients.Find(clientId);
            return Task.FromResult(client);
        }

        public Task<List<Client>> Client()
        {
            return Task.FromResult(dbContext.Clients.ToList());
        }
    }
}
