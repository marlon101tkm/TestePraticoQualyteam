using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePraticoQualyTeam.Model
{
    public class Categoria
    {
        [Required]
        public int id { get; set; }

        public string nome { get; set; }

        public ICollection<Processo> processos { get; set; }

    }
}
