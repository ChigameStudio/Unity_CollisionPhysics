using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンの基礎クラス
/// </summary>
[System.Serializable]
public class BaseCollisionConstruction
{
    [SerializeField]
    protected RpgCollisionDetailsControl rpg_collision_details_control_ = new RpgCollisionDetailsControl();

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
    /// コリジョンのタイプ
    /// </summary>
    protected CollisionLayer collision_type_;
    public CollisionLayer CollisionLayer
    {
        get { return collision_type_; }
    }


    /// <summary>
    /// コンストラクタ
    /// </summary>
    public BaseCollisionConstruction()
    {
    }

    public void Init(GameObject game_object,CollisionLayer layer)
    {
        collision_type_ = layer;
        RpgCollisionConnection.AddCollisionConstruction(this);
        rpg_collision_details_control_ = new RpgCollisionDetailsControl();
        this_object_ = game_object;
        rpg_collision_details_control_.AutoAddCollisionDetails(this);
    }

    public void Update()
    {
        if (rpg_collision_details_control_ == null) return;
        rpg_collision_details_control_.Update(this);
        
    }
}
