using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.Input;

namespace StandardAssets.Characters.CharacterInput
{
	public class NewCharacterInput : MonoBehaviour, ICharacterInput
	{
		public bool hasJumpInput { get; private set; }
		public event Action jumpPressed;
		
		public bool hasMovementInput 
		{ 
			get { return moveInput != Vector2.zero; }
		}

		
//The backing field of the moveInput property
		protected Vector2 moveInputVector;
		
		//The backing field of the lookInput property
		protected Vector2 lookInputVector;

		/// <inheritdoc />
		public Vector2 lookInput
		{
			get { return lookInputVector; }
			set { lookInputVector = value; }
		}

		/// <inheritdoc />
		public Vector2 moveInput
		{
			get { return moveInputVector; }
			set { moveInputVector = value; }
		}

		public Controls controls;


		private void Awake()
		{
			controls.Movement.move.performed += ctx => moveInputVector = ctx.ReadValue<Vector2>();
			controls.Movement.look.performed += ctx => lookInputVector = ctx.ReadValue<Vector2>();

			controls.Movement.jump.performed += Jump;
		}

		private void OnEnable()
		{
			controls.Enable();
			CinemachineCore.GetInputAxis = LookInputOverride;
			
		}

		private void OnDisable()
		{
			controls.Disable();
			
		}
		
	




		/// <summary>
		/// Sets the Cinemachine cam POV to mouse inputs.
		/// </summary>
		private float LookInputOverride(string axis)
		{
			if (axis == "Horizontal")
			{
				return -lookInput.x;
			}

			if (axis == "Vertical")
			{
				
				return -lookInput.y;
			}

			return 0;
		}

		private void Jump(InputAction.CallbackContext ctx)
		{
			if (jumpPressed != null)
			{
				jumpPressed();
			}	
		}
	}
}