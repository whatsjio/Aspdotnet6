using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.HttpTool
{
    public class AnyMessageClient
    {
        private readonly HttpClient _httpClient;
        public AnyMessageClient(HttpClient client)
        {
            _httpClient = client;
        }


    }
}
