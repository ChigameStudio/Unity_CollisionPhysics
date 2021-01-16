using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private BaseCollisionConstruction collision_;

    [SerializeField]
    protected CollisionLayer collision_type_;
    // Start is called before the first frame update
    void Start()
    {
        collision_ = new BaseCollisionConstruction();
        collision_.Init(gameObject, collision_type_);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
