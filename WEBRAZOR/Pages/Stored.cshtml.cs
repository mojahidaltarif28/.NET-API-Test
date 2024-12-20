using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WEBRAZOR.Pages
{
    public class Stored : PageModel
    {
        private readonly ILogger<Stored> _logger;

        public Stored(ILogger<Stored> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}