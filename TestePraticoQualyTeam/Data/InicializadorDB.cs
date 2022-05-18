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
            var processos = new Processo[]
                           {
                new Processo {nome = "Proceso 1" },
                new Processo {nome = "Processo 2" },
                new Processo {nome = "Processo 3" },
                new Processo {nome = "Processo 4" },
                new Processo {nome = "Processo 5" },
                new Processo {nome = "Processo 6" }

                           };

            if (!contex.Processos.Any())
            {

                contex.Processos.AddRange(processos);
                contex.SaveChanges();
            }
           

        }
    }
}
