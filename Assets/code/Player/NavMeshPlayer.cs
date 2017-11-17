using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum CurrentAnimationTypeEnum
{
    NotSet,
    Attack,
    Idle,
    Walk,
    Hit,
    Die
};

public enum ActionAfterApproachEnum
{
    NotSet,
    Attack,
    Worship,
    PickUp,
    Talk,
    Stop
};

public class NavMeshPlayer : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject ClickedPoint;

    public ActionAfterApproachEnum ActionAfterApproach = ActionAfterApproachEnum.NotSet;

    public GameObject ClickedGameObject;
    public GameObject ClickedEnemyGameObject;


    public GameObject GameObjectUnderCursor;

    public Text ObjectUnderCursorLabel;




    public string CurrentAnimationName;
    public CurrentAnimationTypeEnum CurrentAnimationType= CurrentAnimationTypeEnum.NotSet;


    public bool RaycastingUI = false;

    public int Health = 100;

    public int Mana = 100;

    public int Dollars = 0;

    //public int HealthBottlesQuantity = 0;
    //public int ManaBottlesQuantity = 0;
    //public int RegenerationBottlesQuantity = 0;

    public int EnemiesKilled = 0;

    public bool IsWalking = false;

    public bool IsAttacking = false;

    public bool ApproachAndAct = false;

    public bool FreezeModeOn = false;


    public Animation PlayerAnimation;
    public Transform PlayerTransform;

    public Enemy EnemyScript;

    public Potion PotionScript;
    public PickUpAbleItem ItemScript;


    public Vector3 destinationPosition;         // The destination Point
    public float destinationDistance;           // The distance between myTransform and destinationPosition

    public float moveSpeed;                     // The Speed the character will move

    public RaycastHit hit;
    public Ray ray;

    public void ModifyHealth(int Value)
    {
        Health = Health + Value;

        if (Health <= 0)
        {
            if (CurrentAnimationType != CurrentAnimationTypeEnum.Die)
            {
                //int RndNumber = Random.Range(0, 3);

                //CurrentAnimationName = DieAnimationsList[RndNumber];

                CurrentAnimationName = "Die";

               // PlayerAnimation.Play(CurrentAnimationName);
                CurrentAnimationType = CurrentAnimationTypeEnum.Die;
            }


        }
        else if (Health > 0)
        {
            //if (CurrentAnimationType != "Hit") 
            {
                //int RndNumber = Random.Range(0, 2);

                //CurrentAnimationName = HitAnimationsList[RndNumber];

                CurrentAnimationName = "Hit";

               // PlayerAnimation.Play(CurrentAnimationName);
                CurrentAnimationType = CurrentAnimationTypeEnum.Hit;
            }
        }



    }


    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        PlayerTransform = transform;

        agent = PlayerTransform.GetComponent<UnityEngine.AI.NavMeshAgent>();

        PlayerAnimation = PlayerTransform.GetComponent<Animation>();

        destinationPosition = PlayerTransform.position;
    }

    void CheckStopDistance(string AnimationToPlay)
    {
        if (destinationDistance < 1.4f)
        {
            agent.speed = 0;

            IsWalking = false;
            PlayerAnimation.Play(AnimationToPlay);

            //PlayerAnimation.Play("Idle");
            // agent.isStopped = true;
            agent.enabled = false;

        }
    }

    void Update()
    {
        //just change text label of object under cursor on Canvas (so it's like in Diablo when mouseover)
        RaycastUnderCursor();

        if (Health > 0)
        {
            if (RaycastingUI == false)

            {
                RayCastWhenMouseLeft();
                //RaycastUnderCursor();
            }

            //switch (ClickedGameObject.name)
            switch(ActionAfterApproach)
            {
                case ActionAfterApproachEnum.Stop:// "Terrain":                   
                    //ActionAfterApproach = ActionAfterApproachEnum.Stop;

                    ClickedOnTerrainPoint(true);
                    break;

                case ActionAfterApproachEnum.Attack:// "Enemy":
                    //ApproachAndAct = true;// false;
                    //ActionAfterApproach = ActionAfterApproachEnum.Attack;

                    //ClickedEnemyGameObject = hit.transform.gameObject;
                    //ObjectUnderCursorLabel.text = ClickedEnemyGameObject.name;

                    ClickedOnEnemy(true);
                    break;
            }


           

            }
    }

    void RayCastWhenMouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                ClickedGameObject = hit.transform.gameObject;
                ObjectUnderCursorLabel.text = ClickedGameObject.name;

                switch (ClickedGameObject.name)
                {
                    case "Terrain":
                        ApproachAndAct = true;// false;
                        ActionAfterApproach = ActionAfterApproachEnum.Stop ;

                        ClickedOnTerrainPoint(false );
                        break;

                    case "Enemy":
                        ApproachAndAct = true;// false;
                        ActionAfterApproach = ActionAfterApproachEnum.Attack;

                        ClickedEnemyGameObject = hit.transform.gameObject;
                        ObjectUnderCursorLabel.text = ClickedEnemyGameObject.name;

                        ClickedOnEnemy( false );
                        break;
                }
            }
        }
    }

   


    void ClickedOnEnemy(bool Inertia)
    {
        if (Inertia == false)
        {
            //click first time
            ApproachAndAct = true;
            ActionAfterApproach = ActionAfterApproachEnum.Attack;

            destinationPosition = ClickedEnemyGameObject.transform.position;


        }
        //else if (Inertia == true)
        //{

        //}

          


        destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

        if (destinationDistance < 2f)
        {
            agent.enabled = false;
            //agent.destination = destinationPosition;
            //agent.isStopped = true;

            PlayerShouldAttackEnemy();
        }
        else if (destinationDistance > 2f)
        {
            IsAttacking = false;

            PointToWalk_AttackMode();
        }


    }

    //just change label like in Diablo
    void RaycastUnderCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits)
        {
            GameObjectUnderCursor = hit.transform.gameObject;

            ObjectUnderCursorLabel.text = GameObjectUnderCursor.name;           

        }
    }

    void PointToWalk_AttackMode()
    {
        destinationPosition = ClickedEnemyGameObject.transform.position;
        //SetDestinationPosition();

        destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);
        if (destinationDistance > 2f)
        {
            MoveAgent();

            Walk();
        }
        else
        {
            agent.enabled = false;
            PlayerAnimation.Play("Idle");
            IsAttacking = false;
            ActionAfterApproach = ActionAfterApproachEnum.Stop;
            ApproachAndAct = false;

        }

    }

 

    void ClickedOnTerrainPoint(bool Inertia)
    {
        //inertia means to move even when not clicked - in next frames in Update()
        if (Inertia == false)
        {
            SetDestinationPosition();

        }

        ClickedPoint.transform.position = destinationPosition;

        destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

        if (destinationDistance > 0.5f)
        {
            MoveAgent();

            Walk();
        }
        else if (destinationDistance <= 0.5f)
        {
            agent.enabled = false;
            PlayerAnimation.Play("Idle");
        }
        
    }

    //void SetNotWalkDestinationPosition()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit[] hits = Physics.RaycastAll(ray);

    //    foreach (RaycastHit hit in hits)
    //    {
    //        if (hit.transform.name == "Terrain")
    //        {

    //        }
    //        else if (hit.transform.name == "Enemy")
    //        {
    //            destinationPosition = hit.transform.position;

    //            agent.enabled = true;
    //            agent.destination = destinationPosition;
    //            agent.isStopped = false;

    //        }

    //    }
    //}

   // void SetNotWalkDestinationPosition()
   // {
   //     destinationPosition = ClickedGameObject.transform.position;

   // }

    
   //void SetAttackDestinationPosition()
   // {
   //     destinationPosition = ClickedEnemyGameObject.transform.position;

   // }


    void SetDestinationPosition()
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

        }

    }

    void MoveAgent()
    {
        agent.enabled = true;
        agent.destination = destinationPosition;
        agent.isStopped = false;

      //  destinationPosition = hittransformgameObject.transform.position;
       // PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, destinationPosition, moveSpeed * Time.deltaTime);

    }

    void Walk()
    {
        IsWalking = true;
        PlayerAnimation.Play("Walk");
    }


    void PlayerShouldAttackEnemy()
    {
        if (ClickedGameObject != null)
        {
            Vector3 forward = PlayerTransform.TransformDirection(Vector3.forward);
            Vector3 toOther = ClickedGameObject.transform. position - PlayerTransform.position;
            if (Vector3.Dot(forward, toOther) > 0)
            {
                //Debug.Log ("The other transform is ahead of me!");

                if (Vector3.Angle(forward, toOther) <= 45)
                {
                    PlayerTransform.LookAt(ClickedEnemyGameObject.transform.position);

                    destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);
                                        
                    IsAttacking = true;
                        
                            try
                            {
                                EnemyScript = ClickedGameObject.transform.GetComponent<Enemy>();
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



    //void ActOrApproachTo(GameObject hittransformgameObject)
    //{
    //    destinationDistance = Vector3.Distance(destinationPosition, PlayerTransform.position);

    //    if (destinationDistance < 1.4f)
    //    {

    //        if (ClickedGameObject.name != "Terrain")
    //        {
    //            switch (ClickedGameObject.name)
    //            {
    //                case "Enemy":
    //                    PlayerShouldAttackEnemy();

    //                    break;


    //            }


    //            pickup
    //            worship
    //                attack etc

    //                if (PlayerAnimation.IsPlaying("Attack"))
    //            {
    //                PlayerShouldAttackEnemy();
    //            }
    //        }
    //        else if (ClickedGameObject.name == "Terrain")
    //        {
    //            if (destinationDistance < 0.75f)
    //            {
    //                To prevent shakin behavior when near destination
    //                agent.speed = 0;
    //                moveSpeed = 0;
    //                IsWalking = false;
    //                if (PlayerAnimation.IsPlaying("Walk"))
    //                {
    //                    PlayerAnimation.Play("Idle");
    //                }

    //                if (agent.isStopped == false)
    //                {
    //                    agent.destination = PlayerTransform.position;
    //                    agent.isStopped = true;
    //                    agent.enabled = false;
    //                }

    //            }

    //        }
    //    }
    //    else if (destinationDistance > 1.4f)
    //    {
    //        agent.speed = 3;

    //    }
    //}



    //void _animFunc_AttackTime()
    //{
    //    if (IsAttacking == true)
    //    {
    //        if (ClickedGameObject.transform != null)
    //        {
    //            try
    //            {
    //                EnemyScript = ClickedGameObject.transform.GetComponent<Enemy>();
    //                if (EnemyScript.Health > 0)
    //                {
    //                    PlayerTransform.LookAt(ClickedGameObject.transform);
                           
    //                    EnemyScript.ChangeHealth(-50);                       
    //                }
    //            }
    //            catch
    //            {
    //            }
    //        }
    //    }

    //}

    //public void _animFunc_AttackAnimPlayed()
    //{
    //    IsAttacking = false;
    //    PlayerAnimation.Play("Idle");
    //    ApproachAndAct = false;

    //}
















    void ModifyEnemiesKilled(int EnemiesNumber)
    {
        EnemiesKilled = EnemiesKilled + EnemiesNumber;
    }

}