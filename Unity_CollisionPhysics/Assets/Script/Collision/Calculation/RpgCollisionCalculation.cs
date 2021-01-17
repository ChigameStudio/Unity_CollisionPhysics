using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// コリジョン計算
/// </summary>
public static class RpgCollisionCalculation
{

    /// <summary>
    /// 強制終了するMaxカウント
    /// </summary>
    private  static int exit_max_count = 100;

    /// <summary>
    /// カプセル内にコリジョンがあるかどうか
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static Collider[] CapsuleCalculation(CapsuleCollider collider)
    {
        float scale = Mathf.Max(collider.transform.localScale.x, collider.transform.localScale.z);
        float radius = collider.radius * scale + 0.01f;
        float _height = (((collider.height * collider.transform.localScale.y) / 2.0f) - radius);

        Vector3 start_pos = Vector3.zero;
        start_pos = collider.center + collider.transform.position + ((_height * collider.transform.up));
        Vector3 end_pos = Vector3.zero;
        end_pos = collider.center + collider.transform.position + ((_height * (collider.transform.up * -1.0f)));

        Collider[] colliders = OverlapCapsule(start_pos, end_pos, radius);
        return colliders.Where(col => col != collider).ToArray();


    }
    /// <summary>
    /// カプセルOverlap
    /// </summary>
    /// <param name="start_pos">カプセル：スタート</param>
    /// <param name="end_pos">カプセル : エンド</param>
    /// <param name="radius">カプセル : 範囲</param>
    /// <returns></returns>
    public static Collider[] OverlapCapsule(Vector3 start_pos,Vector3 end_pos,float radius)
    {
        return Physics.OverlapCapsule(start_pos, end_pos, radius);
    }

    /// <summary>
    ///球内にコリジョンがあるかどうか
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static Collider[] SphereCalculation(SphereCollider collider)
    {
        float simulationRadius = collider.radius * collider.transform.localScale.x;
        Collider[] colliders = OverlapSphere(collider.transform.position, simulationRadius);
        return colliders.Where(col => col != collider).ToArray();
    }
    /// <summary>
    /// 球体Overlap
    /// </summary>
    /// <param name="pos">球　: 座標</param>
    /// <param name="radius">球　: 範囲</param>
    /// <returns></returns>
    public static Collider[] OverlapSphere(Vector3 pos, float radius)
    {
        return Physics.OverlapSphere(pos, radius);
    }

    /// <summary>
    /// ボックス内にコリジョンがあるかどうか
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static Collider[] BoxCalculation(BoxCollider collider)
    {

        Vector3 size = collider.transform.localScale;
        size.x *= collider.size.x;
        size.y *= collider.size.y;
        size.z *= collider.size.z;
        Vector3 pos = collider.transform.position + collider.center;

        Collider[] colliders = OverlapBox(pos, size, collider.transform.rotation);
        return colliders.Where(col => col != collider).ToArray();

    }
    /// <summary>
    /// ボックスOverlap
    /// </summary>
    /// <param name="pos">ボックス : 座標</param>
    /// <param name="size">ボックス　: サイズ</param>
    /// <param name="rotation">ボックス　: 回転</param>
    /// <returns></returns>
    public static Collider[] OverlapBox(Vector3 pos, Vector3 size, Quaternion rotation)
    {
        return Physics.OverlapBox(pos, size,rotation);
    }

    /// <summary>
    /// 押し出し計算
    /// </summary>
    /// <param name="type"></param>
    /// <param name="this_collider"></param>
    /// <param name="opponent_collider"></param>
    /// <param name="pushBackVector"></param>
    /// <param name="pushBackDistance"></param>
    /// <returns></returns>
    public static bool ComputePenetration(CollisionPushType type, Collider this_collider, Vector3 position, Collider opponent_collider, Vector3 opponent_position, ref Vector3 pushBackVector, ref float pushBackDistance)
    {
        if (type == CollisionPushType.kThisPush)
        {
            return ComputePenetrationThisPush(this_collider, position, opponent_collider, opponent_position ,ref pushBackVector, ref pushBackDistance);
        }
        else
        {
            return ComputePenetrationOpponentPush(this_collider, position, opponent_collider, opponent_position, ref pushBackVector, ref pushBackDistance);
        }
    }

