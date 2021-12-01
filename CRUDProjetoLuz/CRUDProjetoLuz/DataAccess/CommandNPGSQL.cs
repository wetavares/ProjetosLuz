using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using System.Collections.ObjectModel;
using System.Windows;

namespace CRUDProjetoLuz.DataAccess
{
    public class CommandNPGSQL : ICommandSQL
    {
        private ConexaoNPGSQL conexao;
        private NpgsqlCommand _cmd;

    public CommandNPGSQL()
        {
            conexao = new ConexaoNPGSQL();
            _cmd = new NpgsqlCommand();
            _cmd.Connection = conexao.Open();
        }

    public List<Pessoas> SelecionarTodos()
        {
            List<Pessoas> pessoas = new List<Pessoas>();
            _cmd.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
            _cmd.ExecuteNonQuery();
            NpgsqlDataReader lista = _cmd.ExecuteReader();
                if (lista.HasRows)
                {
                    //Ler a lista com os dados da select e adiciona na lista destino
                    while (lista.Read())
                    {
                        pessoas.Add(new Pessoas()
                        {
                            Id = Convert.ToInt32(lista["id_pessoa"]),
                            Nome = lista["nome"].ToString(),
                            Sobrenome = lista["sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(lista["datanascimento"]),
                            Sexo = Enum.Parse<Sexo>(lista[name: "sexo"].ToString()),
                            EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), lista[name: "estadocivil"].ToString()),
                            DataCadastro = Convert.ToDateTime(lista["datacadastro"])
                        });
                    }
                    lista.Close();
                    conexao.Close();
                }
            return pessoas;
        }
        //Inserir registros
        public int InserirRegistro(Pessoas pessoas)
        {
            int idInserido = 0;
            _cmd.Connection.Open();
            _cmd.CommandText = "Insert Into tbl_cadastro(nome,sobrenome,datanascimento,sexo,estadocivil,datacadastro)" +
                " values(@nome,@sobrenome,@datanascimento,@sexo,@estadocivil,@datacadastro) RETURNING id_pessoa;";
            _cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
            _cmd.Parameters.AddWithValue("@sobrenome", pessoas.Sobrenome);
            _cmd.Parameters.AddWithValue("@datanascimento", pessoas.DataNascimento);
            _cmd.Parameters.AddWithValue("@sexo", pessoas.Sexo.ToString());
            _cmd.Parameters.AddWithValue("@estadocivil", pessoas.EstadoCivil.ToString());
            _cmd.Parameters.AddWithValue("@datacadastro", pessoas.DataCadastro);
            //_cmd.Prepare();
            _cmd.ExecuteNonQuery();

            NpgsqlDataReader inserido = _cmd.ExecuteReader();
            idInserido = Convert.ToInt32(inserido["id_pessoa"]);

            inserido.Close();
            conexao.Close();

            return idInserido;
        }
        //Atualiza registros
        public void AtualizarRegistro(Pessoas pessoas)  //int id_pessoa, string nome, string sobrenome, string DataNascimento, string sexo, string estadocivil, DateTime DataCadastro
        {
            //Passa comando sql
            _cmd.Connection.Open();
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
            // _cmd.Prepare();
            _cmd.ExecuteNonQuery();
            conexao.Close();
        }
        //Deleta registros
        public void DeletarRegistro(int Id)
        {
            try
            {
                //Passa instrução sql
                _cmd.Connection.Open();
                _cmd.CommandText = "Delete From tbl_cadastro Where id_pessoa = @Id;";
                _cmd.Parameters.AddWithValue("@Id", Id);
                _cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

        }
    }
}
