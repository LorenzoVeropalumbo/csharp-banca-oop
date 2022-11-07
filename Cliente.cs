public class Cliente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string CodiceFiscale { get; set; }
    public int Stipendio { get; set; }

    public Cliente(string nome, string cognome, string codiceFiscale, int stipendio)
    {
        Nome = nome;
        Cognome = cognome;
        CodiceFiscale = codiceFiscale;
        Stipendio = stipendio;
    }
}