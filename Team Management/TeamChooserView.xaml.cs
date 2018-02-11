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
using ReactiveUI;

namespace PlayBall.Team_Management
{
    /// <summary>
    /// Interaction logic for TeamChooserView.xaml
    /// </summary>
    public partial class TeamChooserView : Window, IViewFor<TeamChooserModel>
    {
        public TeamChooserModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TeamChooserModel) value;
        }

        public TeamChooserView() 
        {
            ViewModel = new TeamChooserModel();
        }

        private static Dictionary<string, string> columnsToDisplay =
            new Dictionary<string, string> {{"yearID", "Year"}, {"lgID", "League"}, {"divID", "Division"}, {"name", "Name"}};
    }
}
