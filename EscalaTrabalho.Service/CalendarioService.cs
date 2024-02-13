using EscalaTrabalho.Model;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace EscalaTrabalho.Services
{
    public class CalendarioService
    {
        public async Task GerarEscalaAsync(CalendarioMOD calendario)
        {
            await ObterFinalSemana(calendario);
            await ObterFeriadosPublicos(calendario);
            AtribuirHomeOffice(calendario);
        }

        public static async Task ObterFeriadosPublicos(CalendarioMOD calendario)
        {
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("https://date.nager.at/api/v3/publicholidays/2024/BR");
            if (response.IsSuccessStatusCode)
            {
                using var jsonStream = await response.Content.ReadAsStreamAsync();
                calendario.ListaFeriadoPublico = JsonSerializer.Deserialize<List<FeriadosPublicos>>(jsonStream, jsonSerializerOptions);

                //Remover da lista de dias uteis os feriados
                //calendario.ListaDiasUteis.RemoveAll(diaUtil => calendario.ListaFeriadoPublico.Any(feriado => feriado.Date.Date == diaUtil.DtInicio.Date));
            }
        }

        public static async Task ObterFinalSemana(CalendarioMOD calendario)
        {
            int ano = 2024;
            int mes = 2; // Fevereiro

            // Obter o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(ano, mes, 1);

            // Obter o último dia do mês
            DateTime ultimoDiaDoMes = primeiroDiaDoMes.AddMonths(1).AddDays(-1);

            // Iterar sobre os dias do mês
            for (DateTime dia = primeiroDiaDoMes; dia <= ultimoDiaDoMes; dia = dia.AddDays(1))
            {
                // Verificar se o dia é sábado ou domingo
                if (dia.DayOfWeek == DayOfWeek.Tuesday || dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    calendario.ListaFinalSemana.Add(dia);
                }
                else
                {
                    DataMOD data = new DataMOD { DtInicio = dia };

                    calendario.ListaDiasUteis.Add(data);
                }
            }
        }

        //private void AtribuirHomeOffice(CalendarioMOD calendario)
        //{
        //    int maxFuncionariosPorDia = 2; // Máximo de 2 pessoas de home office por dia
        //    int diasDeHomeOfficePorSemana = 2; // Máximo de 2 dias de home office por semana

        //    Random rng = new Random(); // Criar uma instância de Random para embaralhar a lista de funcionários

        //    int semanaAtual = -1; // Inicializa a semana como -1 para garantir que seja atribuído home office na primeira semana

        //    foreach (var diaUtil in calendario.ListaDiasUteis)
        //    {
        //        if (calendario.ListaFeriadoPublico.Any(feriado => feriado.Date.Date == diaUtil.DtInicio.Date))
        //        {
        //            // Se for feriado, pular para o próximo dia útil
        //            continue;
        //        }

        //        // Verificar se mudou de semana
        //        int semana = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(diaUtil.DtInicio, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        //        if (semana != semanaAtual)
        //        {
        //            semanaAtual = semana;
        //            // Resetar os dias de home office dos funcionários para a nova semana
        //            foreach (var funcionario in calendario.ListaFuncionario)
        //            {
        //                funcionario.DiasDeHomeOfficeNaSemana = 0;
        //            }
        //        }

        //        // Embaralhar a lista de funcionários
        //        var funcionariosEmbaralhados = calendario.ListaFuncionario.OrderBy(f => rng.Next()).ToList();

        //        int funcionariosAdicionadosNoDia = 0;

        //        foreach (var funcionario in funcionariosEmbaralhados)
        //        {
        //            if (funcionario.DiasDeHomeOfficeNaSemana >= diasDeHomeOfficePorSemana)
        //            {
        //                // Se o funcionário já atingiu o limite de home office para a semana, passar para o próximo
        //                continue;
        //            }

        //            // Adicionar o funcionário à escala de home office para este dia útil
        //            diaUtil.ListaFuncionarioNaEscala.Add(funcionario);
        //            funcionario.DiasDeHomeOfficeNaSemana++;
        //            funcionariosAdicionadosNoDia++;

        //            // Se atingiu o máximo de funcionários por dia, sair do loop
        //            if (funcionariosAdicionadosNoDia >= maxFuncionariosPorDia)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //}

        private void AtribuirHomeOffice(CalendarioMOD calendario)
        {
            int maxFuncionariosPorDiaPadrao = 2; // Máximo de 2 pessoas de home office por dia
            int maxFuncionariosPorDiaExcecao = 3; // Máximo de 3 pessoas de home office por dia para exceções
            int maxDiasDeHomeOfficePorSemana = 2; // Máximo de 2 dias de home office por semana por funcionário

            Random rng = new Random(); // Criar uma instância de Random para embaralhar a lista de funcionários

            int semanaAtual = -1; // Inicializa a semana como -1 para garantir que seja atribuído home office na primeira semana

            foreach (var diaUtil in calendario.ListaDiasUteis)
            {
                if (calendario.ListaFeriadoPublico.Any(feriado => feriado.Date.Date == diaUtil.DtInicio.Date))
                {
                    // Se for feriado, pular para o próximo dia útil
                    continue;
                }

                // Verificar se mudou de semana
                int semana = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(diaUtil.DtInicio, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
                if (semana != semanaAtual)
                {
                    semanaAtual = semana;
                    // Resetar os dias de home office dos funcionários para a nova semana
                    foreach (var funcionario in calendario.ListaFuncionario)
                    {
                        funcionario.DiasDeHomeOfficeNaSemana = 0;
                    }
                }

                // Embaralhar a lista de funcionários
                var funcionariosEmbaralhados = calendario.ListaFuncionario.OrderBy(f => rng.Next()).ToList();

                int funcionariosAdicionadosNoDia = 0;

                foreach (var funcionario in funcionariosEmbaralhados)
                {
                    int maxFuncionariosPorDia = maxFuncionariosPorDiaPadrao; // Definir o máximo padrão por dia

                    if (funcionario.DiasDeHomeOfficeNaSemana < maxDiasDeHomeOfficePorSemana - 1)
                    {
                        // Se o funcionário ainda pode ter mais um dia de home office nesta semana, aumentar o máximo permitido por dia
                        maxFuncionariosPorDia = maxFuncionariosPorDiaExcecao;
                    }

                    if (funcionario.DiasDeHomeOfficeNaSemana >= maxDiasDeHomeOfficePorSemana)
                    {
                        // Se o funcionário já atingiu o limite de home office para a semana, passar para o próximo
                        continue;
                    }

                    if (diaUtil.ListaFuncionarioNaEscala.Any(f => f.Id == funcionario.Id))
                    {
                        // Se o funcionário já foi adicionado ao dia, passar para o próximo
                        continue;
                    }

                    // Verificar se o funcionário tem ID 1 ou ID 2 e se já existe outro funcionário com ID 1 ou ID 2 no dia
                    if ((funcionario.Id == 1 || funcionario.Id == 2) && diaUtil.ListaFuncionarioNaEscala.Any(f => f.Id == 1 || f.Id == 2))
                    {
                        // Se o funcionário tem ID 1 ou ID 2 e já existe outro funcionário com ID 1 ou ID 2 no dia, passar para o próximo
                        continue;
                    }

                    // Adicionar o funcionário à escala de home office para este dia útil
                    diaUtil.ListaFuncionarioNaEscala.Add(funcionario);
                    funcionario.DiasDeHomeOfficeNaSemana++;
                    funcionariosAdicionadosNoDia++;

                    // Se atingiu o máximo de funcionários por dia, sair do loop
                    if (funcionariosAdicionadosNoDia >= maxFuncionariosPorDia)
                    {
                        break;
                    }
                }

                // Se não houver funcionários atribuídos para o dia, adicionar um
                if (diaUtil.ListaFuncionarioNaEscala.Count == 0)
                {
                    // Adicionar o primeiro funcionário disponível à escala de home office para este dia útil
                    var primeiroFuncionarioDisponivel = calendario.ListaFuncionario.FirstOrDefault(f => f.DiasDeHomeOfficeNaSemana < maxDiasDeHomeOfficePorSemana);
                    if (primeiroFuncionarioDisponivel != null)
                    {
                        diaUtil.ListaFuncionarioNaEscala.Add(primeiroFuncionarioDisponivel);
                        primeiroFuncionarioDisponivel.DiasDeHomeOfficeNaSemana++;
                    }
                }

                // Se houver mais de dois funcionários atribuídos para o dia, remover o último para garantir no máximo 2
                if (diaUtil.ListaFuncionarioNaEscala.Count > maxFuncionariosPorDiaPadrao)
                {
                    diaUtil.ListaFuncionarioNaEscala.RemoveAt(diaUtil.ListaFuncionarioNaEscala.Count - 1);
                }
            }
        }

    }
}