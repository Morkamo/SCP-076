using System;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomRoles.API;

using evArgs = Exiled.Events.Handlers.Player;
using evArgs049 = Exiled.Events.Handlers.Scp049;
using evArgs0492 = Exiled.Events.Handlers.Scp0492;
using Map = Exiled.Events.Handlers.Map;
using Server = Exiled.Events.Handlers.Server;

namespace SCP_076
{
    internal class Plugin : Plugin<Config>
    {
        internal static Plugin Instance;
        
        public override string Author => "Morkamo";
        public override string Name => "SCP-076";
        public override string Prefix => Name;
        public override Version Version => new Version(1, 0, 0);

        internal role076 _Role076;
        internal item076 _Item076;
        public RoundHandlers _RoundHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            _Role076 = new role076();
            _Item076 = new item076();
            _RoundHandlers = new RoundHandlers();
            
            Config.Role076.Register();
            Config.Item076.Register();
            
            RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            
            Config.Role076.Unregister();
            Config.Item076.Unregister();

            _RoundHandlers = null;
            _Role076 = null;
            _Item076 = null;
            Instance = null;
            
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            Server.EndingRound.Subscribe(_RoundHandlers.OnEndingRound);
            Server.RoundStarted.Subscribe(_RoundHandlers.OnRoundStarted);
            
            Map.AnnouncingNtfEntrance.Subscribe(_RoundHandlers.OnNtfEntrance);
            
            evArgs049.Attacking.Subscribe(_Role076.OnAttacking049);
            
            evArgs0492.TriggeringBloodlust.Subscribe(_Role076.OnTriggeringBloodlust);
            
            evArgs.Hurting.Subscribe(_Role076.OnHurting);
            evArgs.Dying.Subscribe(_Role076.OnDying);
            evArgs.PickingUpItem.Subscribe(_Role076.OnPickup);
            evArgs.ReceivingEffect.Subscribe(_Role076.OnReceivingEffect);
        }

        public void UnRegisterEvents()
        {
            Server.EndingRound.Unsubscribe(_RoundHandlers.OnEndingRound);
            Server.RoundStarted.Unsubscribe(_RoundHandlers.OnRoundStarted);
            
            Map.AnnouncingNtfEntrance.Unsubscribe(_RoundHandlers.OnNtfEntrance);
            
            evArgs049.Attacking.Unsubscribe(_Role076.OnAttacking049);
            
            evArgs0492.TriggeringBloodlust.Unsubscribe(_Role076.OnTriggeringBloodlust);
            
            evArgs.Hurting.Unsubscribe(_Role076.OnHurting);
            evArgs.Dying.Unsubscribe(_Role076.OnDying);
            evArgs.PickingUpItem.Unsubscribe(_Role076.OnPickup);
            evArgs.ReceivingEffect.Unsubscribe(_Role076.OnReceivingEffect);
        }
    }
}