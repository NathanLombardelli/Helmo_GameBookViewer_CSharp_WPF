
using GameBook.Domain;
using NUnit.Framework;

namespace GameBook_Tests.Domain
{
    class ParagraphsTest
    {
         [Test]
         public void GetNumTest()
         {
             Paragraph para1 = new Paragraph(1, "para 1", null,"");
             Paragraph para2 = new Paragraph(2, "para 2", null,"");
             Assert.AreEqual(1,para1.NumPara);
             Assert.AreEqual(2, para2.NumPara);
        }

         [Test]
         public void GetTxtTest()
         {
             Paragraph para1 = new Paragraph(1, "para 1", null,"");
             Paragraph para2 = new Paragraph(2, "para 2", null,"");
             Assert.AreEqual("para 1", para1.Text);
             Assert.AreEqual("para 2", para2.Text);
        }

    }
}
