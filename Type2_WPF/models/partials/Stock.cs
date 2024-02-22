using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Stock : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "LocatieId" && LocatieId <= 0)
                {
                    return "Selecteer een Locatie";
                }
                if (columnName == "ProductId" && ProductId <= 0)
                {
                    return "Selecteer een Poduct";
                }
                if (columnName == "Aantal" && Aantal < 0)
                {
                    return "Aantal moet een positief getal zijn";
                }
                return "";
            }
        }
    }
}
