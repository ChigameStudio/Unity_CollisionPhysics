using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンの詳細データクラス
/// </summary>
[System.Serializable]
public class RpgCollisionDetails
{
    /// <summary>
    /// コリジョンクラス
    /// </summary>
    [SerializeField]
    private Collider rpg_collision_;
    public Collider RpgCollision
    {
        get { return rpg_collision_; }
    }


    /// <summary>
    /// ヒットフラグ
    /// </summary>
    [SerializeField]
    private RpgCollisionHitFlag hit_flag_collision_ = new RpgCollisionHitFlag();
    public RpgCollisionHitFlag HitFlagCollision
    {
        get { return hit_flag_collision_; }
    }

    /// <summary>
    /// コリジョン座標
    /// </summary>
    [SerializeField]
    private RpgCollisionPosition position_collision_ = new RpgCollisionPosition();
    public RpgCollisionPosition PositionCollision
    {
        get { return position_collision_; }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="value_collider"></param>
    public void Init(BaseCollisionConstruction collision,Collider value_collider)
    {
        if (collision == null) return;
        if (hit_flag_collision_ == null) hit_flag_collision_ = new RpgCollisionHitFlag();
        if (position_collision_ == null) position_collision_ = new RpgCollisionPosition();
        rpg_collision_ = value_collider;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <param name="collision"></param>
    public void Update(BaseCollisionConstruction collision)
    {
        if (collision == null) return;
        if (position_collision_ == null) return;
        if (rpg_collision_ == null) return;
        position_collision_.Update(rpg_collision_.transform.position);
    } 

    /// <summary>
    /// フラグ更新処理
    /// </summary>
    /// <param name="hit_flag"></param>
    public void FlagUpdate(bool hit_flag)
    {
        if (hit_flag_collision_ == null) return;
        hit_flag_collision_.FlagUpdate(hit_flag);
    }
}
