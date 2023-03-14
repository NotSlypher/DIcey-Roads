using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_slide : MonoBehaviour
{
    private GameObject dice;
    private Vector3 dice_pos;
   
    
    [SerializeField]
    private float cube_constant = 2f;

    public AnimationCurve anim;
    //[SerializeField]
    private float addition_number = 50000f;
    private float initial_add;
    //private float reduction_factor = 50f;

    [SerializeField]
    private LayerMask gndLayer;

    private RaycastHit hits;
    
    [SerializeField]
    private float redux_factor = 2f;
    
    
    [SerializeField]
    private float ratio_denom = 200f;
    public bool pause = false;
    private bool begin_descent = false;
    private bool end = false;


    private Vector3 pos_a;
    private Vector3 pos_b;
    private float num = 0f;

    [SerializeField]
    private bool checking;

    private int lastX = 0;
    private int lastZ = 0;

    private Vector3 orig_sky = Vector3.up;

    public cubeMovement cubeScript;

    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        dice = GameObject.FindGameObjectWithTag("Player");
        initial_add = addition_number;
    }

    // Update is called once per frame
    void Update()
    {
        checking = Physics.Raycast(dice_pos, new Vector3(lastX, 0, lastZ), out hits, 1000, gndLayer);
        dice_pos = dice.transform.position;
        //Debug.DrawLine(dice_pos, new Vector3(lastX*1000, dice_pos.y, lastZ*1000));
        //Debug.DrawRay(dice_pos, new Vector3(lastX*1000,0, lastZ*1000),Color.green,10000);

        if (begin_descent == true)
        {
            /*if ((lastX == 1 && cubeScript.isRight) || (lastX == -1 && cubeScript.isLeft) || (lastZ == 1 && cubeScript.isFront) || (lastZ == -1 && cubeScript.isBack))
            {
                begin_descent = false;
                addition_number = initial_add;

                pause = false;
            }*/

            dice.transform.position = Vector3.Lerp(pos_a, pos_b, Mathf.Min(num / ratio_denom, 1));

            if (num / ratio_denom >= 1 || addition_number <= 0)
            {
                begin_descent = false;
                pause = false;
                cubeScript.isMoving = false;
            }
            num = num + addition_number*Time.deltaTime;
            //addition_number = (addition_number) / redux_factor;
        }
        else
        {
            addition_number = initial_add;
            num = 0;

        }

    }

    public void slide(int x, int z, int roll)
    {
        lastX = x;
        lastZ = z;
        checking = Physics.Raycast(dice_pos, new Vector3(lastX, 0, lastZ), out hits, 1000, gndLayer);
        cubeScript.isMoving = true;
        if (hits.distance-1<cube_constant*roll && checking)
        {
            Vector3  a= hits.point;
            pos_b = new Vector3(a.x-x, dice_pos.y,a.z-z);
            Debug.Log(a.x);
            Debug.Log(a.z);
            //Debug.DrawLine(dice_pos,hits.point);
        }
        else
        {
            pos_b = new Vector3(dice_pos.x + (cube_constant * x * roll), dice_pos.y, dice_pos.z + (cube_constant * z * roll));
            Debug.Log("Here");
        }
        pos_a = dice_pos;
        //pos_b = new Vector3(dice_pos.x + (cube_constant * x*roll), dice_pos.y, dice_pos.z + (cube_constant * z*roll));
        pause = true;
        begin_descent = true;
    
    }



    /*
    private void //FixedUpdate()
    {
        dice_pos = dice.transform.position;

        if (begin_descent == true)
        {
            if ((lastX == 1 && cubeScript.isRight) || (lastX == -1 && cubeScript.isLeft) || (lastZ == 1 && cubeScript.isFront) || (lastZ == -1 && cubeScript.isBack))
            {
                begin_descent = false;
                addition_number = initial_add;

                pause = false;
            }

            dice.transform.position = Vector3.Lerp(pos_a, pos_b, Mathf.Min(num / ratio_denom, 1));

            if (num / ratio_denom >= 1 || addition_number <= 0)
            {
                begin_descent = false;
                pause = false;
            }
            num = num + addition_number;
            //addition_number = (addition_number) / redux_factor;
        }
        else
        {
            addition_number = initial_add;
            num = 0;

        }
    }
    */
}
