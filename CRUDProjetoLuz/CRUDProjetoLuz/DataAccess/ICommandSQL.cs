using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace CRUDProjetoLuz.DataAccess
{
    public interface ICommandSQL
    {
        int InserirRegistro(Pessoas pessoas);
        List<Pessoas> SelecionaTodos();
        void AtualizarRegistro(Pessoas pessoas);
        void DeletarRegistro(int Id);

    }
}
