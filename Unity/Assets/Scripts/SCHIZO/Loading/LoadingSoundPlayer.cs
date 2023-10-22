using SCHIZO.Sounds;

namespace SCHIZO.Loading
{
    public sealed partial class LoadingSoundPlayer : SoundPlayer
    {
        protected override string DefaultBus => buses[PDA_VOICE];
        protected override bool Is3D => false;
    }
}
