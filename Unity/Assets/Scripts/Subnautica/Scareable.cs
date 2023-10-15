﻿using NaughtyAttributes;
using SCHIZO.Attributes;
using SCHIZO.Utilities;
using UnityEngine;

public class Scareable : MonoBehaviour
{
    [Foldout(STRINGS.COMPONENT_REFERENCES), Required] public CreatureFear creatureFear;
    [Foldout(STRINGS.COMPONENT_REFERENCES), Required, ValidateType("Creature")] public MonoBehaviour creature;

    public EcoTargetType targetType = EcoTargetType.Shark;
    public CreatureAction fleeAction;
    public float scarePerSecond = 4;
    public float maxRangeScalar = 10;
    public float minMass = 50;
    public float updateTargetInterval = 1;
    public float updateRange = 100;

    [Foldout(STRINGS.UNCHANGED_BY_ECC)] public AnimationCurve daynightRangeMultiplier;
}
