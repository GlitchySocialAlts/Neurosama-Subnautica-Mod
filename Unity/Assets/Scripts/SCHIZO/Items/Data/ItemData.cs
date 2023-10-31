﻿using JetBrains.Annotations;
using NaughtyAttributes;
using SCHIZO.Attributes.Visual;
using SCHIZO.Interop.Subnautica.Enums;
using SCHIZO.Interop.Subnautica.Enums.BelowZero;
using SCHIZO.Interop.Subnautica.Enums.Subnautica;
using SCHIZO.Items.Data.Crafting;
using SCHIZO.Registering;
using UnityEngine;
using UnityEngine.Serialization;
using static NaughtyAttributes.EConditionOperator;

namespace SCHIZO.Items.Data
{
    [CreateAssetMenu(menuName = "SCHIZO/Items/Item Data")]
    public partial class ItemData : ModRegistryItem
    {
        public GameObject prefab;

        [BoxGroup("TechType"), Required_string, Careful]
        public string classId;

        [BoxGroup("TechType"), Required_string]
        public string displayName;

        [BoxGroup("TechType"), ResizableTextArea, ShowIf(nameof(ShowPickupableProps))]
        public string tooltip;

        [BoxGroup("Common Properties"), ShowIf(nameof(ShowPickupableProps)), Required]
        public Sprite icon;

        [BoxGroup("Common Properties"), HideIf(Or, nameof(HidePickupableProps), nameof(isBuildable))]
        public Vector2Int itemSize = new Vector2Int(1, 1);

        [BoxGroup("Common Properties"), HideIf(Or, nameof(HidePickupableProps), nameof(isBuildable))]
        public bool isCraftable;

        [BoxGroup("Common Properties"), HideIf(Or, nameof(HidePickupableProps), nameof(isCraftable))]
        public bool isBuildable;

        [BoxGroup("Common Properties"), ShowIf(nameof(IsActuallyCraftable))]
        public float craftingTime = 2.5f;

        #region Subnautica Data

        [SNData, Label("Register"), SerializeField]
        public bool registerInSN = true;

        [SNData, Label("Recipe"), SerializeField, ShowIf(And, nameof(registerInSN), nameof(IsBuildableOrCraftable))]
        private Recipe recipeSN;

        [SNData, Label("Craft Tree"), ShowIf(And, nameof(registerInSN), nameof(IsActuallyCraftable)), SerializeField]
        private CraftTree_Type_All craftTreeTypeSN = CraftTree_Type_All.None;

        [SNData, Label("Craft Tree Path"), ShowIf(And, nameof(registerInSN), nameof(craftTreePathSN_ShowIf)), CraftTreePath(Game.Subnautica, nameof(craftTreeTypeSN)), SerializeField, UsedImplicitly]
        private string craftTreePathSN = "";

        [SNData, Label("Tech Group"), ValidateInput(nameof(techGroupSN_Validate)), SerializeField, ShowIf(And, nameof(registerInSN), nameof(IsBuildableOrCraftable))]
        private TechGroup_SN techGroupSN = TechGroup_SN.Uncategorized;

        [SNData, Label("Tech Category"), SerializeField, ShowIf(And, nameof(registerInSN), nameof(techCategorySN_ShowIf)), UsedImplicitly]
        private TechCategory_SN techCategorySN;

        [FormerlySerializedAs("databankInfoSN"), SNData, Label("PDA Ency Info"), SerializeField, UsedImplicitly, ShowIf(nameof(registerInSN))]
        private PDAEncyclopediaInfo pdaEncyclopediaInfoSN;

        [SNData, Label("Known Tech Info"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInSN), nameof(ShowPickupableProps))]
        private KnownTechInfo knownTechInfoSN;

