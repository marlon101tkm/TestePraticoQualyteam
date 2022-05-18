using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TestePraticoQualyTeam.Data;
using TestePraticoQualyTeam.Model;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class IndexModel : PageModel
    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;

        public string TituloSort { get; set; }

        public IndexModel(TestePraticoQualyTeamContext context )
        {
            _context = context;
            
        }

        public IList<Documento> Documento { get;set; }

      

        public async Task OnGetAsync(string sortOrder)
        {
            // using System;
            TituloSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            

            IQueryable<Documento> documentoIQ = from s in _context.Documentos
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    documentoIQ = documentoIQ.OrderByDescending(s => s.titulo);
                    break;
                default:
                    documentoIQ = documentoIQ.OrderBy(s => s.titulo);
                    break;
            }

            Documento = await documentoIQ.Include(c => c.processo).AsNoTracking().ToListAsync();

        }
        

        public async Task<IActionResult> OnGetDownloadDbAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.Documentos.SingleOrDefaultAsync(m => m.id == id);

            if (requestFile == null)
            {
                return Page();
            }

            
            return File(requestFile.arquivo, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.nomeArquivo));
        }
        
    }
}
