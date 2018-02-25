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

namespace PlayBall.Team_Management
{
    /// <summary>
    /// Interaction logic for TeamChooserView.xaml
    /// </summary>
    public partial class TeamChooserView : Window
    {
        private TeamChooserModel ViewModel;
        public TeamChooserView()
        {
            InitializeComponent();
            ViewModel = new TeamChooserModel();
            this.DataContext = this.ViewModel;
        }
    }
}
