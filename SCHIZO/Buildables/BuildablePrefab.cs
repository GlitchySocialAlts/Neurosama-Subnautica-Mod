﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using SCHIZO.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SCHIZO.Buildables;

public sealed class BuildablePrefab : CustomPrefab
{
    public string IconAssetName { get; init; }
    public TechGroup TechGroup { get; init; } = TechGroup.Uncategorized;
    public TechCategory TechCategory { get; init; }
    public RecipeData Recipe { get; init; }
    public TechType RequiredForUnlock { get; init; }
    public string PrefabName { get; init; }
    public Action<GameObject> ModifyPrefab { get; init; } = _ => { };

    private readonly ModItem _modItem;
    private readonly List<BuildablePrefab> _oldVersions = new();

    [SetsRequiredMembers]
    public BuildablePrefab(ModItem item) : base(item)
    {
        _modItem = item;
    }

    [SetsRequiredMembers]
    private BuildablePrefab(string classId, string displayName, string tooltip) : base(classId, displayName, tooltip)
    {
    }

    public new void Register()
    {
        _oldVersions.ForEach(v => v.Register());

        if (!string.IsNullOrWhiteSpace(IconAssetName)) Info.WithIcon(ResourceManager.LoadAsset<Sprite>(IconAssetName));
        this.SetRecipe(Recipe);
        if (TechGroup != TechGroup.Uncategorized) this.SetPdaGroupCategory(TechGroup, TechCategory);
        if (RequiredForUnlock != TechType.None) this.SetUnlock(RequiredForUnlock);

        SetGameObject(GetPrefab);
        base.Register();
    }

    public BuildablePrefab WithOldVersion(string oldClassId)
    {
        if (_modItem == null) throw new InvalidOperationException($"Cannot add an old version to buildable which is already an old version (tying to add {oldClassId} to {Info.ClassID})");

        _oldVersions.Add(new BuildablePrefab(oldClassId, _modItem.DisplayName + " (OLD VERSION, PLEASE REBUILD)", _modItem.Tooltip + " (OLD VERSION, PLEASE REBUILD)")
        {
            IconAssetName = IconAssetName,
            Recipe = Recipe,
            PrefabName = PrefabName,
        });

        return this;
    }

    private GameObject GetPrefab()
    {
        GameObject prefab = ResourceManager.LoadAsset<GameObject>(PrefabName);
        GameObject instance = Object.Instantiate(prefab, BuildablesLoader.DisabledParent);
        PrefabUtils.AddBasicComponents(instance, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

        Transform child = instance.transform.GetChild(0); // each buildable should have an unique child with an appropriate collider

        Constructable con = PrefabUtils.AddConstructable(instance, Info.TechType, ConstructableFlags.Outside | ConstructableFlags.Base | ConstructableFlags.Submarine | ConstructableFlags.AllowedOnConstructable | ConstructableFlags.Ground | ConstructableFlags.Inside, child.gameObject);

        con.rotationEnabled = true;

        ModifyPrefab(instance);
        MaterialUtils.ApplySNShaders(instance, 1);

        return instance;
    }
}
