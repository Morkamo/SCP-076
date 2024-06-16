using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using UnityEngine;

namespace SCP_076
{
    public class RoundHandlers
    {
        public void OnRoundStarted()
        {
            if (Player.Dictionary.Count >= Plugin.Instance.Config.MinPlayersForSpawn && Round.SurvivingSCPs >= Plugin.Instance.Config.MinScpUnits && 
                        Random.Range(1, 101) <= Plugin.Instance.Config.SpawnChance)
                    CustomRole.Get(typeof(role076))?.AddRole(Player.Get(player => player.IsScp).GetRandomValue());
        }

        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (CustomRole.Get(typeof(role076))?.TrackedPlayers.Count is 1 &&
                Player.Get(player =>
                    player.IsHuman && player.Role.Type is not RoleTypeId.CustomRole &&
                    player.Role.Type is not RoleTypeId.Tutorial).Any())

                ev.IsAllowed = false;

            else if (CustomRole.Get(typeof(role076))?.TrackedPlayers.Count == Player.List.Count(player => player.IsAlive))
                ev.LeadingTeam = LeadingTeam.Anomalies;
        }

        public void OnNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            if (CustomRole.Get(typeof(role076))?.TrackedPlayers.Count is 1)
                ev.ScpsLeft = ev.ScpsLeft + 1;
        }
    }
}