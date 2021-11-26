using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace CRUDProjetoLuz.DataAccess
{
    class ConexaoNPGSQL : IConexaoDB
    {
        /*Declaração variáveis de conexão com BD */
        private static string _srvName = "127.0.0.1";   //localhost
        private static string _portID = "5432";              //porta default
        private static string _usrName = "postgres";      //nome do administrador
        private static string _pwd = "root123";     //senha do administrador
        private static string _dtbName = "bdCRUD";   //nome do banco de dados
        private SqlCommand _command;
        private SqlConnection _bdados;
        private string _connString = $"Server={_srvName};Port={_portID};User Id={_usrName};Password={_pwd};Database={_dtbName};";

        public ConexaoNPGSQL()
        {
            _bdados = new SqlConnection(_connString);
            _command = new SqlCommand();
        }
        public void Open()
        {
            try
            {
                if (_command.Connection.State == ConnectionState.Closed)
                {
                    _bdados.Open();
                }
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
        }
        public void Closed()
        {
            try
            {
                if (_command.Connection.State == ConnectionState.Open)
                {
                    _bdados.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
        }
    }
}
