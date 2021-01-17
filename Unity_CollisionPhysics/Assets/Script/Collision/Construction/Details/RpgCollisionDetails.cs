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
        position_collision_.Update(collision.ThisObject.transform.position);
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
    /// 更新処理(座標)
    /// </summary>
    /// <param name="collision"></param>
    public void UpdatePosition(Vector3 pos)
    {
        if (position_collision_ == null) return;
        if (rpg_collision_ == null) return;
        position_collision_.Update(pos);
    }

    /// <summary>
    /// 更新処理(当たり判定)
    /// </summary>
    /// <param name="collision"></param>
    public void UpdateCollision(BaseCollisionConstruction collision, List<BaseCollisionConstruction> opponent_collider)
    {
        if (collision == null) return;
        if (rpg_collision_ == null) return;
        Collider[] colliders = CollisionTypeOverLap();
        RpgCollisionCalculation.CollisionCorrection(collision,this,opponent_collider, colliders);
    }

    /// <summary>
    /// コリジョンのタイプで当たり判定が変わる
    /// </summary>
    private Collider[] CollisionTypeOverLap()
    {
        Collider[] colliders = new Collider[0];
        if(rpg_collision_.GetType() == typeof(BoxCollider))
        {
            colliders = RpgCollisionCalculation.BoxCalculation((BoxCollider)rpg_collision_);
        }
        else if(rpg_collision_.GetType() == typeof(SphereCollider))
        {
            colliders = RpgCollisionCalculation.SphereCalculation((SphereCollider)rpg_collision_);
        }
        else if (rpg_collision_.GetType() == typeof(CapsuleCollider))
        {
            colliders = RpgCollisionCalculation.CapsuleCalculation((CapsuleCollider)rpg_collision_);
        }
        return colliders;
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
