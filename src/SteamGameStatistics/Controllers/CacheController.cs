using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Cache;
using SteamGameStatistics.Enums;

namespace SteamGameStatistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CacheController> _logger;

        public CacheController (ILogger<CacheController> logger, ICacheService cacheService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Loading the cache controller index");
          
            return View(GetCacheHits());
        }

        [HttpPost, ActionName("DeleteCacheHit")]
        public IActionResult Delete(string key)
        {
            if(!string.IsNullOrEmpty(key))
            {
                _cacheService.Delete(key);
            }

            return RedirectToAction("Index");
        }

        private Dictionary<string, object> GetCacheHits()
        {
            var cacheHits = new Dictionary<string, object>();
            var cacheKeys = CacheKeys.GetKeysUsersCanAccess();

            foreach (var key in cacheKeys)
            {
                var result = _cacheService.TryGet(key);
                if (result != null)
                {
                    cacheHits.Add(key, result);
                }
            }

            return cacheHits;
        }
    }
}