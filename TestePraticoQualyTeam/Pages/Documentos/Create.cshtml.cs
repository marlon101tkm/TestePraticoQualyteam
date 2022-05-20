using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestePraticoQualyTeam.Data;
using TestePraticoQualyTeam.Model;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using TestePraticoQualyTeam.Utilities;
using CurrieTechnologies.Razor.SweetAlert2;



namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class CreateModel : ProcessosPageModelModel

    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;
        private string[] extencoesPermitidas = { ".pdf", ".doc", ".xls", ".docx", ".xlsx" };

        public CreateModel(TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
            popularDropdownProcessos(_context);
            return Page();
        }
       

        [BindProperty]
        public Documento Documento { get; set; }

        [BindProperty]
        public UploaderArquivoCadastro upload { get; set; }

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var documentoVazio = new Documento();

            if (await TryUpdateModelAsync<Documento>(
                documentoVazio,
                "documento", 
                s => s.id, s => s.processoID, s => s.titulo, s => s.categoria, s => s.codigo, s => s.arquivo))
            {

                var conteudoArquivo =
                 await FileHelpers.ProcessFormFile<UploaderArquivoCadastro>(
                     upload.FormFile, ModelState, extencoesPermitidas);
                
                if (_context.Documentos.Any(x => x.codigo == Documento.codigo ))
                {
                    ModelState.AddModelError("Documento.codigo", "Codigo já exitente");
                }

                if (!ModelState.IsValid)
                {
                    popularDropdownProcessos(_context, documentoVazio.processoID);
                    return Page();
                }

                Documento.arquivo = conteudoArquivo;
                Documento.nomeArquivo = upload.FormFile.FileName;

                
                _context.Documentos.Add(Documento);
                await _context.SaveChangesAsync();
                
                TempData["Message"] = "Documento Cadastrado Com Sucesso ";
                
             
                return RedirectToPage("./Index");
                
            }

            popularDropdownProcessos(_context, documentoVazio.processoID);
            return Page();
       
        }


        [HttpGet]
        public virtual JsonResult CidadesPorDepartamento(int id)
        {
            var xpto = _context.ListAllCities()
                            .Where(x => x.State.Id == codCitySel)
                            .OrderBy(x => x.Name)
                            .Select(x => new { CityId = x.Id, Name = x.Name })
                            .ToList(); //.ToSelectList(x => x.Id, x => x.Name);

            return Json(xpto, JsonRequestBehavior.AllowGet);
        }


    }

    

}
