using System.ComponentModel.DataAnnotations;

namespace WebApi.Kafka.Producer.Api.Models
{
    public class PessoaModel
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}
