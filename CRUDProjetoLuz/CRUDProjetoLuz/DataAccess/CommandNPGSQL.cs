﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using System.Collections.ObjectModel;

namespace CRUDProjetoLuz.DataAccess
{
    public class CommandNPGSQL : ICommandSQL
    {
        private IConexaoDB conexao;
        private NpgsqlCommand _cmd;

        public CommandNPGSQL()
        {
            conexao = new ConexaoNPGSQL();
            conexao.Open();
            _cmd = new NpgsqlCommand();
        }
        public void SelecionaTodos(ObservableCollection<Pessoas> ListaPessoas)
        {
                _cmd.CommandText = $"Select * from tbl_cadastro order by id_pessoa;";
                _cmd.ExecuteNonQuery();
                NpgsqlDataReader lista = _cmd.ExecuteReader();
                if (lista.HasRows)
                {
                    //Ler a lista com os dados da select e adiciona na lista destino
                    while (lista.Read())
                    {
                        ListaPessoas.Add(new Pessoas()
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
                }
            else
            {
                
            }
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
                NpgsqlDataReader lista = _cmd.ExecuteReader();
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
                //_cmd.Prepare();
                _cmd.ExecuteNonQuery();
                NpgsqlDataReader inserido = _cmd.ExecuteReader();
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
               // _cmd.Prepare();
                _cmd.ExecuteNonQuery();
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
              //  _cmd.Prepare();
                _cmd.ExecuteNonQuery();
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
                _cmd.Connection.Close();
            }
        }
    }
}