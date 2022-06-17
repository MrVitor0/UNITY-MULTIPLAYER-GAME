

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

        private float jumpForce;


        public PlayerStatistics(
        GameObject player, 
        Animator animator,
        GameObject camera,
        float speed, 
        float jumpForce,
        float HorizontalLookSensitivity,
        float verticalLookSensitivity)
        {   
            this.player = player;
            this.animator = animator;
            this.camera = camera;

            this.speed = speed;
            this.jumpForce = jumpForce;
            
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
            //calcula qual será o ângulo da câmera após o movimento do mouse Y
            float angle = this.camera.transform.eulerAngles.x - Input.GetAxis("Mouse Y") * this.verticalLookSensitivity;
            if(angle < 0 ||  angle > 90) return;
            //Rotate Player
            this.player.transform.Rotate(0, Input.GetAxis("Mouse X") * this.HorizontalLookSensitivity , 0);
            //verify if xRotation is between 0 and 90, se sim, rotate camera
            this.camera.transform.Rotate(-Input.GetAxis("Mouse Y") * this.verticalLookSensitivity, 0, 0);
        }

        //coroutine to wait for animation to finish
        public IEnumerator CheckJump()
        {
              //jump player if press space
            if (Input.GetKeyDown(KeyCode.Space)){
                this.animator.SetTrigger("hasJump");
                 yield return new WaitForSeconds(0.5f);
                 this.player.GetComponent<Rigidbody>().AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);
            }
            
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
            //não deixar o player "deslizar" (ESTÁ BUGANDO O PULO, CORRIGIR ANTES DE DESCOMENTAR)
            // this.player.GetComponent<Rigidbody>().velocity = new Vector3(
            //     Mathf.Clamp(this.player.GetComponent<Rigidbody>().velocity.x, -this.speed, this.speed),
            //     0,
            //     Mathf.Clamp(this.player.GetComponent<Rigidbody>().velocity.z, -this.speed, this.speed)
            // );

        }


    }
}

