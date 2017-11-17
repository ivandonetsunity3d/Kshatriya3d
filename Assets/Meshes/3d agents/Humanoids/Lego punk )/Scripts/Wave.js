#pragma strict

function Start () {
    GetComponent.<Animation>()["Wave"].layer = 1;
    GetComponent.<Animation>()["Wave"].wrapMode = WrapMode.Once;
}

function Update () {
    if(Input.GetKeyDown("e")){
        GetComponent.<Animation>().Play("Wave");
    }
}



