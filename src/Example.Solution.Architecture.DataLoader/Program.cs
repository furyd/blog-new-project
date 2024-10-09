using Bogus;
using Example.Solution.Architecture.DataLoader;
using Example.Solution.Architecture.Domain.Factories.Implementation;
using Example.Solution.Architecture.Domain.Factories.Interfaces;
using Example.Solution.Architecture.Domain.Repositories.Implementation;
using Example.Solution.Architecture.Domain.Repositories.Models;
using Microsoft.Extensions.Options;

var databaseSettings = Options.Create(new DatabaseSettings { ConnectionString = args[0] });

var connectionFactory = new SqlServerConnectionFactory<DatabaseSettings>(databaseSettings);

var customers = new Faker<Customer>()
    .RuleFor(model => model.Id, faker => faker.Random.Guid())
    .RuleFor(model => model.GivenName, faker => faker.Name.FirstName())
    .RuleFor(model => model.FamilyName, faker => faker.Name.LastName())
    .Generate(10);

await LoadCustomers(connectionFactory, customers);

return;


static async Task LoadCustomers(IConnectionFactory factory, ICollection<Customer> customers)
{
    var repository = new SqlServerCustomersRepository(factory);

    foreach (var customer in customers)
    {
        await repository.Create(customer);
    }
}