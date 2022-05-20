using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestePraticoQualyTeam.Data;

namespace TestePraticoQualyTeam.Pages.Documentos
{
    public class ProcessosPageModelModel : PageModel
    {
        public SelectList processosNomes { get; set; }

        public SelectList categoriasNomes { get; set; }


        public void popularDropdownProcessos(TestePraticoQualyTeamContext  _context, object processoSelecionado = null)
        {
            var processosQuery = from d in _context.Processos
                                 orderby d.nome
                                 select d;
            processosNomes = new SelectList(processosQuery.AsNoTracking(), "id", "nome", processoSelecionado);
        }

        public void popularDropdownCategorias(TestePraticoQualyTeamContext _context, object categoriaSelecionada = null)
        {
            var categoriasQuery = from d in _context.Categorias
                                 orderby d.nome
                                 select d;
            processosNomes = new SelectList(categoriasQuery.AsNoTracking(), "id", "nome", categoriaSelecionada);
        }



    }
}
