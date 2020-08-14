using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SearchService(IOrdersService ordersService,IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }
        [Route("api/search/Get")]
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess?
                            productResult.Products.FirstOrDefault(p => p.Id == item.Id)?.Name :
                        "Product not available";
                    }  
                }
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
