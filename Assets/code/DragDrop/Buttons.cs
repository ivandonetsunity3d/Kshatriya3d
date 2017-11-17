using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Buttons : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {

    public GameObject InventoryPanel;

    public GameObject ShopPanel;

    public GameObject GriswoldPanel;
    public GameObject AdriaPanel;
    public GameObject WirtPanel;
    public GameObject PepinPanel;

    public GameObject FillPanel;


    public GameObject _Player;
    public ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript;

    public NavMeshPlayer NavMeshPlayerScript;
    // Use this for initialization
    void Start ()
    {
        _Player = GameObject.Find("Player");
        //try
        //{
        //    ClickToAttackOrMoveNavMeshScript = _Player.GetComponent<ClickToAttackOrMoveNavMesh>();
        //}
        //catch
        //{
        //}
        NavMeshPlayerScript = _Player.GetComponent<NavMeshPlayer>();
       
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

  
    public void CheckActiveState()
    {
        switch (ShopPanel.activeSelf)
        {
            case true:
                if (NavMeshPlayerScript.RaycastingUI == false)
                {
                    NavMeshPlayerScript.RaycastingUI = true;

                }


                //if (ClickToAttackOrMoveNavMeshScript.RaycastingUI == false)
                //{
                //    ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;

                //}


                break;

            case false:

                if (InventoryPanel.activeSelf == false)
                {
                  
                    {
                        //ClickToAttackOrMoveNavMeshScript.RaycastingUI = false;
                        NavMeshPlayerScript.RaycastingUI = false;

                        
                    }
                }
                else if (InventoryPanel.activeSelf == true)
                {
                    if (NavMeshPlayerScript.RaycastingUI == false)
                    {
                        NavMeshPlayerScript.RaycastingUI = true;

                    }
                    //if (ClickToAttackOrMoveNavMeshScript.RaycastingUI == false)
                    //{
                    //    ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;

                    //}
                }

                break;
        }


    }


    public void FillPanelSetActive(bool activestate)
    {
        FillPanel.SetActive(activestate);
    }

    public void _ShopOpen(CurrentPanelEnum CurrentPanel)
    {
        switch (CurrentPanel)
        {
            case CurrentPanelEnum.Griswold:
                ShopPanel = GriswoldPanel;
                break;

            case CurrentPanelEnum.Adria:
                ShopPanel = AdriaPanel;
                break;

            case CurrentPanelEnum.Pepin:
                ShopPanel = PepinPanel;
                break;

            case CurrentPanelEnum.Wirt:
                ShopPanel = WirtPanel;
                break;
        }

        ShopPanel.SetActive(true);
        CheckActiveState();

    }

    public void _ShopClose(CurrentPanelEnum CurrentPanel)
    {
        switch (CurrentPanel)
        {
            case CurrentPanelEnum.Griswold:
                ShopPanel = GriswoldPanel;
                break;

            case CurrentPanelEnum.Adria:
                ShopPanel = AdriaPanel;
                break;

            case CurrentPanelEnum.Pepin:
                ShopPanel = PepinPanel;
                break;

            case CurrentPanelEnum.Wirt:
                ShopPanel = WirtPanel;
                break;
        }

        ShopPanel.SetActive(false );
        CheckActiveState();

        FillPanel.SetActive(false);
        _InventoryClose();

    }

    public void _ShopOpenClose(CurrentPanelEnum CurrentPanel)
    {
        switch (CurrentPanel)
        {
            case CurrentPanelEnum.Griswold:
                ShopPanel = GriswoldPanel;
                break;

            case CurrentPanelEnum.Adria:
                ShopPanel = AdriaPanel;
                break;

            case CurrentPanelEnum.Pepin:
                ShopPanel = PepinPanel;
                break;

            case CurrentPanelEnum.Wirt:
                ShopPanel = WirtPanel;
                break;
        }

        ShopPanel.SetActive(!ShopPanel.activeSelf);

        CheckActiveState();

    }


    //public void _GriswoldOpen()
    //{
    //    ShopPanel = GriswoldPanel;

    //    _ShopOpen();

    //}

    public void _GriswoldClose()
    {
        ShopPanel = GriswoldPanel;

        _ShopClose(CurrentPanelEnum.Griswold);

    }

    public void _AdriaClose()
    {
        ShopPanel = AdriaPanel;

        _ShopClose(CurrentPanelEnum.Adria);

    }

    public void _WirtClose()
    {
        ShopPanel = WirtPanel;

        _ShopClose(CurrentPanelEnum.Wirt);

    }

    public void _PepinClose()
    {
        ShopPanel = PepinPanel;

        _ShopClose(CurrentPanelEnum.Pepin);

    }

    public void _GriswoldOpenClose()
    {
        ShopPanel = GriswoldPanel;

        _ShopOpenClose(CurrentPanelEnum.Griswold);

    }

    public void _PepinOpenClose()
    {
        ShopPanel = PepinPanel;

        _ShopOpenClose(CurrentPanelEnum.Pepin);

    }

    public void _AdriaOpenClose()
    {
        ShopPanel = AdriaPanel;

        _ShopOpenClose(CurrentPanelEnum.Adria);

    }

    public void _WirtOpenClose()
    {
        ShopPanel = WirtPanel;

        _ShopOpenClose(CurrentPanelEnum.Wirt);

    }




    public void _InventoryOpenClose()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        if (InventoryPanel.activeSelf == true)
        {
            CheckActiveState();

        }

    }

    public void _InventoryOpen()
    {
        InventoryPanel.SetActive(true);
        CheckActiveState();

    }

    public void _InventoryClose()
    {
        InventoryPanel.SetActive(false);
        CheckActiveState();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;
        NavMeshPlayerScript.RaycastingUI = true;

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ClickToAttackOrMoveNavMeshScript.RaycastingUI = false;
        NavMeshPlayerScript.RaycastingUI = false;

    }
}
