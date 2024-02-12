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
        }

        public List<FuncionarioMOD> ListaFuncionario { get; set; }

    }
}
