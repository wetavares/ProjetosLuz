using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Npgsql;
using System.Collections.Generic;
using CRUDProjetoLuz.DataAccess;

namespace CRUDProjetoLuz.Tests
{
    public class CRUDProjetoLuzTests
    {
        [TestFixture]
             
        public class TesteConexaoNPGSQL
        {
            CommandNPGSQL cmd = new CommandNPGSQL();

            [Test]

            public void TestaSeEstaSelecionandoTodosRegistrosDoBanco()
            {
                List<Pessoas> pessoas = new List<Pessoas>();
                pessoas = cmd.SelecionarTodos();

                Assert.IsEmpty(pessoas);
                Assert.IsEmpty(pessoas);
            }
        }
    }
}
