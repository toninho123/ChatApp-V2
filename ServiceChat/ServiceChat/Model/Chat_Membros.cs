using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Chat_Membros
    {
        [Key]
        public int Id { get; set; }
        
        public bool Ativo { get; set; }
        [DefaultValue(false)]
        
        public bool Administrador { get; set; }
        
        public DateTime Dt_Criado { get; set; } = DateTime.Now;
        
        public bool Saiu { get; set; }
        
        public bool Lido { get; set; }
        
        public DateTime Updated_At { get; set; } = DateTime.Now;
        
        public int Id_Grupo { get; set; }
        
        public int Id_Utilizador { get; set; }
        
        [ForeignKey("Id_Grupo")]
        public Chat_Grupos Sala { get; set; }
        
        [ForeignKey("Id_Utilizador")]
        public Utilizador Utilizador { get; set; }
    }
}
