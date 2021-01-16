using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RpgCollisionControl　に　接続する
/// </summary>
public static class RpgCollisionConnection
{
    /// <summary>
    /// 追加コリジョン
    /// </summary>
    /// <param name="add_collision"></param>
    /// <returns></returns>
    public static bool AddCollisionConstruction(BaseCollisionConstruction add_collision)
    {
        return RpgCollisionControl.Instance.AddCollisionConstruction(add_collision);
    }

    /// <summary>
    /// 削除コリジョン
    /// </summary>
    /// <param name="erasure_collision"></param>
    /// <returns></returns>
    public static bool ErasureCollisionConstruction(BaseCollisionConstruction erasure_collision)
    {
        return RpgCollisionControl.Instance.RemoveCollisionConstruction(erasure_collision);
    }

    /// <summary>
    /// 検索用Collision
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public static BaseCollisionConstruction SearchCollisionConstruction(BaseCollisionConstruction search_collision)
    {
        return RpgCollisionControl.Instance.SearchCollisionConstruction(search_collision);
    }

    /// <summary>
    /// 削除用Collision
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public static bool RemoveCollisionConstruction(BaseCollisionConstruction search_collision)
    {
        return RpgCollisionControl.Instance.RemoveCollisionConstruction(search_collision);
    }
}
