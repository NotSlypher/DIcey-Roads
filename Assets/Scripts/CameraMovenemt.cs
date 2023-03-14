using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovenemt : MonoBehaviour
{
    private GameObject dice;
    public Vector3 dice_pos;
    public Vector3 cam_pos;
    [SerializeField]
    private float cam_x_offset=0;
    [SerializeField]
    private float cam_y_offset=0;
    [SerializeField]
    private float cam_z_offset=0;
    private cubeMovement cubeScript;

    public float rotaionSpeed = 3f;
   

    public int index = 0;
    int direction = 0;
    bool isMoving = false;

    [Range(0,1)]
    public float lerpTime;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        dice = GameObject.FindGameObjectWithTag("Player");
        if (dice != null)
        {
            cubeScript = dice.gameObject.GetComponent<cubeMovement>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        dice = GameObject.FindGameObjectWithTag("Player");
        if (dice != null)
        {
            cubeScript = dice.gameObject.GetComponent<cubeMovement>();
        }
        if (cubeScript.followCamera)

        {
            dice_pos = dice.transform.position;
            cam_pos = new Vector3(dice_pos.x + cam_x_offset, dice_pos.y + cam_y_offset, dice_pos.z + cam_z_offset);
            //transform.position = cam_pos;

            transform.position = Vector3.SmoothDamp(transform.position,cam_pos, ref velocity, lerpTime);
        }
       


         if(!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                index += 1;
                index %= 4;
                direction = 1;
                StartCoroutine(rotateCam(direction));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                direction = -1;
                index -= 1;
                if (index == -1)
                {
                    index = 3;
                }
                StartCoroutine(rotateCam(direction));
            }
        }
        
    }


    IEnumerator rotateCam(int dir)
    {
        isMoving = true;
        for (int i = 0; i < (90 / rotaionSpeed); i++)
        {
            /*            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + dir, transform.rotation.z);*/
            if(dir == 1)
            {
                transform.RotateAround(dice.transform.position, Vector3.up, rotaionSpeed);
            }
            else
            {
                transform.RotateAround(dice.transform.position, -Vector3.up, rotaionSpeed);
            }
            
            yield return new WaitForSeconds(0.01f);
        }
        isMoving = false;
    }
}
