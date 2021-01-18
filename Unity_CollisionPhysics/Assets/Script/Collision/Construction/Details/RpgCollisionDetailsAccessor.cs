using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RpgCollisionDetailsのアクセサー
/// </summary>
public static class RpgCollisionDetailsAccessor
{
    public static RpgCollisionPosition CollisionPosition(RpgCollisionDetails collision_detailes)
    {
        if (collision_detailes == null) return null;
        return collision_detailes.PositionCollision;
    }
    public static Vector3 SaveCollisionPosition(RpgCollisionDetails collision_detailes)
    {
        return CollisionPosition(collision_detailes).SaveCollisionPosition;
    }
    public static Vector3 SaveCollisionPosition(RpgCollisionDetails collision_detailes,Vector3 set_pos)
    {
        return CollisionPosition(collision_detailes).SaveCollisionPosition = set_pos;
    }
    public static Vector3 SaveCollisionPositionCaculation(RpgCollisionDetails collision_detailes, Vector3 set_pos)
    {
        return SaveCollisionPosition(collision_detailes,CollisionPosition(collision_detailes).SaveCollisionPosition + set_pos);
    }

    public static Vector3 OldToNowVector(RpgCollisionDetails collision_detailes)
    {
        return CollisionPosition(collision_detailes).OldToNowVector;
    }
    public static Vector3 CalculationPosition(RpgCollisionDetails collision_detailes)
    {
        return CollisionPosition(collision_detailes).CalculationPosition;
    }
    public static Vector3 CalculationPosition(RpgCollisionDetails collision_detailes, Vector3 set_pos)
    {
        return CollisionPosition(collision_detailes).CalculationPosition = set_pos;
    }
    public static Vector3 CalculationPositionCaculation(RpgCollisionDetails collision_detailes, Vector3 set_pos)
    {
        return CalculationPosition(collision_detailes, CollisionPosition(collision_detailes).CalculationPosition + set_pos);
    }
    public static Vector3 SaveOldToNowVector(RpgCollisionDetails collision_detailes)
    {
        return CollisionPosition(collision_detailes).SaveOldToNowVector;
    }


}
