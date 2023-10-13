using CarControl.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Common.DTO
{
    public class OperacaoDTO
    {
        [JsonPropertyName("idTpOperacao")]
        public int IdTpOperacao { get; set; }

        [Required]
        [JsonPropertyName("nmOperacao")]
        public required string NmOperacao { get; set; }

        [JsonIgnore]
        public ICollection<Movimento> Movimentos { get; set; }

        public OperacaoDTO()
        {
            Movimentos = new Collection<Movimento>();
        }
    }
}
