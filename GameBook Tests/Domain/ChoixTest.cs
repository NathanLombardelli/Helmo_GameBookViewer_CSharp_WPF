using GameBook.Domain;
using NUnit.Framework;

namespace GameBook_Tests.Domain
{
    class ChoixTest
    {
        Choix choix2 = new Choix(2,"Aller para 2");
        Choix choix3 = new Choix(3, "");
        
        [Test]
        public void GetRedirTest()
        {
            Assert.AreEqual(2, choix2.Redirection);
            Assert.AreEqual(3, choix3.Redirection);
        }

        [Test]
        public void GetTxtTest()
        {
            Assert.AreEqual("Aller para 2", choix2.ChoixTxt);
            Assert.AreEqual("", choix2.ChoixTxt);
        }




    }
}
