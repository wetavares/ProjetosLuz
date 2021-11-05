using Npgsql;
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
                dtAdapter.Fill(dtSet,"tbl_cadastro");
                foreach (DataRow dataRow in dtSet.Tables[0].Rows)
                {
                    ListaPessoas.Add(new Pessoas()
                    {//string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad
                        Id = (int)dataRow[0],
                        Nome = dataRow["nome"].ToString(),
                        Sobrenome = dataRow["sobrenome"].ToString(),
                        DataNascimento = DateTime.Parse(dataRow["data_nasc"].ToString()),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), dataRow["sexo"].ToString()),
                        EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), dataRow["estadocivil"].ToString()),
                        DataCadastro = DateTime.Parse(dataRow["data_cad"].ToString())
                    });
                }
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
        public void PegaRegistroPorId(int id)
        {
            try
            {
                // abre a conexão com o PgSQL, define a instrução SQL, pega o dado pelo id
                cmd.Connection.Open();
                cmd.CommandText = "Select * from tbl_cadastro Where id = " + id;
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter dtAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dtSet = new DataSet("tbl_cadastro");
                dtAdapter.Fill(dtSet,"tbl_cadastro");
                foreach (DataRow dataRow in dtSet.Tables[0].Rows)
                {
                    ListaPessoas.Add(new Pessoas()
                    {//string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad
                        Id = (int)dataRow[0],
                        Nome = dataRow["nome"].ToString(),
                        Sobrenome = dataRow["sobrenome"].ToString(),
                        DataNascimento = DateTime.Parse(dataRow["data_nasc"].ToString()),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), dataRow["sexo"].ToString()),
                        EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), dataRow["estadocivil"].ToString()),
                        DataCadastro = DateTime.Parse(dataRow["data_cad"].ToString())
                    });
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        //Inserir registros
        public void InserirRegistro(Pessoas pessoas)  //string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad
        {
            try
            {
                //Abra a conexão com o PgSQL
                cmd.Connection.Open();
                //Passar comandos sql
                cmd.CommandText = "Insert Into tbl_cadstro(nome,sobrenome,data_nasc,sexo,estadocivil,data_cad) " +
                    "values('" + pessoas.Nome + "','" + pessoas.Sobrenome + "','" + pessoas.DataNascimento + "','" + pessoas.Sexo + "','" + pessoas.EstadoCivil + "','" + pessoas.DataCadastro + "';";
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
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad
        {
            try
            {
                //Abra a conexão com o PgSQL                  
                cmd.Connection.Open();
                //Passa comando sql
                cmd.CommandText = "Update tbl_cadastro Set nome = '" + pessoas.Nome + " , sobrenome = " + pessoas.Sobrenome  +
                    ", data_nasc = " + pessoas.DataNascimento + ", sexo = " + pessoas.Sobrenome + 
                    ", estadocivil = " + pessoas.EstadoCivil + ", data_cad = " + pessoas.Sobrenome + 
                    " Where id_pessoa = " + pessoas.Id + ";";
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
        /*
        //Deleta registros
        public void DeletarRegistro(string nome)
        {
            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //abre a conexao                
                    pgsqlConnection.Open();
                    //Passa instrução sql
                    string cmdDeletar = String.Format("Delete From tbl_cadastro Where nome = '{0}'", nome);

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletar, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        */
    }
}
