using UnityEngine;
using System.Collections;
using System.IO;
using SpeechLib;
using System.Threading;

public enum ShopCategoryClickEnum
{
    NotSet,
    Swords,
    Staffs,
    Potions
};


public class DiabloActor : MonoBehaviour {

    public GameObject ActivatePanel;
    public ShopCategoryClickEnum ShopCategoryClick= ShopCategoryClickEnum.NotSet;


    public Transform Lego;

    public string SpeakerName;
    //public GameObject PortraitPlane;

    public Light EnemyRedLight;

    public string[] Quests;
    public string Bio;
    public string IsEnemyStr;

    public int Health=100;
    


   // private PhilosopherOrPerson PersonScript;

	// Use this for initialization
	void Start () {

       

	}


	
	// Update is called once per frame
	void Update () {
        //Quaternion Rotatio = Lego.transform.rotation;

        //Rotatio.y = Rotatio.y +1;

        //Lego.transform.rotation = Rotatio;

	}


    void OpenGriswoldPanel()
    {
        GameObject canv = GameObject.Find("Canvas");

        for (int i = 0; i < canv.transform.childCount ; i++)
        {
            if (canv.transform.GetChild(i).name == "ShopGriswold")
            {
                GameObject ButtonsGO = GameObject.Find("Buttons");
                Buttons ButtonsScript = ButtonsGO.GetComponent<Buttons>();

                ButtonsScript.FillPanelSetActive(true);

                ButtonsScript._ShopOpen(CurrentPanelEnum.Griswold);

                GameObject ShopGO = canv.transform.GetChild(i).gameObject;
                Shop ShopScript = ShopGO.GetComponent<Shop>();

                ShopScript._ButtonSwords();


            }


            //canv.GetComponentsInChildren<Transform>(true);//typeof(Transform),

        }
        
    }



    void OpenPepinPanel()
    {
        GameObject canv = GameObject.Find("Canvas");

        for (int i = 0; i < canv.transform.childCount; i++)
        {
            if (canv.transform.GetChild(i).name == "ShopPepin")
            {
                GameObject ButtonsGO = GameObject.Find("Buttons");
                Buttons ButtonsScript = ButtonsGO.GetComponent<Buttons>();
                ButtonsScript.FillPanelSetActive(true);

                ButtonsScript._ShopOpen(CurrentPanelEnum.Pepin);

                GameObject ShopGO = canv.transform.GetChild(i).gameObject;
                Shop ShopScript = ShopGO.GetComponent<Shop>();

                ShopScript._ButtonPepinBigItems();


            }
            //canv.GetComponentsInChildren<Transform>(true);//typeof(Transform),

        }

    }


    void OpenAdriaPanel()
    {
        GameObject canv = GameObject.Find("Canvas");

        for (int i = 0; i < canv.transform.childCount; i++)
        {
            if (canv.transform.GetChild(i).name == "ShopAdria")
            {
                GameObject ButtonsGO = GameObject.Find("Buttons");
                Buttons ButtonsScript = ButtonsGO.GetComponent<Buttons>();
                ButtonsScript.FillPanelSetActive(true);

                ButtonsScript._ShopOpen(CurrentPanelEnum.Adria);

                GameObject ShopGO = canv.transform.GetChild(i).gameObject;
                Shop ShopScript = ShopGO.GetComponent<Shop>();

                ShopScript._ButtonStaffs();



            }
            //canv.GetComponentsInChildren<Transform>(true);//typeof(Transform),

        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            GameObject ButtonsGO = GameObject.Find("Buttons");
            Buttons ButtonsScript = ButtonsGO.GetComponent<Buttons>();

            ButtonsScript._InventoryOpen();

            

            GameObject InventoryGO = GameObject.Find("Inventory");
            Inventory InventoryScript = InventoryGO.GetComponent<Inventory>();

            InventoryScript._BigItemsButton();

            //GameObject ShopGO;
            //Shop ShopScript;

            switch (ShopCategoryClick)
            {
                case ShopCategoryClickEnum.Swords:
                    OpenGriswoldPanel();
                    break;

                case ShopCategoryClickEnum.Staffs:
                    OpenAdriaPanel();

                    break;

                case ShopCategoryClickEnum.Potions:
                    OpenPepinPanel();

                    break;
            }

            ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript= other.transform.GetComponent<ClickToAttackOrMoveNavMesh>();

            ClickToAttackOrMoveNavMeshScript.RaycastingUI = true;


        }
    }

   


    void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            GameObject ButtonsGO = GameObject.Find("Buttons");
            Buttons ButtonsScript = ButtonsGO.GetComponent<Buttons>();

            ButtonsScript._InventoryClose();


            switch (ShopCategoryClick)
            {
                case ShopCategoryClickEnum.Swords:   
                    ButtonsScript._ShopClose(CurrentPanelEnum.Griswold);
                    
                    break;

                case ShopCategoryClickEnum.Staffs:
                    ButtonsScript._ShopClose(CurrentPanelEnum.Adria);
                    
                    break;

                case ShopCategoryClickEnum.Potions:
                    ButtonsScript._ShopClose(CurrentPanelEnum.Pepin);
                    

                    break;
            }


            //GameObject ShopGO = GameObject.Find("Shop");
            //Shop ShopScript = ShopGO.GetComponent<Shop>();

            //GameObject InventoryGO = GameObject.Find("Inventory");
            //Inventory InventoryScript = InventoryGO.GetComponent<Inventory>();
            ClickToAttackOrMoveNavMesh ClickToAttackOrMoveNavMeshScript = other.transform.GetComponent<ClickToAttackOrMoveNavMesh>();

            ClickToAttackOrMoveNavMeshScript.RaycastingUI = false ;


        }
    }


   



}
