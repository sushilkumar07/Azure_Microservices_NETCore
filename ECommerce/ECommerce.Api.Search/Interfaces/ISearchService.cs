using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);

        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsyncProduct();

        Task<(bool IsSuccess, dynamic SearchResults)> SearchCustomerAsync(int customerId);
    }
}
