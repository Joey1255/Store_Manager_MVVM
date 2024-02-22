using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Viewmodels;

namespace test.Viewmodels
{
    [TestFixture]
    public class LocatieAanmakenViewmodelTests
    {
        LocatieAanmakenViewmodel LocatieAanmakenViewmodel = new LocatieAanmakenViewmodel();

        [Test]
        public void Opslaan_Locatie_Compleet()
        {
            LocatieAanmakenViewmodel.LocatieRecord.LocatieId = 3;
            LocatieAanmakenViewmodel.LocatieRecord.Naam = "Zwijndrecht";

            LocatieAanmakenViewmodel.Opslaan();

            Assert.IsNull(LocatieAanmakenViewmodel.Foutmelding);
        }

        [Test]
        public void Opslaan_Locatie_Niet_Compleet()
        {
            LocatieAanmakenViewmodel.LocatieRecord.LocatieId = 7;
            LocatieAanmakenViewmodel.LocatieRecord.Naam = "";

            LocatieAanmakenViewmodel.Opslaan();

            Assert.IsNotEmpty(LocatieAanmakenViewmodel.Foutmelding);
        }
    }
}
