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
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;

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
                if (!buyerHero.IsNecromancer() || !buyerHero.PartyBelongedTo.GetMemberHeroes().Any((Hero x) => x.IsNecromancer()))
                {
                    return -1;
                }
            }            
                
            if (buyerHero.IsEnlisted() && buyerHero == Hero.MainHero)
            {
                return -1;
            }

            int num = baseMaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation);
            return num;
        }

        //this is dumb but idk how to get the Default model
        //i think that TORVolunteer overriding the default model removes the definition for the default one? or at least in a way that blocks me from accessing it
        public int baseMaximumIndexHeroCanRecruitFromHero(Hero buyerHero, Hero sellerHero, int useValueAsRelation = -101)
		{
			Settlement currentSettlement = sellerHero.CurrentSettlement;
			int num = 1;
			int num2 = ((buyerHero == Hero.MainHero) ? Campaign.Current.Models.DifficultyModel.GetPlayerRecruitSlotBonus() : 0);
			int num3 = ((buyerHero != Hero.MainHero) ? 1 : 0);
			int num4 = ((currentSettlement != null && buyerHero.MapFaction == currentSettlement.MapFaction) ? 1 : 0);
			int num5 = ((currentSettlement != null && buyerHero.MapFaction.IsAtWarWith(currentSettlement.MapFaction)) ? (-(1 + num3)) : 0);
			if (buyerHero.IsMinorFactionHero && currentSettlement != null && currentSettlement.IsVillage)
			{
				num5 = 0;
			}
			int num6 = ((useValueAsRelation < -100) ? buyerHero.GetRelation(sellerHero) : useValueAsRelation);
			int num7 = ((num6 >= 100) ? 7 : ((num6 >= 80) ? 6 : ((num6 >= 60) ? 5 : ((num6 >= 40) ? 4 : ((num6 >= 20) ? 3 : ((num6 >= 10) ? 2 : ((num6 >= 5) ? 1 : ((num6 >= 0) ? 0 : (-1)))))))));
			int num8 = 0;
			if (sellerHero.IsGangLeader && currentSettlement != null && currentSettlement.OwnerClan == buyerHero.Clan)
			{
				if (currentSettlement.IsTown)
				{
					Hero governor = currentSettlement.Town.Governor;
					if (governor != null && governor.GetPerkValue(DefaultPerks.Roguery.OneOfTheFamily))
					{
						goto IL_014E;
					}
				}
				if (!currentSettlement.IsVillage)
				{
					goto IL_015E;
				}
				Hero governor2 = currentSettlement.Village.Bound.Town.Governor;
				if (governor2 == null || !governor2.GetPerkValue(DefaultPerks.Roguery.OneOfTheFamily))
				{
					goto IL_015E;
				}
				IL_014E:
				num8 += (int)DefaultPerks.Roguery.OneOfTheFamily.SecondaryBonus;
			}
			IL_015E:
			if (sellerHero.IsMerchant && buyerHero.GetPerkValue(DefaultPerks.Trade.ArtisanCommunity))
			{
				num8 += (int)DefaultPerks.Trade.ArtisanCommunity.SecondaryBonus;
			}
			if (sellerHero.Culture == buyerHero.Culture && buyerHero.GetPerkValue(DefaultPerks.Leadership.CombatTips))
			{
				num8 += (int)DefaultPerks.Leadership.CombatTips.SecondaryBonus;
			}
			if (sellerHero.IsRuralNotable && buyerHero.GetPerkValue(DefaultPerks.Charm.Firebrand))
			{
				num8 += (int)DefaultPerks.Charm.Firebrand.SecondaryBonus;
			}
			if (sellerHero.IsUrbanNotable && buyerHero.GetPerkValue(DefaultPerks.Charm.FlexibleEthics))
			{
				num8 += (int)DefaultPerks.Charm.FlexibleEthics.SecondaryBonus;
			}
			if (sellerHero.IsArtisan && buyerHero.PartyBelongedTo != null && buyerHero.PartyBelongedTo.EffectiveEngineer != null && buyerHero.PartyBelongedTo.EffectiveEngineer.GetPerkValue(DefaultPerks.Engineering.EngineeringGuilds))
			{
				num8 += (int)DefaultPerks.Engineering.EngineeringGuilds.PrimaryBonus;
			}
			return MathF.Min(6, MathF.Max(0, num + num3 + num7 + num2 + num4 + num5 + num8));
		}
    }
}
