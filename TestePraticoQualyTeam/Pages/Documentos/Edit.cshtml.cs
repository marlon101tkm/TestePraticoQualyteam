using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestePraticoQualyTeam.Data;
using TestePraticoQualyTeam.Model;
using TestePraticoQualyTeam.Utilities;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class EditModel : ProcessosPageModelModel
    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;
        private string[] extencoesPermitidas = { ".pdf", ".doc", ".xls", ".docx", ".xlsx" };

        public EditModel(TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Documento Documento { get; set; }

        [BindProperty]
        public UploaderArquivoAtualiza upload { get; set; }


        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Documento = await _context.Documentos.Include(c => c.processo).FirstOrDefaultAsync(d => d.id == id);
         

            popularDropdownProcessos(_context, Documento.processoID);

            if (Documento == null)
            {
                return NotFound();
            }
            return Page();
        }

     

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            _context.Documentos.Attach(Documento).State = EntityState.Modified;

            if (upload.FormFile != null) {
                var conteudoArquivo =
                     await FileHelpers.ProcessFormFile<UploaderArquivoCadastro>(
                         upload.FormFile, ModelState, extencoesPermitidas);
                Documento.arquivo = conteudoArquivo;
                Documento.nomeArquivo = upload.FormFile.FileName;
            }
           
           

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["Message"] = "Documento Atualizado Com Sucesso ";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExists(Documento.id))
                {
                    popularDropdownProcessos(_context);
                    return NotFound();
                }
                else
                {
                    popularDropdownProcessos(_context);
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }




        private bool DocumentoExists(int id)
        {
            return _context.Documentos.Any(e => e.id == id);
        }

      
        
    }
}
