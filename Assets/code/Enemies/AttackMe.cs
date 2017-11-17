using UnityEngine;
using System.Collections;


public class AttackMe : MonoBehaviour {

    public int health = 100;
	public Transform PlayerTransform;
    public Animation EnemyAnimation;
  //  public Rect rect;
    public TextMesh HealthLabel;


	public float destinationDistance;			// The distance between Transform and destinationPosition


	public PotionsGenerator   PotionsGenScript;

	public float CurrentFrameNumber;
	public float AnimLen;


	// Use this for initialization
	void Start () {
        EnemyAnimation = this.transform.GetComponent<Animation>();

        PlayerTransform = GameObject.Find("Player").transform ;

      //  rect.x = 100;
      //  rect.width = 100;
      //  rect.height = 20;

    }
	
	// Update is called once per frame
	void Update () 
	{
		if( EnemyAnimation.IsPlaying("Die"))
		{
		//	EnemyAnimation ["Die"].time = CurrentFrameNumber;
		}

	}

    void OnGUI()
    {
       // HealthLabel.text = System.Convert.ToString(health);
        //GUI.TextField(rect, System.Convert.ToString(health));

        ;
    }

   public void GetDamage()
    {
		health = health - 50;


		if (health <= 0) { 

			EnemyAnimation.Play ("Die");
		} else 
		{
			EnemyAnimation.Play ("Hit");
		}
        
    }


	void HitPlayed()
	{
		
		EnemyAnimation.Play ("IdleAfterHit");



	}


  public  void Dead()
    {
        AnimLen = EnemyAnimation ["Die"].length;

        //EnemyAnimation ["Die"].time =	1.99f;

        CurrentFrameNumber =   EnemyAnimation ["Die"].time;
        EnemyAnimation ["Die"].speed = 0.0f;
        //EnemyAnimation ["Die"].time = 120.0f;//(1f/30f)* ;
        //EnemyAnimation.Stop();

        CreateNewEnemy ();
        //Destroy(gameObject); 
        //Let corpses remain!

        gameObject.name = "Enemy Corpse";
    //	foreach (AnimationState state in EnemyAnimation)
        //{
        //	state.speed = 0;
        //}

			

    //    PlayerTransform.SendMessage ("ModifyEnemiesKilled",1);
    //    PlayerTransform.SendMessage ("StopPlayerAttackAfterAnimationPlayed");

    }

	void CreateNewEnemy()
	{
		//Rigidbody
		//Transform clone;

		PotionsGenScript = PlayerTransform.gameObject.GetComponent<PotionsGenerator> ();
		PotionsGenScript.CreatePotion (transform.position );


		//PlayerTransform.gameObject.GetComponent<PotionsGenerator>().

		//PlayerTransform.gameObject.SendMessage("CreatePotion", transform.position );



	/*	Transform NewPotion;

		NewPotion = Instantiate (Potion, transform.position, Quaternion.identity)  as Transform;
		NewPotion.name = "Potion";*/

	//	clone =	Instantiate  (	transform,new Vector3(transform.position.x + 1.5F, 0, transform.position.z + 1.5F), Quaternion.identity) as Transform;
		//clone.name = "Enemy";
		//clone.SendMessage ("SetCloneHealthTo100");



	}


	void SetCloneHealthTo100()
	{
		health = 100;

	}



	void Attack(Transform PlTransform)
    {
		PlayerTransform = PlTransform;
       // anim.Play("walk");

		if (health > 0) {
            EnemyAnimation.Play("Attack_01");

            //EnemyAnimation.Play ("EnemyAttack");
			transform.LookAt (PlayerTransform);
		}
        //else 
        //{
        //    Dead();
        //}

    }


	void TakeHealthTime()
	{
		PlayerTransform.SendMessage ("ModifyHealth", -5);
		//EnemyAnimation.Play("idle");
		//EnemyAnimation.Play("EnemyAttack");
			
	}

	void EndAttackAnimation()
	{
		

		CheckDistanceAndAttackOrApproach ();

	}


	void CheckDistanceAndAttackOrApproach()
	{
		destinationDistance = Vector3.Distance(transform.position , PlayerTransform.position);

		if (destinationDistance < 1f) 
		{
			transform.LookAt(PlayerTransform);
			EnemyAnimation.Play("EnemyAttack");
		}
		else
		{
			

			ApproachPlayer ();
		}




	}

	void ApproachPlayer()
	{
		EnemyAnimation.Play("idle");

	}


	void Freeze()
	{
		

		foreach (AnimationState anim in EnemyAnimation)
		{
			if (EnemyAnimation.IsPlaying(anim.name ))
			{
				EnemyAnimation[anim.name].speed = 0.0f;

			}
		}
	}

}
