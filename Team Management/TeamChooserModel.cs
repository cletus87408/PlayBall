using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Database;
using PlayBall.Annotations;

namespace PlayBall.Team_Management
{
    public class TeamChooserModel  : INotifyPropertyChanged
    {
        public ObservableCollection<Team> teams;

        public ObservableCollection<Team> Teams
        {
            get { return teams; }
        }
        public TeamChooserModel()
        {
            teams = new ObservableCollection<Team>();

            using (var db = new LahmanEntities())
            {
                foreach (var team in db.Teams)
                {
                    this.teams.Add(team);
                }
            }

            this.OnPropertyChanged("Teams");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
