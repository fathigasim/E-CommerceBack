using EcommerceApplication.Common.EcommerceApplication.Common;
using EcommerceApplication.DTOs;
using EcommerceApplication.Exceptions;
using EcommerceApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.ExternalServices
{
    public class ReqResService : IReqResService
    {

        readonly HttpClient _httpClient;
        public ReqResService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ReqResUserDto> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            // var url = $"posts?_page={PageNumber}&_limit={PageSize}";
            var response = await _httpClient.GetAsync($"users/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var userInfo = await response.Content.ReadFromJsonAsync<ReqResUserDto>(cancellationToken);
            if (userInfo is null )
            {
                throw new NotFoundException("Sorry no data found");
            }

             

            return new ReqResUserDto { 
            ReqResData = userInfo.ReqResData
            };

        }
    }
}
