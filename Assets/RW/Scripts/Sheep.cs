using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; // speed m per sec
    public float gotHayDestroyDelay; // destruction delay
    private bool hitByHay; // true once the sheep was hit

    public float dropDestroyDelay; // time destroyed by falling
    private Collider myCollider; // sheep's collider
    private Rigidbody myRigidbody; // sheep's rigidbody

    private SheepSpawner sheepSpawner; // reference to sheep spawner

    public float heartOffset;
    public GameObject heartPrefab;

    private bool dropped;

    // Start is called before the first frame update
    void Start()
    {
        // assign references
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        dropped = false; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject);
            HitByHay();
        }

        // if hit by dropsheep -> drop sheep
        else if ( other.CompareTag("DropSheep"))
        {
            if (dropped == false)
            {
                Drop(0);
            }
            
        }
        // if hit by haymachine -> delete sheep (count it as dropped)
        else if (other.CompareTag("HayMachine"))
        {
            if (dropped == false)
            {
                Drop(1);
            }
        }
    }

    private void HitByHay()
    {
        GameStateManager.Instance.SavedSheep(); // tell the game state manager that a sheep was saved 
        sheepSpawner.RemoveSheepFromList(gameObject); // remove sheep from list

        hitByHay = true; // to call it only once
        runSpeed = 0; // stop the sheep

        Destroy(gameObject, gotHayDestroyDelay); // delete sheep 

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity); // instantiate heart

        /* downscale sheep once hit by hay */
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        /* add sound */
        SoundManager.Instance.PlaySheepHitClip();
    }

    private void Drop(int i)
    {
        dropped = true;

        GameStateManager.Instance.DroppedSheep(); // tell the game state manager that a sheep was dropped 
        sheepSpawner.RemoveSheepFromList(gameObject); // remove sheep from list

        myRigidbody.isKinematic = false; // make it non-kinematic -> gravity
        myCollider.isTrigger = false; // sheep becomes solid object
        if (i == 0)
        {
            Destroy(gameObject, dropDestroyDelay); // destroy after certain delay
        }
        else if (i == 1)
        {
            Destroy(gameObject, 0); // destroy after certain delay

        }

        /* add sound */
        SoundManager.Instance.PlaySheepDroppedClip();
    }


    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner; 
    }
}
