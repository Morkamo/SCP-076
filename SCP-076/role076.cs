using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Scp0492;
using PlayerRoles;
using UnityEngine;

using Cassie = Exiled.API.Features.Cassie;
using Player = Exiled.API.Features.Player;

namespace SCP_076
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class role076 : CustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public override int MaxHealth { get; set; } = 700;
        public override string Name { get; set; } = "SCP-076";
        public override string Description { get; set; } = "Its SCP-076";
        public override string CustomInfo { get; set; } = "SCP-076";
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();
        public override Vector3 Scale { get; set; } = new(1f, 1f ,1f);

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            "Item076",
        };

        internal Dictionary<string, cassieMessage> cassieMessage { get; set; } = new() { 
            [DamageType.Explosion.ToString()] = new cassieMessage 
            {
                Message = "SCP 0 7 6 terminated by explosion detonated", 
                Subtitle = "SCP-076 Успешно уничтожен в результате взрыва."
            },
            [DamageType.Explosion.ToString()] = new cassieMessage 
            {
                Message = "SCP 0 7 6 terminated by explosion detonated", 
                Subtitle = "SCP-076 Успешно уничтожен в результате взрыва."
            },
            [DamageType.SeveredHands.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by ScpSubject 3 3 0", 
                Subtitle = "SCP-076 Успешно уничтожен, не брать больше двух..."
            },
            [DamageType.Unknown.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by termination cause Unspecified", 
                Subtitle = "SCP-076 Успешно уничтожен. Причина неизвестна."
            },
            [DamageType.Poison.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by .G6 not safe substantial .G2", 
                Subtitle = "SCP-076 Успешно уничтожен, носитель отравлен ядом."
            },
            [DamageType.Hypothermia.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by ScpSubject 2 4 4", 
                Subtitle = "SCP-076 Успешно уничтожен объектом SCP-244."
            },
            [DamageType.Scp207.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by ScpSubject 2 0 7", 
                Subtitle = "SCP-076 Успешно уничтожен объектом SCP-207."
            },
            [DamageType.Decontamination.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 lost in decontamination Sequence", 
                Subtitle = "SCP-076 Утерян в процессе обеззараживания."
            },
            [DamageType.Warhead.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by Alpha Warhead", 
                Subtitle = "SCP-076 Успешно уничтожен боеголовкой Альфа."
            },
            [DamageType.Scp018.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by ScpSubject 0 1 8", 
                Subtitle = "SCP-076 Выведен из строя объектом SCP-018"
            },
            [DamageType.Falldown.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by ground not detected", 
                Subtitle = "SCP-076 Успешно уничтожен, падение с высокой точки."
            },
            [DamageType.Tesla.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 Successfully terminated by Automatic Security System", 
                Subtitle = "SCP-076 Успешно уничтожен Автоматической Системой Охраны"
            },
            [Team.ClassD.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 ContainedSuccessfully ContainmentUnit ClassD Personnel", 
                Subtitle = "SCP-076 Успешно сдержан Персоналлом Класса-Д."
            },
            [Team.Scientists.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 ContainedSuccessfully ContainmentUnit Science Personnel", 
                Subtitle = "SCP-076 Успешно сдержан Научным персоналом."
            },
            [RoleTypeId.FacilityGuard.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 ContainedSuccessfully ContainmentUnit Facility Personnel", 
                Subtitle = "SCP-076 Успешно сдержан Персоналом Охраны."
            },
            [Team.FoundationForces.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 ContainedSuccessfully ContainmentUnit NineTailedFox", 
                Subtitle = "SCP-076 Успешно сдержан отрядом Девятихвостая лиса."
            },
            [Team.ChaosInsurgency.ToString()] = new cassieMessage()
            {
                Message = "SCP 0 7 6 ContainedSuccessfully ContainmentUnit ChaosInsurgency", 
                Subtitle = "SCP-076 Успешно сдержан Повстанцами Хаоса."
            }
        };
        
        protected override void RoleAdded(Player player)
        {
            player.EnableEffect(EffectType.MovementBoost, Plugin.Instance.Config.MovementBoost);
            
            AddToTurnetLists(player);
            
            base.RoleAdded(player);
        }

        internal void OnPickup(PickingUpItemEventArgs ev)
        {
            if (ev.Player.GetCustomRoles().Any(role => role.Id == Id) && !Plugin.Instance.Config.AllowedItemsList.Contains(ev.Pickup.Type))
                ev.IsAllowed = false;
        }
        
        internal void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (ev.Player.GetCustomRoles().Any(role => role.Id == Id) && ev.Player.IsEffectActive<MovementBoost>())
                ev.IsAllowed = false;
        }

        internal void OnDying(DyingEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            
            if (ev.Player.GetCustomRoles().Any(pl => pl.Id == Id))
            {
                if (cassieMessage.TryGetValue(ev.DamageHandler.Type.ToString(), out cassieMessage damageTypeTranslation))
                {
                    Cassie.MessageTranslated(damageTypeTranslation.Message, damageTypeTranslation.Subtitle);
                    return;
                }

                if (ev.Attacker is not null && cassieMessage.TryGetValue(ev.Attacker.Role.Type.ToString(), out cassieMessage roleTypeTranslation))
                {
                    Cassie.MessageTranslated(roleTypeTranslation.Message, roleTypeTranslation.Subtitle);
                    return;
                }

                if (ev.Attacker is not null && cassieMessage.TryGetValue(ev.Attacker.Role.Team.ToString(), out cassieMessage teamTranslation))
                {
                    Cassie.MessageTranslated(teamTranslation.Message, teamTranslation.Subtitle);
                }
                
                RemoveFromTurnetLists(ev.Player);
            }
        }

        internal void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker is not null && ev.Attacker.IsScp && ev.Player.GetCustomRoles().Any(role => role.Id == Id))
            {
                ev.IsAllowed = false;
                return;
            }

            if (ev.Attacker is not null && ev.Attacker.GetCustomRoles().Any(role => role.Id == Id) && ev.Player.IsScp)
                ev.IsAllowed = false;
        }

        internal void OnAttacking049(AttackingEventArgs ev)
        {
            if (ev.Target.GetCustomRoles().Any(role => role.Id == Id))
                ev.IsAllowed = false;
        }

        internal void OnTriggeringBloodlust(TriggeringBloodlustEventArgs ev)
        {
            if (ev.Target.GetCustomRoles().Any(role => role.Id == Id))
                ev.IsAllowed = false;
        }

        private void RemoveFromTurnetLists(Player player)
        {
            Scp173Role.TurnedPlayers.Remove(player);
            Scp096Role.TurnedPlayers.Remove(player);
            Scp049Role.TurnedPlayers.Remove(player);
            Scp079Role.TurnedPlayers.Remove(player);
        }
        
        private void AddToTurnetLists(Player player)
        {
            Scp173Role.TurnedPlayers.Add(player);
            Scp096Role.TurnedPlayers.Add(player);
            Scp049Role.TurnedPlayers.Add(player);
            Scp079Role.TurnedPlayers.Add(player);
        }
    }
    
    public class cassieMessage
    {
        public string Message { get; set; }
        public string Subtitle { get; set; }
    }
}