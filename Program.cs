
Banca intesa = new Banca("Intesa san Paolo");
Console.WriteLine("Sistema amministrazione banca " + intesa.Nome);

AggiungiClienteInfo();
AggiungiPrestitoInfo();
AggiungiPrestitoInfo();
Console.WriteLine(intesa.TotaleAmmontarePrestiti(inserisciIlCodiceFiscale()));

void AggiungiClienteInfo()
{
    Console.WriteLine("inserisci il nome");
    string Nome = Console.ReadLine();
    Console.WriteLine("inserisci il Cognome");
    string Cognome = Console.ReadLine();
    Console.WriteLine("inserisci il CodiceFiscale");
    string CodiceFiscale = Console.ReadLine();
    Console.WriteLine("inserisci il Stipendio");
    int Stipendio = Convert.ToInt32(Console.ReadLine());

    if(intesa.AggiungiCliente(Nome, Cognome, CodiceFiscale, Stipendio))
    {
        Console.WriteLine();
        Console.WriteLine("cliente inserito correttamente");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("cliente non inserito");
        Console.WriteLine();
    }
}

string inserisciIlCodiceFiscale()
{
    Console.WriteLine("inserisci il CodiceFiscale");
    string CodiceFiscale = Console.ReadLine();
    return CodiceFiscale;
}

void AggiungiPrestitoInfo()
{
    Cliente ClientePrestito = intesa.RicercaCliente(inserisciIlCodiceFiscale());
    if(ClientePrestito == null)
    {
        Console.WriteLine("utente non trovato");
        return;
    }
    Console.WriteLine("inserisci ID");
    int ID = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("inserisci ammontare");
    int ammontare = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("inserisci rata");
    int rata = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("inserisci la data fine in formato DD/MM/YYYY:");
    DateOnly datadifine = DateOnly.Parse(Console.ReadLine());

    Prestito nuovo = new Prestito(ID, ammontare, rata, datadifine, ClientePrestito);
    
    if (intesa.AggiungiPrestito(nuovo))
    {
        Console.WriteLine();
        Console.WriteLine("prestito inserito correttamente");
        Console.WriteLine(Math.Abs((int)(nuovo.Inizio.ToDateTime(TimeOnly.Parse("10:00 PM")).Subtract(nuovo.Fine.ToDateTime(TimeOnly.Parse("10:00 PM"))).Days / (365.25 / 12))));
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("prestito non inserito");
        Console.WriteLine();
    }
}