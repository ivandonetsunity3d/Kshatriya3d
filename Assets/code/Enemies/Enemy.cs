using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour {
    public int Health = 100;

    public string[] AttackAnimationsList;

    public float moveSpeed;
    public float destinationDistance;
    public Vector3 destinationPosition;


    public Transform LeftMost;
    public Transform RightMost;

    public GameObject Corner1;
    public GameObject Corner2;
    public GameObject Corner3;
    public GameObject Corner4;

    public float Xmin;
    public float Xmax;

    public float Zmin;
    public float Zmax;



    public bool WalkWithinSquare = true;

    public bool IsAttacking = false;
    public bool IsPlayingHit = false;

    public float WalkIncrement = 0.1f;
    public bool WalkRight = false;
    public bool WalkUp = false;

    public float WalkAnimSpeed = 1f;

    public float CurrentFrameNumber;
    public string CurrentAnimationName;

    private Animation anim;
    private Transform trans;

    public GameObject _Player;

    public bool LeftCollider = true;

    public Transform PlayerTransform;

    public void Attack(Transform PlTransform)
    {
        PlayerTransform = PlTransform;
        // anim.Play("walk");

        if (Health > 0)
        {
            anim.Play("Attack_01");

            //EnemyAnimation.Play ("EnemyAttack");
            transform.LookAt(PlayerTransform);
        }
        //else
        //{
        //    _DieAnimPlayed();
        //}

    }

    void OnTriggerEnter(Collider other) 
    {
        if (Health > 0)
        {
            if (other.transform.name == "Om Sphere(Clone)")
            {
                ChangeHealth(-50);
                Destroy(other.gameObject);
            }

            if (other.transform.name == "Ice Sphere(Clone)")
            {
                //ChangeHealth(-50);
                //Destroy(other.gameObject);
                Freeze();

            }
        }

       

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.name == "LEGO")
    //    {
    //        anim.Play("Attack_01");
    //        trans.LookAt(other.transform);

    //    }
    //}
    public void HitPlayingEnd()
    {
        IsPlayingHit = false;

    }

    public void _AttackEnd()
    {
        if (LeftCollider == true)
        {

        }
        IsAttacking = false;

    }

    void OnTriggerStay(Collider other) 
    {
        if (Health > 0)
        {
            if (other.transform.name == "Player")
            {
                if (IsAttacking == false) 
                {
                    if (IsPlayingHit == false) 
                    {
                        NavMeshPlayer NavMeshPlayerScript = _Player.GetComponent<NavMeshPlayer>();
                      if (NavMeshPlayerScript.Health >0) 
                      {

                          int RndNumber = Random.Range(0, 2);

                          CurrentAnimationName = AttackAnimationsList[RndNumber];

                          anim.Play(CurrentAnimationName);


                          anim[CurrentAnimationName].wrapMode = WrapMode.Loop;

                          //anim["Attack_01"].wrapMode = WrapMode.Once;


                          // trans.LookAt(other.transform);
                          IsAttacking = true;
                          LeftCollider = false;
                      }


                    }

                   
                }


            }
        }
       
    }


   private void LookAtPlayer() 
    {
        Quaternion Rotatio = trans.rotation;

        //Rotatio.y = 180;

        Rotatio.x = 0;
        Rotatio.z = 0;


        if (_Player.transform.position.z > trans.position.z ) 
        {
            //look to left
            Rotatio.y =0;

        }
        else if (_Player.transform.position.z < trans.position.z) 
        {
            //look to right
            Rotatio.y = 180;
        
        }

        trans.rotation = Rotatio;
    }

   private void OnTriggerExit(Collider other)
    {
        if (Health > 0)
        {
            if (other.transform.name == "Player")
            {
                //Debug.Log(anim["Attack_01"].time);
                //CurrentAnimationName

                switch (CurrentAnimationName)
                {
                    case "Attack_01":
                        //if (anim[CurrentAnimationName].time > 1.2f)

                        //if (anim["Attack_01"].time > 1.2f)

                        if (anim[CurrentAnimationName].time * anim[CurrentAnimationName].clip.frameRate >= 31)                        
                        {
                            CurrentAnimationName = "Idle";

                            anim.Play(CurrentAnimationName);

                            IsAttacking = false;


                        }
                        //else if (anim["Attack_01"].time <= 1.2f)

                      //  else if (anim[CurrentAnimationName].time <= 1.2f)
                        else if (anim[CurrentAnimationName].time * anim[CurrentAnimationName].clip.frameRate < 31)                
                        {
                            IsAttacking = false;
                            LeftCollider = true;
                        }

                        break;

                    case "Attack_02":
                        if (anim[CurrentAnimationName].time * anim[CurrentAnimationName].clip.frameRate >= 18)
                        {
                            CurrentAnimationName = "Idle";

                            anim.Play(CurrentAnimationName);
                           

                            IsAttacking = false;


                        }
                      
                        else if (anim[CurrentAnimationName].time * anim[CurrentAnimationName].clip.frameRate < 18)
                        {
                            IsAttacking = false;
                            LeftCollider = true;
                        }


                        break;
                }



                //if (Andy.animation["walk"].time * Andy.animation["walk"].clip.frameRate >= 30)
                //{
                //    Andy.animation.Stop("walk");
                //}


                



            }

        }
            
    }

    void HitPlayer() 
    {
        //Player PlayerScript = _Player.GetComponent<Player>();
        //PlayerScript.ChangeHealth(-5);

        NavMeshPlayer NavMeshPlayerScript = _Player.GetComponent<NavMeshPlayer>();

        if (NavMeshPlayerScript.Health >0)
        {
            NavMeshPlayerScript.ModifyHealth(-5);
        }
        //else if (NavMeshPlayerScript.Health <= 0) 
        //{
        
        //}
        

        //if (transform.position.z > trans.position.z) 
        //{
        
        //}

        
    }

    public void ChangeHealth(int ChangeValue)
    {
        Health = Health + ChangeValue;

        if (Health <= 0)
        {
            CurrentAnimationName = "Die";
            anim.Play(CurrentAnimationName);
            

            //Destroy(gameObject);
        }
        else if (Health > 0)
        {
            CurrentAnimationName = "Hit";
            anim.Play(CurrentAnimationName);

            IsPlayingHit = true;

            IsAttacking = false;

        }
    }

    public void _IdleAfterHit()
    {

        anim.Play("IdleAfterHit");
        CurrentAnimationName = "IdleAfterHit";

        IsAttacking = false;

    }


    public void _DieAnimPlayed()
    {
        
     //   float AnimLen = anim["Die"].length;

        //anim["Die"].time = 1.99f;

         CurrentFrameNumber = anim["Die"].time;
        anim["Die"].speed = 0.0f;
        //EnemyAnimation ["Die"].time = 120.0f;//(1f/30f)* ;
        //EnemyAnimation.Stop();

        //CreateNewEnemy();
        //Destroy(gameObject); 
        //Let corpses remain!

        gameObject.name = "Enemy Corpse";

        PlayerTransform = GameObject.Find("Player").transform;

        PotionsGenerator PotionsGenScript = PlayerTransform.gameObject.GetComponent<PotionsGenerator>();
        PotionsGenScript.CreatePotion(transform.position);


        //Player.SendMessage("ModifyEnemiesKilled", 1);
        //Player.SendMessage("StopPlayerAttackAfterAnimationPlayed");

    }

   public void Freeze()
    {
        anim = GetComponent<Animation>();
        Debug.Log(CurrentAnimationName);

        anim[CurrentAnimationName].speed = 0.0f;

        Debug.Log("Freeze function");
        //foreach (AnimationClip anima in anim)
        //{            
        //    {
        //        anim[anima.name ].speed = 0.0f;

        //    }
        //}
    }




    // Use this for initialization
  private  void Start()
    {
        anim = GetComponent<Animation>();
        trans = GetComponent<Transform>();

        _Player = GameObject.Find("Player");

        int RndNumberRight = Random.Range(0, 2);

        // Debug.Log(RndNumber);

        switch (RndNumberRight)
        {
            case 0:
                WalkRight = true;

                break;

            case 1:
                WalkRight = false;

                break;
        }


        int RndNumberUp = Random.Range(0, 2);

        // Debug.Log(RndNumber);

        switch (RndNumberUp)
        {
            case 0:
                WalkUp = true;

                break;

            case 1:
                WalkUp = false;

                break;
        }

        if (WalkWithinSquare==true )
        {
            Xmin = Corner1.transform.position.x;
            Xmax = Corner3.transform.position.x;

            Zmin = Corner1.transform.position.z;
            Zmax = Corner3.transform.position.z;
    

            GenerateRandomDestination();
        }


        AttackAnimationsList = new string[2];
        AttackAnimationsList[0] = "Attack_01";
        AttackAnimationsList[1] = "Attack_02";



    }

    public void GenerateRandomDestination()
    {
        float DestinationX = Random.Range(Mathf.RoundToInt(Xmin), Mathf.RoundToInt(Xmax));

        float DestinationZ = Random.Range(Mathf.RoundToInt(Zmin), Mathf.RoundToInt(Zmax));


        destinationPosition.x = DestinationX;
        destinationPosition.z = DestinationZ;
        destinationPosition.y = 0;

        moveSpeed = 1;

        trans.position = Vector3.MoveTowards(trans.position, destinationPosition, moveSpeed * Time.deltaTime);

    }


    // Update is called once per frame
  private  void Update()
    {
        if (IsAttacking == false)
        {
            if (IsPlayingHit == false) 
            {

                if (WalkWithinSquare == false)

                {
                    if (WalkRight == false)
                    {
                        if (Health > 0)
                        {
                            MoveToLeft();

                        }

                    }
                    else if (WalkRight == true)
                    {
                        if (Health > 0)
                        { MoveToRight(); }

                    }

                    if (trans.position.z >= LeftMost.position.z)
                    {
                        if (Health > 0)
                        {
                            WalkRight = true;
                            MoveToRight();
                        }
                    }

                    if (trans.position.z <= RightMost.position.z)
                    {
                        if (Health > 0)
                        {
                            WalkRight = false;
                            MoveToLeft();
                        }
                    }

                }
                else if (WalkWithinSquare == true)
                {
                    if (Health > 0)
                    {
                       

                        CheckDistanceAndUpdateSpeed();

                    }


                }





            }

           

        }
        else if (IsAttacking == true)
        {
            if (Health > 0)
            {
                LookAtPlayer();
            }

        }


    }



    void CheckDistanceAndUpdateSpeed()
    {
        destinationDistance = Vector3.Distance(destinationPosition, trans.position);
        if (destinationDistance < 1f)
        {       // To prevent shakin behavior when near destination
            moveSpeed = 0;

            GenerateRandomDestination();

        }
        else if (destinationDistance > 1f)
        {           // To Reset Speed to default
            moveSpeed = 1;
            trans.position = Vector3.MoveTowards(trans.position, destinationPosition, moveSpeed * Time.deltaTime);

            trans.LookAt(destinationPosition);

            anim.Play("Walk");
            CurrentAnimationName = "Walk";
            anim["Walk"].speed = WalkAnimSpeed;
        }




    }


    public void MoveToRight()
    {
        //face to right
        Quaternion Rotatio = trans.rotation;

        Rotatio.y = 180;

        Rotatio.x = 0;
        Rotatio.z = 0;

        trans.rotation = Rotatio;

        //move to right
        Vector3 NewPosVect;
        NewPosVect = trans.position;

        NewPosVect.z = NewPosVect.z - WalkIncrement;


        trans.position = NewPosVect;





        anim.Play("Walk");
        CurrentAnimationName = "Walk";
        anim["Walk"].speed = WalkAnimSpeed;


    }

    public void MoveToLeft()
    {
        //face to left
        Quaternion Rotatio = trans.rotation;

        Rotatio.y = 0;
        Rotatio.x = 0;
        Rotatio.z = 0;

        trans.rotation = Rotatio;

        //move to left
        Vector3 NewPosVect;
        NewPosVect = trans.position;

        NewPosVect.z = NewPosVect.z + WalkIncrement;

        trans.position = NewPosVect;



        anim.Play("Walk");
        CurrentAnimationName = "Walk";

        anim["Walk"].speed = WalkAnimSpeed;



    }

}
