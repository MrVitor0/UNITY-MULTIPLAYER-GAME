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

  
        //Bool variables
        private bool isRunning = false;
        private bool isJumping;


      
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
            @brief This method is used to change isMoving state;
       */
       public void checkRunning()
         {
            Debug.Log(this.speed);
            //check if any horizontal or vertical input is pressed
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {   
                    if (Input.GetKeyDown(KeyCode.LeftShift) ){
                        this.isRunning = true;
                        this.speed = this.speed * 1.5f;
                        this.animator.SetBool("isRunning", true);
                    }
                    if (Input.GetKeyUp(KeyCode.LeftShift)){
                        this.isRunning = false;
                        this.speed = this.speed / 1.5f;
                        this.animator.SetBool("isRunning", false);
                    }
            }
            else
            {
                //if player is running, play idle animation
                if (this.isRunning)
                {
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

