using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{

    private PlayerMovement thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        
    }

    public void LeftArrow()
    {
        thePlayer.Move(-1);
    }

     public void RightArrow()
    {
        thePlayer.Move(1);

    }

    public void UnpressedArrow()
    {
        thePlayer.Move(0);

    }

    public void PickAxe_Attack()
    {
        thePlayer.PickAxe();

    }

    public void JumpButton()
    {
        thePlayer.Jump();

    }
}
