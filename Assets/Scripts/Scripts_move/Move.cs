using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Robot;
    public float speed;
    public float degree;
    public float move_degree;
    public Vector3 direction;
    public Vector3 rotationVector;
    public GameObject Gen_alg;


    void FixedUpdate()
    {
        Move_to();
        transform.Translate(direction * Time.deltaTime);
        rotationVector = new Vector3(0, move_degree, 0);
        Quaternion rotation = Quaternion.Euler(rotationVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 2.5f * Time.deltaTime);

    }

    public void Move_to()
    {
        move_degree += degree;
        float rad = move_degree * Mathf.Deg2Rad;
        direction = new Vector3(speed, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "walls")
        {
            //G.robot_destroy(gameObject.name);
            Gen_alg.GetComponent<Gen_algoritm>().robot_destroy(Robot.name);
        }
    }

}
