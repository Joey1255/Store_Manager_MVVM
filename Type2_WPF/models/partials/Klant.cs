using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Klant : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Telefoonnummer" && string.IsNullOrEmpty(Telefoonnummer))
                {
                    return "Telefoonnummer moet ingevuld zijn!";
                }
                if (columnName == "Telefoonnummer" && !string.IsNullOrEmpty(Telefoonnummer) && Telefoonnummer.Length > 50)
                {
                    return "Telefoonnummer mag niet meer dan 50 tekens zijn";
                }
                if (columnName == "Straat" && string.IsNullOrEmpty(Straat))
                {
                    return "Straat moet ingevuld zijn!";
                }
                if (columnName == "Straat" && !string.IsNullOrEmpty(Straat) && Straat.Length > 150)
                {
                    return "Straat mag niet meer dan 150 tekens zijn";
                }
                if (columnName == "Huisnummer" && string.IsNullOrEmpty(Huisnummer))
                {
                    return "Huisnummer moet ingevuld zijn!";
                }
                if (columnName == "Huisnummer" && !string.IsNullOrEmpty(Huisnummer) && Huisnummer.Length > 10)
                {
                    return "Huisnummer mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Gemeente" && string.IsNullOrEmpty(Gemeente))
                {
                    return "Gemeente moet ingevuld zijn!";
                }
                if (columnName == "Gemeente" && !string.IsNullOrEmpty(Gemeente) && Gemeente.Length > 40)
                {
                    return "Gemeente mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Email" && string.IsNullOrEmpty(Email))
                {
                    return "Email moet ingevuld zijn!";
                }
                if (columnName == "Email" && !string.IsNullOrEmpty(Email) && Email.Length > 40)
                {
                    return "Email mag niet meer dan 40 tekens zijn";
                }
                if (columnName == "Email" && !Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    return "Email moet van een geldig formaat zijn";
                }
                if (columnName == "Voornaam" && !Professioneel && string.IsNullOrEmpty(Voornaam))
                {
                    return "Voornaam moet ingevuld zijn!";
                }
                if (columnName == "Voornaam" && !string.IsNullOrEmpty(Voornaam) && Voornaam.Length > 100)
                {
                    return "Voornaam mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Achternaam" && !Professioneel && string.IsNullOrEmpty(Achternaam))
                {
                    return "Achternaam moet ingevuld zijn!";
                }
                if (columnName == "Achternaam" && !string.IsNullOrEmpty(Achternaam) && Achternaam.Length > 100)
                {
                    return "Achternaam mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Bedrijfsnaam" && Professioneel && string.IsNullOrEmpty(Bedrijfsnaam))
                {
                    return "Bedrijfsnaam moet ingevuld zijn!";
                }
                if (columnName == "Bedrijfsnaam" && !string.IsNullOrEmpty(Bedrijfsnaam) && Bedrijfsnaam.Length > 100)
                {
                    return "Bedrijfsnaam mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Btwnummer" && Professioneel && string.IsNullOrEmpty(Btwnummer))
                {
                    return "Btwnummer moet ingevuld zijn!";
                }
                if (columnName == "Btwnummer" && !string.IsNullOrEmpty(Btwnummer) && Btwnummer.Length > 50)
                {
                    return "Btwnummer mag niet meer dan 50 tekens zijn";
                }

                return "";
            }
        }
    }
}
