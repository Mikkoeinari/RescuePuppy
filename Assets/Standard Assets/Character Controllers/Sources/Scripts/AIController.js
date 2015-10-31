 #pragma strict
 
 var speed : float = 1;  // well...the speed
 var controller:CharacterController;
 public var idleAnimation : AnimationClip;
 public var walkAnimation : AnimationClip;
 public var runAnimation : AnimationClip;
 public var scratchAnimation : AnimationClip;
 public var meowAnimation : AnimationClip;
 public var minDist: float=1;
 public var minPlayerDistance:float=1;
 public var resumeDistance:float=3;
 public var player : GameObject;
 private var interestingObject: GameObject;
 private var _animation: Animation;
 private var objectReached:int=1;
 private var interestings : GameObject[];
 private var coll: Collider;
 private var finishedActivity:int=1;
 private var escaped: int=1;
 private var affection: float =0;
 private var chaseTime: float=0;
 private var incidentTime: float=0;
 
 function Start(){
     controller = gameObject.GetComponent(CharacterController);
     _animation=GetComponent(Animation);
     interestings=(GameObject.FindGameObjectsWithTag("Interesting"));
     player=GameObject.FindGameObjectWithTag("Player");
     incidentTime=Time.time;

 }
 function Update(){
     //transform.LookAt(GameObject.FindWithTag("Player").transform);  // the NPC looks at the player
     //updateAffection();
     
     var playerPos: Vector3=player.transform.localPosition;
     var playerDistance: float=Vector3.Distance(player.transform.localPosition, transform.localPosition);
     if (playerDistance<minPlayerDistance || !escaped){
     	escaped=0;
     	playerPos.y=transform.localPosition.y;
     	transform.LookAt(2*transform.localPosition-playerPos);
     	runFromPlayer();
     	if (playerDistance>resumeDistance){
     		escaped=1;
     		transform.LookAt(playerPos);
     		assess();
     	}
     	}
     if (finishedActivity==1 && escaped){
	    if (objectReached==1){
	     	var rand: int=Random.Range(0,interestings.Length);
	     	interestingObject=interestings[rand];
	     	coll= interestingObject.GetComponent.<Collider>();
	     	objectReached=0;
	    }
     	var target: Vector3=interestingObject.transform.localPosition;
     	target.y=transform.localPosition.y;
     	transform.LookAt(target);
     	var rayDirection : Vector3 = coll.ClosestPointOnBounds(transform.localPosition) - transform.localPosition;
     	rayDirection.y=0;
     	var closeToObject =  rayDirection.sqrMagnitude < minDist;
     	if(!closeToObject && objectReached==0){
     		moveTowardObject();	
     	}
     	else{
     		activity();
     		objectReached=1;
     	}    
	}   
 }
 function updateAffection(){
 	if (!escaped && chaseTime==0){
 		print("case1");
 		affection=affection+Time.time-incidentTime;
 		chaseTime=Time.time;
 		minPlayerDistance=minPlayerDistance-affection;
 		print(minPlayerDistance);
 		}
 	if (escaped && chaseTime>0){
 		print("case2");
 		affection=affection-(Time.time-chaseTime);
 		chaseTime=0;
 		minPlayerDistance=minPlayerDistance-affection;
 		print(minPlayerDistance);
 		incidentTime=Time.time;
 	}
 	else if (escaped && chaseTime==0){
 		print("case3");
 		if (Time.time-incidentTime>10){
 		minPlayerDistance=minPlayerDistance/2;
 		print(minPlayerDistance);
 		}
 	}
 		
 }
 function moveTowardObject(){
 	controller.SimpleMove(speed*transform.forward);
 	_animation.CrossFade(walkAnimation.name);
 	}
 	
 function runFromPlayer(){
 	controller.SimpleMove(speed*2*transform.forward);
 	_animation.CrossFade(runAnimation.name);
 	
 	}
 function assess(){
 	finishedActivity=0;
 	_animation.CrossFade(idleAnimation.name);
 	yield WaitForSeconds(3);
 	_animation.CrossFade(meowAnimation.name);
 	yield WaitForSeconds(2);
 	finishedActivity=1;
 	}
 	
 function activity() {
	finishedActivity=0;
	var currentActionAnim:AnimationClip=scratchAnimation;
    _animation.CrossFade(currentActionAnim.name);
    var amazedTime: int=Random.Range(1,10);
    if(_animation.isPlaying){
        yield WaitForSeconds(currentActionAnim.length);
        _animation.CrossFade(idleAnimation.name);
        yield WaitForSeconds(amazedTime);
        finishedActivity=1;
    }
 }
 function updateSpeed(newSpeed : float ){
 	speed=newSpeed;
 }
 function updateBravery(newBravery : float){
 
 }
 