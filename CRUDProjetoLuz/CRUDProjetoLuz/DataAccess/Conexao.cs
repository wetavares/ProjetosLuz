using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using System.Data.SqlClient;

namespace CRUDProjetoLuz.DataAccess
{
    public class Conexao
    {
        /*Declaração variáveis de conexão com BD */
        private static string serverName = "127.0.0.1";   //localhost
        private static string port = "5432";              //porta default
        private static string userName = "postgres";      //nome do administrador
        private static string password = "root123";     //senha do administrador
        private static string databaseName = "bdCRUD";   //nome do banco de dados

        //Estanciar a conexão                                  
        private NpgsqlConnection connDB;
        private string connString;

        /*Construtor com definição da string de conexão com BD*/

        public Conexao()
        {
            
            connString = string.Format( "Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                        serverName,
                                        port, 
                                        userName,
                                        password, 
                                        databaseName);
            connDB = new NpgsqlConnection(connString);

        }
       //Metodo para Conectar
       public NpgsqlConnection Conectar()
        {
            try
            {
                //Verifica se conexão fechada e caso true abre a conexão
                if (connDB.State == System.Data.ConnectionState.Closed)
                {
                    connDB.Open();
                }
            }
            //Mensagem de erro
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            return connDB;
        }
        //Metodo Desconectar
        public void Desconectar()
        {
            try
            {
                //Verifica se a conexão esta abert e caso true fecha a conexão
                if (connDB.State == System.Data.ConnectionState.Open)
                {
                    connDB.Close();
                }
            }
            //Mensagem de erro
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }

        }
    }
}
