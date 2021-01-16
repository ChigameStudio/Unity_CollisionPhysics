using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (mono == null) mono = self.GetComponentsInParent<T>();
        if (mono == null) mono = self.GetComponentsInChildren<T>();

        return mono;
    }
}
