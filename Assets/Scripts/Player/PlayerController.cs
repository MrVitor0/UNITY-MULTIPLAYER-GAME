using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class uses
using PlayerComponents;

public class PlayerController : MonoBehaviour
{
    //GameObjects used in the script
    private PlayerStatistics playerStatistics;

    private void Start()
    {
      this.playerStatistics = this.GetComponent<PlayerStatistics>();
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
        // StartCoroutine(this.playerStatistics.CheckJump());
        this.playerStatistics.CheckJump();
        //Check if player is trying to run
        this.playerStatistics.checkRunning();
        //Move Player
        this.playerStatistics.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
