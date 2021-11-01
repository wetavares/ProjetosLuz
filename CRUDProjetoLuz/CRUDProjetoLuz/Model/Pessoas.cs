using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProjetoLuz
{
    public class Pessoas : BaseNotifyPropertyChanged, ICloneable
    {
        private int _id;
        public int Id 
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }
        private string _nome;
        public string Nome 
        {
            get { return _nome; }
            set { SetField(ref _nome, value); }
        }
        private string _sobrenome;
        public string Sobrenome 
        {
            get { return _sobrenome; }
            set { SetField(ref _sobrenome, value); }
        }
        public DateTime _dataNascimento;
        public DateTime DataNascimento 
        {
            get { return _dataNascimento; }
            set { SetField(ref _dataNascimento, value); }
        }
        public Sexo _sexo;
        public Sexo Sexo
        {
            get { return _sexo; }
            set { SetField(ref _sexo, value); }
        }
        public EstadoCivil _estadoCivil;
        public EstadoCivil EstadoCivil
        {
            get { return _estadoCivil; }
            set { SetField(ref _estadoCivil, value); }
        }
        public DateTime _dataCadastro;
        public DateTime DataCadastro 
        {
            get { return _dataCadastro; }
            set { SetField(ref _dataCadastro, value); }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
