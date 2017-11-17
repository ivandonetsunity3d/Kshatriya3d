using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _Player;
   // public ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript;
    public NavMeshPlayer NavMeshPlayerScript;

    // Use this for initialization
    void Start () {
        _Player = GameObject.Find("Player");
        //  ClickToAttackOrMoveNavMeshScript = _Player.GetComponent<ClickToAttackOrMoveNavMesh>();
        NavMeshPlayerScript = _Player.GetComponent<NavMeshPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnPointerEnter(PointerEventData eventData)
    {
      //  ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;

        NavMeshPlayerScript.RaycastingUI = true;

        //ClickToAttackOrMoveNavMeshScript.InventoryOrShopIsOpen = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // ClickToAttackOrMoveNavMeshScript.RaycastingUI = false;
        NavMeshPlayerScript.RaycastingUI = false;

        // ClickToAttackOrMoveNavMeshScript.InventoryOrShopIsOpen = false;

    }
}


