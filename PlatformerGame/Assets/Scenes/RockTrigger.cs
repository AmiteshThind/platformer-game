using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    // Start is called before the first frame update


    public Rigidbody2D rockRigidBody;
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        rockRigidBody.isKinematic = false;
        
    }
}
