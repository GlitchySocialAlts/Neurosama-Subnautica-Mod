

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was automatically generated. PLEASE DO NOT MODIFY THIS FILE MANUALLY!
// </auto-generated>
//------------------------------------------------------------------------------

// Resharper disable all

namespace SCHIZO.Resources;

public static class Assets
{
    private const int _rnd = -1309367934;

    private static readonly UnityEngine.AssetBundle _a = ResourceManager.GetAssetBundle("assets");

    public static T[] All<T>() where T : UnityEngine.Object => _a.LoadAllAssets<T>();
    public static UnityEngine.Object[] All() => _a.LoadAllAssets();
        
    // [System.Obsolete] public static SCHIZO.Creatures._old.CustomCreatureData Old_Anneel_AnneelData = _a.LoadAsset<SCHIZO.Creatures._old.CustomCreatureData>("Assets/_old/Anneel/Anneel data.asset");
    public static SCHIZO.Sounds.SoundCollection Old_Credits_SNEasterEgg = _a.LoadAsset<SCHIZO.Sounds.SoundCollection>("Assets/_old/Credits/SN Easter Egg.asset");
    public static SCHIZO.Items.Data.ItemData Old_Erm_BuildableErmData = _a.LoadAsset<SCHIZO.Items.Data.ItemData>("Assets/_old/Erm/Buildable erm data.asset");
    // [System.Obsolete] public static SCHIZO.Creatures._old.PickupableCreatureData Old_Erm_ErmfishData = _a.LoadAsset<SCHIZO.Creatures._old.PickupableCreatureData>("Assets/_old/Erm/Ermfish data.asset");
    public static SCHIZO.Sounds.SoundCollection Old_Erm_Sounds_PlayerDeath_ErmfishPlayerDeath = _a.LoadAsset<SCHIZO.Sounds.SoundCollection>("Assets/_old/Erm/Sounds/Player Death/Ermfish Player Death.asset");
    public static SCHIZO.Items.Data.ItemData Old_Tutel_BuildableTutelData = _a.LoadAsset<SCHIZO.Items.Data.ItemData>("Assets/_old/Tutel/Buildable tutel data.asset");
    public static SCHIZO.Sounds.SoundCollection Old_Tutel_Sounds_Ambient_TutelAmbient = _a.LoadAsset<SCHIZO.Sounds.SoundCollection>("Assets/_old/Tutel/Sounds/Ambient/Tutel Ambient.asset");
    public static SCHIZO.Sounds.CombinedSoundCollection Old_Tutel_Sounds_GetCarried_CarryByErmshark = _a.LoadAsset<SCHIZO.Sounds.CombinedSoundCollection>("Assets/_old/Tutel/Sounds/Get Carried/Carry by ermshark.asset");
    public static SCHIZO.Sounds.CombinedSoundCollection Old_Tutel_Sounds_GetCarried_PickupByErmshark = _a.LoadAsset<SCHIZO.Sounds.CombinedSoundCollection>("Assets/_old/Tutel/Sounds/Get Carried/Pickup by ermshark.asset");
    // [System.Obsolete] public static SCHIZO.Creatures._old.PickupableCreatureData Old_Tutel_TutelData = _a.LoadAsset<SCHIZO.Creatures._old.PickupableCreatureData>("Assets/_old/Tutel/Tutel data.asset");
    public static SCHIZO.Items.Data.CloneItemData Gymbag_GymbagBZ = _a.LoadAsset<SCHIZO.Items.Data.CloneItemData>("Assets/Gymbag/Gymbag BZ.asset");
    public static SCHIZO.Items.Data.CloneItemData Gymbag_GymbagSN = _a.LoadAsset<SCHIZO.Items.Data.CloneItemData>("Assets/Gymbag/Gymbag SN.asset");
    public static UnityEngine.Texture2D Loading_Icon_LoadingIcon = _a.LoadAsset<UnityEngine.Texture2D>("Assets/Loading/Icon/loading icon.png");
    public static SCHIZO.Registering.ModRegistry Registry = _a.LoadAsset<SCHIZO.Registering.ModRegistry>("Assets/Registry.asset");
}
