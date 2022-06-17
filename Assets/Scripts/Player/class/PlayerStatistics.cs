using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerStatistics : MonoBehaviour 
    {

        //GameObjects used in the script
        public GameObject player;
        public GameObject camera;
        public GameObject groundChecker;
        public GameObject Sword;

        public Animator animator;
        //Sensivity of the mouse
        [Range(0f, 10f)]
        public float HorizontalLookSensitivity = 1;
        [Range(0f, 10f)]
        public float verticalLookSensitivity = 1;

        

        //Float/Integers variables
        [Range(0f, 10f)]
        public float speed;
        [Range(0f, 10f)]
        public  float force;
        [Range(0f, 10f)]
        public  float JumpMultiplier;
        //Bool variables
        private bool isRunning = false;
        private bool isGround;
 
        public  bool AllowDoubleJump = false;
        private int jumpCount = 0;
        private bool isJumping;

        //method to check if player object is tounch the ground with tag "Ground"
        public bool IsGrounded()
        {       
            this.isGround = Physics.Raycast(groundChecker.transform.position, Vector3.down, 0.1f);
            if(this.isGround){
                this.jumpCount = 0;
                this.isJumping = false;
            }
            return this.isGround;
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

       /**
            @author Vitor Hugo
            @version 1.0
            @brief This method is used to change hasJump state;
       */
        public void CheckJump()
        {
            bool isGrounded = this.IsGrounded();
            //jump player if press space    
            if ( Input.GetKeyDown(KeyCode.Space) && isGrounded ){
                 this.isJumping = true;
                 this.isRunning = false;
                 this.animator.SetTrigger("hasJump");
                //  yield return new WaitForSeconds(0.5f);
                 this.player.GetComponent<Rigidbody>().AddForce(Vector3.up * (this.speed * this.JumpMultiplier), ForceMode.Impulse);
              
            }
            else if ( Input.GetKeyDown(KeyCode.Space) && !isGrounded && this.AllowDoubleJump){
                if( this.jumpCount < 1){
                    this.isJumping = true;
                    this.isRunning = false;
                    //bool DoubleJump = true;
                    this.animator.SetTrigger("DoubleJump");
                    //  yield return new WaitForSeconds(0.5f);
                    this.player.GetComponent<Rigidbody>().AddForce(Vector3.up * (this.speed * this.JumpMultiplier), ForceMode.Impulse);
                    this.jumpCount++;
                }
            }
        }
        /**
            @author Vitor Hugo
            @version 1.0
            @brief This method is used to change isMoving state;
       */
       public void checkRunning()
         {
            //check if any horizontal or vertical input is pressed
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {       
                    if (Input.GetKeyDown(KeyCode.LeftShift) && this.isGround && !this.isJumping ){
                        //hide sword if player is running
                        this.Sword.SetActive(false);
                        this.isRunning = true;
                        this.speed = this.speed * this.force;
                        this.animator.SetBool("isRunning", true);
                    }
                    if (Input.GetKeyUp(KeyCode.LeftShift)){
                        this.isRunning = false;
                        this.Sword.SetActive(true);
                        this.speed = this.speed / this.force;
                        if(this.speed < 1){
                            this.speed = 1;
                        }
                        this.animator.SetBool("isRunning", false);
                    }
            }
            else
            {
                //if player is running, play idle animation
                if (this.isRunning)
                {   
                    this.Sword.SetActive(true);
                    this.animator.SetBool("isRunning", false);
                    this.isRunning = false;
                }
            }
         }
        /**
            @author Vitor Hugo
            @version 1.0
            @brief This method is used to move player;
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
        }
    }
}

