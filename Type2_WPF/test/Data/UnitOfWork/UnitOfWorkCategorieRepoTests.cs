using dal.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using System.Collections.ObjectModel;
using models;

namespace test.Data.UnitOfWork
{
    [TestFixture]
    public class UnitOfWorkCategorieRepoTests
    {
        private IUnitOfWork unitOfWork = A.Fake<IUnitOfWork>();
        [Test]
        public void Ophalen_ReturnObservableCollectionTypeCategorieen()
        {
            ObservableCollection<Categorie> Categorieen;

            Categorieen= new ObservableCollection<Categorie>(unitOfWork.CategorieRepo.Ophalen());

            Assert.NotNull(Categorieen);
            Assert.IsInstanceOf<ObservableCollection<Categorie>>(Categorieen);
        }

        [Test]
        public void ZoekOpPK_Return1Categorie()
        {
            Categorie categorie = A.Fake<Categorie>();

            categorie = unitOfWork.CategorieRepo.ZoekOpPK(categorie.CategorieId);

            Assert.NotNull(categorie);
            Assert.IsInstanceOf<Categorie>(categorie);

        }
    }
}