        [SNData, Label("Unlock At Start"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInSN), nameof(IsBuildableOrCraftable))]
        private bool unlockAtStartSN = true;

        [SNData, Label("Required For Unlock"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInSN), nameof(requiredForUnlockSN_ShowIf))]
        private Item requiredForUnlockSN;

        #endregion

        #region Below Zero Data

        [BZData, Label("Register"), SerializeField]
        public bool registerInBZ = true;

        [BZData, Label("Recipe"), SerializeField, ShowIf(And, nameof(registerInBZ), nameof(IsBuildableOrCraftable))]
        private Recipe recipeBZ;

        [BZData, Label("Can Be Recycled"), ShowIf(And, nameof(registerInBZ), nameof(IsActuallyCraftable))]
        public bool canBeRecycledBZ = true;

        [BZData, Label("Craft Tree"), ShowIf(And, nameof(registerInBZ), nameof(IsActuallyCraftable)), SerializeField]
        private CraftTree_Type_All craftTreeTypeBZ = CraftTree_Type_All.None;

        [BZData, Label("Craft Tree Path"), ShowIf(And, nameof(registerInBZ), nameof(craftTreePathBZ_ShowIf)), CraftTreePath(Game.BelowZero, nameof(craftTreeTypeBZ)), SerializeField, UsedImplicitly]
        private string craftTreePathBZ = "";

        [BZData, Label("Tech Group"), ValidateInput(nameof(techGroupBZ_Validate)), SerializeField, ShowIf(And, nameof(registerInBZ), nameof(IsBuildableOrCraftable))]
        private TechGroup_BZ techGroupBZ = TechGroup_BZ.Uncategorized;

        [BZData, Label("Tech Category"), SerializeField, ShowIf(And, nameof(registerInBZ), nameof(techCategoryBZ_ShowIf)), UsedImplicitly]
        private TechCategory_BZ techCategoryBZ;

        [FormerlySerializedAs("databankInfoBZ")] [BZData, Label("PDA Ency Info"), SerializeField, UsedImplicitly, ShowIf(nameof(registerInBZ))]
        private PDAEncyclopediaInfo pdaEncyclopediaInfoBZ;

        [BZData, Label("Known Tech Info"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInBZ), nameof(ShowPickupableProps))]
        private KnownTechInfo knownTechInfoBZ;

        [BZData, Label("Unlock At Start"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInBZ), nameof(IsBuildableOrCraftable))]
        private bool unlockAtStartBZ = true;

        [BZData, Label("Required For Unlock"), SerializeField, UsedImplicitly, ShowIf(And, nameof(registerInBZ), nameof(requiredForUnlockBZ_ShowIf))]
        private Item requiredForUnlockBZ;

        #endregion

        #region NaughtyAttributes stuff

        private bool techGroupSN_Validate(TechGroup_SN val) => !isBuildable || val != TechGroup_SN.Uncategorized;
        private bool techGroupBZ_Validate(TechGroup_BZ val) => !isBuildable || val != TechGroup_BZ.Uncategorized;

        private bool techCategorySN_ShowIf() => techGroupSN != TechGroup_SN.Uncategorized && IsBuildableOrCraftable();
        private bool techCategoryBZ_ShowIf() => techGroupBZ != TechGroup_BZ.Uncategorized && IsBuildableOrCraftable();

        private bool craftTreePathSN_ShowIf() => IsActuallyCraftable() && craftTreeTypeSN != CraftTree_Type_All.None;
        private bool craftTreePathBZ_ShowIf() => IsActuallyCraftable() && craftTreeTypeBZ != CraftTree_Type_All.None;

        private bool requiredForUnlockSN_ShowIf() => !unlockAtStartSN && IsBuildableOrCraftable();
        private bool requiredForUnlockBZ_ShowIf() => !unlockAtStartBZ && IsBuildableOrCraftable();

        protected virtual bool ShowPickupableProps() => true;
        private bool HidePickupableProps() => !ShowPickupableProps();

        private bool IsActuallyCraftable() => isCraftable && ShowPickupableProps();

        private bool IsBuildableOrCraftable() => isBuildable || IsActuallyCraftable();

        private protected sealed class CommonData : BoxGroupAttribute
        {
            public CommonData() : base("Common Properties")
            {
            }
        }

        private protected class SNData : BoxGroupAttribute
        {
            public SNData() : base("Subnautica Data")
            {
            }
        }

        private protected class BZData : BoxGroupAttribute
        {
            public BZData() : base("Below Zero Data")
            {
            }
        }

        #endregion
    }
}