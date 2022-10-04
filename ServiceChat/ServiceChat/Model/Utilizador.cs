using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.Model
{
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }

        public int Numero_Identificacao { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }
        
        public ICollection<Chat_Membros> Grupo { get; set; }
        
        public ICollection<Chat_Mensagens> Mensagem { get; set; }
    }

}
