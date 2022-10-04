using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Chat_Grupos
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        
        public int Id_Curso { get; set; }
        
        public int Id_Entidade { get; set; }
        
        public string Capa { get; set; }
        
        public DateTime Dt_Criado { get; set; } = DateTime.Now;
        
        public bool Ativo { get; set; }
        
        public int Tipo { get; set; }
        
        public DateTime Updated_At { get; set; } = DateTime.Now;
        
        public ICollection<Chat_Membros> Grupo { get; set; }
        
        public ICollection<Chat_Mensagens> Mensagem { get; set; }
        
        [ForeignKey("Id_Curso")]
        public Curso Curso { get; set; }
        
        [ForeignKey("Id_Entidade")]
        public Empresa Empresa { get; set; }
    }
}
