using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public Transform moveSpots;
    int randomSpot;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Im " + this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, new Vector3(moveSpots.position.x, transform.position.y, moveSpots.position.z), speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Leaf" && this.gameObject.tag == "EWater") {
            MasterControl.Instance.Kills++;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Fire" && this.gameObject.tag == "ELeaf") {
            MasterControl.Instance.Kills++;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Water" && this.gameObject.tag == "EFire") {
            MasterControl.Instance.Kills++;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Gate") {
            MasterControl.Instance.Health--;
            Destroy(gameObject);
        }
    }
}
