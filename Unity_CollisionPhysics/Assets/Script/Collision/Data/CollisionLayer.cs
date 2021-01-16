using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンレイヤー
/// </summary>
public enum CollisionLayer : int
{
    kNone,                         //指定なし
    kPlayer,                       //プレイヤー
    kEnemy,                        //エネミー
    kNoneUnit,                     //指定しないUnit
    kBackGround,                   //背景
}