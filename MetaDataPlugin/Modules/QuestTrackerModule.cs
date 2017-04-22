using System.Linq;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using HDT.Plugins.Custom.Models;

namespace HDT.Plugins.Custom.Modules
{
    internal class QuestTrackerModule : IModule
    {
       
        public QuestTrackerModule()
        {

        }

        Entity _localPlayerQuestEntity { get; set; }
        Entity _opponentQuestEntity { get; set; }

        public PlayerQuestModel LocalPlayerQuestModel { get; private set; }
        public PlayerQuestModel OpponentQuestModel { get; private set; }
        
        public string[] ModelName => new string[] { nameof(LocalPlayerQuestModel), nameof(LocalPlayerQuestModel) };
        public object[] Model => new PlayerQuestModel[] { LocalPlayerQuestModel, OpponentQuestModel };

        internal void CheckForAndUpdateQuest(Card c = null)
        {
            if (_localPlayerQuestEntity == null)
                _localPlayerQuestEntity = CoreAPI.Game.Player.RevealedEntities.Where(e => e.IsQuest && e.IsInZone(Zone.SECRET)).FirstOrDefault();

            if (_opponentQuestEntity == null)
                _opponentQuestEntity = CoreAPI.Game.Opponent.RevealedEntities.Where(e => e.IsQuest && e.IsInZone(Zone.SECRET)).FirstOrDefault();

            if (_localPlayerQuestEntity != null)
            {
                var e = _localPlayerQuestEntity;
                LocalPlayerQuestModel = new PlayerQuestModel() { QuestName = e.LocalizedName, Progress = e.GetTag(GameTag.QUEST_PROGRESS), Goal = e.GetTag(GameTag.QUEST_PROGRESS_TOTAL) };
               // MainWindowViewModel.LocalPlayerQuestProgress.Set(e.LocalizedName, e.GetTag(GameTag.QUEST_PROGRESS), e.GetTag(GameTag.QUEST_PROGRESS_TOTAL));
            }

            if (_opponentQuestEntity != null)
            {
                var e = _opponentQuestEntity;
                OpponentQuestModel = new PlayerQuestModel() { QuestName = e.LocalizedName, Progress = e.GetTag(GameTag.QUEST_PROGRESS), Goal = e.GetTag(GameTag.QUEST_PROGRESS_TOTAL) };
                // MainWindowViewModel.OpponentQuestProgress.Set(e.LocalizedName, e.GetTag(GameTag.QUEST_PROGRESS), e.GetTag(GameTag.QUEST_PROGRESS_TOTAL));
            }
        }

        public void Update()
        {
            CheckForAndUpdateQuest();
        }
    }
}