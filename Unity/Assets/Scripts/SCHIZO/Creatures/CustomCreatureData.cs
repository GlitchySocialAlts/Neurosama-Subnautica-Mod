﻿using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace SCHIZO.Unity.Creatures
{
    [CreateAssetMenu(menuName = "SCHIZO/Creatures/Custom Creature Data")]
    public class CustomCreatureData : ScriptableObject
    {
        [BoxGroup("Creature Prefabs")][FormerlySerializedAs("prefab")] public GameObject regularPrefab;

        [BoxGroup("Databank Info")] public Sprite unlockSprite;
        [BoxGroup("Databank Info")] public Texture2D databankTexture;
        [BoxGroup("Databank Info")] public TextAsset databankText;

        [BoxGroup("Creature Sounds")][FormerlySerializedAs("sounds")] public CreatureSoundData soundData;

        [BoxGroup("Creature Data")] public bool AcidImmune = true;
        [BoxGroup("Creature Data")] public float BioReactorCharge = 0;
    }
}
