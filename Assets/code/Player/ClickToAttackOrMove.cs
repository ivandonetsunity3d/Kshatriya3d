using UnityEngine;
using System.Collections;

//public enum ItemKind{ 
//	Potion, 
//	Ring
//};

public class ClickToAttackOrMove : MonoBehaviour {
	public string ObjectUnderCursor = ""; //Var to store info about object under cursor
	//public TextMesh PlayerLabel; //Label to show Health; Object under Mouse,  etc

    public string[] MagicAnimationsList;
    public string CurrentAnimationName;
    public string CurrentAnimationType;

    public bool UseMagic = false;

    //show inventory
    public bool InventoryIsOpen = true ;

	public int HeroHealth = 100;
	public int HeroHealthMaximum = 1000;
	public int HeroManaMaximum = 1000;


	public int HeroHealthBottlesQuantity = 0;
	public int HeroManaBottlesQuantity = 0;
	public int HeroRegenerationBottlesQuantity = 0;


	public int HeroGoldCoinsQuantity = 0;

	public int HeroEnemiesKilled = 0;
	public int EnemyHealth=0;

	public int HeroMana = 10;


	public Rect rect, RectHealth, RectHealthBottles, RectGold, RectEnemiesKilled,RectEnemyHealth,RectFreeze, RectMana, RectManaBottles, RectRegenerationBottles;


	public bool IsAttacking = false;
	public bool IsWalking = false;

	public bool Pause = false;
	public bool ApproachAndAttack = false;
	public bool ApproachAndWorship = false;
    public bool ApproachAndTalk = false;


	//potion pickup mode
	public bool ApproachAndPickUp = false;
	public bool 	FreezeModeOn=false;



	public Animation PlayerAnimation;
	public Transform PlayerTransform, EnemyTransform, ItemTransform, StatueTransform;	 // Selectible with mouse 
	//public AttackMe   EnemyScript;
    public Enemy EnemyScript;

	public Potion PotionScript;
	public PickUpAbleItem ItemScript;

	public FileSaveLoad SaveLoadScript;

	//public Potion PotionScript;



	//public Transform zombie;


	public Vector3 destinationPosition;			// The destination Point
	public float destinationDistance;			// The distance between myTransform and destinationPosition

	public float moveSpeed;						// The Speed the character will move

	public RaycastHit hit;
	public Ray ray;




    



	// Use this for initialization
	void Start () 
	{
        MagicAnimationsList = new string[25];
        MagicAnimationsList[0] = "1H Magic Attack 01";
        MagicAnimationsList[1] = "1H Magic Attack 02";
        MagicAnimationsList[2] = "1H Magic Attack 03";
        MagicAnimationsList[3] = "1Hand up cast spell 01";
        MagicAnimationsList[4] = "2H Cast Spell 01";
        MagicAnimationsList[5] = "2H Magic Area Attack 01";
        MagicAnimationsList[6] = "2H Magic Area Attack 02";
        MagicAnimationsList[7] = "2H Magic Attack 01";
        MagicAnimationsList[8] = "2H Magic Attack 02";
        MagicAnimationsList[9] = "2H Magic Attack 03";
        MagicAnimationsList[10] = "2H Magic Attack 04";
        MagicAnimationsList[11] = "2H Magic Attack 05";

        MagicAnimationsList[12] = "Frisbee Throw";
        MagicAnimationsList[13] = "Goalkeeper Overhand Throw";
        MagicAnimationsList[14] = "Long_Casting Spell";
        MagicAnimationsList[15] = "Magic Spell Casting";
        MagicAnimationsList[16] = "Spell Cast";
        MagicAnimationsList[17] = "Throw In";
        MagicAnimationsList[18] = "Throwing";
        MagicAnimationsList[19] = "Wide Arm Spell Casting";
        MagicAnimationsList[20] = "Goalie Throw";
        MagicAnimationsList[21] = "Two Hand Spell Casting";

        MagicAnimationsList[22] = "Magic Heal";
        MagicAnimationsList[23] = "Spell Casting";
        MagicAnimationsList[24] = "Throw";


        SaveLoadScript = transform.GetComponent<FileSaveLoad> ();

		rect.x = 0;
		rect.y = 40;
		rect.width = 200;
		rect.height = 20;

		RectHealth.x = 100;
		RectHealth.width = 100;
		RectHealth.height = 20;

		RectHealthBottles.x = 200;
		RectHealthBottles.width = 150;
		RectHealthBottles.height = 20;

		RectManaBottles.y = 20;
		RectManaBottles.x = 200;
		RectManaBottles.width = 150;
		RectManaBottles.height = 20;

		RectRegenerationBottles.y = 40;
		RectRegenerationBottles.x = 200;
		RectRegenerationBottles.width = 150;
		RectRegenerationBottles.height = 20;



		RectGold.x = 350;
		RectGold.width = 100;
		RectGold.height = 20;

		RectEnemiesKilled.x = 450;
		RectEnemiesKilled.width = 100;
		RectEnemiesKilled.height = 20;


		RectEnemyHealth.x = 550;
		RectEnemyHealth.width = 150;
		RectEnemyHealth.height = 20;



		RectFreeze.x = 0;
		RectFreeze.y = 20;
		RectFreeze.width = 100;
		RectFreeze.height = 20;

		RectMana.x=100;
		RectMana.y = 20;
		RectMana.width = 100;
		RectMana.height = 20;




		PlayerTransform = transform;					
		destinationPosition = PlayerTransform.position;			// prevents myTransform reset
	}



