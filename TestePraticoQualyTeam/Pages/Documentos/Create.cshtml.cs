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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class CreateModel : PageModel

    {
        private readonly TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext _context;
        private string[] extencoesPermitidas = { ".pdf", ".doc", ".xls", ".docx", ".xlsx" };



        public CreateModel(TestePraticoQualyTeam.Data.TestePraticoQualyTeamContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
            popularDropdownProcessos();
            return Page();
        }


        public SelectList processosNomes { get; set; }
        // public SelectList categoriasNomes { get; set; }

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
                s => s.id, s => s.processoID, s => s.titulo, s => s.categoriaID, s => s.codigo, s => s.arquivo))
            {

                var conteudoArquivo =
                 await FileHelpers.ProcessFormFile<UploaderArquivoCadastro>(
                     upload.FormFile, ModelState, extencoesPermitidas);

                if (_context.Documentos.Any(x => x.codigo == Documento.codigo))
                {
                    ModelState.AddModelError("Documento.codigo", "Codigo já exitente");
                }

                if (!ModelState.IsValid)
                {
                    popularDropdownProcessos(documentoVazio.processoID);
                    return Page();
                }

                Documento.arquivo = conteudoArquivo;
                Documento.nomeArquivo = upload.FormFile.FileName;


                _context.Documentos.Add(Documento);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Documento Cadastrado Com Sucesso ";


                return RedirectToPage("./Index");

            }

            popularDropdownProcessos(documentoVazio.processoID);
            return Page();

        }

        public void popularDropdownProcessos(object processoSelecionado = null)
        {
            var processosQuery = from d in _context.Processos
                                 orderby d.nome
                                 select d;
            processosNomes = new SelectList(processosQuery.AsNoTracking(), "id", "nome", processoSelecionado);
        }


        public JsonResult OnGetPopularDropdownCategorias(int idProcesso)
        {


            var categoriaQuery = _context.Processos.Where(pros => pros.id == idProcesso)
                .Select(pros => new
                {
                    categorias = pros.categorias.Select(categ => new { id = categ.id, nome = categ.nome }).ToList()

                }).ToList();



            return new JsonResult(categoriaQuery.First().categorias);
        }



    }



}
