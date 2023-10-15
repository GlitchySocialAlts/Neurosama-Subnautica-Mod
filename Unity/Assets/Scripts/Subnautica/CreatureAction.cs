﻿using NaughtyAttributes;
using SCHIZO.Attributes;
using SCHIZO.Utilities;
using UnityEngine;

public class CreatureAction : MonoBehaviour
{
    [Foldout(STRINGS.COMPONENT_REFERENCES), Required, ValidateType("Creature")] public MonoBehaviour creature;
    [Foldout(STRINGS.COMPONENT_REFERENCES), Required] public SwimBehaviour swimBehaviour;

    [BoxGroup("Base Creature Action"), Range(0, 1)] public float evaluatePriority = 0.4f;

    [Foldout(STRINGS.UNCHANGED_BY_ECC)] public AnimationCurve priorityMultiplier;
    [Foldout(STRINGS.UNCHANGED_BY_ECC)] public float minActionCheckInterval = -1;
}
