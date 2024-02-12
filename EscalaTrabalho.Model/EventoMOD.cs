using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalaTrabalho.Model
{
    public sealed class EventoMOD
    {
        public Int32 Id { get; set; }

        public String NmEvento { get; set; }

        //public String DtInicio { get; set; }

        public DateTime DtInicio { get; set; }

        public String? DtFim { get; set; }
    }
}
