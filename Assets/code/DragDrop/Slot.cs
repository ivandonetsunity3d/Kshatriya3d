using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;


public class Slot : MonoBehaviour, IDropHandler
{
    public CurrentPanelEnum CurrentPanel = CurrentPanelEnum.NotSet;

    public SmallBigItemEnum ItemSize = SmallBigItemEnum.NotSet;

    public SlotKindEnum SlotKind = SlotKindEnum.NotSet;

    DraggableItem DraggableItemScript;

    public  GameObject DragDropHolderGO;
    public  DragDropMemory DragDropHolderScript;

    

    public RaycastHit hit;
    public Ray ray;


   //public string ObjectUnderCursorName;
    public GameObject ObjectUnderCursorGO;

    public  GameObject InventoryGO;
    public  Inventory InvScript;

    public void Start()
    {
        //DragDropHolderGO = GameObject.Find("Canvas");

        //DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();

        //DraggableItemScript = DragDropHolderScript.ItemBeingDragged.GetComponent<DraggableItem>();


        // InventoryGO = GameObject.Find("Inventory");
        //InvScript = InventoryGO.GetComponent<Inventory>();

       
    }


    public void OnDrop(PointerEventData eventData)
    {  
         if (transform.childCount == 0)
            {
            DragDropHolderGO = GameObject.Find("Canvas");

            DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();

            DraggableItemScript = DragDropHolderScript.ItemBeingDragged.GetComponent<DraggableItem>();

            if (DraggableItemScript.ItemSize == ItemSize)
            {

                switch (DragDropHolderScript.SlotKind)

               // switch (DraggableItemScript.SlotKind)
                {
                    case SlotKindEnum.Shop:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.InventoryBig:
                                BuyFromShopToInventory();
                                break;

                            case SlotKindEnum.InventorySmall:
                                BuyFromShopToInventory();
                                break;

                            case SlotKindEnum.Amulet:
                                FromSlotToAmulet(true);
                                break;

                            case SlotKindEnum.Ring:
                                FromSlotToRing(true);
                                break;

                            case SlotKindEnum.RightHand:
                                FromSlotToRightHand(true);
                                break;

                            case SlotKindEnum.LeftHand:
                                FromSlotToLeftHand(true);
                                break;

                            case SlotKindEnum.Head:
                                FromSlotToHead(true);
                                break;

                            case SlotKindEnum.Armor:
                                FromSlotToArmor(true);
                                break;



                        }                       
                        break;

                    case SlotKindEnum.InventoryBig:

                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventoryBig:
                                DropItem();
                                break;

                            case SlotKindEnum.RightHand:
                                FromSlotToRightHand(false);                               
                                break;

                            case SlotKindEnum.LeftHand:
                                FromSlotToLeftHand(false);
                                break;

                            case SlotKindEnum.Head:
                                FromSlotToHead(false);
                                break;

                            case SlotKindEnum.Armor:
                                FromSlotToArmor(false);
                                break;

                            
                        }
                        break;

                    case SlotKindEnum.RightHand:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventoryBig:
                                DropItem();
                                break;

                        }
                        break;

                    case SlotKindEnum.LeftHand:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventoryBig:
                                DropItem();
                                break;
                        }
                        break;


                    case SlotKindEnum.Head:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventoryBig:
                                DropItem();
                                break;
                        }
                        break;


                    case SlotKindEnum.Armor:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventoryBig:
                                DropItem();
                                break;
                        }
                        break;

                    case SlotKindEnum.Amulet:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventorySmall:
                                DropItem();
                                break;
                        }
                        break;

                    case SlotKindEnum.Ring:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventorySmall:
                                DropItem();
                                break;
                        }
                        break;


                    case SlotKindEnum.InventorySmall:
                        switch (SlotKind)
                        {
                            case SlotKindEnum.Amulet:
                                FromSlotToAmulet(false);
                                break;

                            case SlotKindEnum.Ring:
                                FromSlotToRing(false);
                                break;

                            case SlotKindEnum.Shop:
                                SellFromInventoryToShop();
                                break;

                            case SlotKindEnum.InventorySmall:
                                DropItem();

                                break;
                        }

                        break;




                }                
            }               
        }
    }


    public void FromSlotToRing(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {

            case ItemKindEnum.Ring:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

        }
    }


    public void FromSlotToAmulet(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {

            case ItemKindEnum.Amulet:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

        }
    }

    public void FromSlotToArmor(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {

            case ItemKindEnum.Armor:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

        }
    }

    public void FromSlotToHead(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {          

            case ItemKindEnum.Helm:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

        }
    }


    public void FromSlotToRightHand(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {
            case ItemKindEnum.Sword:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

            case ItemKindEnum.Mace:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

            case ItemKindEnum.Axe:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }                
                break;

            case ItemKindEnum.Bow:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;



            case ItemKindEnum.Staff:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;

        }       
    }


    public void FromSlotToLeftHand(bool MustBuy)
    {
        switch (DraggableItemScript.ItemKind)
        {
            case ItemKindEnum.Shield:
                if (MustBuy == true)
                {
                    BuyFromShopToInventory();
                }
                else { DropItem(); }
                break;
                     
        }
    }




    public void SellFromInventoryToShop()
    {
       // if (DraggableItemScript.price <= InvScript.Dollars)
        {
            //if enough money

            Sell();

            DropItem();
        }
    }


    public void Sell()
    {
        GameObject InventoryGO = GameObject.Find("Inventory");
        InvScript = InventoryGO.GetComponent<Inventory>();

        DragDropHolderGO = GameObject.Find("Canvas");
        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();
        DraggableItemScript = DragDropHolderScript.ItemBeingDragged.GetComponent<DraggableItem>();

        InvScript.Dollars = InvScript.Dollars + DraggableItemScript.price;

        InvScript.UpdateDollarsLabel();

    }

    public void BuyFromShopToInventory()
    {
        DragDropHolderGO = GameObject.Find("Canvas");
        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();
        DraggableItemScript = DragDropHolderScript.ItemBeingDragged.GetComponent<DraggableItem>();

        GameObject InventoryGO = GameObject.Find("Inventory");
        InvScript = InventoryGO.GetComponent<Inventory>();

        if (DraggableItemScript.price <= InvScript.Dollars)
        {
            //if enough money

            Buy();

            DropItem();  
        }
    }

    public void Buy()
    {
        GameObject InventoryGO = GameObject.Find("Inventory");
        InvScript = InventoryGO.GetComponent<Inventory>();

        DragDropHolderGO = GameObject.Find("Canvas");
        DragDropHolderScript = DragDropHolderGO.GetComponent<DragDropMemory>();
        DraggableItemScript = DragDropHolderScript.ItemBeingDragged.GetComponent<DraggableItem>();

        InvScript.Dollars = InvScript.Dollars - DraggableItemScript.price;

        InvScript.UpdateDollarsLabel();
    }

    public void DropItem()
    {
        DragDropHolderScript.ItemBeingDragged.transform.SetParent(transform);

        DragDropHolderScript.ParentOfItemBeingDragged = transform.gameObject;

        string ItemName = DragDropHolderScript.ItemBeingDragged.name;

        GameObject NewDroppedItem = Instantiate(DragDropHolderScript.ItemBeingDragged, transform);

        NewDroppedItem.name = ItemName;

        Destroy(DragDropHolderScript.ItemBeingDragged);

        DragDropHolderScript.ItemBeingDragged = null;
    }

}
