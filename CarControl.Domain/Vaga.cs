using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain
{
    public class Vaga
    {
        public int IdVaga { get; set; }

        [Required]
        [Display(Name = "Nome da vaga")]
        [JsonPropertyName("nmVaga")]
        public string NmVaga { get; set; }

        [Required]
        [Display(Name = "Situação da vaga")]
        [JsonPropertyName("flVaga")]
        public char FlVaga { get; set; }

        [JsonIgnore]
        public ICollection<Movimento> Movimentos { get; set; }

        public Vaga()
        {
            Movimentos = new Collection<Movimento>();
        }

       
    }
}
