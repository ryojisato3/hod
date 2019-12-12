using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour {
	
    [SerializeField]
    public GameObject model;
    [SerializeField]
    Animator animator;
    [SerializeField]
    public Rigidbody trans;
    [SerializeField]
    public GameObject hud;
    
    public void Initialize()
    {
	    animator = model.GetComponent<Animator>();
    }
    
    public void Wait()
    {
	    animator.Play("Wait");
    }
    
    public void Run(bool is_run)
    {
	    animator.SetBool("is_run", is_run);
    }
    
    public void Attack()
    {
	    GetComponent<Animator>().SetTrigger("attack");
    }

    public void SetPosition(Vector3 position)
    {
	    model.transform.position = position;
    }
    
    public void SetAngles(Vector3 angles)
    {
	    model.transform.eulerAngles = angles;
    }
    public Rigidbody GetTransForm()
    {
	    return trans;
    }
        
    public void UpdateHud(int per)
    {
	    HudView hudView = hud.GetComponent<HudView>();
	    hudView.updateHealth(per);
    }
}
