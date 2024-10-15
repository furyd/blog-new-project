using Example.Solution.Architecture.Domain.Repositories.Interfaces;

namespace Example.Solution.Architecture.Api.Models;

public record Pagination(int PageSize, int CurrentPage) : IPagination;