	void OnGUI()
    {

       // if (Event.current.type == EventType.Repaint)
        {
            //PlayerLabel.text = ObjectUnderCursor;
            GUI.TextField(rect, ObjectUnderCursor);


            if (InventoryIsOpen)
            {
                GUI.TextField(RectHealth, "Health: " + System.Convert.ToString(HeroHealth));
                GUI.TextField(RectHealthBottles, "Health bottles: " + System.Convert.ToString(HeroHealthBottlesQuantity));
                GUI.TextField(RectManaBottles, "Mana bottles: " + System.Convert.ToString(HeroManaBottlesQuantity));
                GUI.TextField(RectRegenerationBottles, "Regeneration bottles: " + System.Convert.ToString(HeroRegenerationBottlesQuantity));


                GUI.TextField(RectGold, "Gold: " + System.Convert.ToString(HeroGoldCoinsQuantity));
                GUI.TextField(RectEnemiesKilled, "Killed: " + System.Convert.ToString(HeroEnemiesKilled));
                GUI.TextField(RectEnemyHealth, "Enemy health: " + System.Convert.ToString(EnemyHealth));
                GUI.TextField(RectMana, "Mana: " + System.Convert.ToString(HeroMana));

            }



            if (FreezeModeOn)
            {
                GUI.TextField(RectFreeze, "Click on enemy to freeze it");

            }

            if (GUI.Button(new Rect(0, 60, 50, 20), "Save"))
            {
                //SaveLoadScript.SaveFileString("HAREKRISHNA","Chant and be happy");
                SaveLoadScript.SaveFileInt("HAREKRISHNA", HeroHealth);


            }
            if (GUI.Button(new Rect(0, 80, 50, 20), "Load"))
            {
                //Debug.Log (SaveLoadScript.LoadFileString("HAREKRISHNA" ));

                HeroHealth = SaveLoadScript.LoadFileInt("HAREKRISHNA");


            }
        }


      


    }

	void GetEnemyHealth()
	{
		if (EnemyTransform != null) {


            EnemyScript = EnemyTransform.gameObject.GetComponent<Enemy>();

			//EnemyScript = EnemyTransform.gameObject.GetComponent<AttackMe> ();
			EnemyHealth =	EnemyScript.Health;
		} 
		else if (EnemyTransform = null) 
		{
			EnemyHealth = 0;
		}

	}


	void ModifyEnemiesKilled(int EnemiesNumber)
	{
		HeroEnemiesKilled = HeroEnemiesKilled + EnemiesNumber;
		EnemyTransform = null;
		EnemyHealth = 0;
		IsAttacking = false;
		ApproachAndAttack = false;
	}






