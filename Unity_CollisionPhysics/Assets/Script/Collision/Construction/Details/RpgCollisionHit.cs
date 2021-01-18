using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ヒットしたコリジョン
/// </summary>
[System.Serializable]
public class RpgCollisionHit
{
    /// <summary>
    /// ヒットしたコリジョン
    /// </summary>
    [SerializeField]
    private Collider hit_collision_ ;
    public Collider HitCollision
    {
        get { return hit_collision_; }
    }

    /// <summary>
    /// ヒットコリジョンフラグ
    /// </summary>
    [SerializeField]
    private RpgCollisionHitFlag hit_flag_ = new RpgCollisionHitFlag();
    public bool GetHitEnter
    {
        get { return hit_flag_.HitEnter; }
    }
    public bool GetHitSecond
    {
        get { return hit_flag_.HitSecond; }
    }
    public bool GetHitRelease
    {
        get { return hit_flag_.HitRelease; }
    }

    /// <summary>
    /// 全体的なフラグ
    /// </summary>
    [SerializeField]
    private bool hit_enter_ = false;
    public bool GetCollisionHit
    {
        get { return hit_enter_; }
    }

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
    /// <param name="value_collision"></param>
    public void Init(Collider value_collision,GameObject value_object)
    {
        hit_collision_ = value_collision;
        hit_flag_ = new RpgCollisionHitFlag();
        this_object_ = value_object;
    }

    /// <summary>
    /// フラグ更新処理
    /// </summary>
    /// <param name="enter_flag"></param>
    public void FlagUpdate()
    {
        if (hit_flag_ == null) return;
        hit_flag_.FlagUpdate(hit_enter_);
        hit_enter_ = false;
    }

    /// <summary>
    /// ヒットしたときに呼ぶ関数
    /// </summary>
    public void FlagHit()
    {
        hit_enter_ = true;
    }

    /// <summary>
    /// 確認フラグ
    /// </summary>
    /// <returns></returns>
    public bool CheckFlag()
    {
        if (hit_flag_ == null) return false;
        return hit_flag_.CheckHit();
    }
}
