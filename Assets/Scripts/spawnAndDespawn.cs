using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAndDespawn : MonoBehaviour
{
    public Transform spawnpoint;
    public cubeMovement cubeScript;
    public GameObject dice;

    private GameObject currentDice;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        Spawn();
        cubeScript = currentDice.GetComponent<cubeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cubeScript.isDespawn)
        {
            Debug.Log("player detected");
            currentDice.gameObject.SetActive(false);
            currentDice.transform.position = spawnpoint.transform.position;
            currentDice.gameObject.SetActive(true);
            cubeScript.isDespawn = false;
        }
    }

    

    void Spawn()
    {
        currentDice = Instantiate(dice, spawnpoint.position, Quaternion.Euler(-90,0,0));
    }
   
}
