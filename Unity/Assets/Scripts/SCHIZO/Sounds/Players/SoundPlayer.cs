using JetBrains.Annotations;
using NaughtyAttributes;
using SCHIZO.Helpers;
using SCHIZO.Interop.Subnautica;
using SCHIZO.Sounds.Collections;
using UnityEngine;

namespace SCHIZO.Sounds.Players
{
    public abstract partial class SoundPlayer : MonoBehaviour
    {
        [SerializeField, Required, UsedImplicitly]
        protected SoundCollection soundCollection;

        [SerializeField, Dropdown(nameof(buses)), ShowIf(nameof(NeedsBus)), UsedImplicitly]
        protected string bus;

        [SerializeField, Required, ShowIf(nameof(NeedsEmitter)), UsedImplicitly]
        private _FMOD_CustomEmitter emitter;

        protected abstract string DefaultBus { get; }
        protected abstract bool Is3D { get; }

        private bool NeedsBus => string.IsNullOrEmpty(DefaultBus);
        private bool NeedsEmitter => Is3D;

        [StaticHelpers.Cache]
        private protected static readonly BetterDropdownList<string> buses = new BetterDropdownList<string>()
        {
            {PDA_VOICE, "Nautilus.Utility.AudioUtils+BusPaths:PDAVoice"},
            {UNDERWATER_CREATURES, "Nautilus.Utility.AudioUtils+BusPaths:UnderwaterCreatures"},
            {INDOOR_SOUNDS, $"SCHIZO.Sounds.Players.{nameof(SoundPlayer)}:{nameof(INDOOR_SOUNDS_BUS)}"}
        };

        protected const string PDA_VOICE = "PDA Voice";
        protected const string UNDERWATER_CREATURES = "Underwater Creatures";
        protected const string INDOOR_SOUNDS = "Indoor Sounds";

        private const string INDOOR_SOUNDS_BUS = "bus:/master/SFX_for_pause/PDA_pause/all/indoorsounds";
    }
}