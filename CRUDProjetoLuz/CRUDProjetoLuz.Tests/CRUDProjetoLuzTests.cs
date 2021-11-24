using System;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace CRUDProjetoLuz.Tests
{
    public class CRUDProjetoLuzTests
    {
        [TestFixture]
        public class ConexaoNPGSQL
        {
            [Test]
            public void TestaSelecionaTodos(ObservableCollection<Pessoas> Listapessoas)
            {
                Assert.IsNotNull(Listapessoas.Count);
            }
        }
    }
}
