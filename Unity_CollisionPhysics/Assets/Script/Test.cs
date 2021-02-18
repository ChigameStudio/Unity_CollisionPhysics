using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Test : MonoBehaviour
{
    [SerializeField]
    private BaseCollisionConstruction collision_;

    [SerializeField]
    protected CollisionLayer collision_type_;


    [SerializeField]
    protected CollisionPushType collision_push_type_;

    [SerializeField]
    Vector3 move;
    [SerializeField]
    float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        collision_ = new BaseCollisionConstruction();
        collision_.Init(gameObject, collision_type_, collision_push_type_,LayerMask.NameToLayer("Player"));
        collision_.RpgCollisionDetailsControl.HitSecondFunc = Enter;
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += move * speed;
    }

    void Enter(Collider col)
    {
        Debug.Log(col.gameObject);
    }
}
