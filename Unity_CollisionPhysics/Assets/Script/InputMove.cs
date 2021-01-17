using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputMove : MonoBehaviour
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

    void Start()
    {
        collision_ = new BaseCollisionConstruction();
        collision_.Init(gameObject, collision_type_, collision_push_type_);
    }

    // Update is called once per frame
    void Update()
    {
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);

        move = Vector3.zero;
        if (w)
        {
            move.z = 1.0f;
        }
        else if(s)
        {
            move.z = -1.0f;
        }

        if(a)
        {
            move.x = -1.0f;
        }
        else if(d)
        {
            move.x = 1.0f;
        }
        transform.position += move * speed;
    }
}
