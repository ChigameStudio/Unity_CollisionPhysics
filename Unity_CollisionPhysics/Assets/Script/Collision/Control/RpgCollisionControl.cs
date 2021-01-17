using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rpgのコリジョンコントロール
/// </summary>
[DefaultExecutionOrder(100)]
public class RpgCollisionControl : MonoBehaviour
{
    /// <summary>
    /// グロ―バル変数
    /// </summary>
    [SerializeField]
    private static RpgCollisionControl rpg_collision_control_;
    public static RpgCollisionControl Instance
    {
        get
        {
            if (rpg_collision_control_ == null)
            {
                var obj = new GameObject("RpgCollisionControl");
                rpg_collision_control_ = obj.AddComponent<RpgCollisionControl>();
            }

            return rpg_collision_control_;
        }
    }
    

    /// <summary>
    /// 全コリジョンを保持しているList
    /// </summary>
    [SerializeField]
    private List<BaseCollisionConstruction> list_all_collision_construction_ = new List<BaseCollisionConstruction>();
    public List<BaseCollisionConstruction> ListAllCollisionConstruction
    {
        get { return list_all_collision_construction_; }
    }

    /// <summary>
    /// 追加コリジョン
    /// </summary>
    /// <param name="add_collision"></param>
    /// <returns></returns>
    public bool AddCollisionConstruction(BaseCollisionConstruction add_collision)
    {
        if (add_collision == null) return false;
        if (list_all_collision_construction_ == null) list_all_collision_construction_ = new List<BaseCollisionConstruction>();

        BaseCollisionConstruction base_collision = SearchCollisionConstruction(add_collision);

        if (base_collision != null) return false;

        list_all_collision_construction_.Add(add_collision);
        CollisionSort();
        return true;
    }

    /// <summary>
    /// 削除コリジョン
    /// </summary>
    /// <param name="erasure_collision"></param>
    /// <returns></returns>
    public bool ErasureCollisionConstruction(BaseCollisionConstruction erasure_collision)
    {
        if (erasure_collision == null) return false;
        if (list_all_collision_construction_ == null) list_all_collision_construction_ = new List<BaseCollisionConstruction>();
        CollisionSort();


        return RemoveCollisionConstruction(erasure_collision);
    }

    /// <summary>
    /// 検索用Collision
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public BaseCollisionConstruction SearchCollisionConstruction(BaseCollisionConstruction search_collision)
    {
        if (list_all_collision_construction_.Count == 0) return null;
        return list_all_collision_construction_.Find(collision => collision == search_collision);
    }

    /// <summary>
    /// 削除用Collision
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public bool RemoveCollisionConstruction(BaseCollisionConstruction search_collision)
    {
        return list_all_collision_construction_.Remove(search_collision);
    }

    /// <summary>
    /// 自動削除Collision
    /// </summary>
    public void AutoRemoveCollisionConstruction()
    {
        list_all_collision_construction_.RemoveAll(collision => collision.ThisObject == null);
    }

    /// <summary>
    /// コリジョン更新処理
    /// </summary>
    public void CollisionUpdate()
    {
        if (list_all_collision_construction_.Count == 0) return;
        AutoRemoveCollisionConstruction();
        foreach (var collision in list_all_collision_construction_)
        {
            collision.Update();
        }

        int index = 1;
        int max_index = list_all_collision_construction_.Count - index;
        foreach (var collision in list_all_collision_construction_)
        {
            collision.UpdateCollision(list_all_collision_construction_);
            //collision.UpdateCollision(list_all_collision_construction_.GetRange(index,max_index));
            //index++;
            //max_index = list_all_collision_construction_.Count - index;
        }
    }

    /// <summary>
    /// コリジョンソート
    /// </summary>
    public void CollisionSort()
    {
        if (list_all_collision_construction_.Count == 0) return;

        list_all_collision_construction_.Sort((a, b) => a.CollisionLayer - b.CollisionLayer);
    }

    public void Update()
    {
        CollisionUpdate();
    }

}
