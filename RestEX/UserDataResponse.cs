using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestEX
{
    public class UserDataResponse
    {
        [JsonProperty("data")]
        public UserData? Data { get; set; }
    }
}
