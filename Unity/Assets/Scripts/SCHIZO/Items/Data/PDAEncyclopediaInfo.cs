using System;
using NaughtyAttributes;
using SCHIZO.Attributes.Validation;
using SCHIZO.Interop.NaughtyAttributes;
using SCHIZO.Registering;
using SCHIZO.Sounds.Collections;
using TriInspector;
using UnityEngine;

namespace SCHIZO.Items.Data
{
    [CreateAssetMenu(menuName = "SCHIZO/Items/PDA Encyclopedia Info")]
    [DeclareBoxGroup("Scanning")]
    [DeclareBoxGroup("Databank")]
    public sealed class PDAEncyclopediaInfo : ScriptableObject
    {
        [Group("Scanning")] public float scanTime = 3;
        [Group("Scanning")] public Sprite unlockSprite;
        [Group("Scanning")] public bool isImportantUnlock;
        [Group("Scanning")] public SoundCollectionInstance scanSounds;

        [BoxGroup("Databank"), EncyPath(Game.Subnautica)] public string encyPathSN;
        [BoxGroup("Databank"), EncyPath(Game.BelowZero)] public string encyPathBZ;
        [Group("Databank")] public string title;
        [Group("Databank")] public Texture2D texture;
        [Group("Databank")] public TextAsset description;

        private sealed class EncyPathAttribute : SwitchDropdownAttribute
        {
            private readonly Game _game;

            public EncyPathAttribute(Game game)
            {
                _game = game;
            }

            public override string GetDropdownListName(SerializedPropertyHolder property)
            {
                switch (_game)
                {
                    case Game.Subnautica:
                        return nameof(_encyPaths_SN);

                    case Game.BelowZero:
                        return nameof(_encyPaths_BZ);

                    default:
                        throw new NotSupportedException($"Choose either Subnautica or BelowZero for {nameof(EncyPathAttribute)}");
                }
            }

        }

        private readonly DropdownList<string> _encyPaths_SN = new DropdownList<string>()
        {
            {"(root)", ""},

            {"Advanced Theories", "Advanced"},

            {"Blueprints/(root)", "Tech"},
                {"Blueprints/Equipment", "Tech/Equipment"},
                {"Blueprints/Habitat Installations", "Tech/Habitats"},
                {"Blueprints/Vehicles", "Tech/Vehicles"},
                {"Blueprints/Power", "Tech/Power"},

            {"Data Downloads/(root)", "DownloadedData"},
                {"Data Downloads/Alien Data/(root)", "DownloadedData/Precursor"},
                    {"Data Downloads/Alien Data/Artifacts", "DownloadedData/Precursor/Artifacts"},
                    {"Data Downloads/Alien Data/Scan Data", "DownloadedData/Precursor/Scan"},
                    {"Data Downloads/Alien Data/Terminal Data", "DownloadedData/Precursor/Terminal"},
                {"Data Downloads/Aurora Survivors", "DownloadedData/AuroraSurvivors"},
                {"Data Downloads/Codes && Clues", "DownloadedData/Codes"},
                {"Data Downloads/Degasi Survivors/(root)", "DownloadedData/Degasi"},
                    {"Data Downloads/Degasi Survivors/Alterra Search && Rescue Mission", "DownloadedData/Degasi/Orders"},
                {"Data Downloads/Operations Logs", "DownloadedData/BeforeCrash"},
                {"Data Downloads/Public Documents", "DownloadedData/PublicDocs"},

            {"Indigenous Lifeforms/(root)", "Lifeforms"},
                {"Indigenous Lifeforms/Coral", "Lifeforms/Coral"},
                {"Indigenous Lifeforms/Fauna/(root)", "Lifeforms/Fauna"},
                    {"Indigenous Lifeforms/Fauna/Carnivores", "Lifeforms/Fauna/Carnivores"},
                    {"Indigenous Lifeforms/Fauna/Deceased", "Lifeforms/Fauna/Deceased"},
                    {"Indigenous Lifeforms/Fauna/Herbivores - Large", "Lifeforms/Fauna/LargeHerbivores"},
                    {"Indigenous Lifeforms/Fauna/Herbivores - Small", "Lifeforms/Fauna/SmallHerbivores"},
                    {"Indigenous Lifeforms/Fauna/Leviathans", "Lifeforms/Fauna/Leviathans"},
                    {"Indigenous Lifeforms/Fauna/Scavengers && Parasites", "Lifeforms/Fauna/Scavengers"},

                {"Indigenous Lifeforms/Flora/(root)", "Lifeforms/Flora"},
                    {"Indigenous Lifeforms/Flora/Exploitable", "Lifeforms/Flora/Exploitable"},
                    {"Indigenous Lifeforms/Flora/Land", "Lifeforms/Flora/Land"},
                    {"Indigenous Lifeforms/Flora/Sea", "Lifeforms/Flora/Sea"},

            {"Geological Data", "PlanetaryGeology"},

            {"Survival Package/(root)", "Welcome"},
                {"Survival Package/Additional Technical", "Welcome/StartGear"},

            {"Time Capsules", "TimeCapsules"},
        };

