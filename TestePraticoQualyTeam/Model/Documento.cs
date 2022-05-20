
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestePraticoQualyTeam.Data;

namespace TestePraticoQualyTeam.Model
{
    
    public class Documento 
    {
        public int id { get; set; }

        [Required(ErrorMessage = " Necessario informar o Titulo ", AllowEmptyStrings = false)]
        [Display(Name = "Titulo")]
        public string titulo  { get; set; }

        
        [Required(ErrorMessage = "Necessario informar o Codigo")]
        [Column(TypeName = "int")]
        [Display(Name = "Codigo")]
        
        public int codigo { get; set; }



        [Required (ErrorMessage = "Necessario Informar a Categoria", AllowEmptyStrings = false) ]
        [ForeignKey("Categoria")]
        public string categoriaID { get; set; }

        [Display(Name ="Categoria")]
        public string categoria { get; set; }

       



        [Required(ErrorMessage = "Necessario informar o Processo")]
        [ForeignKey("Processo")]
        public int processoID { get; set; }
        [Display(Name = "Processo")]
        public Processo processo { get; set; }



        [Display(Name = "Arquivo")]
        public string nomeArquivo { get; set; }
        public byte[] arquivo { get; set; }

      
    }
}
