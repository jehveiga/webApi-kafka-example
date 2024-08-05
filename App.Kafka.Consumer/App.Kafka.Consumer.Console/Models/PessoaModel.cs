using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Kafka.Consumer.Console.Models
{
    public class PessoaModel
    {
        [JsonPropertyName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonPropertyName("Idade")]
        public int Idade { get; set; }

        public override string ToString()
        {
            return $"Nome é {Nome} - idade: {Idade}";
        }
    }
}
