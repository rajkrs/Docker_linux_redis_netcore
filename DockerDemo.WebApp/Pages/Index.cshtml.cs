using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace DockerDemo.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDistributedCache _distributedCache;
        public IndexModel(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public void OnGet()
        {
            string cacheData = string.Empty;
            var cacheKey = "TheTime";
            var existingTime = _distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                cacheData =  "Fetched from cache : " + existingTime;
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _distributedCache.SetString(cacheKey, existingTime);
                cacheData = "Added to cache : " + existingTime;
            }

            ViewData["CacheData"] = cacheData;
        }
    }
}
