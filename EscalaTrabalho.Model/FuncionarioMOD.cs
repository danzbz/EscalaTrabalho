namespace EscalaTrabalho.Model
{
    public sealed class FuncionarioMOD
    {
        public FuncionarioMOD()
        {
            ListaEvento = new List<EventoMOD>();
        }

        public String NmFuncionario { get; set; }

        public Int32? NrMatricula { get; set; }

        public List<EventoMOD> ListaEvento { get; set; }
    }
}