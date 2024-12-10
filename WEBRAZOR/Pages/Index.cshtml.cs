using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEBRAZOR.Services;

namespace WEBRAZOR.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
  private readonly OrderServices _orderServices;
    public IndexModel(ILogger<IndexModel> logger,OrderServices orderServices)
    {
        _logger = logger;
        _orderServices=orderServices;
    }

    public List<Product> products { get; set; } = new List<Product>();

    public async Task OnGetAsync()
    {
       
        products=await _orderServices.GetProductsAsync();
    }
}
