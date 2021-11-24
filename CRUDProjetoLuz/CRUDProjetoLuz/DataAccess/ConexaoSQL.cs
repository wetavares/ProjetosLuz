using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace CRUDProjetoLuz.DataAccess
{ 
    public class ConexaoSQL : IComandSQL
    { /*Declaração variáveis de conexão com BD */
        private static string _srvName = "127.0.0.1";   //localhost
        private static string _portID = "5432";              //porta default
        private static string _usrName = "postgres";      //nome do administrador
        private static string _pwd = "root123";     //senha do administrador
        private static string _dtbName = "bdCRUD";   //nome do banco de dados
        private SqlCommand _comand;
        private SqlConnection _bdados;
        private string _connString = $"Server={_srvName};Port={_portID};User Id={_usrName};Password={_pwd};Database={_dtbName};";


        public ConexaoSQL()
        {
            _bdados = new SqlConnection(_connString);
            _comand = new SqlCommand();
            _bdados.Open();
        }
        public void SelecionaTodos(ObservableCollection<Pessoas> ListaPessoas)
        {
            try
            {
                if (_comand.Connection.State == ConnectionState.Closed)
                {
                    _comand.Connection.Open();
                }
                _comand.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                _comand.ExecuteNonQuery();
                SqlDataReader lista = _comand.ExecuteReader();
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
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _comand.Connection.Close();
            }
        }
        //Pega um registro pelo codigo
        public int SelecionaRegistroID(ObservableCollection<Pessoas> ListaPessoas)
        {
            //Abra a conexão com o PgSQL
            if (_comand.Connection.State == ConnectionState.Closed)
            {
                _comand.Connection.Open();
            }
            try
            {
                int _id = 0;
                //Iinstrução SQL, pega o ultimo id dado
                _comand.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                _comand.ExecuteNonQuery();
                SqlDataReader lista = _comand.ExecuteReader();
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
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _comand.Connection.Close();
            }
        }
        //Inserir registros
        public int InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            int idInserido = 0;
            try
            {
                //Abra a conexão com o PgSQL
                if (_comand.Connection.State == ConnectionState.Closed)
                {
                    _comand.Connection.Open();
                }
                //Passar comandos sql
                _comand.CommandText = "Insert Into tbl_cadastro(nome,sobrenome,datanascimento,sexo,estadocivil,datacadastro)" +
                    " values(@nome,@sobrenome,@datanascimento,@sexo,@estadocivil,@datacadastro) RETURNING id_pessoa;";
                _comand.Parameters.AddWithValue("@nome", pessoas.Nome);
                _comand.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                _comand.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                _comand.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                _comand.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                _comand.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                _comand.Prepare();
                _comand.ExecuteNonQuery();
                SqlDataReader inserido = _comand.ExecuteReader();
                return idInserido = Convert.ToInt32(inserido["id_pessoa"]);
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _comand.Connection.Close();
            }
        }
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL                  
                if (_comand.Connection.State == ConnectionState.Closed)
                {
                    _comand.Connection.Open();
                }
                //Passa comando sql
                _comand.CommandText = "Update tbl_cadastro Set nome = @nome, sobrenome = @sobrenome, datanascimento = @datanascimento," +
                    " sexo = @sexo, estadocivil = @estadocivil, datacadastro = @datacadastro " +
                    "where id_pessoa = @id;";
                _comand.Parameters.AddWithValue("@nome", pessoas.Nome);
                _comand.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                _comand.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                _comand.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                _comand.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                _comand.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                _comand.Parameters.AddWithValue("@id", (int)pessoas.Id);
                // cmd.Parameters.AddWithValue("@id", pessoas.Id);
                _comand.Prepare();
                _comand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _comand.Connection.Close();
            }
        }
        //Deleta registros
        public void DeletarRegistro(int Id)
        {
            try
            {
                //abre a conexao                
                if (_comand.Connection.State == ConnectionState.Closed)
                {
                    _comand.Connection.Open();
                }
                //Passa instrução sql
                _comand.CommandText = "Delete From tbl_cadastro Where id_pessoa = @Id;";
                _comand.Parameters.AddWithValue("@Id", (int)Id);
                _comand.Prepare();
                _comand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _comand.Connection.Close();
            }
        }
    }
}
