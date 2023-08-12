using System.Collections.Generic;
using GameBook.Domain;
using Moq;
using NUnit.Framework;

namespace GameBook_Tests.Domain
{
    class BookTest
    {
        [Test]
        public void GetTitleTest()
        {

            //Creating a Mock
            var pMock1 = new Mock<Paragraph>();
            var pMock2 = new Mock<Paragraph>();
            //Perform method setup
            pMock1.Setup(p1 => p1.Image).Returns("c:\\Backgound.jpg");
            pMock2.Setup(p2 => p2.Image).Returns("c:\\Backgound2.jpg");

            pMock1.Setup(p1 => p1.Text).Returns("Paragraph n 1");
            pMock2.Setup(p2 => p2.Text).Returns("Paragraph n 2");

            List<Paragraph> list = new List<Paragraph>();


            //Book livre1 = new Book("livre 1", pMock1);
            Book livre2 = new Book("livre 2", null);

           // Assert.AreEqual("livre 1",livre1.Titre);
            Assert.AreEqual("livre 2", livre2.Titre);
        }


    }
}
