using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        public ICollection<Chat_Grupos> Sala { get; set; }        
    }

}
