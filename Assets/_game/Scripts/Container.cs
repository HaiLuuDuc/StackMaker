using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Container : MonoBehaviour
{
    [SerializeField] MenuManager MenuMa;
    [SerializeField] GameObject player;
    [SerializeField] GameObject eatenStacks;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] GameObject raycastStart;
    [SerializeField] float raycastLength;
    [SerializeField] GameObject gameManager;
    [SerializeField] Text score;


    private float speed = 7f;
    private float stackHeight = 0.2f;
    private float triangleDelayTime = 0.2f;
    private bool isLose = false;
    private bool isFacingWall = false;
    private Vector3 playerOldPosition;
    private Vector3 targetSpot;
    private Vector3 directionVector;
    private Vector3 firstMousePosition;
    private Vector3 lastMousePosition;
    private Vector3 mouseDirection;


    public bool onBridge=false;
    public Vector3 offset;


    enum Direction { Forward, Back, Left, Right }
    Direction direction;
    void Start()
    {
        MenuMa.HideLose();
        gameManager.GetComponent<MenuManager>().HideMenu();
        Time.timeScale = 1f;
        raycastStart.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        playerOldPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLose) return;

        score.text = eatenStacks.transform.childCount.ToString();
        isFacingWall = CheckFacingWall();
        CheckWallFront(); // ban raycast de xac dinh targetSpot
        if (isFacingWall) // khi nao up mat vao tuong moi duoc di chuyen tiep
        {
            //mouse control
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                lastMousePosition = Input.mousePosition;
                mouseDirection = lastMousePosition - firstMousePosition;
                if(mouseDirection.x>0 && Mathf.Abs(mouseDirection.x)> Mathf.Abs(mouseDirection.y))
                {
                    TurnRight();
                }
                else if (mouseDirection.x < 0 && Mathf.Abs(mouseDirection.x) > Mathf.Abs(mouseDirection.y))
                {
                    TurnLeft();
                }
                else if (mouseDirection.y > 0 && Mathf.Abs(mouseDirection.y) > Mathf.Abs(mouseDirection.x))
                {
                    TurnForward();
                }
                else if (mouseDirection.y < 0 && Mathf.Abs(mouseDirection.y) > Mathf.Abs(mouseDirection.x))
                {
                    TurnBack();
                }
            }
            /*if (Input.GetKeyDown(KeyCode.W))
            {
                TurnForward();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                TurnLeft();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TurnBack();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                TurnRight();
            }*/
        }
       

        if (isFacingWall)
        {
            StopMoving();
        }
        else
        {
            Move();
        }

        // neu di tren cau ma het stack thi dead
        if (onBridge && eatenStacks.transform.childCount == 0)
        {
            isLose = true;
            MenuMa.ShowLose();
            
        }
  
    }

    public void btn_replay()
    {
        MenuMa.Replay();
    }

    // rotate raycastStart
    void TurnForward()
    {
        raycastStart.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        direction = Direction.Forward;
    }
    void TurnBack()
    {
        raycastStart.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        direction = Direction.Back;
    }
    void TurnLeft()
    {
        raycastStart.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        direction = Direction.Left;
    }
    void TurnRight()
    {
        raycastStart.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        direction = Direction.Right;
    }



    private void StopMoving()
    {
        
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetSpot, speed * Time.deltaTime);
    }

    // check up mat vao tuong
    bool CheckFacingWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastStart.transform.position, raycastStart.transform.forward, out hit, raycastLength, wallLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ban raycast de xac dinh targetSpot
    bool CheckWallFront()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastStart.transform.position, raycastStart.transform.forward, out hit, 300f, wallLayer))
        {
            directionVector = (hit.transform.position - transform.position).normalized;
            directionVector.y = 0;
            directionVector = beautifyVector(directionVector);
            targetSpot = new Vector3(hit.transform.position.x - directionVector.x,transform.position.y, hit.transform.position.z - directionVector.z);
            return true;
        }
        else
        {
            return false;
        }
    }

    // chuan hoa vector
    Vector3 beautifyVector(Vector3 vector)
    {
        if (Mathf.Abs(vector.x) > 0.6f)
        {
            vector.x = 1f;
        }
        else
        {
            vector.x = 0f;
        }
        if (Mathf.Abs(vector.z) > 0.6f)
        {
            vector.z = 1f;
        }
        else
        {
            vector.z = 0f;

        }
        return vector;
    }

    // nang player
    void MovePlayerUp()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + stackHeight, player.transform.position.z);
    }
    // ha player
    void MovePlayerDown()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - stackHeight, player.transform.position.z);

    }

    // tha stack da an duoc
    public void RemoveStack()
    {
        Destroy(eatenStacks.transform.GetChild(eatenStacks.transform.childCount - 1).gameObject);
        MovePlayerDown();
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "chestZoneTrigger")
        {
            onBridge = false;
        }

        if(other.tag == "stack")
        {
            GameObject stack = other.gameObject;
            stack.transform.position = player.transform.position+offset;
            MovePlayerUp();
            stack.transform.SetParent(eatenStacks.transform);
        }

        //xu ly triangle
        {
            if (other.tag == "triangle69")
            {
                if (direction == Direction.Right)
                {
                    Invoke(nameof(TurnBack), triangleDelayTime);
                }
                else if (direction == Direction.Forward)
                {
                    Invoke(nameof(TurnLeft), triangleDelayTime);
                }
            }
            else if (other.tag == "triangle90")
            {
                if (direction == Direction.Right)
                {
                    Invoke(nameof(TurnForward), triangleDelayTime);
                }
                else if (direction == Direction.Back)
                {
                    Invoke(nameof(TurnLeft), triangleDelayTime);
                }
            }
            else if (other.tag == "triangle36")
            {
                if (direction == Direction.Left)
                {
                    Invoke(nameof(TurnBack), triangleDelayTime);
                }
                else if (direction == Direction.Forward)
                {
                    Invoke(nameof(TurnRight), triangleDelayTime);
                }
            }
            else if (other.tag == "triangle03")
            {
                if (direction == Direction.Left)
                {
                    Invoke(nameof(TurnForward), triangleDelayTime);
                }
                else if (direction == Direction.Back)
                {
                    Invoke(nameof(TurnRight), triangleDelayTime);
                }
            }
        }
        
    }


}
