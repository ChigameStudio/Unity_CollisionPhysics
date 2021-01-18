using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 詳細コリジョンのコントロール
/// </summary>
[System.Serializable]
public class RpgCollisionDetailsControl
{
    //===========================================================
    // ヒットデリゲート
    public delegate void HitEnter  (Collider collider);
    public delegate void HitSecond (Collider collider);
    public delegate void HitRelease(Collider collider);

    public HitEnter hit_enter_func_;
    public HitSecond hit_second_func_;
    public HitRelease hit_release_func_;

    public HitEnter HitEnterFunc
    {
        set 
        {
            hit_enter_func_ = value;
        }
    }
    public HitSecond HitSeconFunc
    {
        set
        {
            hit_second_func_ = value;
        }
    }
    public HitRelease HitReleaseFunc
    {
        set
        {
            hit_release_func_ = value;
        }
    }

    //===========================================================

    /// <summary>
    /// リスト
    /// </summary>
    [SerializeField]
    private List<RpgCollisionDetails> list_rpg_collision_details_ = new List<RpgCollisionDetails>();
    public List<RpgCollisionDetails> ListRpgCollisionDetaile
    {
        get { return list_rpg_collision_details_; }
    }

    /// <summary>
    /// ヒットしたコリジョンのフラグ管理
    /// </summary>
    [SerializeField]
    private List<RpgCollisionHit> list_rpg_collision_hit_ = new List<RpgCollisionHit>();

    /// <summary>
    /// オブジェクト
    /// </summary>
    [SerializeField]
    protected GameObject this_object_;
    public GameObject ThisObject
    {
        get { return this_object_; }
    }


    /// <summary>
    /// 初期化
    /// </summary>
    public void Init(GameObject value_object)
    {
        list_rpg_collision_details_ = new List<RpgCollisionDetails>();
        list_rpg_collision_hit_ = new List<RpgCollisionHit>();
        this_object_ = value_object;
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
    /// 更新処理(当たり判定)
    /// </summary>
    /// <param name="collision_construction"></param>
    public void UpdateCollision(BaseCollisionConstruction collision_construction, List<BaseCollisionConstruction> opponent_collider)
    {
        foreach (var details in list_rpg_collision_details_)
        {
            details.UpdateCollision(collision_construction, this ,opponent_collider);
        }
        foreach(var flag in list_rpg_collision_hit_)
        {
            flag.FlagUpdate();
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
    /// 追加詳細コリジョンヒットフラグ
    /// </summary>
    /// <param name="add_collision"></param>
    /// <returns></returns>
    public RpgCollisionHit AddCollisionHit(Collider collider, RpgCollisionDetails add_collision, bool comple_add = false)
    {
        if (collider == null) return null;
        if (list_rpg_collision_hit_ == null) list_rpg_collision_hit_ = new List<RpgCollisionHit>();

        if (comple_add == false)
        {
            RpgCollisionHit hit = SearchCollisionHit(collider);
            if (hit != null) return null;
        }

        RpgCollisionHit new_hit = new RpgCollisionHit();
        new_hit.Init(collider,add_collision.ThisObject);

        list_rpg_collision_hit_.Add(new_hit);
        new_hit.FlagHit();
        return new_hit;
    }

    /// <summary>
    /// ヒットしたときに呼ぶ関数
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public bool CollisionHitEnter(Collider collider,RpgCollisionDetails opponent_col)
    {
        if (collider == null || opponent_col == null) return false;
        if (list_rpg_collision_hit_ == null) list_rpg_collision_hit_ = new List<RpgCollisionHit>();

        RpgCollisionHit hit = SearchCollisionHit(collider);
        if(hit == null)
        {
            hit = AddCollisionHit(collider, opponent_col, true);
            return true;
        }

        hit.FlagHit();
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
            details.Init(collision_construction, col, collision_construction.ThisObject);
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
    /// 検索用詳細コリジョンヒット
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public RpgCollisionHit SearchCollisionHit(Collider search_collision)
    {
        if (list_rpg_collision_hit_.Count == 0) return null;
        return list_rpg_collision_hit_.Find(collision => collision.HitCollision == search_collision);
    }

    /// <summary>
    /// 検索用詳細コリジョンヒット
    /// </summary>
    /// <param name="search_collision"></param>
    /// <returns></returns>
    public RpgCollisionHit SearchCollisionHit(Collider search_collision, RpgCollisionDetails opponent_col)
    {
        if (list_rpg_collision_hit_.Count == 0) return null;
        return list_rpg_collision_hit_.Find(collision => collision.HitCollision == search_collision || collision.ThisObject == opponent_col.ThisObject);
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

    /// <summary>
    /// 削除用詳細コリジョンヒット
    /// </summary>
    /// <returns></returns>
    public void RemoveAllCollisionHit()
    {
        if (list_rpg_collision_hit_.Count == 0) return;
        list_rpg_collision_hit_.RemoveAll( hit_collision => (hit_collision.CheckFlag() == false && hit_collision.GetCollisionHit == false) || hit_collision.HitCollision == null);
    }

    /// <summary>
    /// ヒットUpdate
    /// </summary>
    /// <param name="hit"></param>
    private void HiUpdate(RpgCollisionHit hit)
    {
        if (hit.GetHitEnter == true) hit_enter_func_(hit.HitCollision);
        if (hit.GetHitSecond == true) hit_second_func_(hit.HitCollision);
        if(hit.GetHitRelease == true) hit_release_func_(hit.HitCollision);
    }

    /// <summary>
    /// 計算結果
    /// </summary>
    /// <param name="pos"></param>
    public void CollisionPositionCalculation(Vector3 pos)
    {
        foreach(var collision in list_rpg_collision_details_)
        {
            RpgCollisionDetailsAccessor.SaveCollisionPositionCaculation(collision, pos);
            RpgCollisionDetailsAccessor.CalculationPositionCaculation(collision, pos);
        }
    }


}
