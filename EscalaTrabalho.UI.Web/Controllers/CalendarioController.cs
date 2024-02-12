using EscalaTrabalho.Model;
using Microsoft.AspNetCore.Mvc;

namespace EscalaTrabalho.UI.Web.Controllers
{
    public class CalendarioController : Controller
    {
        public IActionResult Index()
        {

            FuncionarioMOD Funcionario = new FuncionarioMOD();
            Funcionario.NmFuncionario = "Danilo";

            EventoMOD Evento = new EventoMOD();
            Evento.NmEvento = "Escala";
            Evento.DtInicio = DateTime.Now;
            Evento.Id = 1;

            Funcionario.ListaEvento.Add(Evento);

            CalendarioMOD Calendario = new CalendarioMOD();
            Calendario.ListaFuncionario.Add(Funcionario);

            return View(Calendario);
        }
    }
}
