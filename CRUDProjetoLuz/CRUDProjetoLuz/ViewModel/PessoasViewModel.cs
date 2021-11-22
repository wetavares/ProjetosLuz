
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CRUDProjetoLuz.DataAccess;

namespace CRUDProjetoLuz.ViewModel
{

    public class PessoasViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<Pessoas> ListaPessoas { get; private set; }
        public ICommand DeletarCommand { get; private set; }
        public ICommand NovoCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }

        public Pessoas PessoasSelecionado { get; set; }
        //private DataRepository dadosBD;
        private ConexaoSQL dadosBD;

        private Pessoas novapessoa;
        public PessoasViewModel()
        {
            novapessoa = new Pessoas();
            ListaPessoas = new ObservableCollection<Pessoas>();
            dadosBD = new DataRepository();

            DeletarCommand = new RelayCommand((object parameter) => { Deletar(); });
            NovoCommand = new RelayCommand((object parameter) => { Novo(); });
            EditarCommand = new RelayCommand((object parameter) => { Editar(); });

            dadosBD.PegaTodosRegistros(ListaPessoas);
            //PessoasSelecionado = ListaPessoas.FirstOrDefault();
           
        }
        //Comandos - Delete / Novo / Editar - utilizando o RelayCammand
        //Implementando o comando Deletar
        private void Deletar()
        {
            //novapessoa = PessoasSelecionado;
            string nome;
            int id;
            id = Convert.ToInt32(PessoasSelecionado.Id.ToString());
            nome = PessoasSelecionado.Nome.ToString();
            try
            {
                if (MessageBox.Show("Deseja realmente DELETAR " + nome + " do cadastro?", "Deletar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    dadosBD.DeletarRegistro(id);
                    ListaPessoas.Remove(PessoasSelecionado);
                    MessageBox.Show("Deletado com sucesso!");
                };
                PessoasSelecionado = ListaPessoas.FirstOrDefault();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: ", ex.Message);
            }
        }
        //Implementando comando Novo
        private void Novo()
        {
            
            int maxId = 0;
           /*if (ListaPessoas.Any())
            {
                maxId = ListaPessoas.Max(f => f.Id);
            }*/
            novapessoa.Id = maxId;
            NovoCadastroWindow novoCadastro = new NovoCadastroWindow();
            novoCadastro.DataContext = novapessoa;
            novoCadastro.ShowDialog();
            if (novoCadastro.DialogResult.HasValue && novoCadastro.DialogResult.Value)
            {
                try
                {
                    maxId = dadosBD.InserirRegistro(novapessoa);
                    ListaPessoas.Add(new Pessoas()
                    {
                        Id = maxId,
                        Nome = novapessoa.Nome,
                        Sobrenome = novapessoa.Sobrenome,
                        DataNascimento = novapessoa.DataNascimento,
                        Sexo = novapessoa.Sexo,
                        EstadoCivil = novapessoa.EstadoCivil,
                        DataCadastro = novapessoa.DataCadastro
                    });
                    //PessoasSelecionado = novapessoa;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro: ", ex.Message, MessageBoxButton.OK);
                }
            }
        }
        //Implementando comando Editar
        public void Editar()
        {
            NovoCadastroWindow novoCadastro = new NovoCadastroWindow();
            novoCadastro.DataContext = PessoasSelecionado;
            try
            {
                novoCadastro.ShowDialog();
                dadosBD.AtualizarRegistro(PessoasSelecionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: ",ex.Message);
            }

        }
    }
}
