using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンのポジション制御
/// </summary>
[System.Serializable]
public class RpgCollisionPosition
{
    /// <summary>
    /// 前回の座標
    /// </summary>
    [SerializeField]
    private Vector3 old_collision_position_ = Vector3.zero;
    public Vector3 SaveCollisionPosition
    {
        get { return old_collision_position_; }
    }
    /// <summary>
    /// 現在の座標
    /// </summary>
    [SerializeField]
    private Vector3 now_collision_position_ = Vector3.zero;

    /// <summary>
    /// 前の座標から現在の座標までのベクトル
    /// </summary>
    [SerializeField]
    private Vector3 old_to_now_position_vector = Vector3.zero;
    public Vector3 OldToNowVector
    {
        get { return old_to_now_position_vector; }
    }

    /// <summary>
    /// 計算用座標
    /// </summary>
    [SerializeField]
    private Vector3 calculation_position_ = Vector3.zero;
    public Vector3 CalculationPosition
    {
        get { return calculation_position_; }
    }


    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="position"></param>
    public void Init(Vector3 position)
    {
        now_collision_position_ = old_collision_position_ = position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <param name="now_position"></param>
    public void Update(Vector3 now_position)
    {
        old_collision_position_ = now_collision_position_;
        now_collision_position_ = now_position;

        old_to_now_position_vector = (now_collision_position_ - old_collision_position_);
        calculation_position_ = old_collision_position_ + old_to_now_position_vector;
    }
}
