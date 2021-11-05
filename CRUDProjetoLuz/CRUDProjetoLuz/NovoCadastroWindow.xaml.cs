using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUDProjetoLuz
{
    /// <summary>
    /// Lógica interna para NovoCadastroWindow.xaml
    /// </summary>
    public partial class NovoCadastroWindow : Window
    {
        public NovoCadastroWindow()
        {
            InitializeComponent();
            SexoComboBox.ItemsSource = Enum.GetValues(typeof(Sexo)).Cast<Sexo>();
            EstadoCivilComboBox.ItemsSource = Enum.GetValues(typeof(EstadoCivil)).Cast<EstadoCivil>();
            DataAccess.DataRepository cad = new(Pessoas); //DataAccess.Cadastrar
            MessageBox.Show("Aqui"); //(cad.msgEX);
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
