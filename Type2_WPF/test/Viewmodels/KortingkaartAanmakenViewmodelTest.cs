using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Viewmodels;

namespace test.Viewmodels
{
    [TestFixture]
    public class KortingkaartAanmakenViewmodelTest
    {
        KortingkaartAanmakenViewmodel KortingkaartAanmakenViewmodel = new KortingkaartAanmakenViewmodel();

        [Test]
        public void Opslaan_Kortingkaart_Compleet()
        {
            KortingkaartAanmakenViewmodel.KortingkaartRecord.KortingskaartId = 3;
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Code = "1234567";
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Professioneel = true;
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Teller = 5;

            KortingkaartAanmakenViewmodel.Opslaan();

            Assert.IsNull(KortingkaartAanmakenViewmodel.Foutmelding);
        }

        [Test]

        public void Opslaan_Kortingkaart_Niet_Compleet()
        {
            KortingkaartAanmakenViewmodel.KortingkaartRecord.KortingskaartId = 10;
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Code = "";
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Professioneel = true;
            KortingkaartAanmakenViewmodel.KortingkaartRecord.Teller = 1;

            KortingkaartAanmakenViewmodel.Opslaan();

            Assert.IsNotEmpty(KortingkaartAanmakenViewmodel.Foutmelding);
        }
    }
}
