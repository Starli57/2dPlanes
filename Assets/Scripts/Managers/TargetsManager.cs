using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : Singleton<TargetsManager>
{
    public List<PlaneController> targets { get; private set; } = new List<PlaneController>();

    public List<PlaneController> GetTargetsWithoutThis(PlaneController plane)
    {
        var newTargets = new List<PlaneController>(targets);
        newTargets.Remove(plane);
        return newTargets;
    }

    public void Add(PlaneController plane)
    {
        targets.Add(plane);
    }

    public void Remove(PlaneController plane)
    {
        targets.Remove(plane);
    }
}
