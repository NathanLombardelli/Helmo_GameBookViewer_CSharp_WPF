using NUnit.Framework;

namespace GameBook_Tests
{
    [TestFixture]
    public class Test {

        [Test]
        public void JeMesureUnTrucBidon() {


            int valeur = 50;

            Assert.That(valeur,Is.EqualTo(50));
        
        }
    
    
    }
 
}
