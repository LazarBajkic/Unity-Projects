using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;

    public float speed=30f;

    [SerializeField] private float attackDamage=10f;
    [SerializeField] private float attackSpeed=1f;
    private float CanAttack;
    bool SlimeMove=true;

    private Transform target;

    private void Update()
    {
        if(target != null){
            float step=speed * Time.deltaTime;
            transform.position=Vector2.MoveTowards(transform.position,target.position,step);
            animator.SetBool("MovementState", true);
        }else{
            animator.SetBool("MovementState", false);
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
     if(other.gameObject.tag == "Player"){
       if(attackSpeed <= CanAttack){
        
        CanAttack=0f;
       }else{
        CanAttack +=Time.deltaTime;
       }
     }   
     }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            target=other.transform;
            animator.SetBool("MovementState", true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.tag == "Player"){
            target=null;
            animator.SetBool("MovementState", false);

        }
        
    }

   public float Health{
        set {
            health = value;
            
            if(health <= 0){
                Defeated();
            }
        }
        get {
            return health;
        }
   }

    public float health=1;

    void Start()
    {
        animator=GetComponent<Animator>();
    }

    public void Defeated(){
         animator.SetTrigger("Defeated");
    }
    
    public void RemoveEnemy(){
        Destroy(gameObject);
    }
   
}