	// Update is called once per frame
	void Update () 
	{
        if (HeroHealth > 0) 
        {
            CheckKeyboardButtons(); //Space to attack
            CheckDistanceAndUpdateSpeed();

            if (FreezeModeOn == false)
            {
                //Debug.Log ("Freeze off");
                CheckObjectUnderMouseCursor();

            }
            else if (FreezeModeOn == true)
            {
                //Debug.Log ("Freeze on. Clicked on Enemy");
                //FreezeModeOn = !FreezeModeOn;
                CheckObjectUnderMouseCursorFreeze();


            }



            //Attack after Approached or no
            CheckAttackMode();

            GetEnemyHealth();
        }
        else if (HeroHealth <= 0) 
        {
            if (CurrentAnimationName != "Die") 
            {
                CurrentAnimationName = "Die";

                PlayerAnimation.Play(CurrentAnimationName);
                CurrentAnimationType = "Die";
            }
            
        }


	}


	void CheckKeyboardButtons()
	{
		if (Input.GetKeyUp (KeyCode.Space)) {
			// anim.Play("idle");
			//  IsAttacking = false;

			PlayerTransform.LookAt (EnemyTransform);
			//IsAttacking = true;


   //         int RndNumber = Random.Range(0, 25);
   //         CurrentAnimationName = MagicAnimationsList[RndNumber];
   //         PlayerAnimation.Play(CurrentAnimationName);
   //         PlayerAnimation[CurrentAnimationName].wrapMode = WrapMode.Once;


   //         PlayerAnimation["Attack"].wrapMode = WrapMode.Once;
			//PlayerAnimation.Play ("Attack");

			//Debug.Log (PlayerAnimation ["Attack"].time);
            Debug.Log(PlayerAnimation[CurrentAnimationName].time);




        }
        else if (Input.GetKeyUp (KeyCode.I)) {//Open inventory
			InventoryIsOpen = !InventoryIsOpen;

		} else if (Input.GetKeyUp (KeyCode.P)) { //(Un)Pause
			PauseOrUnpause ();
		} else if (Input.GetKeyUp (KeyCode.S)) {//Speed up
			FastGame ();
		} else if (Input.GetKeyUp (KeyCode.H)) {
			//use health bottle
			if (HeroHealthBottlesQuantity > 0) {				
				HeroHealth = HeroHealth + 75;
				HeroHealthBottlesQuantity = HeroHealthBottlesQuantity - 1;
			}
		} else if (Input.GetKeyUp (KeyCode.M))
		{//drink mana bottle
			if (HeroManaBottlesQuantity > 0) {				
				HeroMana = HeroMana + 75;
				HeroManaBottlesQuantity = HeroManaBottlesQuantity - 1;
			}
		}else if (Input.GetKeyUp (KeyCode.R))
		{//drink regeneration bottle
			if (HeroRegenerationBottlesQuantity > 0) {				
				HeroMana = HeroMana + 75;
				HeroHealth = HeroHealth + 75;
				HeroRegenerationBottlesQuantity = HeroRegenerationBottlesQuantity - 1;
			}
		}



		else if (Input.GetKeyUp (KeyCode.F)) //Freeze monster
		{
			if (HeroMana >= 3) 
			{
				FreezeModeOn = !FreezeModeOn;
			}



		}




	}


	void PauseOrUnpause()
	{
		Pause = !Pause;
		if (Pause == true)
		{
			Time.timeScale = 0;
		} 
		else if (Pause == false) 
		{
			Time.timeScale = 1;
		}

	}

	void FastGame()
	{		
		Time.timeScale = Time.timeScale + 1;

	}

	void CheckPickupMode()
	{
		if (ApproachAndPickUp == true)
		{
			if (ItemTransform != null)
			{
				ItemScript = ItemTransform.GetComponent<PickUpAbleItem > ();
				switch (ItemScript.itemkind) 
				{
				case ItemKind.Potion:
					PickUpPotion ();
					break;
				case ItemKind.Ring:

					PickUp ();

					break;

				}

				ItemTransform.SendMessage ("PickUp");
				ItemTransform = null;
				ApproachAndPickUp = false;


			}





		}

	}