    /// <summary>
    /// 押し出し計算(自分)
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="pushBackVector"></param>
    /// <param name="pushBackDistance"></param>
    /// <returns></returns>
    private static bool ComputePenetrationThisPush(Collider this_collider,Vector3 position ,Collider opponent_collider, Vector3 opponent_position, ref Vector3 pushBackVector, ref float pushBackDistance)
    {
        return Physics.ComputePenetration(
                        this_collider,
                        position,
                        this_collider.transform.rotation,
                        opponent_collider,
                        opponent_position,
                        opponent_collider.transform.rotation,
                        out pushBackVector,
                        out pushBackDistance
                    );
    }

    /// <summary>
    /// 押し出し計算(相手)
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="pushBackVector"></param>
    /// <param name="pushBackDistance"></param>
    /// <returns></returns>
    private static bool ComputePenetrationOpponentPush(Collider this_collider, Vector3 position, Collider opponent_collider, Vector3 opponent_position, ref Vector3 pushBackVector, ref float pushBackDistance)
    {
        return Physics.ComputePenetration(
                        opponent_collider,
                        opponent_position,
                        opponent_collider.transform.rotation,
                        this_collider,
                        position,
                        this_collider.transform.rotation,
                        out pushBackVector,
                        out pushBackDistance
                    );

    }

    /// <summary>
    /// 当たり判定の計算
    /// </summary>
    /// <param name="this_collison"></param>
    /// <param name="collision_details"></param>
    /// <param name="collider"></param>
    public static void CollisionCorrection(BaseCollisionConstruction this_collison, RpgCollisionDetails collision_details, List<BaseCollisionConstruction> opponent_collider,Collider[] colliders)
    {
        if (this_collison == null || collision_details == null) return;
        if (opponent_collider.Count == 0) return;

        //強制終了用のカウント
        int cnt = 0;

        for (int i = 0; i < opponent_collider.Count; i++)
        {
            cnt++;
            Exit(cnt);

            Vector3 pushBackVector = Vector3.zero;
            float pushBackDistance = 0.0f;
            List<RpgCollisionDetails> detaile =  opponent_collider[i].RpgCollisionDetailsControl.ListRpgCollisionDetaile;
            foreach (var opponent_col in detaile)
            {
                if (opponent_col == collision_details) continue;

                ///めり込んでいた場合
                if (ComputePenetration(this_collison.CollisionPushType, collision_details.RpgCollision, RpgCollisionDetailsAccessor.CalculationPosition(collision_details)
                                       , opponent_col.RpgCollision, RpgCollisionDetailsAccessor.SaveCollisionPosition(opponent_col)
                                       , ref pushBackVector, ref pushBackDistance))
                {
                    if(pushBackDistance >= 0.001f)
                    {
                        Vector3 updatedPostion = (pushBackVector) * (pushBackDistance);
                        this_collison.ThisObject.transform.position +=  new Vector3(updatedPostion.x, updatedPostion.y, updatedPostion.z);
                        RpgCollisionDetailsAccessor.SaveCollisionPosition(collision_details, this_collison.ThisObject.transform.position);
                        RpgCollisionDetailsAccessor.CalculationPosition(collision_details, this_collison.ThisObject.transform.position);
                    }
                }
               
                
            }
        }

    }

    /// <summary>
    /// 強制終了
    /// </summary>
    /// <param name="cnt"></param>
    private static void Exit(int cnt)
    {
        if (cnt >= exit_max_count)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
#else
        Application.Quit ();
#endif
        }
    }
}
