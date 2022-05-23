using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePraticoQualyTeam.Model;

namespace TestePraticoQualyTeam.Data
{
    public class InicializadorDB
    {
        public static void Initialize(TestePraticoQualyTeamContext contex)
        {
            string[] nomesCategorias = { "Categoria 1", "Categoria 2", "Categoria 3", "Categoria 4" };
            string[] nomesProcessos = { "Proceso 1", "Proceso 2", "Proceso 3" };

            var categoria = new Categoria[]
                           {
                new Categoria {nome = nomesCategorias[0]},
                new Categoria {nome = nomesCategorias[1]},
                new Categoria {nome = nomesCategorias[2]},
                new Categoria {nome = nomesCategorias[3]}};


            var processos = new Processo[]
                           {
                new Processo {nome = nomesProcessos[0] },
                new Processo {nome = nomesProcessos[1] },
                new Processo {nome = nomesProcessos[2] } };




            processos[0].categorias = new List<Categoria> {categoria[0], categoria[1], categoria[2] };
            processos[1].categorias = new List<Categoria> { categoria[3], categoria[0] };
            processos[2].categorias = new List<Categoria> { categoria[1]};


            categoria[0].processos = new List<Processo> {processos[0], processos[1] };
            categoria[1].processos = new List<Processo> { processos[0], processos[2] };
            categoria[2].processos = new List<Processo> { processos[0] };
            categoria[3].processos = new List<Processo> { processos[1] };



            if (!contex.Processos.Any())
            {

                contex.Processos.AddRange(processos);
                contex.SaveChanges();
            }
            if (!contex.Categorias.Any())
            {

                contex.Categorias.AddRange(categoria);
                contex.SaveChanges();
            }





        }
    }
}
