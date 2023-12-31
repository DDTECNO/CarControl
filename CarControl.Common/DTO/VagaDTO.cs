﻿using CarControl.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Common.DTO
{
    public class VagaDTO
    {
        public int IdVaga { get; set; }

        [Required]
        [JsonPropertyName("nmVaga")]
        public required string NmVaga { get; set; }

        [Required]
        [JsonPropertyName("flVaga")]
        public char FlVaga { get; set; }

        [JsonIgnore]
        public ICollection<Movimento> Movimentos { get; set; }

        public VagaDTO()
        {
            Movimentos = new Collection<Movimento>();
        }
    }
}
