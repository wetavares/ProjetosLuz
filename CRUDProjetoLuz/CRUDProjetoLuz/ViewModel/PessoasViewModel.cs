
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Pessoas _pessoasSelecionado;

        public Pessoas PessoasSelecionado
        {
            get { return _pessoasSelecionado; }
            set
            { 
                SetField(ref _pessoasSelecionado, value);
            }
        } 
        public PessoasViewModel()
        {
            Pessoas novapessoa = new Pessoas();
            DeletarCommand = new RelayCommand((object parameter) => { Deletar(); });
            NovoCommand = new RelayCommand((object parameter) => { Novo(novapessoa); });
            EditarCommand = new RelayCommand((object parameter) => { Editar(); });

            ListaPessoas = new ObservableCollection<Pessoas>();
            DataRepository dadosBD = new DataRepository(novapessoa);

            dadosBD.PegaTodosRegistros(ListaPessoas);
            PessoasSelecionado = ListaPessoas.FirstOrDefault();
        }
        //Comandos - Delete / Novo / Editar - utilizando o RelayCammand
        //Implementando o comando Deletar
        private void Deletar()
        {
            ListaPessoas.Remove(PessoasSelecionado);
            PessoasSelecionado = ListaPessoas.FirstOrDefault();
        }
        //Implementando comando Novo
        private void Novo(Pessoas novapessoa)
        {
            
            int maxId = 0;
            if (ListaPessoas.Any())
            {
                maxId = ListaPessoas.Max(f => f.Id);
            }
            novapessoa.Id = maxId + 1;
            NovoCadastroWindow novoCadastro = new NovoCadastroWindow();
            novoCadastro.DataContext = novapessoa;
            novoCadastro.ShowDialog();
            DataRepository cadastrado = new DataRepository(novapessoa);
            

            if (novoCadastro.DialogResult.HasValue && novoCadastro.DialogResult.Value)
            {
                ListaPessoas.Add(novapessoa);
                cadastrado.InserirRegistro(novapessoa);
                PessoasSelecionado = novapessoa;
            }
        }
        //Implementando comando Editar
        public void Editar()
        {
            NovoCadastroWindow novoCadastro = new NovoCadastroWindow();
            novoCadastro.DataContext = PessoasSelecionado;//cloneFuncionario;
            novoCadastro.ShowDialog();
        }            
    }
}
