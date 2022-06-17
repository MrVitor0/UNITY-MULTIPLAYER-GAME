
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerComponents;

public class PlayerController : MonoBehaviour
{

    public GameObject currentPlayer;
    public GameObject camera;

    [SerializeField]
    private float speed;
    
    [Range(0f, 10f)]
    public float HorizontalLookSensitivity = 1;
     
    [Range(0f, 10f)]
    public float verticalLookSensitivity = 1;
    

    [SerializeField]
    private Animator animator;

    private PlayerStatistics playerStatistics;
    


    private void Start()
    {
        //change value of this.playerStatistics to this.currentPlayer
        this.playerStatistics = new PlayerStatistics(
        this.currentPlayer, 
        this.animator,
        this.speed, 
        this.camera,
        this.HorizontalLookSensitivity,
        this.verticalLookSensitivity
        );
    }


    /**
    @author Vitor Hugo
    @version 1.0
    @brief This class is used to move player;
    */
    void Update()
    {   
        //Rotate Player and Camera
        this.playerStatistics.RotatePlayerWithMousePosition();
        //Move Player
        this.playerStatistics.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       
    }
}
