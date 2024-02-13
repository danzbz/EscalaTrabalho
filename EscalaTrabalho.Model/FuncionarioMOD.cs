namespace EscalaTrabalho.Model
{
    public sealed class FuncionarioMOD
    {

        public FuncionarioMOD(Int32 diasDeHomeOfficeNaSemana = 0)
        {

            DiasDeHomeOfficeNaSemana = diasDeHomeOfficeNaSemana;

        }


        public String NmFuncionario { get; set; }

        public Int32 Id { get; set; }

        public Int32 QtdMaximaPorSemana { get; set; }

        public int DiasDeHomeOfficeNaSemana { get; set; }


    }
}