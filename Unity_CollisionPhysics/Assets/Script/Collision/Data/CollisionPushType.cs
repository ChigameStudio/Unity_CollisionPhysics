using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コリジョンでの押し出しタイプ
/// </summary>
public enum CollisionPushType : int
{
    kNone,                    //指定なし
    kThisPush,                //自分自身を押し出す
    kOpponentPush,            //相手を押し出す
}
