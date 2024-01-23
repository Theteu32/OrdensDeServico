using System.Runtime.CompilerServices;

namespace Ordens_de_serviço.Ordens
{
    public class Ordem
    {
        public Guid Id { get; init; }  
        public string Nome { get; private set; }         
        public string Setor { get; private set; }
        public string Motivo { get; private set; }
        public string Hora { get; private set; }
        public bool Concluida { get; set; }





        public Ordem(string nome, string setor, string motivo, string hora)
        {

            Id = Guid.NewGuid();
            Nome = nome;
            Setor = setor;
            Motivo = motivo;
            Hora = hora;
            Concluida = false;

        }
    }
}
