namespace TamayoConde_IIUREC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int usuario_id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string apellido { get; set; }

        [Required]
        [StringLength(30)]
        public string nombreusuario { get; set; }

        [Required]
        [StringLength(50)]
        public string clave { get; set; }

        [Required]
        [StringLength(20)]
        public string nivel { get; set; }

        [StringLength(250)]
        public string avatar { get; set; }

        public int estado { get; set; }
    }
}
