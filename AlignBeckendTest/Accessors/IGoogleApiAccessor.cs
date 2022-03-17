using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlignBeckendTest.Accessors
{
    public interface IGoogleApiAccessor
    {
        Task<HttpResponseMessage> GetDistanceMatrixAsync(string origin, string destination);
        //Task<HttpResponseMessage> GetGeoCodingAsync(string origin);
    }
}
