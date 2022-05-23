using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestePraticoQualyTeam.Data;
using TestePraticoQualyTeam.Model;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class DeleteModel : PageModel
    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Documento Documento { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documento = await _context.Documentos.Include(p => p.processo).Include(c => c.categoria).AsNoTracking().FirstOrDefaultAsync(m => m.id == id);


            if (Documento == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documento = await _context.Documentos.FindAsync(id);


            if (Documento != null)
            {
                _context.Documentos.Remove(Documento);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            return Page();

        
          

        }

      
    }
}
