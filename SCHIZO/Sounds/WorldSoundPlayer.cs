﻿using System;
using UnityEngine;
using Random = System.Random;

namespace SCHIZO.Sounds;

public sealed class WorldSoundPlayer : MonoBehaviour
{
    [SerializeField] private Pickupable _pickupable;
    [SerializeField] private FMOD_CustomEmitter _emitter;
    [SerializeField] private SoundCollection3D _sounds;
    [SerializeField] private float _pitch;

    private float _timer = -1;
    private Random _random;

    public static void Add(GameObject obj, SoundCollection3D sounds, float pitch = 1)
    {
        if (sounds == null) throw new ArgumentNullException(nameof(sounds));
        WorldSoundPlayer player = obj.AddComponent<WorldSoundPlayer>();
        player._sounds = sounds;
        player._pitch = pitch;
    }

    private void Awake()
    {
        _random = new Random(GetInstanceID());

        _pickupable = GetComponent<Pickupable>();
        _emitter = gameObject.AddComponent<FMOD_CustomEmitter>();
        _emitter.followParent = true;

        _timer = _random.Next(CONFIG.MinWorldNoiseDelay, CONFIG.MaxWorldNoiseDelay);
    }

    private void Update()
    {
        if (CONFIG.DisableAllNoises) return;
        if (CONFIG.DisableWorldNoises) return;

        if (_pickupable && Inventory.main.Contains(_pickupable)) return;

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _timer = _random.Next(CONFIG.MinWorldNoiseDelay, CONFIG.MaxWorldNoiseDelay);
            _emitter.evt.setPitch(_pitch);
            _sounds.Play(_emitter);
            _emitter.evt.setPitch(_pitch);
        }
    }
}
