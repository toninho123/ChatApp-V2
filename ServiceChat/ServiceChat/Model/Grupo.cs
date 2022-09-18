using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceChat.Model
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        public string Estado { get; set; }
        [DefaultValue(false)]
        public bool Administrador { get; set; }
        public int Id_Sala { get; set; }
        public int Id_Utilizador { get; set; }
        [ForeignKey("Id_Sala")]
        public Sala Sala { get; set; }
        [ForeignKey("Id_Utilizador")]
        public Utilizador Utilizador { get; set; }
    }
}
