using models.partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public partial class Factuur : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "FactuurId" && FactuurId < 0)
                {
                    return "FactuurId moet een positief getal zijn!";
                }
                if (columnName == "BtwPercentage" && BtwPercentage <= 0)
                {
                    return "Selecteer een BtwPercentage";
                }
                if (columnName == "OrderId" && OrderId <= 0)
                {
                    return "Selecteer een OrderId";
                }
                return "";
            }
        }
    }
}
