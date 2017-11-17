using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragDropMemory : MonoBehaviour
{
    //public GameObject GriswoldHint;
    //public Text GriswoldName;
    //public Text GriswoldPrice;
    //public Text GriswoldInfo;

    //public GameObject AdriaHint;
    //public Text AdriaName;
    //public Text AdriaPrice;
    //public Text AdriaInfo;

    public GameObject HintPanel;
    public Text HintName;
    public Text HintPrice;
    public Text HintInfo;

    public GameObject MantraPanel;
    public Text MantraText;

    public GameObject InventoryPanel;
    public Inventory InventoryScript;

    public CurrentPanelEnum CurrentPanel= CurrentPanelEnum.NotSet ;

    public float OffsetX = 80f;
    public float OffsetY = -80f;

   
    public GameObject ItemBeingDragged;

    public SlotKindEnum SlotKind = SlotKindEnum.NotSet;


    public bool NowDragging = false;

    public GameObject ParentOfItemBeingDragged;

    public bool ShouldShowToolTip = false;

    private void Start()
    {
        InventoryScript= InventoryPanel.GetComponent<Inventory>();

    }

    public void Update()
    {

        if (HintPanel.activeSelf == true)
        {
            Vector3 pos = Input.mousePosition ; 

            pos.x = pos.x + OffsetX;
            pos.y = pos.y + OffsetY;

            HintPanel.transform.position = pos;

        }

        //if (AdriaHint.activeSelf == true)
        //{
        //    Vector3 pos = Input.mousePosition;

        //    pos.x = pos.x + OffsetX;
        //    pos.y = pos.y + OffsetY;

        //    AdriaHint.transform.position = pos;

        //}

        //if (InventoryHint.activeSelf == true)
        //{
        //    Vector3 pos = Input.mousePosition; 

        //    pos.x = pos.x + OffsetX;
        //    pos.y = pos.y + OffsetY;

        //    InventoryHint.transform.position = pos;

        //}



    }





    }
