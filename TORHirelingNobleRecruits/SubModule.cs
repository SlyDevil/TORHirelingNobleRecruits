﻿using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Localization;


namespace TORHirelingNobleRecruits
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }
        
        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
        }
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (Game.Current.GameType is Campaign && gameStarterObject is CampaignGameStarter)
            {
                var existingModel = GetGameModel<VolunteerModel>(gameStarterObject);
                //GetGameModel will return the default model if it doesn't find any others?
                gameStarterObject.AddModel(new MaximumIndexHeroCanRecruitFromHeroOverRideModel(existingModel));
            }
        }

        private T? GetGameModel<T>(IGameStarter gameStarterObject) where T : GameModel
        {
            var models = gameStarterObject.Models.ToArray();

            for (int index = models.Length - 1; index >= 0; --index)
            {
                if (models[index] is T gameModel1)
                    return gameModel1;
            }
            return default;
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("hirleing recruits loaded", Colors.Green));
        }
    }
}