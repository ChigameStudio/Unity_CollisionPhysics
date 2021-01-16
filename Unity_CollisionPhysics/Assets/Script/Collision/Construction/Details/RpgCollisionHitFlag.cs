using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンヒットフラグ
/// </summary>
[System.Serializable]
public class RpgCollisionHitFlag
{
    /// <summary>
    /// ヒットし続けている
    /// </summary>
    [SerializeField]
    private bool hit_enter_ = false;
    public bool HitEnter
    {
        get { return hit_enter_; }
    }

    /// <summary>
    /// ヒットしていない
    /// </summary>
    [SerializeField]
    private bool hit_exit_ = false;
    public bool HitExit
    {
        get { return hit_exit_; }
    }

    /// <summary>
    /// ヒットした瞬間
    /// </summary>
    [SerializeField]
    private bool hit_second_ = false;
    public bool HitSecond
    {
        get { return hit_second_; }
    }

    /// <summary>
    /// 離れた瞬間
    /// </summary>
    [SerializeField]
    private bool hit_release_ = false;
    public bool HitRelease
    {
        get { return hit_release_; }
    }

    /// <summary>
    /// 保存フラグ
    /// </summary>
    [SerializeField]
    private bool hit_state_ = false;

    /// <summary>
    /// フラグUpdate
    /// </summary>
    /// <param name="hit_flag">hitしたかどうか</param>
    public void FlagUpdate(bool hit_flag)
    {

        hit_enter_ = hit_flag;
        hit_exit_ = !hit_flag;

        hit_second_ = (hit_state_ ^ hit_flag) & hit_flag;
        hit_release_ = (hit_state_ ^ hit_flag) & !hit_flag;

        hit_state_ = hit_flag;
    }
}
