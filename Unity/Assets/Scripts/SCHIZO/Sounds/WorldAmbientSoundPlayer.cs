using JetBrains.Annotations;
using SCHIZO.Attributes.Typing;
using SCHIZO.Helpers;
using UnityEngine;

namespace SCHIZO.Sounds
{
    public sealed partial class WorldAmbientSoundPlayer : SoundPlayer
    {
        [Space]

        [SerializeField, ExposedType("Pickupable"), UsedImplicitly]
        private MonoBehaviour pickupable;

        [SerializeField, ExposedType("Constructable"), UsedImplicitly]
        private MonoBehaviour constructable;

        protected override string DefaultBus => null;
        protected override bool Is3D => false;
    }
}
