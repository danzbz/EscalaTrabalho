using EscalaTrabalho.Model;
using EscalaTrabalho.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace EscalaTrabalho.UI.Web.Controllers
{
    public class CalendarioController : Controller
    {
        CalendarioService calendarioService = new CalendarioService();

        public async Task<IActionResult> Index()
        {
            CalendarioMOD Calendario = new CalendarioMOD();
            return View(Calendario);
        }

        [HttpPost]
        public async Task<IActionResult> Escala(CalendarioMOD dadosCalendario)
        {

            await calendarioService.GerarEscalaAsync(dadosCalendario);

            return View(dadosCalendario);
        }
    }
}
