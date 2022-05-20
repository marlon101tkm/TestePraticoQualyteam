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

            var categoria = new Categoria[]
                           {
                new Categoria {nome = "Categoria 1" },
                new Categoria {nome = "Categoria 2" },
                new Categoria {nome = "Categoria 3" },
                new Categoria {nome = "Categoria 4" },
                new Categoria {nome = "Categoria 5" },
                new Categoria {nome = "Categoria 6" }

                           };

            if (!contex.Categorias.Any())
            {

                contex.Categorias.AddRange(categoria);
                contex.SaveChanges();
            }
            var processos = new Processo[]
                           {
                new Processo {nome = "Proceso 1" , categorias = new List<Categoria>() {categoria[0],categoria[1]} },
                new Processo {nome = "Processo 2",categorias = new List<Categoria>() {categoria[2],categoria[3]}},
                new Processo {nome = "Processo 3",categorias = new List<Categoria>() {categoria[4],categoria[1]} },
              

                           };

            if (!contex.Processos.Any())
            {

                contex.Processos.AddRange(processos);
                contex.SaveChanges();
            }

            


        }
    }
}
