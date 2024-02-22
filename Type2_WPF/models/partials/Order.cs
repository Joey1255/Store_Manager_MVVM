using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Order : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Ordernummer" && string.IsNullOrEmpty(Ordernummer))
                {
                    return "Ordernummer moet ingevuld worden";
                }
                if (columnName == "KlantId" && KlantId <= 0)
                {
                    return "Selecteer een Klant";
                }
                return "";
            }
        }
    }
}
