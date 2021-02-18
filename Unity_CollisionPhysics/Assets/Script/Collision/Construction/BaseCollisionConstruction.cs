using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンの基礎クラス
/// </summary>
[System.Serializable]
public class BaseCollisionConstruction
{
    /// <summary>
    /// コリジョン管理
    /// </summary>
    [SerializeField]
    protected RpgCollisionDetailsControl rpg_collision_details_control_ = new RpgCollisionDetailsControl();
    public RpgCollisionDetailsControl RpgCollisionDetailsControl
    {
        get { return rpg_collision_details_control_; }
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
    /// コリジョンのタイプ
    /// </summary>
    protected CollisionLayer collision_type_;
    public CollisionLayer CollisionLayer
    {
        get { return collision_type_; }
    }

    protected CollisionPushType collision_push_type_;
    public CollisionPushType CollisionPushType
    {
        get { return collision_push_type_; }
    }



    /// <summary>
    /// コンストラクタ
    /// </summary>
    public BaseCollisionConstruction()
    {
    }

    public void Init(GameObject game_object,CollisionLayer layer,CollisionPushType push)
    {
        collision_push_type_ = push;
        collision_type_ = layer;
        RpgCollisionConnection.AddCollisionConstruction(this);
        rpg_collision_details_control_ = new RpgCollisionDetailsControl();
        this_object_ = game_object;
        rpg_collision_details_control_.Init(this_object_);
        rpg_collision_details_control_.AutoAddCollisionDetails(this);
    }

    public void Init(GameObject game_object, CollisionLayer layer, CollisionPushType push,int find_layer)
    {
        collision_push_type_ = push;
        collision_type_ = layer;
        RpgCollisionConnection.AddCollisionConstruction(this);
        rpg_collision_details_control_ = new RpgCollisionDetailsControl();
        this_object_ = game_object;
        rpg_collision_details_control_.Init(this_object_);
        rpg_collision_details_control_.AutoAddCollisionDetails(this,find_layer);
    }

    public void Update()
    {
        if (rpg_collision_details_control_ == null) return;
        rpg_collision_details_control_.RemoveAllCollisionHit();
        rpg_collision_details_control_.Update(this);


    }

    public void UpdateCollision(List<BaseCollisionConstruction> opponent_collider)
    {
        if (rpg_collision_details_control_ == null) return;
        rpg_collision_details_control_.UpdateCollision(this, opponent_collider);
    }

    public void UpdateCollisionFlag()
    {
        if (rpg_collision_details_control_ == null) return;
        rpg_collision_details_control_.UpdateCollisionFlag();
    }
}
