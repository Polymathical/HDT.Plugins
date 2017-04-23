using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class PlayerQuestViewModel : ViewModelBase
    {
        private string _questName = String.Empty;
        private int _progress = 0;
        private int _goal = 0;

        public PlayerQuestViewModel()
        {
            QuestName = "Quest1";
            Progress = 0;
            Goal = 4;
        }

        public void Set(PlayerQuestModel questModel, bool isLocalPlayer)
        {
            QuestName = questModel.QuestName;
            Progress = questModel.Progress;
            Goal = questModel.Goal;
        }

        public void Set(string questName, int progress, int goal)
        {
            QuestName = questName;
            Progress = progress;
            Goal = goal;
        }

        public string QuestName
        {
            get { return _questName; }
            set { Set(ref _questName, value, nameof(QuestName)); }
        }

        public int Progress
        {
            get { return _progress; }
            set { Set(ref _progress, value, nameof(Progress)); }
        }

        public int Goal
        {
            get { return _goal; }
            set { Set(ref _goal, value, nameof(Goal)); }
        }
    }
}
