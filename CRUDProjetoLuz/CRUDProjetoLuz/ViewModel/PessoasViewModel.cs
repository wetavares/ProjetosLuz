
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
               // EditarCommand.CanExecuteChanged(_pessoasSelecionado);
            }
        }
       
        public PessoasViewModel()
        {
            DeletarCommand = new RelayCommand((object parameter) => { Deletar(); });
            NovoCommand = new RelayCommand((object parameter) => { Novo(); });

            ListaPessoas = new ObservableCollection<Pessoas>();
            ListaPessoas.Add(new Pessoas()
            {
                Id = 1,
                Nome = "Wemerson",
                Sobrenome = "Tavares",
                DataNascimento = new DateTime(1973, 11, 23),
                Sexo = Sexo.Masculino,
                EstadoCivil = EstadoCivil.Casado,
                DataCadastro = new DateTime(2021, 10, 21)       
            });
            PessoasSelecionado = ListaPessoas.FirstOrDefault();
        }

        private void Deletar()
        {
            ListaPessoas.Remove(PessoasSelecionado);
            PessoasSelecionado = ListaPessoas.FirstOrDefault();

        }

        //Implementando os comandos - Delete / Novo / Editar - mudar para RelayCammand


        //Implementando comando Novo

        private void Novo()
        {
            Pessoas novapessoa = new Pessoas();
            int maxId = 0;
            if (ListaPessoas.Any())
            {
                maxId = ListaPessoas.Max(f => f.Id);
            }
            novapessoa.Id = maxId + 1;

            NovoCadastroWindow fw = new NovoCadastroWindow();
            fw.DataContext = novapessoa;
            fw.ShowDialog();

            if (fw.DialogResult.HasValue && fw.DialogResult.Value)
            {
                ListaPessoas.Add(novapessoa);
                PessoasSelecionado = novapessoa;

            }

        }

        //Implementando comando Editar
        public void Editar(object parameter) 
        {
            PessoasViewModel viewModel = (PessoasViewModel)parameter;
            Pessoas cloneFuncionario = (Pessoas)viewModel.PessoasSelecionado.Clone();
            NovoCadastroWindow fw = new NovoCadastroWindow();
            fw.DataContext = cloneFuncionario;
            fw.ShowDialog();

            if (fw.DialogResult.HasValue && fw.DialogResult.Value)
            {
                viewModel.PessoasSelecionado.Nome = cloneFuncionario.Nome;
                viewModel.PessoasSelecionado.Sobrenome = cloneFuncionario.Sobrenome;
                viewModel.PessoasSelecionado.DataNascimento = cloneFuncionario.DataNascimento;
                viewModel.PessoasSelecionado.Sexo = cloneFuncionario.Sexo;
                viewModel.PessoasSelecionado.EstadoCivil = cloneFuncionario.EstadoCivil;
                viewModel.PessoasSelecionado.DataCadastro = cloneFuncionario.DataCadastro;
            }
        }
        
        /*
        public EditarCommand Editar { get; private set; } = new EditarCommand();

        public class EditarCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as PessoasViewModel;
                return viewModel != null && viewModel.PessoasSelecionado != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (PessoasViewModel)parameter;
                var cloneFuncionario = (Pessoas)viewModel.PessoasSelecionado.Clone();
                var fw = new NovoCadastroWindow();
                fw.DataContext = cloneFuncionario;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.PessoasSelecionado.Nome = cloneFuncionario.Nome;
                    viewModel.PessoasSelecionado.Sobrenome = cloneFuncionario.Sobrenome;
                    viewModel.PessoasSelecionado.DataNascimento = cloneFuncionario.DataNascimento;
                    viewModel.PessoasSelecionado.Sexo = cloneFuncionario.Sexo;
                    viewModel.PessoasSelecionado.EstadoCivil = cloneFuncionario.EstadoCivil;
                    viewModel.PessoasSelecionado.DataCadastro = cloneFuncionario.DataCadastro;
                }
            }
        }
        */
    }
}
