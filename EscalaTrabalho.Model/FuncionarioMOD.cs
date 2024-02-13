namespace EscalaTrabalho.Model
{
    public sealed class FuncionarioMOD
    {

        public FuncionarioMOD()
        {

            DiasDeHomeOfficeNaSemana = 2;

        }

        public String NmFuncionario { get; set; }

        public Int32 Id { get; set; }

        public Int32 QtdMaximaPorSemana { get; set; }

        public Int32 DiasDeHomeOfficeNaSemana { get; set; }

        public String NaoPodeFazerHomeEmConjunto { get; set; }
    }
}