	void CheckWorshipMode()
	{
		if (ApproachAndWorship == true)
		{
			PlayerShouldWorshipStatue ();


		}
	}



	void PickUpPotion()
	{
		if (ItemTransform != null)
		{
			PotionScript = ItemTransform.GetComponent<Potion> ();
			switch (PotionScript.potionkind) 
			{
			case PotionKind.Health:
				HeroHealthBottlesQuantity = HeroHealthBottlesQuantity + 1;
				break;
			case PotionKind.Mana :
				HeroManaBottlesQuantity = HeroManaBottlesQuantity + 1;
				break;
			case PotionKind.Regeneration :
				HeroRegenerationBottlesQuantity = HeroRegenerationBottlesQuantity + 1;
				break;
			}


		}
	}


	void CheckDistanceAndUpdateSpeed()
	{
		// keep track of the distance between this gameObject and destinationPosition
		destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

		if (destinationDistance < 1f) {		// To prevent shakin behavior when near destination
			moveSpeed = 0;
			IsWalking = false;

			if (PlayerAnimation.IsPlaying ("Walk"))
			{
					PlayerAnimation.Play ("Idle");
			}


			//	Debug.Log("destinationDistance < .5f");

			CheckAttackMode ();
			CheckPickupMode ();
			CheckWorshipMode ();

			if (PlayerAnimation.IsPlaying ("Attack")) {
				PlayerShouldAttackEnemy ();
			} else { //if walking or idle
				//	PlayerAnimation.Play ("idle");
			}


		} /*else if (destinationDistance < 1.5f)			
		{
			
		}*/
		else if(destinationDistance > 1f){			// To Reset Speed to default
			moveSpeed = 3;
		}

	}




	//Check  Object over  Mouse Cursor: Enemy or Terrain?
	void CheckObjectUnderMouseCursorFreeze()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			ObjectUnderCursor = hit.transform.gameObject.name;

			//	Debug.Log("Checking Object Under Mouse Cursor. It is: " + hit.transform.name);

