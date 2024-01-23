namespace Ordens_de_serviço.Ordens
{
    public record AddOrdemRequest(string nome, string setor, string motivo, string hora);
}
