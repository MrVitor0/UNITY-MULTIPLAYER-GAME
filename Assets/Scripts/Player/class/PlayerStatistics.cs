
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerComponents
{

    public class PlayerStatistics : MonoBehaviour 
    {

        private float speed;
      
        private float HorizontalLookSensitivity = 1;
        private float verticalLookSensitivity = 1;

        private GameObject player;
        private GameObject camera;
        private Animator animator;

        public PlayerStatistics(
        GameObject player, 
        Animator animator,
        float speed, 
        GameObject camera,
        float HorizontalLookSensitivity,
        float verticalLookSensitivity)
        {   
            this.player = player;
            this.animator = animator;
            this.speed = speed;
            this.camera = camera;
            
            this.HorizontalLookSensitivity = HorizontalLookSensitivity;
            this.verticalLookSensitivity = verticalLookSensitivity;
        }


        /**
            @author Vitor Hugo
            @version 1.0
            @brief This class is used to rotate player and camera;
        */
        public void RotatePlayerWithMousePosition()
        {
           //Rotate Player
            this.player.transform.Rotate(0, Input.GetAxis("Mouse X") * this.HorizontalLookSensitivity , 0);
            //Rotate Camera
            this.camera.transform.Rotate(-Input.GetAxis("Mouse Y") * this.verticalLookSensitivity, 0, 0);
        }




        /**
        @author Vitor Hugo
        @version 1.0
        @brief This class is used to move player;
        */
        public void Move(float Horizontal = 0, float Vertical = 0)
        {   
            //Verifica se o player está querendo se movimentar
            if (Horizontal == 0 && Vertical == 0){
                 this.animator.SetBool("isWalking", false);
                 return;
            }; 
            //Initializa o vetor de movimentação         
            Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
            //Corrige a rotação do personagem
            movement = this.player.transform.rotation * movement;
            this.player.transform.position += movement * this.speed * Time.deltaTime;

            //ativa o animator
            this.animator.SetBool("isWalking", true);

            //Atualiza a Força adicionada no objeto
            this.player.GetComponent<Rigidbody>().AddForce(movement * this.speed);
            //não deixar o player "deslizar"
            this.player.GetComponent<Rigidbody>().velocity = new Vector3(
                Mathf.Clamp(this.player.GetComponent<Rigidbody>().velocity.x, -this.speed, this.speed),
                0,
                Mathf.Clamp(this.player.GetComponent<Rigidbody>().velocity.z, -this.speed, this.speed)
            );

        

        }


    }
}

