using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class InstancesControl : MonoBehaviour {

    public string[] PhilosophersList;// = new string[31];
    public bool[] ScrollsListInInventory;// = new string[31];
    
    
    public GameObject LEGOPrefab;
    public GameObject LEGOOnly;

    public Transform FirstLEGOSpawnPoint;

    public GameObject PlatformForLEGO;


	// Use this for initialization
    void Start()
    {
        string PhilosophersFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string PortraitFile = PhilosophersFolder + "PhilosophersList.txt";
        
        //string[]
        PhilosophersList = System.IO.File.ReadAllLines(PortraitFile);
        ScrollsListInInventory = new bool[PhilosophersList.Length];

        for (int i = 0; i < ScrollsListInInventory.Length; i++)
        {
            ScrollsListInInventory[i] = false;
        }

        Vector3 LEGOLocation;
        
        LEGOLocation.x = 16f;// 8.25f;
        LEGOLocation.y = 0f;
        LEGOLocation.z = -55f;

  
        Quaternion Rotatio = LEGOOnly.transform.rotation;


        Vector3 PlatformLocation = LEGOLocation;
        PlatformLocation.y = 8f;
       


        for (int i = 0; i < PhilosophersList.Length; i++) 
        {
            


            int RndNumber = Random.Range(0, 2);

           // Debug.Log(RndNumber);

            switch (RndNumber) 
            {
                case 0:
                    LEGOLocation.y = 0f;

                    break;
                
                case 1:
                    Instantiate(PlatformForLEGO, PlatformLocation, Rotatio);

                    LEGOLocation.y = PlatformLocation.y;

                    break;
             }

            GameObject NewLego = Instantiate(LEGOOnly, LEGOLocation, Rotatio) as GameObject;


            Instantiatable PersonScript = NewLego.GetComponent<Instantiatable>();

            PersonScript.SpeakerName = PhilosophersList[i];

            LEGOLocation.z = LEGOLocation.z - 10f;

            PlatformLocation.z = PlatformLocation.z - 10f;

            //Instantiate(LEGOPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
