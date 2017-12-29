using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ReactiveUI;
using Syncfusion.Data;

namespace PlayBall.Team_Management
{
    public class TeamChooserModel : ReactiveObject
    {
        private ReactiveList<Team> _teams = new ReactiveList<Team>();

        public ReactiveList<Team> TeamsCollection
        {
            get { return _teams; }
            set { this.RaiseAndSetIfChanged(ref _teams, value); }
        }

        public TeamChooserModel()
        {
            using (var db = new LahmanEntities())
            {
                foreach (var team in db.Teams)
                {
                    this._teams.Add(team);
                }
            }
        }
    }
}
