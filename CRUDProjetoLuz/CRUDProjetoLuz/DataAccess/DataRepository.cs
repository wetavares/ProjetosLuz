using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity.Core.Metadata.Edm;

namespace CRUDProjetoLuz.DataAccess
{
    public class DataRepository
    {


        public Pessoas Pessoas { get; set; }

        // public DataRepository() { }
        //Estanciar class de conexão e comandos 
        private Conexao conexao;
        private NpgsqlCommand cmd;
        public DataRepository()
        {
            conexao = new Conexao();
            cmd = new NpgsqlCommand();
            cmd.Connection = conexao.Conectar();
        }
        //Definição dos metodos
        //Pega todos os registros
        public void PegaTodosRegistros(ObservableCollection<Pessoas> ListaPessoas)
        {
            try
            {
                //Abra a conexão com o PgSQL
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                // Define a instrução SQL
                cmd.CommandText = "Select * from tbl_cadastro order by id_pessoa;";
                cmd.ExecuteNonQuery();
                NpgsqlDataReader lista = cmd.ExecuteReader();
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
                            Sexo = (Sexo)Enum.Parse(typeof(Sexo), lista[name: "sexo"].ToString()),
                            EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), lista[name: "estadocivil"].ToString()),
                            DataCadastro = Convert.ToDateTime(lista["datacadastro"])
                        });
                    }
                };
                lista.Close();
            }
            catch (NpgsqlException ex)
            {
               throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Pega um registro pelo codigo
        public int PegaIdRegistro(ObservableCollection<Pessoas> ListaPessoas)
        {
            //Abra a conexão com o PgSQL
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            try
            {
                int _id = 0;
                //Iinstrução SQL, pega o ultimo id dado
                cmd.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                cmd.ExecuteNonQuery();
                NpgsqlDataReader lista = cmd.ExecuteReader();
                if(lista.Read())
                {
                    Pessoas p = new Pessoas();
                    p.Id = Convert.ToInt32(lista["id_pessoa"]);
                    p.Nome = lista["nome"].ToString();
                    p.Sobrenome = lista["sobrenome"].ToString();
                    p.DataNascimento = Convert.ToDateTime(lista["datanascimento"]);
                    p.Sexo = (Sexo)Enum.Parse(typeof(Sexo), lista[name: "sexo"].ToString());
                    p.EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), lista[name: "estacivil"].ToString());
                    p.DataCadastro = Convert.ToDateTime(lista["datacadastro"]);
                    ListaPessoas.Add(p);
                    _id = p.Id;
                };
                lista.Close();
                return _id;

            }
            catch (NpgsqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Inserir registros
        public int InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            int idInserido = 0;
            try
            {
                //Abra a conexão com o PgSQL
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                //Passar comandos sql
                cmd.CommandText = "Insert Into tbl_cadastro(nome,sobrenome,datanascimento,sexo,estadocivil,datacadastro)" +
                    " values(@nome,@sobrenome,@datanascimento,@sexo,@estadocivil,@datacadastro) RETURNING id_pessoa;";
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                NpgsqlDataReader inserido = cmd.ExecuteReader();
                return idInserido = Convert.ToInt32(inserido["id_pessoa"]);
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL                  
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                //Passa comando sql
                cmd.CommandText = "Update tbl_cadastro Set nome = @nome, sobrenome = @sobrenome, datanascimento = @datanascimento," +
                    " sexo = @sexo, estadocivil = @estadocivil, datacadastro = @datacadastro " +
                    "where id_pessoa = @id;";
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo = ", pessoas.Sexo);
                cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil);
                cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                cmd.Parameters.AddWithValue("@Where id_pessoa", pessoas.Id);
                cmd.Parameters.AddWithValue("@id", pessoas.Id);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Deleta registros
        public void DeletarRegistro(int Id)
        {
            try
            {
                //abre a conexao                
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                //Passa instrução sql
                cmd.CommandText = "Delete From tbl_cadastro Where id_pessoa = @Id;";
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deletado com sucesso!");
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}
