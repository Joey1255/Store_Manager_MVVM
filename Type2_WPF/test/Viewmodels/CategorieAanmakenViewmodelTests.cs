using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using wpf.Viewmodels;

namespace test.Viewmodels
{
    [TestFixture]
    public class CategorieAanmakenViewmodelTests
    {
        CategorieAanmakenViewmodel CategorieAanmakenViewmodel = new CategorieAanmakenViewmodel();

        [Test]
        public void Opslaan_Categorie_Compleet()
        {
            CategorieAanmakenViewmodel.CategorieRecord.CategorieId = 1;
            CategorieAanmakenViewmodel.CategorieRecord.Naam = "Hamers";
            CategorieAanmakenViewmodel.CategorieRecord.Beschrijving = "Alle hamers";

            CategorieAanmakenViewmodel.Opslaan();

            Assert.IsEmpty(CategorieAanmakenViewmodel.Foutmelding);
        }

        [Test]
        public void Opslaan_Categorie_Niet_Compleet()
        {
            CategorieAanmakenViewmodel.CategorieRecord.CategorieId = 1;
            CategorieAanmakenViewmodel.CategorieRecord.Naam = "test";
            CategorieAanmakenViewmodel.CategorieRecord.Beschrijving = "";

            CategorieAanmakenViewmodel.Opslaan();

            Assert.IsNotEmpty(CategorieAanmakenViewmodel.Foutmelding);
        }
    }
}
