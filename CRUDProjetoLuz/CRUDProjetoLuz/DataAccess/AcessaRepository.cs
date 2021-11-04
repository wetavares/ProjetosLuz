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
    public class AcessaRepository
    {
        //Estanciar class de conexão e comandos 
        Conexao conexao = new Conexao();
        NpgsqlCommand cmd = new NpgsqlCommand();
        public String msgERRO = "...";
        public Pessoas Pessoas { get; set; }
        public ObservableCollection<Pessoas> ListaPessoas { get; private set; }


        public AcessaRepository() { }
        public AcessaRepository(Pessoas pessoas)
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
                            Sexo = (Sexo)Enum.Parse(typeof(Sexo),dataRow["sexo"].ToString()),
                            EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil),dataRow["estadocivil"].ToString()),
                            DataCadastro = DateTime.Parse(dataRow["data_cad"].ToString())
                        })
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
        public void pegaRegistroPorId(int id)
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
        //Inserir registros
        public void InserirRegistro(string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad)
        {
            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();
                    //Passar comandos sql
                    string cmdInserir = String.Format("Insert Into tbl_cadstro(nome,sobrenome,data_nasc,sexo,estadocivil,data_cad) values('{0}','{1}','{2}','{  3}','{4}','{5}')", sobrenome, data_nasc, sexo, estadocivil, data_cad);

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }
        //Atualiza registros
        public void AtualizarRegistro(int id_pessoa, string nome, string sobrenome, string data_nasc, string sexo, string estadocivil, DateTime data_cad)
        {
            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();
                    //Passa comando sql
                    string cmdAtualiza = String.Format("Update tbl_cadastro Set nome = '" + nome + "' , sobrenome = " + sobrenome + " Where id = " + id_pessoa);

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }
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
