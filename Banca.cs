﻿public class Banca
{
    public string Nome { get; set; }
    static List<Cliente> Clienti { get; set; }
    List<Prestito> Prestiti { get; set; }


    public Banca(string nome)
    {
        Nome = nome;
        Clienti = new List<Cliente>();
        Prestiti = new List<Prestito>();
    }

    public Cliente RicercaCliente(string codiceFiscale)
    {
        foreach (Cliente cliente in Clienti)
        {
            if (cliente.CodiceFiscale == codiceFiscale)
                return cliente;
        }

        return null;
    }

    public bool AggiungiCliente(string nome, string cognome, string codiceFiscale, int stipendio)
    {

        if (
            nome == null || nome == "" ||
            cognome == null || cognome == "" ||
            codiceFiscale == null || codiceFiscale == "" ||
            stipendio < 0
            )
        {
            return false;
        }

        Cliente esistente = RicercaCliente(codiceFiscale);

        //se il cliente esiste l'istanza sarà diversa dal null
        if (esistente != null)
            return false;

        Cliente cliente = new Cliente(nome, cognome, codiceFiscale, stipendio);
        Clienti.Add(cliente);

        return true;
    }

    public void ModificaCliente(Cliente cliente)
    {
        Console.WriteLine("inserisci il nome");
        string Nome = Console.ReadLine();
        if (Nome != "")
        {
            cliente.Nome = Nome;
        }
        
        Console.WriteLine("inserisci il Cognome");
        string Cognome = Console.ReadLine();
        if (Cognome != "")
        {          
            cliente.Cognome = Cognome;
        }

        Console.WriteLine("inserisci il CodiceFiscale");
        string CodiceFiscale = Console.ReadLine();
        if (CodiceFiscale != "")
        {
            cliente.CodiceFiscale = CodiceFiscale;
        }

        Console.WriteLine("inserisci il Stipendio");
        int Stipendio = Convert.ToInt32(Console.ReadLine());
        if (Stipendio > 200)
        {           
            cliente.Stipendio = Convert.ToInt32(Console.ReadLine());
        }
    }

    public bool AggiungiPrestito(Prestito nuovoPrestito)
    {
        if (
           nuovoPrestito.ID == null || nuovoPrestito.ID < 0 ||
           nuovoPrestito.Intestatario == null ||
           nuovoPrestito.Ammontare == null || nuovoPrestito.Ammontare < 0 ||
           nuovoPrestito.ValoreRata == null || nuovoPrestito.ValoreRata < 0 ||
           nuovoPrestito.Fine == null
           )
        {
            return false;
        }
        else
        {
            if ((nuovoPrestito.Intestatario.Stipendio / 2) > ControlloPrestito(nuovoPrestito.Intestatario.CodiceFiscale))
            {
                Prestiti.Add(nuovoPrestito);
                return true;
            }
            
            return false;
        }
    }

    public int ControlloPrestito(string codiceFiscale)
    {
        List<Prestito> PrestitiAlCliente = PrenstitiConcessiCliente(codiceFiscale);
        int totalAmount = 0;

        foreach (Prestito prestito in PrestitiAlCliente)
        {
            totalAmount += prestito.ValoreRata;
        }

        return totalAmount;
    }

    public List<Prestito> PrenstitiConcessiCliente(string codiceFiscale)
    {
        Cliente esistente = RicercaCliente(codiceFiscale);
        List<Prestito> PrestitiAlCliente = new List<Prestito>();
        if (esistente != null)
        {
            foreach (Prestito prestito in Prestiti)
            {
                if(prestito.Intestatario.CodiceFiscale == esistente.CodiceFiscale)
                {
                    PrestitiAlCliente.Add(prestito);
                }
            }
        }
        
        return PrestitiAlCliente;
    }

    public int TotaleAmmontarePrestiti(string codiceFiscale)
    {
        List<Prestito> PrestitiAlCliente = PrenstitiConcessiCliente(codiceFiscale);
        int totalAmount = 0;
        
        foreach (Prestito prestito in PrestitiAlCliente)
        {
            totalAmount += prestito.Ammontare;
        }
        
        return totalAmount;
    }
}
