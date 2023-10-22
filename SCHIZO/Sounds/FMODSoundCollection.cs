using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Nautilus.Handlers;
using Nautilus.Utility;
using SCHIZO.DataStructures;
using UnityEngine;
using UWE;

namespace SCHIZO.Sounds;

[SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Serialization")]
public sealed class FMODSoundCollection
{
    private static ConcurrentDictionary<(BaseSoundCollection, string), FMODSoundCollection> _cache = new();

    private string _busPath;
    private List<string> _sounds = new();

    private Bus _bus;
    private RandomList<string> _randomSounds;
    private List<Coroutine> _runningCoroutines;

    public static FMODSoundCollection For(BaseSoundCollection soundCollection, string bus)
    {
        if (!_cache.TryGetValue((soundCollection, bus), out FMODSoundCollection cached))
            cached = _cache[(soundCollection, bus)] = new FMODSoundCollection(soundCollection, bus);
        return cached;
    }

    private FMODSoundCollection(BaseSoundCollection soundCollection, string bus)
    {
        _busPath = bus;
        _bus = RuntimeManager.GetBus(_busPath);

        foreach (AudioClip audioClip in soundCollection.GetSounds())
        {
            string id = Guid.NewGuid().ToString();
            RegisterSound(id, audioClip);
            _sounds.Add(id);
        }
    }

    private void Initialize()
    {
        if (_randomSounds is { Count: > 0 }) return;

        _randomSounds = new RandomList<string>();
        _runningCoroutines = new List<Coroutine>();
        _randomSounds.AddRange(_sounds);
    }

    public float LastPlay { get; private set; } = -1;

    private void StartSoundCoroutine(IEnumerator coroutine)
    {
        _runningCoroutines.Add(CoroutineHost.StartCoroutine(coroutine));
    }

    private void RegisterSound(string id, AudioClip audioClip)
    {
        Sound s = CustomSoundHandler.RegisterCustomSound(id, audioClip, _bus, AudioUtils.StandardSoundModes_3D);
        _bus.unlockChannelGroup();
        s.set3DMinMaxDistance(1, 30);
    }

    public void CancelAllDelayed()
    {
        Initialize();

        foreach (Coroutine c in _runningCoroutines)
        {
            CoroutineHost.StopCoroutine(c);
        }

        _runningCoroutines.Clear();
    }

    public void Play2D(float delay = 0) => Play(null, delay);

    public void Play(FMOD_CustomEmitter emitter, float delay = 0)
    {
        Initialize();
        if (CONFIG.DisableAllNoises) return;

        if (delay <= 0)
        {
            PlaySound(emitter);
            return;
        }

        StartSoundCoroutine(PlayWithDelay(delay));
        return;

        IEnumerator PlayWithDelay(float del)
        {
            yield return new WaitForSeconds(del);
            PlaySound(emitter);
        }
    }

    private void PlaySound(FMOD_CustomEmitter emitter = null)
    {
        LastPlay = Time.time;

        string sound = _sounds.GetRandom();

        if (emitter)
        {
            emitter.SetAsset(AudioUtils.GetFmodAsset(sound));
            emitter.Play();
        }
        else
        {
            CustomSoundHandler.TryPlayCustomSound(sound, out Channel channel);
            channel.set3DLevel(0);
        }
    }
}
