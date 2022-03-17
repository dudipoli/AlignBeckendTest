using AlignBeckendTest.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlignBeckendTest.Accessors
{
    public class GoogleApiAccessor : IGoogleApiAccessor
    {
        private readonly HttpClient _client;
        private readonly GoogleApiOptions _options;

        public GoogleApiAccessor(HttpClient httpClient, IOptions<GoogleApiOptions> options)
        {
            _options = options.Value;
            _client = httpClient;
        }

        public async Task<HttpResponseMessage> GetDistanceMatrixAsync(string origin, string destination)
        {
            var uri = new Uri(String.Format(_options.BaseUrl + _options.DistanceMatrix, origin, destination, _options.ApiKey));
            var res = await _client.GetAsync(uri);
            return res;
        }

        //public async Task<HttpResponseMessage> GetGeoCodingAsync(string address)
        //{
        //    var uri = new Uri(String.Format(_options.BaseUrl + _options.GeoCoding, address, _options.ApiKey));
        //    var res = await _client.GetAsync(uri);
        //    return res;
        //}
    }
}
