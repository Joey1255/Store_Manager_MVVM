using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Viewmodels;

namespace test.Viewmodels
{
    public class KortingkaartBewerkenViewmodelTest
    {
        KortingkaartBewerkenViewmodel KortingkaartBewerkenViewmodel = new KortingkaartBewerkenViewmodel();

        [Test]
        public void Opslaan_Kortingkaart_Compleet()
        {
            Kortingskaart SelectedKortingkaart = new Kortingskaart();
            SelectedKortingkaart.KortingskaartId = 1;
            SelectedKortingkaart.Code = "absci";
            SelectedKortingkaart.Percentage = 15;
            SelectedKortingkaart.Professioneel = true;
            SelectedKortingkaart.Teller = 2;

            KortingkaartBewerkenViewmodel.SelectedKortingkaart = SelectedKortingkaart;
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Code = "ejkmwiel";
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Professioneel = true;
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Teller = 10;

            KortingkaartBewerkenViewmodel.Opslaan();

            Assert.IsNull(KortingkaartBewerkenViewmodel.Foutmelding);
        }

        [Test]

        public void Opslaan_Kortingkaart_Niet_Compleet()
        {
            Kortingskaart SelectedKortingkaart = new Kortingskaart();
            SelectedKortingkaart.KortingskaartId = 5;
            SelectedKortingkaart.Code = "absci";
            SelectedKortingkaart.Percentage = 15;
            SelectedKortingkaart.Professioneel = true;
            SelectedKortingkaart.Teller = 2;

            KortingkaartBewerkenViewmodel.SelectedKortingkaart = SelectedKortingkaart;
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Code = "";
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Professioneel = false;
            KortingkaartBewerkenViewmodel.SelectedKortingkaart.Teller = 5;

            KortingkaartBewerkenViewmodel.Opslaan();

            Assert.IsNotEmpty(KortingkaartBewerkenViewmodel.Foutmelding);
        }
    }
}
