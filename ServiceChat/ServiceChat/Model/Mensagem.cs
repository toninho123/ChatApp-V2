using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Mensagem
    {
        [Key]
        public int Id { get; set; }
        public string Texto { get; set; }
        public string Ficheiro { get; set; }
        public DateTime Data_Mensagem { get; set; } = DateTime.Now;
        public int Id_Sala { get; set; }
        public int Id_Utilizador { get; set; }
        [ForeignKey("Id_Sala")]
        public Sala Sala { get; set; }
        [ForeignKey("Id_Utilizador")]
        public Utilizador Utilizador { get; set; }

    }
}
