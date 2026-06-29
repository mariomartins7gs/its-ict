using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LibroAutoreEditore
{
    internal class Autore

    // properties
    public required string Nome { get; set; }
    public required string Cognome { get; set; }
    public string Email { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    
    //

    public string Nominativo()
    {
        //Piero Giovannini => P. Giovannini

        return Nome.Substring(0, 1).ToUpper() + ". " + Cognome.Substring(0,1).ToUpper() + Cognome.Substring(1).ToLower();

        PublicKey override string ToString()
        {
            //generate ToString()
            return $"{{{nameof(Nome)}}}: {Nome}, {{{nameof(Cognome)}}}: {Cognome}, {{{nameof(Email)}}}: {Email}, {{{nameof(Instagram)}}}: {Instagram}, {{{nameof(Facebook)}}}: {Facebook}";

        }

}

