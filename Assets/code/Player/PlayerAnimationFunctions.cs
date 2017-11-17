using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour {




    void _animFunc_AttackTime()
    {
    //    if (IsAttacking == true)
        {
      //      if (ClickedGameObject.transform != null)
            {
                try
                {
                    GameObject _player = GameObject.Find("Player");
                    ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript = _player.GetComponent<ClickToAttackOrMoveNavMesh>();


                    Enemy EnemyScript = ClickToAttackOrMoveNavMeshScript.EnemyTransform.GetComponent<Enemy>();
                    if (EnemyScript.Health > 0)
                    {
                        _player.transform.LookAt(ClickToAttackOrMoveNavMeshScript.EnemyTransform);

                        EnemyScript.ChangeHealth(-50);
                    }
                }
                catch
                {
                }

                try
                {
                    GameObject _player = GameObject.Find("Player");

                    NavMeshPlayer NavMeshPlayerScript = _player.GetComponent<NavMeshPlayer>();


                    Enemy EnemyScript = NavMeshPlayerScript.ClickedGameObject.transform.GetComponent<Enemy>();
                    if (EnemyScript.Health > 0)
                    {
                        _player.transform.LookAt(NavMeshPlayerScript.ClickedGameObject.transform );

                        EnemyScript.ChangeHealth(-50);
                    }
                }
                catch
                {
                }

                try
                {
                    GameObject _player = GameObject.Find("Player");

                    StableNMPlayer StableNMPlayerScript = _player.GetComponent<StableNMPlayer>();


                    Enemy EnemyScript = StableNMPlayerScript.EnemyTransform.GetComponent<Enemy>();
                    if (EnemyScript.Health > 0)
                    {
                        _player.transform.LookAt(StableNMPlayerScript.EnemyTransform);

                        EnemyScript.ChangeHealth(-50);
                    }
                }
                catch
                {
                }


                


            }
        }

    }

    public void _animFunc_AttackAnimPlayed()
    {

        try
        {
            GameObject _player = GameObject.Find("Player");
            NavMeshPlayer NavMeshPlayerScript = _player.GetComponent<NavMeshPlayer>();


            NavMeshPlayerScript.IsAttacking = false;
            NavMeshPlayerScript.PlayerAnimation.Play("Idle");
            NavMeshPlayerScript.ApproachAndAct = false;
        }
        catch
        {
        }

        try
        {
            GameObject _player = GameObject.Find("Player");
            StableNMPlayer StableNMPlayerScript = _player.GetComponent<StableNMPlayer>();


            StableNMPlayerScript.IsAttacking = false;
            StableNMPlayerScript.PlayerAnimation.Play("Idle");
            StableNMPlayerScript.ApproachAndAttack = false;
        }
        catch
        {
        }


    }



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
    

}
