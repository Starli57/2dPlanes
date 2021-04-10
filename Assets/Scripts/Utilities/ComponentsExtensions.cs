using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentsExtensions
{
    public static Component AddComponentIfNotExist(this GameObject go, Type type)
    {
        var component = go.GetComponent(type);
        if (component == null)
            component = go.AddComponent(type);

        return component;
    }
}
