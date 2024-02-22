using models.partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace models
{
    public partial class Medewerker: Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "MedewerkerId" && MedewerkerId < 0)
                {
                    return "Medewerker Id moet een positief getal zijn!";
                }
                if (columnName == "Voornaam" && string.IsNullOrEmpty(Voornaam))
                {
                    return "Voornaam moet ingevuld worden";
                }
                if (columnName == "Voornaam" && !string.IsNullOrEmpty(Voornaam) && Voornaam.Length > 100)
                {
                    return "Voornaam mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Achternaam" && string.IsNullOrEmpty(Achternaam))
                {
                    return "Achternaam moet ingevuld worden";
                }
                if (columnName == "Achternaam" && !string.IsNullOrEmpty(Achternaam) && Achternaam.Length > 100)
                {
                    return "Achternaamnaam mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Email" && string.IsNullOrEmpty(Email))
                {
                    return "Email moet ingevuld worden";
                }
                if (columnName == "Email" && !string.IsNullOrEmpty(Email) && Email.Length > 100)
                {
                    return "Email mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Email" && !Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    return "Email moet van een geldig formaat zijn";
                }
                if (columnName == "Paswoord" && string.IsNullOrEmpty(Paswoord))
                {
                    return "Paswoord moet ingevuld worden";
                }
                if (columnName == "Paswoord" && !string.IsNullOrEmpty(Paswoord) && Paswoord.Length > 40)
                {
                    return "Paswoord mag niet meer dan 40 tekens zijn";
                }

                return "";
            }
        }
    }
}
