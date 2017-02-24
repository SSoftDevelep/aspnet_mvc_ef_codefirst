using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace aspnet_mvc_ef_codefirst.Models
{
    [Table("Kisiler")]  //tablo ismi verme
    public class Kisiler
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id colonumuz primary key tanimi ve Identity otomatik artan kolon
        public int ID { get; set; }

        [StringLength(20),Required]  //Ad kolonu boyutu 20 karakterle sinirlandirdik. Required ile bos gecilemez dedik.(not null)
        public string Ad { get; set; }

        [StringLength(20),Required  ]
        public string Soyad { get; set; }

        [Required]
        public int Yas { get; set; }

        public virtual List<Adresler> Adressler { get; set; }
    }
}