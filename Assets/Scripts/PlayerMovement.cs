using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	public static bool crouch = false;

	// Update is called once per frame
	void Update() {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		//Debug.Log(Input.GetAxisRaw("Horizontal"));

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
			if (CharacterController2D.isHurt)
            {
				CharacterController2D.isHurt = false;
            }
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
			if (CharacterController2D.isHurt)
			{
				CharacterController2D.isHurt = false;
			}
		} 
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
			if (CharacterController2D.isHurt)
			{
				CharacterController2D.isHurt = false;
			}
		}

		if (controller.isJumping)
		{
			animator.SetBool("IsJumping", true);
			animator.SetBool("IsFalling", false);
			if (CharacterController2D.isHurt)
			{
				CharacterController2D.isHurt = false;
			}
		}

		if (controller.isFalling)
		{
			animator.SetBool("IsFalling", true);
			animator.SetBool("IsJumping", false);

		}

		if (controller.m_Grounded)
		{
			animator.SetBool("IsFalling", false);
			//animator.SetBool("IsJumping", false);
		}

		if (CharacterController2D.isHurt)
		{
			//animator.SetBool("IsHurt", true);
			animator.Play("Player_Hurt");
			
			
		}

		if (Input.GetAxisRaw("Horizontal") != 0)
        {
			CharacterController2D.isHurt = false;
        }

		if (!CharacterController2D.isHurt)
        {
			animator.SetBool("IsHurt", false);
        }

		



	} 


	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
		animator.SetBool("IsFalling", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		if (controller.m_Grounded)
        {
			controller.isJumping = false;
			
        }

		controller.Void();

		jump = false;

		

	}
}
