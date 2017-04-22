using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.Models
{
    public class PlayerQuestModel
    {
        public string QuestName { get; set; }
        public int Progress { get; set; }
        public int Goal { get; set; }

        public PlayerQuestModel(string questName, int progress, int goal)
        {
            QuestName = questName;
            Progress = progress;
            Goal = goal;
        }
    }
}