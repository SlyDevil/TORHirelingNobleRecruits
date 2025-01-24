using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TOR_Core;
using TOR_Core.Extensions;
using TOR_Core.Models;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TOR_Core.CampaignMechanics.ServeAsAHireling;
using TOR_Core.BattleMechanics.AI.TeamAI.FormationBehavior;

namespace TORHirelingNobleRecruits
{/*
    [HarmonyPatch(typeof(TORVolunteerModel), "MaximumIndexHeroCanRecruitFromHero")]
    internal class MaximumIndexHeroCanRecruitFromHeroPatch
    {
        private static bool Prefix(Hero buyerHero, Hero sellerHero, int useValueAsRelation, TORVolunteerModel __instance, ref int __result)
        {
            useValueAsRelation = -101;
            if (__instance.GetBasicVolunteer(sellerHero).IsUndead())
            {
                if (buyerHero.IsNecromancer())
                {
                    if (buyerHero.PartyBelongedTo.GetMemberHeroes().Any((Hero x) => x.IsNecromancer()))
                    {
                        goto HasNecro;
                    }
                }
                __result = -1;
            }

            HasNecro:
            int num = Campaign.Current.Models.VolunteerModel.MaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation);
            if (buyerHero == Hero.MainHero && buyerHero.IsEnlisted())
            {
                __result = -1;
            }
            __result = num;

            return false;
        }
        /*
        private static void Postfix(TORVolunteerModel __instance, Hero buyerHero, ref int __result)
        {
            
            if (__result == -1 && buyerHero == Hero.MainHero) { 
                InformationManager.DisplayMessage(new InformationMessage(__result.ToString() + " result", Colors.Red));
            InformationManager.DisplayMessage(new InformationMessage(buyerHero.ToString() + " hero", Colors.Magenta));
            InformationManager.DisplayMessage(new InformationMessage(buyerHero.IsEnlisted().ToString() + " enlisted", Colors.Cyan));
                __result = -1;
            }
            else { __result = 6; }
        }

        public MaximumIndexHeroCanRecruitFromHeroPatch()
		{
		}
    }
*/
}
