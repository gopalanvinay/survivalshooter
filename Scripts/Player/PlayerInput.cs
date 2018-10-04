using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public FixedJoystick LeftJoystick;
    protected PlayerMovement playerMovement;
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();	
	}
	
	// Update is called once per frame
	void Update () {
        
        playerMovement.hInput = LeftJoystick.inputVector.x;
        playerMovement.vInput = LeftJoystick.inputVector.y;
	}
}
