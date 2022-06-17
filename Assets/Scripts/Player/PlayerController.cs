
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class uses
using PlayerComponents;

public class PlayerController : MonoBehaviour
{
    //GameObjects used in the script
    public GameObject currentPlayer;
    public GameObject camera;
    public Animator animator;
   
    //Sensivity of the mouse
    [Range(0f, 10f)]
    public float HorizontalLookSensitivity = 1;
    [Range(0f, 10f)]
    public float verticalLookSensitivity = 1;
    
    //Float/Integers variables
    [SerializeField]
    private float speed;
    [Range(0f, 10f)]
    public float jumpForce;

    //Class Type variables
    private PlayerStatistics playerStatistics;

    private void Start()
    {
        //change value of this.playerStatistics to this.currentPlayer
        this.playerStatistics = new PlayerStatistics(
        this.currentPlayer, 
        this.animator,
        this.camera,
        this.speed, 
        this.jumpForce,
        this.HorizontalLookSensitivity,
        this.verticalLookSensitivity
        );
    }

    /**
        @author Vitor Hugo
        @version 1.0
        @brief This class is used to move player and other objects;
    */
    void Update()
    {   
        //Rotate Player and Camera
        this.playerStatistics.RotatePlayerWithMousePosition();
        //Check if player is trying to jump
        StartCoroutine(this.playerStatistics.CheckJump());
        //Check if player is trying to run
        this.playerStatistics.checkRunning();
        //Move Player
        this.playerStatistics.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
