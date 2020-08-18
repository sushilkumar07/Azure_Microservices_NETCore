using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using ECommerce.Api.Search.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly ICustomersClient customersClient;


        public SearchController(ISearchService searchService, ICustomersClient customersClient)
        {
            this.searchService = searchService;
            this.customersClient = customersClient;
        }
        [HttpGet("{id}")]
        [Route("api/search/Nswag")]
        public async Task<IActionResult> GetCustomerAsyncNswag(SearchTerm term)
        {
            FileResponse result = await customersClient.GetCustomerAsync(term.CustomerId);
            StreamReader sr = new StreamReader(result.Stream);
            string text = sr.ReadToEnd();
            return Ok(text);
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> SearchAsyncProdcut()
        {
            var result = await searchService.SearchAsyncProduct();
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Customer")]
        public async Task<IActionResult> SearchAsyncCustomer(SearchTerm term)
        {
            var result = await searchService.SearchCustomerAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
