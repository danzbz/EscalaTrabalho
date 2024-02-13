using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalaTrabalho.Model
{
    public class FiltroMOD
    {
        public FiltroMOD()
        {
            ListaIdFuncionarioNaoPodemEstarNoMesmoDia = new List<int>();
        }

        public Int32 QtdMaximaFuncionarioPorDia { get; set; }

        public List<Int32> ListaIdFuncionarioNaoPodemEstarNoMesmoDia { get; set; }

    }
}
