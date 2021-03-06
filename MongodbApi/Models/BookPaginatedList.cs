using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MongodbApi.Models
{
    public class BookPaginatedList
    {
        [JsonProperty("result")]
        public List<Book> Result { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }
}
