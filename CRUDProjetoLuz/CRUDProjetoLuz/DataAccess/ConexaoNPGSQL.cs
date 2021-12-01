using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace CRUDProjetoLuz.DataAccess
{
    public class ConexaoNPGSQL : IConexaoDB
    {
        /*Declaração variáveis de conexão com BD */
        private static string _srvName = "127.0.0.1";   //localhost
        private static string _portID = "5432";              //porta default
        private static string _usrName = "postgres";      //nome do administrador
        private static string _pwd = "root123";     //senha do administrador
        private static string _dtbName = "bdCRUD";   //nome do banco de dados
        private readonly NpgsqlConnection _connection;
        private string _connString = $"Server={_srvName};Port={_portID};User Id={_usrName};Password={_pwd};Database={_dtbName};";

        public ConexaoNPGSQL()
        {
            _connection = new NpgsqlConnection(_connString);
        }
        public NpgsqlConnection Open()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("---------------------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return _connection;
        }
        public NpgsqlConnection Close()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("---------------------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return _connection;
        }
    }
}
