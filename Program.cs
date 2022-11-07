
Banca intesa = new Banca("Intesa san Paolo");
Console.WriteLine("Sistema amministrazione banca " + intesa.Nome);
while (true)
{
    Console.WriteLine("scegli un opzione");
    Console.WriteLine("1) Registra utente");
    Console.WriteLine("2) Modifica utente");
    Console.WriteLine("3) Ricerca utente");
    Console.WriteLine("4) Aggiungi un prestito");
    Console.WriteLine("5) Stampa prestiti");
    Console.WriteLine("6) Ammontare totale dei prestiti di un cliente");
    Console.WriteLine("7) Rate mancanti all'estinsione del presito");
    int risposta = Convert.ToInt32(Console.ReadLine());

    switch (risposta)
    {
        case 1:
            AggiungiClienteInfo();
            break;
        case 2:
            inserisciIlCodiceFiscale();
            Cliente ClientePrestito = intesa.RicercaCliente(inserisciIlCodiceFiscale());
            if (ClientePrestito == null)
            {
                Console.WriteLine("utente non trovato");
                break;
            }
            else
            {
                intesa.ModificaCliente(ClientePrestito);
                break;
            }
        case 3:
            StampaCliente();
            break;
        case 4:
            AggiungiPrestitoInfo();
            break;
        case 5:
            StampaPresiti();
            break;
        case 6:
            Console.WriteLine(intesa.TotaleAmmontarePrestiti(inserisciIlCodiceFiscale()));
            break;
        case 7:
            InfoPrestitiRateDaPagare(inserisciIlCodiceFiscale());
            break;
        default:
            break;
    }
}



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

void StampaCliente()
{
    Cliente ClientePrestito = intesa.RicercaCliente(inserisciIlCodiceFiscale());
    Console.WriteLine("Nome : " + ClientePrestito.Nome);
    Console.WriteLine("Cognome : " + ClientePrestito.Cognome);
    Console.WriteLine("Codice fiscale : " + ClientePrestito.CodiceFiscale);
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
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("prestito non inserito");
        Console.WriteLine();
    }
}

void StampaInfoGenerali() {

    Cliente ClientePrestito = intesa.RicercaCliente(inserisciIlCodiceFiscale());
    Console.WriteLine("Nome : " + ClientePrestito.Nome);
    Console.WriteLine("Cognome : " + ClientePrestito.Cognome);
    Console.WriteLine("Codice fiscale : " + ClientePrestito.CodiceFiscale);
    InfoPrestitiRateDaPagare(ClientePrestito.CodiceFiscale);
    Console.WriteLine("ammontare totale dei tuoi prestiti : " + intesa.TotaleAmmontarePrestiti(ClientePrestito.CodiceFiscale));
}

void StampaPresiti()
{
    List<Prestito> Pestiti = intesa.PrenstitiConcessiCliente(inserisciIlCodiceFiscale());

    foreach (Prestito item in Pestiti)
    {
        Console.WriteLine("ID : " + item.ID);
        Console.WriteLine("Ammontare : " + item.Ammontare);
        Console.WriteLine("Rata : " + item.ValoreRata);
        Console.WriteLine("data Inizio : " + item.Inizio);
        Console.WriteLine("data Fine : " + item.Fine);
    }
}

void InfoPrestitiRateDaPagare(string codiceFiscale)
{
    List<Prestito> PrestitiAlCliente = intesa.PrenstitiConcessiCliente(codiceFiscale);

    foreach (Prestito prestito in PrestitiAlCliente)
    {
        Console.WriteLine("Prestito con id " + prestito.ID);
        Console.WriteLine("Valore rata da pagare " + prestito.ValoreRata);
        Console.WriteLine("numero rate rimanenti " + Math.Abs((int)(prestito.Inizio.ToDateTime(TimeOnly.Parse("10:00 PM")).Subtract(prestito.Fine.ToDateTime(TimeOnly.Parse("10:00 PM"))).Days / (365.25 / 12))));
    }
}