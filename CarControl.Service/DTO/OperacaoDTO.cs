using CarControl.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarControl.Service.DTO
{
    public class OperacaoDTO
    {
        [JsonPropertyName("idTpOperacao")]
        public int IdTpOperacao { get; set; }

        [Required]
        [JsonPropertyName("nmOperacao")]
        public string NmOperacao { get; set; }

        [JsonIgnore]
        public ICollection<Movimento> Movimentos { get; set; }

        public OperacaoDTO()
        {
            Movimentos = new Collection<Movimento>();
        }
    }
}
