using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHay : MonoBehaviour
{
    public float limitHayMov = 21;
    public GameObject hayPrefab;

    public string tagFilter;

    public float shootInterval; //The smallest amount of time between shots 
    private float shootTimer; //A timer that to keep track whether the machine can shoot or not

    // change MovingHay's color
    public Transform modelParent;

    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadModel();
    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // Destroy current model

        // Instantiate desired hay machine
        switch (GameSettings.hayMachineColor) 
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //val. between -1 (left) and 1 (right)

        if (horizontalInput < 0 &&  transform.position.x > -limitHayMov)
            { transform.Translate(-transform.right * 10 * Time.deltaTime); }

        if (horizontalInput > 0 && transform.position.x < +limitHayMov)
            { transform.Translate(transform.right * 10 * Time.deltaTime); }

        UpdateShooting();

        if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector3(-limitHayMov, 0, -34);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(limitHayMov, 0, -34);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(1.28f, 0, -34);
        }
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }
    
    private void ShootHay()
    {
        Instantiate(hayPrefab, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }

}