			if (ObjectUnderCursor == "Enemy") 
			{
				
				// Freeze monster on click
				if (Input.GetMouseButtonDown (0)) 
				{
					EnemyTransform = hit.transform;

                 Enemy _EnemyScript=   EnemyTransform.GetComponent<Enemy>();

                 _EnemyScript.Freeze();

					//EnemyTransform.SendMessage ("Freeze");
					//HeroMana = HeroMana - 3;

					FreezeModeOn = false;
				}

				GetEnemyHealth ();

			}
		}

	}





	//Check  Object over  Mouse Cursor: Enemy or Terrain?
	void CheckObjectUnderMouseCursor()
	{
		 ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			ObjectUnderCursor = hit.transform.gameObject.name;

		//	Debug.Log("Checking Object Under Mouse Cursor. It is: " + hit.transform.name);

			if (ObjectUnderCursor == "Enemy") {
				

				if (IsAttacking == true) {
					
					PlayerShouldAttackEnemy ();

				}
				
				// Moves the Player if the Left Mouse Button was clicked
				if (Input.GetMouseButtonDown (0)) {
					SetApproachAndAttackModeOn ();
					EnemyTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerClicksLeftMouseButtonOnEnemy ();
				}
				// Moves the player if the mouse button is hold down
				else if (Input.GetMouseButton (0)) {
					SetApproachAndAttackModeOn ();
					EnemyTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerHoldsLeftMouseButtonOnEnemy ();
				} else {
					EnemyTransform = hit.transform;
					GetEnemyHealth ();
				}
				//PlayerTransform.position = Vector3.MoveTowards (PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);

				GetEnemyHealth ();

			} else if (ObjectUnderCursor == "Enemy Corpse") 
			{
				EnemyHealth=0;
			} else if (ObjectUnderCursor == "Ganesha")
			{
				EnemyHealth=0;
				//Debug.Log (ObjectUnderCursor);

				// Moves the Player if the Left Mouse Button was clicked
				if (Input.GetMouseButtonDown (0)) {
					SetApproachAndWorshipModeOn ();
					StatueTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerClicksLeftMouseButtonOnGanesha ();
				}
				// Moves the player if the mouse button is hold down
				else if (Input.GetMouseButton (0)) {
					SetApproachAndWorshipModeOn ();
					StatueTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerHoldsLeftMouseButtonOnGanesha ();
				} else {
					StatueTransform = hit.transform;
				}
			}
			else if (ObjectUnderCursor == "Krishna")
			{
				EnemyHealth=0;
				//Debug.Log (ObjectUnderCursor);

				// Moves the Player if the Left Mouse Button was clicked
				if (Input.GetMouseButtonDown (0)) {
					SetApproachAndWorshipModeOn ();
					StatueTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerClicksLeftMouseButtonOnKrishna ();
				}
				// Moves the player if the mouse button is hold down
				else if (Input.GetMouseButton (0)) {
					SetApproachAndWorshipModeOn ();
					StatueTransform = hit.transform;
					destinationPosition =	hit.transform.position;

					PlayerHoldsLeftMouseButtonOnKrishna ();
				} else {
					StatueTransform = hit.transform;
				}






			} else if (ObjectUnderCursor == "Player")
			{
				EnemyHealth=0;
			}

			else if (ObjectUnderCursor == "Terrain")
			{
				
				EnemyHealth=0;

				if (Input.GetMouseButtonDown (0)) {
					EnemyTransform = null;
					ApproachAndAttack = false;
					IsAttacking = false;
					PlayerClicksLeftMouseButtonOnTerrain ();
				}
				// Moves the player if the mouse button is hold down
				else if (Input.GetMouseButton (0)) {
					//EnemyTransform = null;
					//ApproachAndAttack = false;
					//IsAttacking = false;
					PlayerHoldsLeftMouseButtonOnTerrain ();
				}

				PlayerTransform.position = Vector3.MoveTowards (PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
			} 
			else if (ObjectUnderCursor == "Potion")
			{
				EnemyHealth=0;
				if (Input.GetMouseButtonDown (0)) {
					destinationPosition =	hit.transform.position;
					ItemTransform = hit.transform;
					//ApproachAndAttack = false;
					//IsAttacking = false;
					PlayerClicksLeftMouseButtonOnPotion ();
				}
				// Moves the player if the mouse button is hold down
				else if (Input.GetMouseButton (0)) {					
					destinationPosition =	hit.transform.position;
					//PotionTransform = hit.transform;
					//ApproachAndAttack = false;
					//IsAttacking = false;
					PlayerHoldsLeftMouseButtonOnPotion ();
				}
			}
            else if (ObjectUnderCursor == "Pepin")
            { 
            
            }
            else if (ObjectUnderCursor == "Cain")
            {

            }
            else if (ObjectUnderCursor == "Griswold")
            {

            }
            else if (ObjectUnderCursor == "Wirt")
            {

            }
            else if (ObjectUnderCursor == "Adria")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    destinationPosition = hit.transform.position;
                    ItemTransform = hit.transform;
                    //ApproachAndAttack = false;
                    //IsAttacking = false;
                    PlayerClicksLeftMouseButtonOnAdria();
                }
            }
            else if (ObjectUnderCursor == "Shiva Shakti yantra ring")
            {
                EnemyHealth = 0;
                if (Input.GetMouseButtonDown(0))
                {
                    destinationPosition = hit.transform.position;
                    ItemTransform = hit.transform;
                    //ApproachAndAttack = false;
                    //IsAttacking = false;
                    PlayerClicksLeftMouseButtonOnRing("Shiva Shakti yantra ring");
                }
                // Moves the player if the mouse button is hold down
                else if (Input.GetMouseButton(0))
                {
                    destinationPosition = hit.transform.position;

                    //PlayerHoldsLeftMouseButtonOnRing ();
                }
            }
            else
            {
                //Debug.Log (ObjectUnderCursor);
                ObjectUnderCursor = hit.transform.gameObject.name;
            }



		}
	}

    void PlayerClicksLeftMouseButtonOnAdria() 
    {
        CheckDistanceAndTalkOrMoveToNPC("Adria");
    }

    void SetApproachAndTalkModeOn() 
    {
        ApproachAndTalk=true;
    }

    void PlayerShouldTalkTo(string NPCName)
    {

    }

    void CheckDistanceAndTalkOrMoveToNPC(string NPCName)
    {
        destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

        if (destinationDistance > .5f)
        {
            SetApproachAndTalkModeOn();

            MovePlayerTowards(NPCName);

        }
        else
        {
            //Debug.Log ("Player Should Pickup a Potion");

            PlayerShouldTalkTo(NPCName);

        }
    }


	void PlayerClicksLeftMouseButtonOnRing(string RingName)
	{
		switch (RingName)
		{
		case "Shiva Shakti yantra ring":
			FacePlayerTowardsRing ();

			CheckDistanceAndPickupOrMoveTo (RingName);

			break;
		}
	}

	void FacePlayerTowardsRing()
	{
		FacePlayerTowardsItem ();
	}

	void FacePlayerTowardsItem()
	{
		if (ItemTransform != null) {
			PlayerTransform.LookAt (ItemTransform.position); //destinationPosition);//hit.transform.position);
		}
		else 
		{			
			ApproachAndPickUp = false;
		}
	}

	void PlayerClicksLeftMouseButtonOnGanesha()
	{
		FacePlayerTowardsStatue ();
		CheckDistanceAndWorshipOrMove ("Ganesha");
	}

	void PlayerHoldsLeftMouseButtonOnGanesha()
	{
		FacePlayerTowardsStatue ();
	}


	void SetApproachAndWorshipModeOn()
	{
		ApproachAndWorship = true;
	}

	void PlayerClicksLeftMouseButtonOnKrishna()
	{
		FacePlayerTowardsStatue ();
		CheckDistanceAndWorshipOrMove ("Krishna");

	}

	void PlayerHoldsLeftMouseButtonOnKrishna()
	{
		FacePlayerTowardsStatue ();

	}

	void FacePlayerTowardsStatue()
	{
		if (StatueTransform != null) {
			PlayerTransform.LookAt (StatueTransform.position); //destinationPosition);//hit.transform.position);
		}
		else 
		{			
			ApproachAndWorship = false;
		}
	}

	void CheckDistanceAndWorshipOrMove(string StatueName)
	{
		destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

		if (destinationDistance > .5f) 
		{
			SetApproachAndWorshipModeOn ();
			MovePlayerTowardsStatue (StatueName);

		}
		else
		{
			//if (ApproachAndWorship == true) 
			{
				PlayerShouldWorshipStatue ();
			}
			//Debug.Log ("Player Should Pickup a Potion");



		}
	}


	void PlayerShouldWorshipStatue()
	{
		switch (StatueTransform.gameObject.name)
		{
		case "Krishna":
			HeroHealth = HeroHealthMaximum;
			HeroMana = HeroManaMaximum;
			break;

		case "Ganesha":
			HeroGoldCoinsQuantity = 1000;
			break;

		}


		//HeroHealth = HeroHealth+1;
		//HeroMana = HeroMana+1;
		//Debug.Log("Player fully rejuvenated after Krishna worship");

		ApproachAndWorship = false;
		StatueTransform = null;


	}

	void MovePlayerTowardsStatue(string StatueName)
	{
		MovePlayerTowards (StatueName);
	}

	void MovePlayerTowards(string TransformName)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			//Debug.Log(	hit.transform.name + ", RaycastHit[] hits = Physics.RaycastAll");

			if (hit.transform.name == TransformName ) 
			{
				destinationPosition =	hit.transform.position;
				//PlayerTransform.position  =	hit.transform.position;

				PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
			}

		}
	}



	void PlayerClicksLeftMouseButtonOnPotion()
	{
		//ApproachAndPickUp

		CheckDistanceAndPickupOrMove ();

	}

	void CheckDistanceAndPickupOrMoveTo(string ItemName)
	{
		destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

		if (destinationDistance > .5f) 
		{
			SetApproachAndPickUpModeOn ();

			MovePlayerTowards (ItemName);

		}
		else
		{
			//Debug.Log ("Player Should Pickup a Potion");

			PlayerShouldPickup (ItemName);

		}
	}

	void PlayerShouldPickup(string ItemName)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			//Debug.Log(	hit.transform.name + ", RaycastHit[] hits = Physics.RaycastAll");

			if (hit.transform.name == ItemName) 
			{		
				ItemTransform = hit.transform;
				PickUp ();

				//hit.transform.gameObject.SendMessage ("PickUp");

			}

		}
	}

	void PickUp()
	{
		if (ItemTransform != null)
		{
			Debug.Log ("Item picked up");


		}
	}


	void CheckDistanceAndPickupOrMove()
	{
		destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

		if (destinationDistance > .5f) 
		{
			SetApproachAndPickUpModeOn ();

			MovePlayerTowardsPotion ();

		}
		else
		{
			//Debug.Log ("Player Should Pickup a Potion");

			PlayerShouldPickupPotion ();

		}

	}

	void SetApproachAndPickUpModeOn()
	{
		ApproachAndPickUp = true;
	}


	void MovePlayerTowardsPotion()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			//Debug.Log(	hit.transform.name + ", RaycastHit[] hits = Physics.RaycastAll");

			if (hit.transform.name == "Potion") 
			{
				destinationPosition =	hit.transform.position;
				//PlayerTransform.position  =	hit.transform.position;

				PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
			}

		}	
	}


	void PlayerShouldPickupPotion()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			//Debug.Log(	hit.transform.name + ", RaycastHit[] hits = Physics.RaycastAll");

			if (hit.transform.name == "Potion") 
			{		
				ItemTransform = hit.transform;
				PickUpPotion ();

				//hit.transform.gameObject.SendMessage ("PickUp");
			
			}

		}
	}




	void PlayerHoldsLeftMouseButtonOnPotion()
	{
		//later maybe
	}



	void CheckAttackMode()
	{
		if (ApproachAndAttack == true)
		{
			if (destinationDistance < 1f) 
			{
				PlayerShouldAttackEnemy ();
			}
			
		} 
	}


	void CheckDistanceAndAttackOrMove ()
	{
		// keep track of the distance between this gameObject and destinationPosition
		destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

		if (destinationDistance < 1f) 
		{
			//Debug.Log ("Player Should Attack Enemy");
			//SetApproachAndAttackModeOn ();
			PlayerShouldAttackEnemy ();

		}
		else
		{
			IsAttacking = false  ;
			MovePlayerTowardsEnemy ();
		}
	}


	void PlayerClicksLeftMouseButtonOnEnemy()
	{	
		SetApproachAndAttackModeOn ();
		FacePlayerTowardsEnemy ();
		CheckDistanceAndAttackOrMove ();
	}

	void PlayerHoldsLeftMouseButtonOnEnemy()
	{
		FacePlayerTowardsEnemy ();
		//CheckDistanceAndAttackOrMove ();
	}

	void SetApproachAndAttackModeOn()
	{
	//	Debug.Log ("Approach and Attack mode On");
		ApproachAndAttack = true;
		//FacePlayerTowardsEnemy ();//maybe err
	}


	void FacePlayerTowardsEnemy()
	{
		if (EnemyTransform != null) {
			PlayerTransform.LookAt (EnemyTransform.position); //destinationPosition);//hit.transform.position);
		}
		else 
		{
			IsAttacking = false;
			PlayerAnimation.Play ("Idle");
			ApproachAndAttack = false;
		}

	}

	void PlayerShouldAttackEnemy()
	{
		if (EnemyTransform != null)
		{
			Vector3 forward = PlayerTransform.TransformDirection(Vector3.forward);
			Vector3 toOther = EnemyTransform.position - PlayerTransform.position;
			if (Vector3.Dot (forward, toOther) > 0) 
			{
				//Debug.Log ("The other transform is ahead of me!");

				if (Vector3.Angle (forward, toOther)<=45) 
				{
					FacePlayerTowardsEnemy ();

					destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

					if (destinationDistance < 1f)
					{
						IsAttacking = true ;

                        if (UseMagic == true)
                        {

                        }
                        else if (UseMagic == false)
                        {
                            try
                            {
                                 EnemyScript = EnemyTransform.GetComponent<Enemy>();
                                if (EnemyScript.Health > 0)
                                {
                                    PlayerAnimation.Play("Attack");
                                }
                            }
                            catch
                            {
                            }



                            

                        }
                    }


				}



			}


		}



	}

    //EndAttackPlaying
 public   void _AttackAnimPlayed () 
	{
		IsAttacking = false ;
		PlayerAnimation.Play ("Idle");
		ApproachAndAttack = false;

	}



	void MovePlayerTowardsEnemy ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);

		foreach (RaycastHit hit in hits) 
		{
			//Debug.Log(	hit.transform.name + ", RaycastHit[] hits = Physics.RaycastAll");

			if (hit.transform.name == "Terrain") 
			{
				
			}
			else if (hit.transform.name == "Enemy") 
			{
			destinationPosition =	hit.transform.position;
				//PlayerTransform.position  =	hit.transform.position;

				PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
			}

		}	


	}


	










	void PlayerClicksLeftMouseButtonOnTerrain()
	{
		MovePlayerToTerrainPoint ();	
	}
	void PlayerHoldsLeftMouseButtonOnTerrain()
	{
		MovePlayerToTerrainPoint ();	
	}


	void MovePlayerToTerrainPoint()
	{
		Plane playerPlane = new Plane(Vector3.up, PlayerTransform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;

		if (playerPlane.Raycast(ray, out hitdist))
		{
			Vector3 targetPoint = ray.GetPoint(hitdist);
			destinationPosition = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			PlayerTransform.rotation = targetRotation;

			IsWalking = true;
			PlayerAnimation.Play ("Walk");
		}
		//PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
	
	}






  



	void AttackTime()
	{
		if (IsAttacking == true)
		{
           
			
			if (EnemyTransform != null)
			{
                try
                {
                     EnemyScript = EnemyTransform.GetComponent<Enemy>();
                    if (EnemyScript.Health > 0)
                    {
                        PlayerTransform.LookAt(EnemyTransform);

                        try
                        {
                            AttackMe AttackMeScript = EnemyTransform.GetComponent<AttackMe>();
                            AttackMeScript.GetDamage();
                        }
                        catch
                        {
                        }


                        try
                        {
                             EnemyScript = EnemyTransform.GetComponent<Enemy>();
                            EnemyScript.ChangeHealth(-50);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }



				

               


                //EnemyTransform.SendMessage("GetDamage");
            }

			// IsAttacking = false;
		}

	}

	public void ModifyHealth(int Value)
	{
		HeroHealth = HeroHealth + Value;

        if (HeroHealth <= 0)
        {
            if (CurrentAnimationType != "Die")
            {
                //int RndNumber = Random.Range(0, 3);

                //CurrentAnimationName = DieAnimationsList[RndNumber];

                CurrentAnimationName = "Die";

                PlayerAnimation.Play(CurrentAnimationName);
                CurrentAnimationType = "Die";
            }


        }
        else if (HeroHealth > 0)
        {
            //if (CurrentAnimationType != "Hit") 
            {
                //int RndNumber = Random.Range(0, 2);

                //CurrentAnimationName = HitAnimationsList[RndNumber];

                CurrentAnimationName = "Hit";

                PlayerAnimation.Play(CurrentAnimationName);
                CurrentAnimationType = "Hit";
            }
        }

       

	}


	void OnTriggerStay(Collider other)

	//void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Enemy") 
		{
			ForceEnemyToAttackPlayer (other.transform);
			//Debug.Log("OnTriggerStay");
		}
						
			
	}


	void OnCollisionEnter(Collision other) 
	{		
		if (other.transform.name == "Enemy") 
		{
			ForceEnemyToAttackPlayer (other.transform);
			//Debug.Log("OnCollisionEnter");
		}
	}


	void ForceEnemyToAttackPlayer(Transform other)
	{
		
			EnemyTransform = other.transform ;

			PlayerTransform.LookAt(EnemyTransform);

			EnemyTransform.LookAt(transform);
			//EnemyTransform.SendMessage("Attack", PlayerTransform );

      EnemyScript =  EnemyTransform.GetComponent<Enemy>();

      EnemyScript.Attack(PlayerTransform);

	}


}
