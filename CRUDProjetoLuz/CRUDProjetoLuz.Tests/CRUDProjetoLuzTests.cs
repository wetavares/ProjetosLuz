using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Npgsql;

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
                NpgsqlDataReader novaLista = new 
                Assert.IsNotNull(Listapessoas.Count);
            }
        }
    }
}
