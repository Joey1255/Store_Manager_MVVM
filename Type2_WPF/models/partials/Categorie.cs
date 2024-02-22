using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Categorie : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "CategorieId" && CategorieId < 0)
                {
                    return "CategorieId moet een positief getal zijn!" ;
                }
                if (columnName == "Naam" && string.IsNullOrEmpty(Naam))
                {
                    return "Naam moet ingevuld worden" ;
                }
                if (columnName == "Naam" && !string.IsNullOrEmpty(Naam) && Naam.Length>100)
                {
                    return "Naam mag niet meer dan 100 tekens zijn" ;
                }
                if (columnName == "Beschrijving" && string.IsNullOrEmpty(Beschrijving))
                {
                    return "Beschrijving moet ingevuld worden" ;
                }
                if (columnName == "Naam" && !string.IsNullOrEmpty(Beschrijving) && Beschrijving.Length > 250)
                {
                    return "Beschrijving mag niet meer dan 250 tekens zijn";
                }
                return "";
            }
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}
