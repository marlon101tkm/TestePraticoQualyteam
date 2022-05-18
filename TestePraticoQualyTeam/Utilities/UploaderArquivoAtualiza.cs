using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePraticoQualyTeam.Utilities
{
    public class UploaderArquivoAtualiza
    {
        
        [Display(Name = "Arquivo")]
        public IFormFile FormFile { get; set; }
    }
}
