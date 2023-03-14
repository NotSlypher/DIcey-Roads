using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    
   
    public float rollSpeed = 3f;
    public bool isMoving = false;
    
    int xDir, zDir;

    
    public Player_slide playerSlideScript;
   // public Dice_roll diceRollScript;
    public CameraMovenemt cameraScript;
    public Transform ray_origin;
    public LayerMask groundLayer;
    public LayerMask despawnLayer;


    public float gravity;
    
    public bool followCamera;
    [SerializeField]
    bool isGrounded;
    
    public bool isLeft;
   
    public bool isRight;
    public bool isFront;
    
    public  bool isBack;

    public bool isDespawn;
    bool isTopLeft;
    bool isTopRight;
    bool isTopFront;
    bool isTopBack;



    public float length = 1.5f;

    bool keyInput;
   
    public int diceRoll;
    //  private bool[] rays = new bool[6];
    private RaycastHit[] rays = new RaycastHit[6];
    private KeyCode[] InputMappingForward = {KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D};
    private KeyCode[] InputMappingBackward = { KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A };
    private KeyCode[] InputMappingLeft = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
    private KeyCode[] InputMappingRight = { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S };

    private void Awake()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovenemt>();
    }
    private void Start()
    {
       
    }
    void Update()
    {
        
        Physics.Raycast(ray_origin.position, ray_origin.transform.forward,out rays[0] ,5);
        Physics.Raycast(ray_origin.position, ray_origin.transform.right, out rays[1], 5);
        Physics.Raycast(ray_origin.position, ray_origin.transform.up, out rays[2], 5);
        Physics.Raycast(ray_origin.position, -ray_origin.transform.up, out rays[3], 5);
        Physics.Raycast(ray_origin.position, -ray_origin.transform.right, out rays[4], 5);
        Physics.Raycast(ray_origin.position, -ray_origin.transform.forward, out rays[5], 5);
        isDespawn = Physics.Raycast(ray_origin.position, Vector3.down, 1.5f, despawnLayer);
        for(int i=0;i<6;i++)
        {
            if((rays[i].normal == Vector3.up))
            {
                diceRoll = i + 1;
            }
        }
        followCamera = isGrounded;

        isGrounded = Physics.Raycast(ray_origin.position,Vector3.down,length, groundLayer);
        
        isLeft = Physics.Raycast(ray_origin.position, Vector3.left, 1.5f, groundLayer);
        isRight = Physics.Raycast(ray_origin.position, Vector3.right, 1.5f, groundLayer);
        isFront = Physics.Raycast(ray_origin.position, Vector3.forward, 1.5f, groundLayer);
        isBack = Physics.Raycast(ray_origin.position, Vector3.back, 1.5f, groundLayer);

        isTopLeft = Physics.Raycast(ray_origin.position+new Vector3(0,2,0), Vector3.left, 1.5f, groundLayer);
        isTopRight = Physics.Raycast(ray_origin.position + new Vector3(0, 2, 0), Vector3.right, 1.5f, groundLayer);
        isTopFront = Physics.Raycast(ray_origin.position + new Vector3(0, 2, 0), Vector3.forward, 1.5f, groundLayer);
        isTopBack = Physics.Raycast(ray_origin.position + new Vector3(0, 2, 0), Vector3.back, 1.5f, groundLayer);

       
        if (isMoving)
        {
            return;
        }
        if (!isMoving && !playerSlideScript.pause && isGrounded)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round( transform.position.y), Mathf.Round( transform.position.z));
        }

        if(isLeft && isRight)
        {
            Front();
            Back();
        }
        else if(isFront&&isBack)
        {
            Right();
            Left();
        }
        else
        {
            Front();
            Back();
            Right();
            Left();
        }

        
        
        if (!isGrounded && !isMoving)
        {
            gameObject.transform.position += new Vector3(0, -0.5f, 0);
        }


    }


    void Front()
    {
        if (Input.GetKeyDown(InputMappingForward[cameraScript.index]) && !keyInput && !isMoving && isGrounded)
        {
            keyInput = true;
            Invoke("makeInputFalse",0.8f);
            if (isFront)
            {
                if (isTopFront)
                {
                    var wallAnchor = transform.position + new Vector3(0, 1, 1);
                    var wallAxis = Vector3.Cross(Vector3.up, Vector3.forward);
                    xDir = 0;
                    zDir = 1;
                    StartCoroutine(roll(wallAnchor, wallAxis,0));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var wallAnchor = transform.position + new Vector3(0, 1, 1);
                        var wallAxis = Vector3.Cross(Vector3.up, Vector3.forward);
                        xDir = 0;
                        zDir = 1;
                        StartCoroutine(roll(wallAnchor, wallAxis,0));
                    }
                }
            }
            else
            {
                var anchor = transform.position + new Vector3(0f, -1f, 1f);
                var axis = Vector3.Cross(Vector3.up, Vector3.forward);
                xDir = 0;
                zDir = 1;
                StartCoroutine(roll(anchor, axis,1));
            }

        }
    }

    void Back()
    {
        if (Input.GetKeyDown(InputMappingBackward[cameraScript.index]) && !keyInput && !isMoving && isGrounded)
        {
            keyInput = true;
            Invoke("makeInputFalse", 0.8f);
            if (isBack)
            {
                if (isTopBack)
                {
                    var wallAnchor = transform.position + new Vector3(0, 1, -1);
                    var wallAxis = Vector3.Cross(Vector3.up, -Vector3.forward);
                    xDir = 0;
                    zDir = -1;
                    StartCoroutine(roll(wallAnchor, wallAxis,0));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var wallAnchor = transform.position + new Vector3(0, 1, -1);
                        var wallAxis = Vector3.Cross(Vector3.up, -Vector3.forward);
                        xDir = 0;
                        zDir = -1;
                        StartCoroutine(roll(wallAnchor, wallAxis,0));

                    }
                }

            }
            else
            {
                var anchor = transform.position + new Vector3(0f, -1f, -1f);
                var axis = Vector3.Cross(Vector3.up, -Vector3.forward);
                xDir = 0;
                zDir = -1;
                StartCoroutine(roll(anchor, axis,1));
            }

        }
    }

    void Right()
    {
        if (Input.GetKeyDown(InputMappingRight[cameraScript.index]) && !keyInput && !isMoving && isGrounded)
        {
            keyInput = true;
            Invoke("makeInputFalse", 0.8f);
            if (isRight)
            {
                if (isTopRight)
                {
                    var wallAnchor = transform.position + new Vector3(1, 1, 0);
                    var wallAxis = Vector3.Cross(Vector3.up, Vector3.right);
                    xDir = 1;
                    zDir = 0;
                    StartCoroutine(roll(wallAnchor, wallAxis,0));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var wallAnchor = transform.position + new Vector3(1, 1, 0);
                        var wallAxis = Vector3.Cross(Vector3.up, Vector3.right);
                        xDir = 1;
                        zDir = 0;
                        StartCoroutine(roll(wallAnchor, wallAxis,0));
                    }
                }

            }
            else
            {
                var anchor = transform.position + new Vector3(1f, -1f, 0);
                var axis = Vector3.Cross(Vector3.up, Vector3.right);
                xDir = 1;
                zDir = 0;
                StartCoroutine(roll(anchor, axis,1));
            }


        }

    }

    void Left()
    {

        if (Input.GetKeyDown(InputMappingLeft[cameraScript.index]) && !keyInput && !isMoving && isGrounded)
        {
            keyInput = true;
            Invoke("makeInputFalse", 0.8f);
            if (isLeft)
            {

                if (isTopLeft)
                {
                    var wallAnchor = transform.position + new Vector3(-1, 1, 0);
                    var wallAxis = Vector3.Cross(Vector3.up, Vector3.left);
                    xDir = -1;
                    zDir = 0;
                    StartCoroutine(roll(wallAnchor, wallAxis,0));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {

                        var wallAnchor = transform.position + new Vector3(-1, 1, 0);
                        var wallAxis = Vector3.Cross(Vector3.up, Vector3.left);
                        xDir = -1;
                        zDir = 0;
                        StartCoroutine(roll(wallAnchor, wallAxis,0));
                    }
                }

            }
            else
            {
                var anchor = transform.position + new Vector3(-1f, -1f, 0);
                var axis = Vector3.Cross(Vector3.up, Vector3.left);
                xDir = -1;
                zDir = 0;
                StartCoroutine(roll(anchor, axis,1));
            }
        }
    }
    IEnumerator roll(Vector3 anchor,Vector3 axis, int climbCheck)
    {
        isMoving = true;
        for(int i=0;i<(90/rollSpeed);i++)
        {
            transform.RotateAround(anchor,axis,rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        if(climbCheck == 1  && isGrounded)
        {
            playerSlideScript.slide(xDir, zDir, diceRoll);
        }
        audioManager.instance.Play("Dice_thud");
        
        
        isMoving = false;
        /*isBreak = true;*/
        
    }

    void makeInputFalse()
    {
        keyInput = false;
    }
    
   
}
