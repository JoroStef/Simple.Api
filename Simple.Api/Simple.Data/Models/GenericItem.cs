using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Simple.Data.Models
{
    public class GenericItem<T>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("body")]
        public T? Body { get; set; }
    }
}
