using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;

        public SearchService(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            if (orderResult.IsSuccess)
            {
                var result = new
                {
                    orders = orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
        //public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        //{
        //    await Task.Delay(1);
        //    return (true, new { Message = "Hello" });
        //}

    }
}
