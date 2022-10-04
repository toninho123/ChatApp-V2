using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Chat_Mensagens
    {
        [Key]
        public int Id { get; set; }

        public int Id_Utilizador { get; set; }

        public int Id_Grupo { get; set; }
        
        public DateTime Dt_Criado { get; set; } = DateTime.Now;
        
        public string Texto { get; set; }
        
        public string Anexo { get; set; }
        
        public string Anexo_Nome { get; set; }
                        
        public DateTime Updated_At { get; set; } = DateTime.Now;

        [ForeignKey("Id_Grupo")]
        public Chat_Grupos Sala { get; set; }
        
        [ForeignKey("Id_Utilizador")]
        public Utilizador Utilizador { get; set; }

    }
}
