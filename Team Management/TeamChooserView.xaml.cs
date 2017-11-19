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
            get { return ViewModel; }
            set { ViewModel = (TeamChooserModel) value; }
        }

        public TeamChooserView()
        {
            InitializeComponent();
            ViewModel = new TeamChooserModel();

            this.WhenActivated(d =>
                // Look in the xaml for this view!!!  This code caused me to lose some hair before I figured out,
                // after some hours with google, that I needed to have a data binding for anything that displays
                // the view model data, EVEN IF THE COLLECTION IS JUST A SERIES OF STRINGS.  Default binding does 
                // not work with ReactiveUI
                this.OneWayBind(this.ViewModel, vm => vm.TeamsCollection, view => view.teamGrid.ItemsSource)
            );
        }

        private static Dictionary<string, string> columnsToDisplay =
            new Dictionary<string, string> {{"yearID", "Year"}, {"lgID", "League"}, {"divID", "Division"}, {"name", "Name"}};

        private bool shouldHideThisColumn(Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            return !columnsToDisplay.ContainsKey(e.Column.HeaderText);
        }

        private string replaceColumnHeader(Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            columnsToDisplay.TryGetValue(e.Column.MappingName, out var replacementText);
            return replacementText;
        }

        private void teamGrid_AutoGeneratingColumn(object sender, Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            e.Cancel = shouldHideThisColumn(e);
            e.Column.HeaderText = replaceColumnHeader(e);
        }
    }
}
