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
using ExamenParcial.Entidades;
using ExamenParcial.BLL;

namespace ExamenParcial.UI
{
    /// <summary>
    /// Interaction logic for RLlamadas.xaml
    /// </summary>
    public partial class RLlamadas : Window
    {
        Llamadas llamada = new Llamadas();
        public RLlamadas()
        {
            InitializeComponent();
            LlamadaIdTextBox.Text = "0";
            this.DataContext = llamada;
        }

        private void Limpiar()
        {
            /*LlamadaIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;*/
            llamada = new Llamadas();
            DetalleDataGrid.ItemsSource = new List<LlamadaDetalle>();
            Actualizar();
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = llamada;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            llamada.LlamadasDetalle.Add(new LlamadaDetalle
            (
                llamada.LlamadaId,
                ProblemaTextBox.Text,
                SolucionTextBox.Text
            )
            );

            Actualizar();
            ProblemaTextBox.Clear();
            SolucionTextBox.Clear();
            ProblemaTextBox.Focus();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDataGrid.Items.Count >1 && DetalleDataGrid.SelectedIndex < DetalleDataGrid.Items.Count - 1)
            {
                llamada.LlamadasDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Actualizar();
            }
        }
        
        private bool ExisteEnLaBasedeDatos()
        {
            Llamadas llamadas = LlamadasBLL.Buscar(llamada.LlamadaId);
            return (llamadas != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Convert.ToInt32(LlamadaIdTextBox.Text) == 0)
                paso = LlamadasBLL.Guardar(llamada);
            else
            {
                if (!ExisteEnLaBasedeDatos())
                {
                    MessageBox.Show("No se puede modificar una llamada que no existe","Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = LlamadasBLL.Modificar(llamada);
                }
            }

            if (paso)
                Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Llamadas llamadaBuscada = LlamadasBLL.Buscar(Convert.ToInt32(LlamadaIdTextBox.Text));
            if (llamadaBuscada != null)
            {
                llamada = llamadaBuscada;
                Actualizar();
            }
            else
            {
                MessageBox.Show("Llamada no encontrada");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (LlamadasBLL.Eliminar(Convert.ToInt32(LlamadaIdTextBox.Text))){
                MessageBox.Show("Llamada eliminada");
                Limpiar();
            }else
                MessageBox.Show("No puede ser eliminada");
        }
    }
}
