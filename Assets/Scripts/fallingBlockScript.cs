using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingBlockScript : MonoBehaviour
{

    public Rigidbody blockRB;
    public BoxCollider blockCol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(startFallingTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            blockRB.isKinematic = false;
            blockCol.enabled = false;
        }
    }
    IEnumerator startFallingTime()
    {
        Debug.Log("tiggered");
        yield return new  WaitForSeconds(3);
        blockRB.isKinematic = false; 
        blockCol.enabled = false;
        Destroy(gameObject, 4f);
    }
}
