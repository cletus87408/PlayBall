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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StatsLibrary;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LahmanEntities())
            {
                // Find player ID by name in the master database
                var id = (from player in db.Masters
                          where player.nameLast == this.lastNameTextBox.Text
                          where (player.nameGiven == this.firstNameTextBox.Text || player.nameFirst == this.firstNameTextBox.Text)
                          orderby player.nameLast
                          select player.playerID);

                if (id.Any())
                {
                    int year = Convert.ToInt16(this.yearTextBox.Text);
                    ;
                    var playerId = id.First();

                    //Player was found.  See if he has any stats for the given year
                    var stats = from battingStats in db.Battings where battingStats.playerID == playerId where battingStats.yearID == year select battingStats;

                    if (stats.Any())
                    {
                        // At least one entry found
                        int atBats = 0, hits = 0;

                        // Accumulate every entry in case the individual played for more than a single team
                        // during the year in question
                        foreach (var entry in stats)
                        {
                            if (entry.AB != null && entry.H != null)
                            {
                                atBats += entry.AB.Value;
                                hits += entry.H.Value;
                            }
                        }

                        this.abLabel.Content = atBats;
                        this.hitsLabel.Content = hits;
                        this.baLabel.Content = $"{BasicStats.BattingAverage(atBats, hits):#.###}";
                    }
                }
            }
        }
    }
}
