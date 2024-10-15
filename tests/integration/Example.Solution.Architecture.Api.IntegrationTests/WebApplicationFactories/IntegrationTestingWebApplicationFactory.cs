﻿using Example.Solution.Architecture.Api.IntegrationTests.Models;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Text.Json;
using Example.Solution.Architecture.Api.Models;

namespace Example.Solution.Architecture.Api.IntegrationTests.WebApplicationFactories;

public sealed class IntegrationTestingWebApplicationFactory : WebApplicationFactory<Program>
{
    private List<Customer> _customers = [];

    public static WebApplicationFactory<Program> Create() => new IntegrationTestingWebApplicationFactory();

    public static WebApplicationFactory<Program> Create(IEnumerable<Customer> customers) => new IntegrationTestingWebApplicationFactory().WithCustomers(customers);

    private IntegrationTestingWebApplicationFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(SwitchDatabase);

        base.ConfigureWebHost(builder);
    }

    private IntegrationTestingWebApplicationFactory WithCustomers(IEnumerable<Customer> customers)
    {
        _customers = customers.ToList();
        return this;
    }

    private void SwitchDatabase(IServiceCollection services)
    {
        var mockRepository = new Mock<ICustomersRepository>();

        mockRepository.Setup(repository => repository.List(new Pagination(20, 1))).ReturnsAsync(_customers.Count == 0 ? [] : [JsonSerializer.Serialize(_customers)]);

        mockRepository.Setup(repository => repository.Create(It.IsAny<ICustomer>())).ReturnsAsync((ICustomer customer) =>
        {
            var id = Guid.NewGuid();

            _customers.Add(new Customer
            {
                Id = id,
                GivenName = customer.GivenName,
                FamilyName = customer.FamilyName
            });

            return id;
        });

        mockRepository.Setup(repository => repository.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var customer = _customers.FirstOrDefault(customer => customer.Id == id);

            if (customer is null)
            {
                return [];
            }

            return [JsonSerializer.Serialize(customer)];
        });

        mockRepository.Setup(repository => repository.Update(It.IsAny<Guid>(), It.IsAny<ICustomer>())).Returns((Guid id, ICustomer customer) =>
        {
            var existingCustomer = _customers.FirstOrDefault(item => item.Id == id);

            if (existingCustomer is null)
            {
                return Task.CompletedTask;
            }

            existingCustomer.GivenName = customer.GivenName;
            existingCustomer.FamilyName = customer.FamilyName;

            return Task.CompletedTask;
        });

        mockRepository.Setup(repository => repository.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
        {
            var customer = _customers.FirstOrDefault(customer => customer.Id == id);

            if (customer is null)
            {
                return Task.CompletedTask;
            }

            _customers.Remove(customer);

            return Task.CompletedTask;
        });

        services.Replace(ServiceDescriptor.Scoped(typeof(ICustomersRepository), _ => mockRepository.Object));
    }
}