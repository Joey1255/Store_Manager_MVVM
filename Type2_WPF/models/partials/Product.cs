using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Product : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "ProdcutId" && ProductId < 0)
                {
                    return "ProductId moet een positief getal zijn!";
                }
                if (columnName == "Naam" && string.IsNullOrEmpty(Naam))
                {
                    return "Naam moet ingevuld worden";
                }
                if (columnName == "Naam" && !string.IsNullOrEmpty(Naam) && Naam.Length > 100)
                {
                    return "Naam mag niet meer dan 15 tekens zijn";
                }
                if (columnName == "Beschrijving" && string.IsNullOrEmpty(Beschrijving))
                {
                    return "Beschrijving moet ingevuld worden";
                }
                if (columnName == "Beschrijving" && !string.IsNullOrEmpty(Beschrijving) && Naam.Length > 250)
                {
                    return "Beschrijving mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "Prijs" && Prijs < 0)
                {
                    return "Prijs moet een positief getal zijn";
                }
                if (columnName == "Productnummer" && string.IsNullOrEmpty(Productnummer))
                {
                    return "Productnummer moet ingevuld worden";
                }
                if (columnName == "Productnummer" && !string.IsNullOrEmpty(Productnummer) && Productnummer.Length > 100)
                {
                    return "Productnummer mag niet meer dan 100 tekens zijn";
                }
                if (columnName == "CategorieId" && CategorieId <= 0 )
                {
                   return "Selecteer een Categorie";
                }
                return "";
            }
        }
    }
}
