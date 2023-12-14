using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyNunit
{
    public class BookingId
    {
        [JsonProperty("bookingid")]
        public string? Id { get; set; }
    }
}
