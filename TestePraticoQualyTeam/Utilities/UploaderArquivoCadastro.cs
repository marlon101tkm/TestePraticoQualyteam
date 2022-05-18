using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePraticoQualyTeam.Utilities
{
    public class UploaderArquivoCadastro
    {
        [Required(ErrorMessage = "Necessario enviar o Arquivo do Documento")]
        [Display(Name = "Arquivo")]
        public IFormFile FormFile { get; set; }
    }
}
