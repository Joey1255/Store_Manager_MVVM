using models.partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public partial class Locatie : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "LocatieId" && LocatieId < 0)
                {
                    return "LocatieId moet een positief getal zijn!";
                }
                if (columnName == "Naam" && string.IsNullOrEmpty(Naam))
                {
                    return "Naam moet ingevuld worden";
                }
                if (columnName == "Naam" && !string.IsNullOrEmpty(Naam) && Naam.Length > 100)
                {
                    return "Naam mag niet meer dan 100 tekens zijn";
                }
                return "";
            }
        }
    }
}
