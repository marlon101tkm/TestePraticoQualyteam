using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestePraticoQualyTeam.Data;
using TestePraticoQualyTeam.Model;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class DetailsModel : PageModel
    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;

        public DetailsModel(TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext context)
        {
            _context = context;
        }

        public Documento Documento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documento = await _context.Documentos.FirstOrDefaultAsync(m => m.id == id);

            if (Documento == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
