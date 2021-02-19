using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morocoto.Infraestructure.Tools;

namespace Morocoto.Tests
{
    [TestClass]
    public class AccountNumberGenerationTest
    {
        [TestMethod]
        public void GenerateAccountNumber()
        {
            string accountNumberExpected = "111111111111";
            string accountNumberActual = AccountNumberGeneration.GenerateAccountNumber();

            Assert.AreEqual(accountNumberExpected, accountNumberActual);
        }
    }
}
