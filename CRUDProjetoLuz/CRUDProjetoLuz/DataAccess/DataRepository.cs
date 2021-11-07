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

namespace CRUDProjetoLuz.DataAccess
{
    public class DataRepository
    {
        //Estanciar class de conexão e comandos 
        Conexao conexao = new Conexao();
        NpgsqlCommand cmd = new NpgsqlCommand();

        public Pessoas Pessoas { get; set; }
        public ObservableCollection<Pessoas> ListaPessoas { get; private set; }


        public DataRepository() { }
        public DataRepository(Pessoas pessoas)
        {
            cmd.Connection = conexao.Conectar();
        }
        //Definição dos metodos
        //Pega todos os registros
        public void PegaTodosRegistros()
        {
            try
            {
                // abre a conexão com o PgSQL e define a instrução SQL
                //cmd.Connection = conexao.Conectar();//??? posso chamar direto?
                cmd.Connection.Open();
                cmd.CommandText = "Select * from tbl_cadastro order by id_pessoa;";
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter dtAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dtSet = new DataSet();
                dtAdapter.Fill(dtSet, "tbl_cadastro");
                foreach (DataRow dataRow in dtSet.Tables[0].Rows)
                {
                    ListaPessoas.Add(new Pessoas()
                    {//string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
                        Id = (int)dataRow[0],
                        Nome = dataRow["nome"].ToString(),
                        Sobrenome = dataRow["sobrenome"].ToString(),
                        DataNascimento = DateTime.Parse(dataRow["DataNascimento"].ToString()),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), dataRow["sexo"].ToString()),
                        EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), dataRow["estadocivil"].ToString()),
                        DataCadastro = DateTime.Parse(dataRow["DataCadastro"].ToString())
                    });
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
                cmd.Connection.Close();
            }
        }
        //Pega um registro pelo codigo
        public void PegaRegistroPorId(int id)
        {
            try
            {
                // abre a conexão com o PgSQL, define a instrução SQL, pega o dado pelo id
                cmd.Connection.Open();
                cmd.CommandText = $"Select * from tbl_cadastro Where id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter dtAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dtSet = new DataSet("tbl_cadastro");
                dtAdapter.Fill(dtSet, "tbl_cadastro");
                foreach (DataRow dataRow in dtSet.Tables[0].Rows)
                {
                    ListaPessoas.Add(new Pessoas()
                    {//string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
                        Id = (int)dataRow[0],
                        Nome = dataRow["nome"].ToString(),
                        Sobrenome = dataRow["sobrenome"].ToString(),
                        DataNascimento = DateTime.Parse(dataRow["DataNascimento"].ToString()),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), dataRow["sexo"].ToString()),
                        EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), dataRow["estadocivil"].ToString()),
                        DataCadastro = DateTime.Parse(dataRow["DataCadastro"].ToString())
                    });
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
                cmd.Connection.Close();
            }
        }
        //Inserir registros
        public Pessoas InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL
                cmd.Connection.Open();
                //Passar comandos sql
                cmd.CommandText = "Insert Into tbl_cadstro(nome,sobrenome,DataNascimento,sexo,estadocivil,DataCadastro) values(@nome,@sobrenome,@DataNascimento,@sexo,@estacivil,@DataCadastro)";
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@DataNascimento", pessoas.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo);
                cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil);
                cmd.Parameters.AddWithValue("@DataCadastro", pessoas.DataCadastro);
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
            return Pessoas;//a definir
        }
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            try
            {
                //Abra a conexão com o PgSQL                  
                cmd.Connection.Open();
                //Passa comando sql
                cmd.CommandText = "Update tbl_cadastro Set nome = @nome, sobrenome = @sobrenome, DataNascimento = @DataNascimento, sexo = @sexo, estadocivil = @estadocivil, DataCadastro = @DataCadastro where id_pessoa = @id;";
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@DataNascimento", pessoas.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo = ", pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil);
                cmd.Parameters.AddWithValue("@DataCadastro", pessoas.Sobrenome);
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
                cmd.Connection.Open();
                //Passa instrução sql
                cmd.CommandText = "Delete From tbl_cadastro Where nome = @nome";
                cmd.Parameters.AddWithValue("@nome", nome);
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
    }
}
