using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// 拡張システム
/// </summary>
public　static class ExpansionSystem
{
    public static T AllComponent<T>(this GameObject self) where T : Component
    {
        T mono = self.GetComponent<T>();
        if (mono == null) mono = self.GetComponentInParent<T>();
        if (mono == null) mono = self.GetComponentInChildren<T>();

        return mono;
    }

    public static T[] AllComponents<T>(this GameObject self) where T : Component
    {
        T[] mono = self.GetComponents<T>();
        T[] mono1 = self.GetComponentsInParent<T>();
        T[] mono2 = self.GetComponentsInChildren<T>();

        mono = mono.Concat(mono1).ToArray();
        mono = mono.Concat(mono2).ToArray();

        return mono;
    }
}
