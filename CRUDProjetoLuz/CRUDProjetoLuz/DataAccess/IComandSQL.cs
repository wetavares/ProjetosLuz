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
    public interface IConexao
    {
        //void Conectar();
        //void Desconectar();
        void SelecionaTodos(ObservableCollection<Pessoas> ListaPessoas);
        int SelecionaRegistroID(ObservableCollection<Pessoas> ListaPessoas);
        int InserirRegistro(Pessoas pessoas);
        void AtualizarRegistro(Pessoas pessoas);
        void DeletarRegistro(int Id);

    }
}
