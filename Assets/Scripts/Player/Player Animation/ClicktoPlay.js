var animationObject: GameObject;

function OnGUI () {
     
     if (GUI.Button(Rect(10,10,100,50),"RunCycle")){
		animationObject.GetComponent.<Animation>().Play("RunCycle");
     }
     
     if (GUI.Button(Rect(120,10,50,50),"Idle_1")){
		animationObject.GetComponent.<Animation>().Play("Idle_1");
     }
     
     if (GUI.Button(Rect(180,10,50,50),"Idle_2")){
		animationObject.GetComponent.<Animation>().Play("Idle_2");
     }
     
     if (GUI.Button(Rect(240,10,80,50),"Attack_1")){
		animationObject.GetComponent.<Animation>().Play("Attack_1");
     }
     if (GUI.Button(Rect(330,10,80,50),"Attack_2")){
		animationObject.GetComponent.<Animation>().Play("Attack_2");
     }
     if (GUI.Button(Rect(420,10,80,50),"Attack_3")){
		animationObject.GetComponent.<Animation>().Play("Attack_3");
     }
     if (GUI.Button(Rect(510,10,60,50),"GetHit")){
		animationObject.GetComponent.<Animation>().Play("GetHit");
     }
     if (GUI.Button(Rect(580,10,60,50),"Die")){
		animationObject.GetComponent.<Animation>().Play("Die");
     }
     
}