        private readonly DropdownList<string> _encyPaths_BZ = new DropdownList<string>()
        {
            {"(root)", ""},

            {"Logs && Communications/(root)", "DownloadedData"},
                {"Logs && Communications/Alterra", "DownloadedData/Alterra"},
                {"Logs && Communications/Alterra Personnel", "DownloadedData/AlterraPersonnel"},
                {"Logs && Communications/Maps", "DownloadedData/Maps"},
                {"Logs && Communications/Marguerit", "DownloadedData/Marguerit"},
                {"Logs && Communications/Memos && Miscellany", "DownloadedData/Memos"},
                {"Logs && Communications/Mercury II Logs", "DownloadedData/ShipWreck"},
                {"Logs && Communications/News", "DownloadedData/News"},
                {"Logs && Communications/Sam", "DownloadedData/Sam"},

            {"Personal Log", "PersonalLog"},

            {"Research/(root)", "Research"},
                {"Research/Alien Data", "Research/Precursor"},
                {"Research/Geological Data", "Research/PlanetaryGeology"},
                {"Research/Indigenous Lifeforms/(root)", "Research"},
                    {"Research/Indigenous Lifeforms/Coral", "Research/Lifeforms/Coral"},
                    {"Research/Indigenous Lifeforms/Fauna/(root)", "Research/Lifeforms/Fauna"},
                        {"Research/Indigenous Lifeforms/Fauna/Carnivores", "Research/Lifeforms/Fauna/Carnivores"},
                        {"Research/Indigenous Lifeforms/Fauna/Herbivores - Large", "Research/Lifeforms/Fauna/LargeHerbivores"},
                        {"Research/Indigenous Lifeforms/Fauna/Herbivores - Small", "Research/Lifeforms/Fauna/SmallHerbivores"},
                        {"Research/Indigenous Lifeforms/Fauna/Leviathans/(root)", "Research/Lifeforms/Fauna/Leviathans"},
                            {"Research/Indigenous Lifeforms/Fauna/Leviathans/Frozen Creature", "Research/Lifeforms/Fauna/Leviathans/FrozenCreature"},
                        {"Research/Indigenous Lifeforms/Fauna/Other", "Research/Lifeforms/Fauna/Other"},
                        {"Research/Indigenous Lifeforms/Fauna/Scavengers && Parasites", "Research/Lifeforms/Fauna/Scavengers"},
                    {"Research/Indigenous Lifeforms/Flora/(root)", "Lifeforms/Flora"},
                        {"Research/Indigenous Lifeforms/Flora/Exploitable", "Lifeforms/Flora/Exploitable"},
                        {"Research/Indigenous Lifeforms/Flora/Land", "Lifeforms/Flora/Land"},
                        {"Research/Indigenous Lifeforms/Flora/Sea", "Lifeforms/Flora/Sea"},

            {"Survival", "Survival"},

            {"Tech/(root)", "Tech"},
                {"Tech/Equipment", "Tech/Equipment"},
                {"Tech/Habitat Installations", "Tech/Habitats"},
                {"Tech/Power", "Tech/Power"},
                {"Tech/Vehicles", "Tech/Vehicles"},
        };
    }
}
