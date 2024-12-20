using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WEBRAZOR.Pages
{
    public class QueryJ : PageModel
    {
        private readonly ILogger<QueryJ> _logger;

        public QueryJ(ILogger<QueryJ> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}