﻿using JetBrains.Annotations;
using NaughtyAttributes;
using SCHIZO.Interop.Subnautica.Enums;
using UnityEngine;

namespace SCHIZO.Items.Data
{
    [CreateAssetMenu(menuName = "SCHIZO/Items/Clone Item Data")]
    public sealed partial class CloneItemData : ItemData
    {
        [SNData, Label("Clone Target"), SerializeField, UsedImplicitly, ShowIf(nameof(registerInSN))]
        private TechType_All cloneTargetSN;

        [BZData, Label("Clone Target"), SerializeField, UsedImplicitly, ShowIf(nameof(registerInBZ))]
        private TechType_All cloneTargetBZ;

#if UNITY_EDITOR
        [ContextMenu("Set Loader/Gymbag")]
        private void CreateGymbagLoader() => AssignItemLoader(CreateInstance<Gymbag.GymbagLoader>());
#endif
    }
}
