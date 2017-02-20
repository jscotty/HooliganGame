// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

public class CharacterStateManager : Singleton<CharacterStateManager> {

	private const string ANIM_STATE = "State"; // define state name

	[SerializeField] private Animator animator;
	private MoveState animMove;

	void Update(){
		if(Input.GetMouseButtonDown(0)) SetNewState(MoveState.AIM);
	}

	public void SetNewState(MoveState state){
		if(animator == null) return;
		animator.SetInteger(ANIM_STATE, (int) state);
	}
}

public enum MoveState{
	IDLE = 0,
	WALK_BACKWARD = 1,
	WALK_FORWARD = 2,
	AIM = 3,
}
