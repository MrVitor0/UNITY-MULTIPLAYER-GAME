using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public GameObject Sword;
    public GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Attack(){
        Sword.SetActive(true);
         //check if shoot is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
                animator.SetTrigger( "hasAttack" );
        }
    }


    // Update is called once per frame
    void Update()
    {

        this.Attack();

    }
}
