using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
//using MySqlConnector;

namespace CRUDProjetoLuz.DataAccess
{
    public class CommandMYSQL : ICommandSQL
    {
        /*Declaração variáveis de conexão com BD */
        private static string _srvName = "127.0.0.1";   //localhost
        private static string _portID = "5432";              //porta default
        private static string _usrName = "postgres";      //nome do administrador
        private static string _pwd = "root123";     //senha do administrador
        private static string _dtbName = "bdCRUD";   //nome do banco de dados
        private MySqlCommand _cmd;
        private MySqlConnection _bd;
        private string _conString = $"Server={_srvName};Port={_portID};User Id={_usrName};Password={_pwd};Database={_dtbName};";
        public CommandMYSQL()
        {
            _bd = new MySqlConnection(_conString);
            _cmd = new MySqlCommand();
            _bd.Open();
        }
        public List<Pessoas> SelecionarTodos()
        {
            List<Pessoas> ListaPessoas = new List<Pessoas>();
            try
            {
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                _cmd.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                _cmd.ExecuteNonQuery();
                MySqlDataReader lista = _cmd.ExecuteReader();
                if (lista.HasRows)
                {
                    while (lista.Read())
                    {
                        ListaPessoas.Add(new Pessoas()
                        {
                            Id = Convert.ToInt32(lista["id_pessoa"]),
                            Nome = lista["nome"].ToString(),
                            Sobrenome = lista["sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(lista["datanascimento"]),
                            Sexo = Enum.Parse<Sexo>(lista[name: "sexo"].ToString()),//(Sexo)Enum.Parse(typeof(Sexo), lista[name: "sexo"].ToString())
                            EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), lista[name: "estadocivil"].ToString()),
                            DataCadastro = Convert.ToDateTime(lista["datacadastro"])
                        });
                    }
                };
                lista.Close();
            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _cmd.Connection.Close();
            }
            return ListaPessoas;
        }
        //Pega um registro pelo codigo
        public int SelecionaRegistroID(ObservableCollection<Pessoas> ListaPessoas)
        {
            //Abra a conexão com o PgSQL
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            try
            {
                int _id = 0;
                //Iinstrução SQL, pega o ultimo id dado
                _cmd.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                _cmd.ExecuteNonQuery();
                MySqlDataReader lista = _cmd.ExecuteReader();
                if (lista.Read())
                {
                    Pessoas p = new Pessoas();
                    p.Id = Convert.ToInt32(lista["id_pessoa"]);
                    p.Nome = lista["nome"].ToString();
                    p.Sobrenome = lista["sobrenome"].ToString();
                    p.DataNascimento = Convert.ToDateTime(lista["datanascimento"]);
                    p.Sexo = Enum.Parse<Sexo>(lista[name: "sexo"].ToString());
                    p.EstadoCivil = Enum.Parse<EstadoCivil>(lista[name: "estacivil"].ToString());
                    p.DataCadastro = Convert.ToDateTime(lista["datacadastro"]);
                    ListaPessoas.Add(p);
                    _id = p.Id;
                };
                lista.Close();
                return _id;

            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }
        //Inserir registros
        public int InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            int idInserido = 0;
            try
            {
                //Abra a conexão com o PgSQL
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                //Passar comandos sql
                _cmd.CommandText = "Insert Into tbl_cadastro(nome,sobrenome,datanascimento,sexo,estadocivil,datacadastro)" +
                    " values(@nome,@sobrenome,@datanascimento,@sexo,@estadocivil,@datacadastro) RETURNING id_pessoa;";
                _cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                _cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                _cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                _cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                _cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                _cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                _cmd.Prepare();
                _cmd.ExecuteNonQuery();
                MySqlDataReader inserido = _cmd.ExecuteReader();
                return idInserido = Convert.ToInt32(inserido["id_pessoa"]);
            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL                  
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                //Passa comando sql
                _cmd.CommandText = "Update tbl_cadastro Set nome = @nome, sobrenome = @sobrenome, datanascimento = @datanascimento," +
                    " sexo = @sexo, estadocivil = @estadocivil, datacadastro = @datacadastro " +
                    "where id_pessoa = @id;";
                _cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                _cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                _cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                _cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                _cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                _cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                _cmd.Parameters.AddWithValue("@id", (int)pessoas.Id);
                // cmd.Parameters.AddWithValue("@id", pessoas.Id);
                _cmd.Prepare();
                _cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }
        //Deleta registros
        public void DeletarRegistro(int Id)
        {
            try
            {
                //abre a conexao                
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                //Passa instrução sql
                _cmd.CommandText = "Delete From tbl_cadastro Where id_pessoa = @Id;";
                _cmd.Parameters.AddWithValue("@Id", (int)Id);
                _cmd.Prepare();
                _cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }
    }
}
