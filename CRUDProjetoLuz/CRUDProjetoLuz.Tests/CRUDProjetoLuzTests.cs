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
             
        public class TesteConexaoNPGSQL: ICommandSQL
        {
            CommandNPGSQL cmd = new CommandNPGSQL();
            
            [Test]

            public void TestaSeEstaSelecionandoTodos()
            {
                ObservableCollection<Pessoas> pessoas = new ObservableCollection<Pessoas>();
                pessoas = cmd.SelecionarTodos();

                Assert.IsNotNull(pessoas.Count);
            }
            [Test]
            public void AtualizarRegistro(Pessoas pessoas)
            {
                throw new NotImplementedException();
            }
            [Test]
            public void DeletarRegistro(int Id)
            {
                throw new NotImplementedException();
            }
            [Test]
            public int InserirRegistro(Pessoas pessoas)
            {
                throw new NotImplementedException();
            }
            [Test]
            public ObservableCollection<Pessoas> SelecionarTodos()
            {
                throw new NotImplementedException();
            }
        }
    }
}
