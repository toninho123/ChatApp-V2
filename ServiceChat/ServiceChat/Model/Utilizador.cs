using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.Model
{
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Numero_Aluno { get; set; }
        [MaxLength(30)]
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string Estado { get; set; }  
        public ICollection<Grupo> Grupo { get; set; }
        public ICollection<Mensagem> Mensagem { get; set; }
    }

}
