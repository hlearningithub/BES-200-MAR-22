using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace LibraryApi.Controllers
{
    public class CacheController : Controller
    {
        IDistributedCache Cache;

        public CacheController(IDistributedCache cache)
        {
            Cache = cache;
        }

        [HttpGet("/time")]
        [ResponseCache(Duration =130, Location =ResponseCacheLocation.Any)]
        public ActionResult <string> GetTime()
        {
            return Ok(new { data = $"This is time {DateTime.Now.ToLongTimeString()}"});
        }

        [HttpGet("/time2")]
        public async Task<ActionResult> GetTheTimeFromDistributedCache()
        {
            var time = await Cache.GetAsync("time");
            string newTime = null;

            if (time == null)
            {
                newTime = DateTime.Now.ToLongTimeString();

                var encodedTime = Encoding.UTF8.GetBytes(newTime);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));

                await Cache.SetAsync("time", encodedTime, options);
            }
            else
            {
                newTime = Encoding.UTF8.GetString(time);
            }

            return Ok($"Ok, It is now {newTime}");
        }
    }
}
