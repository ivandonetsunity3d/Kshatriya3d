using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotKindEnum
{
    NotSet,
    InventoryBig,
    InventorySmall,
    Head,
    Armor,
    LeftHand,
    RightHand,
    Amulet,
    Ring,
    Shop
};


public enum SmallBigItemEnum
{
    NotSet,
    SmallItem,
    BigItem
};

public enum CurrentPanelEnum
{
    NotSet,
    Griswold,
    Adria,
    Pepin,
    Wirt,
    Inventory,
    Character
};

public enum ItemKindEnum
{
    NotSet,
    Sword,
    Axe,
    Mace,
    Staff,
    Bow,
    Shield,
    Helm,
    Armor,
    Ring,
    Amulet,
    Potion,
    Scroll
};

public enum ItemCategoryEnum
{
    NotSet,
    Weapon,
    Armor,    
    Jewelry,
    Potion,
    Scroll,
    Book
};


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler,  IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
   

    
    public Transform StartParent;
    public Vector3 StartPosition;

    public DragDropMemory DragDropHolderScript;
    public GameObject  DragDropHolderGO;

    public GameObject me;

    public RaycastHit hit;
    public Ray ray;

    public ItemKindEnum ItemKind= ItemKindEnum.NotSet ;
    public SmallBigItemEnum ItemSize = SmallBigItemEnum.NotSet;
    public ItemCategoryEnum ItemCategory;

    // public SlotKindEnum SlotKind = SlotKindEnum.NotSet;

    public string ObjectUnderCursorName;
    public GameObject ObjectUnderCursorGO;


    public string mantra = "";


    public string _name= "";

    public int price =0;

    public int DamageMin=0;

    public int DamageMax=0;

    public int DefenceMin = 0;

    public int DefenceMax = 0;

    public int AppearanceLevel=1 ;

    public int Durability =0;

    public int Hands =0;

    public int RequiredDex=0;

    public int RequiredStr=0;



    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosition = transform.position;
        StartParent = gameObject.transform.parent;
        
        me = gameObject;
        
        DragDropHolderGO = GameObject.Find("Canvas");

        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();
       
        DragDropHolderScript.ItemBeingDragged = me;

        DragDropHolderScript.ParentOfItemBeingDragged = me.transform.parent.gameObject;

        Slot SlotScript = me.transform.parent.GetComponent<Slot>();
        
        DragDropHolderScript.SlotKind = SlotScript.SlotKind;

        DragDropHolderScript.NowDragging = true;


        GameObject _player= GameObject.Find("Player");
        //ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript = _player.GetComponent<ClickToAttackOrMoveNavMesh>();

        NavMeshPlayer NavMeshPlayerScript= _player.GetComponent<NavMeshPlayer>();

        //ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;

        NavMeshPlayerScript.RaycastingUI = true;



    }

    public void OnDrag(PointerEventData eventData)
    {  
        transform.position = Input.mousePosition;
        GameObject _player = GameObject.Find("Player");
        //ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript = _player.GetComponent<ClickToAttackOrMoveNavMesh>();

        //ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;

        NavMeshPlayer NavMeshPlayerScript = _player.GetComponent<NavMeshPlayer>();
        NavMeshPlayerScript.RaycastingUI = true;

    }



    public void OnEndDrag(PointerEventData eventData)
    {
     
        if (DragDropHolderScript.ParentOfItemBeingDragged != StartParent)
        {
            transform.position = StartPosition;
          
            DragDropHolderScript.ParentOfItemBeingDragged = null;
            DragDropHolderScript.ItemBeingDragged = null;
        } 
        DragDropHolderScript.NowDragging = false;

        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragDropHolderGO = GameObject.Find("Canvas");

        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();

       
        DragDropHolderScript.ShouldShowToolTip = true;

        Slot SlotScript= transform.parent.GetComponent<Slot>();
        //DragDropHolderScript.CurrentPanel = SlotScript.CurrentPanel;

        //switch (DragDropHolderScript.CurrentPanel)
        //{
        //    case CurrentPanelEnum.Inventory:
                DragDropHolderScript.HintPanel.SetActive(true);
                DragDropHolderScript.HintName.text = _name;

                DragDropHolderScript.HintPrice.text = price.ToString() + "$";


                switch (ItemCategory)
                {
                    case ItemCategoryEnum.Weapon:
                        DragDropHolderScript.HintInfo.text = "Damage: " + DamageMin + "-" + DamageMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

                        break;
                  
                    case ItemCategoryEnum.Armor:
                        DragDropHolderScript.HintInfo.text = "Defence: " + DefenceMin + "-" + DefenceMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

                        break;

                    case ItemCategoryEnum.Scroll:
                        DragDropHolderScript.HintInfo.text = "Mantra: " + mantra ;
                        DragDropHolderScript.MantraText.text = mantra;
                        DragDropHolderScript.MantraPanel.SetActive(true);
                
                        break   ;
        }


              //  break;


            //case CurrentPanelEnum.Griswold:
            //    DragDropHolderScript.GriswoldHint.SetActive(true);
            //    DragDropHolderScript.GriswoldName.text = _name;

            //    DragDropHolderScript.GriswoldPrice.text = price.ToString() + "$";

            //    //DragDropHolderScript.ItemBeingDragged.

            //    switch (ItemCategory)
            //    {
            //        case ItemCategoryEnum.Weapon:
            //            DragDropHolderScript.GriswoldInfo.text = "Damage: " + DamageMin + "-" + DamageMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

            //            break;                 


            //        case ItemCategoryEnum.Armor :
            //            DragDropHolderScript.GriswoldInfo.text = "Defence: " + DefenceMin + "-" + DefenceMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

            //            break;
            //    }


                


            //    break;


            //case CurrentPanelEnum.Adria:
            //    DragDropHolderScript.AdriaHint.SetActive(true);
            //    DragDropHolderScript.AdriaName.text = _name;

            //    DragDropHolderScript.AdriaPrice.text = price.ToString() + "$";


            //    switch (ItemCategory)
            //    {
                   
            //        case ItemCategoryEnum.Weapon:
            //            DragDropHolderScript.AdriaInfo.text = "Damage: " + DamageMin + "-" + DamageMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

            //            break;

            //        case ItemCategoryEnum.Armor:
            //            DragDropHolderScript.AdriaInfo.text = "Defence: " + DefenceMin + "-" + DefenceMax + "\n" + "Durability: " + Durability.ToString() + "\n" + "Required Str: " + RequiredStr.ToString() + "\n" + "Required Dex: " + RequiredDex.ToString();

            //            break;
            //    }


            //     break;

      //  }



      





       // Debug.Log("Show hint");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragDropHolderScript.ShouldShowToolTip = false ;

        DragDropHolderGO = GameObject.Find("Canvas");

        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();


        DragDropHolderScript.HintName.text = "";

        DragDropHolderScript.HintPrice.text = "";

        DragDropHolderScript.HintInfo.text = "";

        DragDropHolderScript.HintPanel.SetActive(false);

        DragDropHolderScript.MantraText.text = "";
        DragDropHolderScript.MantraPanel.SetActive(false );

        //switch (DragDropHolderScript.CurrentPanel)
        //{
        //    case CurrentPanelEnum.Inventory:


        //        break;

        //case CurrentPanelEnum.Griswold:
        //    DragDropHolderScript.GriswoldName.text = "";

        //    DragDropHolderScript.GriswoldPrice.text = "";

        //    DragDropHolderScript.GriswoldInfo.text = "";

        //    DragDropHolderScript.GriswoldPanel.SetActive(false);

        //    break;

        //case CurrentPanelEnum.Adria:
        //    DragDropHolderScript.AdriaName.text = "";

        //    DragDropHolderScript.AdriaPrice.text = "";

        //    DragDropHolderScript.AdriaInfo.text = "";

        //    DragDropHolderScript.AdriaPanel.SetActive(false);

        //    break;
        //}




        // Debug.Log("Hide Hint");

    }
}
