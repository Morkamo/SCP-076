using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using InventorySystem.Items.Jailbird;
using MEC;
using UnityEngine;

using evItem = Exiled.Events.Handlers.Item;
using evArgs = Exiled.Events.Handlers.Player;

namespace SCP_076
{
    [CustomItem(ItemType.Jailbird)]
    public class item076 : CustomItem
    {
        public override uint Id { get; set; } = 1;
        public override float Weight { get; set; } = 1f;
        public override string Name { get; set; } = "Item076";
        public override string Description { get; set; } = "Item for SCP-076";
        public override SpawnProperties SpawnProperties { get; set; } = new();
        public override ItemType Type { get; set; } = ItemType.Jailbird;
        public override Vector3 Scale { get; set; } = Vector3.one;
        
        protected override void SubscribeEvents()
        {
            evArgs.Dying.Subscribe(OnDyingEventArgs);
            evArgs.DroppingItem.Subscribe(OnDropping);
            evItem.ChargingJailbird.Subscribe(OnChargingJailbird);
            evItem.Swinging.Subscribe(OnSwinging);
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            evArgs.Dying.Unsubscribe(OnDyingEventArgs);
            evArgs.DroppingItem.Unsubscribe(OnDropping);
            evItem.ChargingJailbird.Unsubscribe(OnChargingJailbird);
            evItem.Swinging.Unsubscribe(OnSwinging);
            base.UnsubscribeEvents();
        }
        
        protected override void ShowSelectedMessage(Player player) { }

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            base.OnAcquired(player, item, true);
        }

        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            if (Check(ev.Item))
                ev.IsAllowed = Plugin.Instance.Config.CanDrop;
            
            base.OnDropping(ev);
        }

        protected override void OnUpgrading(UpgradingItemEventArgs ev)
        {
            if (Check(ev.Item))
                ev.IsAllowed = Plugin.Instance.Config.CanUpgradeIn914;
            
            base.OnUpgrading(ev);
        }

        internal void OnSwinging(SwingingEventArgs ev)
        {
            if (Check(ev.Item) && ev.Item.Is(out Jailbird jailbird))
            {
                jailbird.MeleeDamage = Plugin.Instance.Config.MeleeDamage;
                jailbird.TotalDamageDealt = 0;
            }
        }
        
        internal void OnChargingJailbird(ChargingJailbirdEventArgs ev)
        {
            if (Check(ev.Item) && ev.Item.Is(out Jailbird jailbird))
            {
                Timing.CallDelayed(6f, () =>
                {
                    jailbird.TotalCharges = 1;
                    jailbird.ChargeDamage = Plugin.Instance.Config.ChargeDamage;
                    jailbird.WearState = JailbirdWearState.Healthy;
                });
            }
        }

        internal void OnDyingEventArgs(DyingEventArgs ev)
        {
            if (ev.Attacker.GetCustomRoles().Any(role => role.Id == Id))
            {
                ev.Player.Kill(Plugin.Instance.Config.DeathReason);
                return;
            }
            
            var jailbirdItem = ev.Player.Items.FirstOrDefault(item =>item.Type == ItemType.Jailbird);

            if (jailbirdItem != null && Check(jailbirdItem))
                ev.Player.Inventory.ServerRemoveItem(jailbirdItem.Serial, jailbirdItem.Base.PickupDropModel);
        }
    }
}