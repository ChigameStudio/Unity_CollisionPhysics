using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 詳細コリジョンのコントロール
/// </summary>
[System.Serializable]
public class RpgCollisionDetailsControl
{
    /// <summary>
    /// リスト
    /// </summary>
    [SerializeField]
    private List<RpgCollisionDetails> list_rpg_collision_details_ = new List<RpgCollisionDetails>();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        list_rpg_collision_details_ = new List<RpgCollisionDetails>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <param name="collision_construction"></param>
    public void Update(BaseCollisionConstruction collision_construction)
    {
        foreach(var details in list_rpg_collision_details_)
        {
            details.Update(collision_construction);
        }
    }

    /// <summary>
    /// 追加詳細コリジョン
    /// </summary>
    /// <param name="add_collision"></param>
    /// <returns></returns>
    public bool AddCollisionDetails(RpgCollisionDetails add_collision)
    {
        if (add_collision == null) return false;
        if (list_rpg_collision_details_ == null) list_rpg_collision_details_ = new List<RpgCollisionDetails>();

        RpgCollisionDetails base_collision = SearchCollisionDetails(add_collision);

        if (base_collision != null) return false;

        list_rpg_collision_details_.Add(add_collision);

        return true;
    }

    /// <summary>
    /// 自動的に追加する
    /// </summary>
    /// <returns></returns>
    public bool AutoAddCollisionDetails(BaseCollisionConstruction collision_construction)
    {
        if (list_rpg_collision_details_ == null) list_rpg_collision_details_ = new List<RpgCollisionDetails>();

        Collider[] colliders = collision_construction.ThisObject.AllComponents<Collider>();
        foreach (var col in colliders)
        {
            if (SearchCollisionDetails(col) != null) continue;
            RpgCollisionDetails details = new RpgCollisionDetails();
            details.Init(collision_construction, col);
            list_rpg_collision_details_.Add(details);
        }

        return true;
    }

    /// <summary>
    /// 削除詳細コリジョン
    /// </summary>
    /// <param name="erasure_collision"></param>
    /// <returns></returns>
    public bool ErasureCollisionDetails(RpgCollisionDetails erasure_collision)
    {
        if (erasure_collision == null) return false;
        if (list_rpg_collision_details_ == null) list_rpg_collision_details_ = new List<RpgCollisionDetails>();



        return RemoveCollisionDetails(erasure_collision);
    }

    /// <summary>
    /// 検索用詳細コリジョン
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public RpgCollisionDetails SearchCollisionDetails(RpgCollisionDetails search_collision)
    {
        if (list_rpg_collision_details_.Count == 0) return null;
        return list_rpg_collision_details_.Find(collision => collision == search_collision);
    }

    /// <summary>
    /// 検索用詳細コリジョン
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public RpgCollisionDetails SearchCollisionDetails(Collider search_collision)
    {
        if (list_rpg_collision_details_.Count == 0) return null;
        return list_rpg_collision_details_.Find(collision => collision.RpgCollision == search_collision);
    }

    /// <summary>
    /// 削除用詳細コリジョン
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public bool RemoveCollisionDetails(RpgCollisionDetails search_collision)
    {
        return list_rpg_collision_details_.Remove(search_collision);
    }


}
