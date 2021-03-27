using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    float speedFall = 1f;
    float fallTime ;
    public GameObject tetromino;
    int xSideTrigger = 0;
    private void Update() 
    {
        UserInput();    
    }
    private void Start() 
    {
        tetromino = RandomTetromino();    
    }
    public GameObject RandomTetromino()
    {
        string tetro = "Prefabs/Tetromino_E";
        int rnd = Random.Range(1,7);
        switch (rnd)
            {
                case 1:
                {
                    tetro = "Prefabs/Tetromino_E";
                    break;
                }
                case 2:
                {
                    tetro = "Prefabs/Tetromino_I";
                    break;
                }
                case 3:
                {
                    tetro = "Prefabs/Tetromino_J";
                    break;
                }
                case 4:
                {
                    tetro = "Prefabs/Tetromino_L";
                    break;
                }
                case 5:
                {
                    tetro = "Prefabs/Tetromino_O";
                    break;
                }
                case 6:
                {
                    tetro = "Tetromino_S";
                    break;
                }
                case 7:
                {
                    tetro = "Prefabs/Tetromino_Z";
                    break;
                }
            }
        return (GameObject)Instantiate(Resources.Load(tetro,typeof(GameObject)),new Vector2(6f,18f),Quaternion.identity);
        
    }
    void UserInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tetromino.transform.position += new Vector3 (-1,0,0);
            if(CheckIsInvalidPositionX())
            {
                tetromino.transform.position += new Vector3 (1,0,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            tetromino.transform.position += new Vector3 (1,0,0);
            if(CheckIsInvalidPositionX())
            {
                tetromino.transform.position += new Vector3 (-1,0,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fallTime >= speedFall)
        {
            tetromino.transform.position += new Vector3 (0,-1,0);
            fallTime = Time.time;
            if(CheckIsInvalidPositionY())
            {
                tetromino.transform.position += new Vector3 (0,1,0);
                tetromino = RandomTetromino();
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            tetromino.transform.Rotate(0,0,90);
            if(CheckIsInvalidPositionY())
            {
                tetromino.transform.Rotate(0,0,-90);
            }
            while(CheckIsInvalidPositionX() & xSideTrigger == -1)
            {
                tetromino.transform.position += new Vector3 (1,0,0);
            }
            while(CheckIsInvalidPositionX() & xSideTrigger == 1)
            {
                tetromino.transform.position += new Vector3 (-1,0,0);
            }
        }
    }
    bool CheckIsInvalidPositionX()
    {
        foreach(Transform child in tetromino.transform)
        {
            if(child.position.x <= 0 || child.position.x > 10)
            {
                if(child.position.x <= 0)
                {
                    xSideTrigger = -1;
                }
                else xSideTrigger = 1;
                return true;
            }
        }
        return false;
    }
    bool CheckIsInvalidPositionY()
    {
        foreach(Transform child in tetromino.transform)
        {
            if(child.position.y <= 0)
            {
                return true;
            }
        }
        return false;
    }    
}
