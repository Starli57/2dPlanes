using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { hit, planeDestroy }

public class EffectsManager : Singleton<EffectsManager>
{
    public void ShowEffect(EffectType effectType, Vector3 position, Quaternion rotation)
    {
        var effect = Instantiate(_effectsMap[effectType], position, rotation);
    }
    
    [SerializeField] private List<VisualEffect> _effects;
    private Dictionary<EffectType, GameObject> _effectsMap;

    protected override void Awake()
    {
        base.Awake();
        _effectsMap = new Dictionary<EffectType, GameObject>();
        foreach (var effect in _effects)
            _effectsMap.Add(effect.effectType, effect.prefab);
    }
}

[System.Serializable]
public class VisualEffect
{
    public EffectType effectType;
    public GameObject prefab;
}
