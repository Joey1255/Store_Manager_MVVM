using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Views;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace test.Views
{
    [TestFixture]
    public class CategorieAanmakenViewTests
    {
        string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        string WpfAppId = @"C:\Users\joeym\Desktop\Project agile\2022-GPR-Agile-Lier-Type2\Type2_WPF\Type2\bin\Debug\net6.0-windows\wpf.exe";

        WindowsDriver<WindowsElement> session;

        WindowsElement naam, beschrijving, opslaanButton, annuleerButton;

        [SetUp]
        public void Setup()
        {
            if (session == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

                naam = session.FindElementByAccessibilityId("Naam");
                beschrijving = session.FindElementByAccessibilityId("Beschrijving");
                annuleerButton = session.FindElementByAccessibilityId("AnnuleerButton");
                opslaanButton = session.FindElementByAccessibilityId("OpslaanButton");
            }
        }

        [Test]
        public void AddNewCategorie()
        {
            naam.SendKeys("Hamers");
            beschrijving.SendKeys("Alle hamers");
            opslaanButton.Click();

            Assert.AreEqual("", naam.Text);
            Assert.AreEqual("", beschrijving.Text);
            
        }

        [TearDown]
        public void TearDown()
        {
            naam.Clear();
            beschrijving.Clear();
        }

        [OneTimeTearDown]
        public void CloseSession()
        {
            session.Close();
        }
    }
}
