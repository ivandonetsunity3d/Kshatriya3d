using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public string[] MagicAnimationsList;
    public string[] DieAnimationsList;
    public string[] HitAnimationsList;


    public GameObject MagicBallPrefab;
    public GameObject IceBallPrefab;

    public Transform MagicBallSpawn;

    private Animation anim;

    public Transform PlayerTransform;
    public float JumpIncrement = 0.5f;
    public float WalkIncrement = 0.5f;
    public float WalkAnimSpeed = 2f;

    public float MagicAnimSpeed = 0.5f;

    public float XConstPositionGoRight = 8.65f;
    public float XConstPositionGoLeft = 9.65f;

    MagicBall MagicBallScript;

    public string CurrentAnimationName;
    public string CurrentAnimationType;


    //public Material Material1;
    //public Material Material2;
    //public Material Material3;
    //public Material Material4;
    //public Material Material5;
    //public Material Material6;
    //public Material Material7;

    public int SelectedMagicNumber = 2;

    public bool WalkRight = true;

    public int Health=100;

    public bool CastingSpell = false;

    public bool FireInQuee = false;

    public bool PlayingAttackAnim = false;

   //public bool isPlaying = false;


    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();

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
        //MagicAnimationsList[25] = "";

        //MagicAnimationsList[26] = "Praying";





        DieAnimationsList = new string[3];
        DieAnimationsList[0] = "Dying Backwards";
        DieAnimationsList[1] = "Flying Back Death";
        DieAnimationsList[2] = "Sword And Shield Death";

        HitAnimationsList = new string[2];
        HitAnimationsList[0] = "Hit";
        HitAnimationsList[1] = "Hit2";

        PlayerTransform = GetComponent<Transform>();

    }

    public void StartAttackAnimPlay()
    {
        PlayingAttackAnim = true;
    }

    void EndPlayingMagicCast()
    {

        //if (FireInQuee == true)
        //{
        //    anim.Rewind("Staff-Cast-Attack2");

        //    //PlayingAttackAnim = false;

        //    //CastingSpell = false;

        //    // Fire();
        //    FireInQuee = false;

        //}
        ////else
        //if (FireInQuee == false)
        {
            PlayingAttackAnim = false;
            CastingSpell = false;

            CurrentAnimationName = "Idle";
            anim.Play(CurrentAnimationName);
        }

    }

    public void InstantiateIceBall()
    {
        var IcyBall = (GameObject)Instantiate(
           IceBallPrefab,
           MagicBallSpawn.position,
           MagicBallSpawn.rotation);


        // Add velocity to the bullet
        IceBall IceBallScript = IcyBall.GetComponent<IceBall>();

        //if (WalkRight == true) 
        {
            IceBallScript.StartMoving(WalkRight);

        }
        
        // Destroy the ball after 30 seconds
        Destroy(IcyBall, 1.0f);
    }

    void InstantiateMagicBall()
    {
        //Material MagicBallMaterial = MagicBall.GetComponent<Material>();

        // MagicBallMaterial = Material7;

        //Material[] mats;
        //mats = MagicBall.GetComponent<Renderer>().materials;

        //switch (MagicNumber)
        //{
        //    case 1:
        //        mats[0] = Material1;

        //        break;

        //    case 2:
        //        mats[0] = Material2;

        //        break;


        //    case 3:
        //        mats[0] = Material3;

        //        break;

        //    case 4:
        //        mats[0] = Material4;

        //        break;

        //    case 5:
        //        mats[0] = Material5;

        //        break;


        //    case 6:
        //        mats[0] = Material6;

        //        break;


        //    case 7:
        //        mats[0] = Material7;
        //        break;
        //}

        // MagicBall.GetComponent<Renderer>().materials = mats;



        // Create the Bullet from the Bullet Prefab
        var MagicBall = (GameObject)Instantiate(
            MagicBallPrefab,
            MagicBallSpawn.position,
            MagicBallSpawn.rotation);


        // Add velocity to the bullet
        MagicBallScript = MagicBall.GetComponent<MagicBall>();

        //if (WalkRight == true) 
        {
            MagicBallScript.StartMoving(WalkRight);

        }
        //else if  (WalkRight == false )
        //{
        //MagicBallScript.StartMoving();

        //} 


        // MagicBall.GetComponent<Rigidbody>().velocity = MagicBall.transform.right * 6;

        // Destroy the ball after 30 seconds
        Destroy(MagicBall, 0.2f);
    }

    void Fire()
    {


        switch (SelectedMagicNumber)
        {
            case 1:
                anim.Play("Staff-Cast-Attack1");
                CurrentAnimationName = "Staff-Cast-Attack1";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;

            case 2:
                //int RndNumber = Random.Range(0, 11);

                int RndNumber = Random.Range(0, 25);

                CurrentAnimationName = MagicAnimationsList[RndNumber];

                //DieAnimationsList


                //CurrentAnimationName = "1H Magic Attack 01";

                anim.Play(CurrentAnimationName);
                CurrentAnimationType = "Magic";

                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                //anim.PlayQueued("Staff-Cast-Attack2");
                //PlayingAttackAnim = true;
                CastingSpell = true;
                

                break;

            case 3:
                anim.Play("Staff-Cast-Attack3");
                CurrentAnimationName = "Staff-Cast-Attack3";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;

            case 4:
                anim.Play("Staff-Cast-Attack1");
                CurrentAnimationName = "Staff-Cast-Attack1";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;

            case 5:
                anim.Play("Staff-Cast-Attack2");
                CurrentAnimationName = "Staff-Cast-Attack2";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;

            case 6:
                anim.Play("Staff-Cast-Attack3");
                CurrentAnimationName = "Staff-Cast-Attack3";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;

            case 7:
                anim.Play("Staff-Cast-Attack2");
                CurrentAnimationName = "Staff-Cast-Attack2";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;

                CastingSpell = true;
                break;
        }





    }

    void Jump()
    {
        Vector3 NewPosVect;
        NewPosVect = PlayerTransform.position;
        NewPosVect.y = NewPosVect.y + JumpIncrement;
        PlayerTransform.position = NewPosVect;
    }

    public void ChangeHealth(int ChangeValue)
    {
        Health = Health + ChangeValue;

        if (Health<=0)
        {
            if (CurrentAnimationType != "Die") 
            {
                int RndNumber = Random.Range(0, 3);

                CurrentAnimationName = DieAnimationsList[RndNumber];

                
                
                anim.Play(CurrentAnimationName);
                CurrentAnimationType = "Die";
            }
            

        }
        else if (Health > 0)
        {
            //if (CurrentAnimationType != "Hit") 
            {
                int RndNumber = Random.Range(0, 2);

                CurrentAnimationName = HitAnimationsList[RndNumber];


                

                anim.Play(CurrentAnimationName);
                CurrentAnimationType = "Hit";
            }
            
           


        }
    }

    public void DeadAnimPlayed()
    {
       // string DeathAnimationName = "Staff-Death";
       //float AnimLen = anim[DeathAnimationName].length;

        //anim["Die"].time = 1.99f;

        // CurrentFrameNumber = anim[DeathAnimationName].time;


        //anim[DeathAnimationName].speed = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {




                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }

                Fire();


            }

            if (Input.GetKey(KeyCode.F))
            {
                ThrowIceMagic();

            }


            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                SelectedMagicNumber = 1;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();

            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                SelectedMagicNumber = 2;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                SelectedMagicNumber = 3;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                SelectedMagicNumber = 4;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                SelectedMagicNumber = 5;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                SelectedMagicNumber = 6;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                SelectedMagicNumber = 7;
                if (CastingSpell == true)
                {
                    FireInQuee = true;
                }
                if (PlayingAttackAnim == true)
                {
                    anim.Rewind();

                    //FireInQuee = true;               

                }
                Fire();
            }
            if (Input.GetKey(KeyCode.Q))
            {
                anim.Play("2Hand-Spear-Attack5");
                CurrentAnimationName = "2Hand-Spear-Attack5";
                anim[CurrentAnimationName].speed = MagicAnimSpeed;
                CurrentAnimationType = "Magic";

            }



            {
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    anim.Play("Idle");
                    CurrentAnimationName = "Idle";
                    CurrentAnimationType = "Idle";


                }

                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    anim.Play("Idle");
                    CurrentAnimationName = "Idle";
                    CurrentAnimationType = "Idle";

                }


                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Jump();
                }






                //==============================


                if (Input.GetKey(KeyCode.RightArrow))
                {
                    // if (Health > 0)
                    {
                        WalkRight = true;
                        anim.Play("Walk");
                        CurrentAnimationName = "Walk";
                        anim["Walk"].speed = WalkAnimSpeed;

                        CurrentAnimationType = "Walk";

                        Quaternion Rotatio = PlayerTransform.rotation;

                        Rotatio.y = 180;

                        Rotatio.x = 0;
                        Rotatio.z = 0;


                        PlayerTransform.rotation = Rotatio;


                        Vector3 NewPosVect;
                        NewPosVect = PlayerTransform.position;

                        NewPosVect.z = NewPosVect.z - WalkIncrement;
                        NewPosVect.x = XConstPositionGoRight;

                        PlayerTransform.position = NewPosVect;
                    }


                }






                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    // if (Health > 0)
                    {
                        WalkRight = false;
                        anim.Play("Walk");
                        CurrentAnimationName = "Walk";
                        anim["Walk"].speed = WalkAnimSpeed;

                        CurrentAnimationType = "Walk";

                        Quaternion Rotatio = PlayerTransform.rotation;

                        Rotatio.y = 0;
                        Rotatio.x = 0;
                        Rotatio.z = 0;

                        PlayerTransform.rotation = Rotatio;

                        Vector3 NewPosVect;
                        NewPosVect = PlayerTransform.position;

                        NewPosVect.z = NewPosVect.z + WalkIncrement;
                        NewPosVect.x = XConstPositionGoLeft;



                        PlayerTransform.position = NewPosVect;
                    }

                }

            }
          



                if (Input.GetKey(KeyCode.Escape))
                {
                    SpeechControl spCtrl = this.gameObject.GetComponent<SpeechControl>();
                    spCtrl.CloseSpeechPanel();

                }

               
                

            
        }




    }



    public void ThrowIceMagic()
    {
        anim.Play("Staff-Cast-IceBall");
        CurrentAnimationName = "Staff-Cast-IceBall";
        anim[CurrentAnimationName].speed = MagicAnimSpeed;

        CurrentAnimationType = "Magic";

        CastingSpell = true;

    }







}
