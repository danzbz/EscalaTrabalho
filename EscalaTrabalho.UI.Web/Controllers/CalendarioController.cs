using EscalaTrabalho.Model;
using EscalaTrabalho.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace EscalaTrabalho.UI.Web.Controllers
{
    public class CalendarioController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {

            CalendarioService calendarioService = new CalendarioService();

            // Criando um calendário
            CalendarioMOD Calendario = new CalendarioMOD();

            // Lista de nomes de funcionários
            string[] nomesFuncionarios = { "Danilo", "Maria", "João" , "Ota" };
            Int32[] idFuncionario = { 1, 2, 3, 4, 5 };


            // Iterando sobre os nomes dos funcionários e adicionando ao calendário
            for (int i = 0; i < nomesFuncionarios.Length; i++)
            {
                // Criando um novo objeto FuncionarioMOD
                FuncionarioMOD funcionario = new FuncionarioMOD();

                // Definindo o nome do funcionário
                funcionario.NmFuncionario = nomesFuncionarios[i];
                funcionario.Id = idFuncionario[i];

                // Adicionando o funcionário ao calendário
                Calendario.ListaFuncionario.Add(funcionario);
            }

            await calendarioService.GerarEscalaAsync(Calendario);

            // Dicionário para armazenar o total de vezes que cada funcionário aparece na escala
            Dictionary<int, int> totalPorFuncionario = new Dictionary<int, int>();

            // Iterando sobre os dias úteis no calendário
            foreach (var diaUtil in Calendario.ListaDiasUteis)
            {
                // Iterando sobre os funcionários na escala para este dia útil
                foreach (var funcionarioNaEscala in diaUtil.ListaFuncionarioNaEscala)
                {
                    // Verificando se o funcionário já foi contabilizado
                    if (totalPorFuncionario.ContainsKey(funcionarioNaEscala.Id))
                    {
                        // Se o funcionário já foi contabilizado, incrementa o contador
                        totalPorFuncionario[funcionarioNaEscala.Id]++;
                    }
                    else
                    {
                        // Se é a primeira vez que o funcionário é contabilizado, adiciona-o ao dicionário com um contador inicial de 1
                        totalPorFuncionario[funcionarioNaEscala.Id] = 1;
                    }
                }
            }

            // Exibindo o total de vezes que cada funcionário aparece na escala
            foreach (var kvp in totalPorFuncionario)
            {
                Console.WriteLine($"Funcionário ID: {kvp.Key}, Total de vezes na escala: {kvp.Value}");
            }



            return View(Calendario);
        }
    }
}
