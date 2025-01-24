using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TOR_Core.Extensions;
using TOR_Core.Models;

namespace TORHirelingNobleRecruits
{
    public class MaximumIndexHeroCanRecruitFromHeroOverRideModel : VolunteerModel
    {
        VolunteerModel _previousModel;

        public MaximumIndexHeroCanRecruitFromHeroOverRideModel(VolunteerModel previousModel)
        {
            _previousModel = previousModel;
            _previousModel ??= new DefaultVolunteerModel();
        }

        public override int MaxVolunteerTier => _previousModel.MaxVolunteerTier;

        public override bool CanHaveRecruits(Hero hero)
        {
            return _previousModel.CanHaveRecruits(hero);
        }

        public override CharacterObject GetBasicVolunteer(Hero hero)
        {
            return _previousModel.GetBasicVolunteer(hero);
        }

        public override float GetDailyVolunteerProductionProbability(Hero hero, int index, Settlement settlement)
        {
            return _previousModel.GetDailyVolunteerProductionProbability(hero, index, settlement);
        }

        public override int MaximumIndexHeroCanRecruitFromHero(Hero buyerHero, Hero sellerHero, int useValueAsRelation = -101)
        {
            if (this.GetBasicVolunteer(sellerHero).IsUndead())
            {
                if (buyerHero.IsNecromancer())
                {
                    if (buyerHero.PartyBelongedTo.GetMemberHeroes().Any((Hero x) => x.IsNecromancer()))
                    {
                        goto HasNecro;
                    }
                }
                return -1;
            }

            HasNecro:
            int num = Campaign.Current.Models.VolunteerModel.MaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation);
            if (buyerHero == Hero.MainHero && buyerHero.IsEnlisted())
            {
                return -1;
            }
            return num;
        }
    }
}
