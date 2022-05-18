using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestePraticoQualyTeam.Model
{
    public class Processo
    {
       [Required]
        public int id { get; set; }

        public string nome { get; set; }
    }
}
