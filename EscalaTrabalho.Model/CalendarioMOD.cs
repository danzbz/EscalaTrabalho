using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalaTrabalho.Model
{
    public sealed class CalendarioMOD
    {
        public CalendarioMOD()
        {
            ListaFuncionario = new List<FuncionarioMOD>();
            ListaFeriadoPublico = new List<FeriadosPublicos>();
            ListaDiasUteis = new List<DataMOD>();
            ListaFinalSemana = new List<DateTime>();
        }

        public List<DateTime> ListaFinalSemana { get; set; }

        public List<DataMOD> ListaDiasUteis { get; set; }

        public List<FeriadosPublicos> ListaFeriadoPublico { get; set; }

        public List<FuncionarioMOD> ListaFuncionario { get; set; }

        public FiltroMOD Filtro { get; set; }

    }
}
