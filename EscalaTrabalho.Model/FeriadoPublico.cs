using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalaTrabalho.Model
{
    public class FeriadosPublicos
    {
        public DateTime Date { get; set; }
        public string LocalName { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public bool Fixed { get; set; }
        public bool Global { get; set; }
        public string[] Counties { get; set; }
        public int? LaunchYear { get; set; }
        public string[] Types { get; set; }
    }
}
