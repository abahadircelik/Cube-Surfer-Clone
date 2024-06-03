using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    private String _fallStr;
    private String _celebrateStr;

    // Start is called before the first frame update
    void Start()
    {
        _fallStr = "isFalling";
        _celebrateStr = "isCelebrating";
    }

    // Update is called once per frame
    void Update()
    {
        bool isFalling = animator.GetBool(_fallStr);
        
        if (!isFalling)
        {
            animator.SetBool(_fallStr, false) ;
        }
    }
    
    public void StartFallingAnimation()
    {
        animator.SetBool(_fallStr, true);
    }
    

    public void FinishFallingAnimation()
    {
        animator.SetBool(_fallStr, false);
    }

    public void StartCelebrationAnimation()
    {
        animator.SetBool(_celebrateStr, true);
    }

}
