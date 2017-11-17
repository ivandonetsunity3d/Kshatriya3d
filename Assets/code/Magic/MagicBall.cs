using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;


public class MagicBall : MonoBehaviour {

    public bool Moving = false;
    public float MoveIncrement = 1f;
    public Transform MagicBallTransform;

    public string SpeakPhrase = "Hit.";

    public bool RightDir = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Moving==true)
        {
            Vector3 NewPosVect;
            NewPosVect = MagicBallTransform.position;

            if (RightDir == true)
            {
                NewPosVect.z = NewPosVect.z - MoveIncrement;
            }
            else if (RightDir == false)
            {
                NewPosVect.z = NewPosVect.z + MoveIncrement;
            }

           

            MagicBallTransform.position = NewPosVect;
        }
	}

    public void StartMoving(bool RightDirection) 
    {
        Moving = true;
        RightDir = RightDirection;

    }


    void OnTriggerEnter(Collider other)
    {
    	//Debug.Log(other.transform.name);
        if (other.transform.name == "Tobacco tree")
        {
            SpVoice voice;
            voice = new SpVoice();
            voice.Speak(SpeakPhrase);

        }
        if (other.transform.name == "LEGO(Clone)")
        {
        	//Debug.Log(other.transform.name);
        	
			Instantiatable InstantiatableScript = other.GetComponent<Instantiatable>();            
			if (InstantiatableScript.IsEnemyStr == "Enemy")
			{
			InstantiatableScript.Health=InstantiatableScript.Health-50;
			//Debug.Log(InstantiatableScript.Health);
            gameObject.SetActive(false);

			if (InstantiatableScript.Health<=0)
			{
                other.transform.gameObject.SetActive(false);
                //other.SetActive(false);
				//Destroy(other);
			}
			
			}
			
        }

        if (other.transform.name == "Troll")
        {
            Enemy EnemyScript = other.GetComponent<Enemy>();

            EnemyScript.ChangeHealth(-50);

            transform.gameObject.SetActive(false);
        }


    }



}
