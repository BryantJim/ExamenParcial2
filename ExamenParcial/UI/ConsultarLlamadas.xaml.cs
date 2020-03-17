using ExamenParcial.BLL;
using ExamenParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExamenParcial.UI
{
    /// <summary>
    /// Interaction logic for ConsultarLlamadas.xaml
    /// </summary>
    public partial class ConsultarLlamadas : Window
    {
        public ConsultarLlamadas()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Llamadas>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    //Todo
                    case 0:
                        listado = LlamadasBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(CriterioTextBox.Text);
                        listado = LlamadasBLL.GetList(p => p.LlamadaId == id);
                        break;
                }
            }
            else
            {
                listado = LlamadasBLL.GetList(p => true);
            }

            ConsultarDataGrid.ItemsSource = null;
            ConsultarDataGrid.ItemsSource = listado;
        }
    }
}
