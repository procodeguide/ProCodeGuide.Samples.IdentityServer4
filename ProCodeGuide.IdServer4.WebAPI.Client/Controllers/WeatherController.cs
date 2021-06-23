using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProCodeGuide.IdServer4.WebAPI.Client.Models;
using ProCodeGuide.IdServer4.WebAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProCodeGuide.IdServer4.WebAPI.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IIdentityServer4Service _identityServer4Service = null;
        public WeatherController(IIdentityServer4Service identityServer4Service)
        {
            _identityServer4Service = identityServer4Service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var OAuth2Token = await _identityServer4Service.GetToken("weatherApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(OAuth2Token.AccessToken);
                var result = await client.GetAsync("https://localhost:44394/weatherforecast");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<WeatherForecast>>(model);
                }
                else
                {
                    throw new Exception("Some Error while fetching Data");
                }
            }
        }
    }
}