using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models.partials;

namespace models
{
    public partial class Kortingskaart : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {

                if (columnName == "Code" && string.IsNullOrEmpty(Code))
                {
                    return "Code moet ingevuld worden";
                }
                if (columnName == "Teller" && Teller < 0)
                {
                    return "Aantal voorgaande aankopen moet een positief getal zijn!";
                }
                return "";
            }
        }
    }
}
