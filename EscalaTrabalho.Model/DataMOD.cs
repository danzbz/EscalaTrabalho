using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalaTrabalho.Model
{
    public sealed class DataMOD
    {
        public DataMOD()
        {
            ListaFuncionarioNaEscala = new List<FuncionarioMOD>();
        }

        public Int32 Id { get; set; }

        public String NmEvento { get; set; }

        public DateTime DtInicio { get; set; }

        public List<FuncionarioMOD> ListaFuncionarioNaEscala { get; set; }

    }
}
