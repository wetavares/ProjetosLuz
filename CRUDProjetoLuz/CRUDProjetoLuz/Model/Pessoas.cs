using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProjetoLuz
{
    public class Pessoas : BaseNotifyPropertyChanged, ICloneable
    {
        //Variáveis 
        private int _id;
        private string _nome;
        private string _sobrenome;
        public DateTime _dataNascimento;
        public Sexo _sexo;
        public EstadoCivil _estadoCivil;
        public DateTime _dataCadastro;
        public int Id 
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }
        public string Nome 
        {
            get { return _nome; }
            set { SetField(ref _nome, value); }
        }
        public string Sobrenome 
        {
            get { return _sobrenome; }
            set { SetField(ref _sobrenome, value); }
        }
        public DateTime DataNascimento 
        {
            get { return _dataNascimento; }
            set { SetField(ref _dataNascimento, value); }
        }
        public Sexo Sexo
        {
            get { return _sexo; }
            set { SetField(ref _sexo, value); }
        }
        public EstadoCivil EstadoCivil
        {
            get { return _estadoCivil; }
            set { SetField(ref _estadoCivil, value); }
        }
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
