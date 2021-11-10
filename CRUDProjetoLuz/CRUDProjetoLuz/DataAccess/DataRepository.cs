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
        //Estanciar class de conexão e comandos 
        Conexao conexao = new Conexao();
        NpgsqlCommand cmd = new NpgsqlCommand();

        public Pessoas Pessoas { get; set; }

       // public DataRepository() { }
        public DataRepository(Pessoas pessoas)
        {
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
                    Pessoas p = new Pessoas();
                    while (lista.Read())
                    {
                        p.Id = Convert.ToInt32(lista["id_pessoa"]);
                        p.Nome = lista["nome"].ToString();
                        p.Sobrenome = lista["sobrenome"].ToString();
                        p.DataNascimento = Convert.ToDateTime(lista["datanascimento"]);
                        p.Sexo = (Sexo)Enum.Parse(typeof(Sexo), lista[name:"sexo"].ToString());
                        p.EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), lista[name:"estadocivil"].ToString());
                        p.DataCadastro = Convert.ToDateTime(lista["datacadastro"]);
                        ListaPessoas.Add(p);
                    }
                };
                lista.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Pega um registro pelo codigo
        public void PegaRegistroPorId(ObservableCollection<Pessoas> ListaPessoas, int id)
        {
            try
            {
                // abre a conexão com o PgSQL, define a instrução SQL, pega o dado pelo id
                cmd.Connection.Open();
                cmd.CommandText = $"Select * from tbl_cadastro Where id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
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
                };
                lista.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Inserir registros
        public void InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                //Passar comandos sql
                cmd.CommandText = "Insert Into tbl_cadastro(nome,sobrenome,datanascimento,sexo,estadocivil,datacadastro)" +
                    " values(@nome,@sobrenome,@datanascimento,@sexo,@estadocivil,@datacadastro)";
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
                cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
                cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            //return Pessoas;
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
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Deleta registros
        public void DeletarRegistro(string nome)
        {
            try
            {
                //abre a conexao                
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                //Passa instrução sql
                cmd.CommandText = "Delete From tbl_cadastro Where nome = @nome";
                cmd.Parameters.AddWithValue("@nome", nome);
                if (MessageBox.Show("Deseja realmente DELETAR " + nome + " do cadastro?", "Deletar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Deletado com sucesso!");
                cmd.Connection.Close();
            }
        }
    }
}
