using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        public bool isAtiva { get; set; }
        public ICollection<Grupo> Grupo { get; set; }
        public ICollection<Mensagem> Mensagem { get; set; }



    }
}
