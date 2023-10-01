using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain
{
    public class Veiculo
    {

        [Required]
        public int IdVeiculo { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Marca { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Modelo { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string TpVeiculo { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Cor { get; set; }

        [MinLength(7)]
        [MaxLength(7)]
        [Required]
        public string PlacaVeiculo { get; set; }


        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string NmCondutor { get; set; }


        [MinLength(11)]
        [MaxLength(11)]
        [Required]
        public string CpfCondutor { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }


    }
}
