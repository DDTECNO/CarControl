using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Operacao : BaseModel
    {
        public int IdTpOperacao { get; set; }

        [Required]
        public string nmOperacao { get; set; }

        public Operacao()
        {

        }

    }
}
