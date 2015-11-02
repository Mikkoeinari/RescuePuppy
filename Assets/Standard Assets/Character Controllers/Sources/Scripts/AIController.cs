using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIController:MonoBehaviour
{
 
	public float speed;  // well...the speed
	public CharacterController controller;
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip scratchAnimation;
	public AnimationClip meowAnimation;
	public AnimationClip sitAnimation;
	public float minDist = 1;
	public float minPlayerDistance = 1;
	public float resumeDistance = 3;
	public GameObject player;
	public GameObject NPC;
	private GameObject interestingObject;
	private Animation _animation;
	private int objectReached = 1;
	private GameObject[] interestings;
	private Collider coll;
	private int finishedActivity = 1;
	private int escaped = 1;
	private float affection = 0;
	private float chaseTime = 0;
	private float incidentTime = 0;
	private Attributes attributes;
	private float ticks;
	private bool busy = false;

	void Start ()
	{
		controller = GetComponent<CharacterController> ();
		_animation = GetComponent<Animation> ();
		interestings = (GameObject.FindGameObjectsWithTag ("Interesting"));
		player = GameObject.FindGameObjectWithTag ("Player");
		incidentTime = Time.time;
		attributes = GetComponent<Attributes> ();
		ticks = Time.time;
	}
	void Update ()
	{
		//transform.LookAt(GameObject.FindWithTag("Player").transform);  // the NPC looks at the player
		//updateAffection();
		setSpeed (attributes.getEnergy ());
		if (Time.time - ticks > 10) {
			print (Time.time);
			attributes.setHunger (attributes.getHunger () - 0.1f);
			ticks = Time.time;
		}
		if (attributes.getEnergy () < 0.5f && !busy) {
			StartCoroutine (rest ());
		}
		Vector3 playerPos = player.transform.localPosition;
		float playerDistance = Vector3.Distance (player.transform.localPosition, transform.localPosition);
		if (playerDistance < minPlayerDistance || escaped == 0) {
			busy = false;
			escaped = 0;
			playerPos.y = transform.localPosition.y;
			transform.LookAt (2 * transform.localPosition - playerPos);
			runFromPlayer ();
			if (playerDistance > resumeDistance) {
				escaped = 1;
				attributes.setEnergy (attributes.getEnergy () - 0.1f);
				attributes.setHunger (attributes.getHunger () - 0.1f);
				transform.LookAt (playerPos);
				StartCoroutine (assess ());
				return;
			}
		}
		if (!busy && escaped == 1) {
			if (objectReached == 1) {
				int rand = Random.Range (0, interestings.Length);
				interestingObject = interestings [rand];
				coll = interestingObject.GetComponent<Collider> ();
				objectReached = 0;
			}
			Vector3 target = interestingObject.transform.localPosition;
			target.y = transform.localPosition.y;
			transform.LookAt (target);
			Vector3 rayDirection = coll.ClosestPointOnBounds (transform.localPosition) - transform.localPosition;
			rayDirection.y = 0;
			bool closeToObject = rayDirection.sqrMagnitude < minDist;
			if (!closeToObject && objectReached == 0) {
				moveTowardObject ();	
			} else {
				StartCoroutine (activity ());
				attributes.setMorale (attributes.getMorale () + 0.1f);
				objectReached = 1;
			}    
		}   
	}
	public void updateAffection ()
	{
		if (escaped == 0 && chaseTime == 0) {
			print ("case1");
			affection = affection + Time.time - incidentTime;
			chaseTime = Time.time;
			minPlayerDistance = minPlayerDistance - affection;
			print (minPlayerDistance);
		}
		if (escaped == 1 && chaseTime > 0) {
			print ("case2");
			affection = affection - (Time.time - chaseTime);
			chaseTime = 0;
			minPlayerDistance = minPlayerDistance - affection;
			print (minPlayerDistance);
			incidentTime = Time.time;
		} else if (escaped == 1 && chaseTime == 0) {
			print ("case3");
			if (Time.time - incidentTime > 10) {
				minPlayerDistance = minPlayerDistance / 2;
				print (minPlayerDistance);
			}
		} 		
	}
	public void moveTowardObject ()
	{
		controller.SimpleMove (speed * transform.forward);
		_animation.CrossFade (walkAnimation.name);
	}
 	
	public void runFromPlayer ()
	{
		controller.SimpleMove (speed * 2 * transform.forward);
		_animation.CrossFade (runAnimation.name);
 	
	}
	IEnumerator assess ()
	{
		while (busy) {
			yield return new WaitForEndOfFrame ();
		}
		busy = true;
		_animation.CrossFade (idleAnimation.name);
		yield return new WaitForSeconds (3);
		_animation.CrossFade (meowAnimation.name);
		yield return new WaitForSeconds (2);
		busy = false;
	}
	IEnumerator rest ()
	{
		while (busy) {
			yield return new WaitForEndOfFrame ();
		}
		busy = true;
		_animation.CrossFade (sitAnimation.name);
		while (attributes.getEnergy()<1.5f) {
			if (busy == false) {
				return true;
			}
			yield return new WaitForSeconds (5);
			attributes.setEnergy (attributes.getEnergy () + 0.1f);
		}
		busy = false;
	}
	IEnumerator activity ()
	{
		while (busy) {
			yield return new WaitForEndOfFrame ();
		}
		busy = true;
		int rand = Random.Range (1, 10);
		_animation.CrossFade (idleAnimation.name);
		yield return new WaitForSeconds (rand);
		busy = false;
	}
	public void setSpeed (float newSpeed)
	{
		speed = newSpeed;
	}
	public void updateBravery (float newBravery)
	{
 
	}
}
 