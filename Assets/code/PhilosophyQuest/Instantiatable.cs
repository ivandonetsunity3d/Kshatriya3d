using UnityEngine;
using System.Collections;
using System.IO;
using SpeechLib;
using System.Threading;

public class Instantiatable : MonoBehaviour {
    public Transform Lego;

    public string SpeakerName;
    public GameObject PortraitPlane;

    public Light EnemyRedLight;

    public string[] Quests;
    public string Bio;
    public string IsEnemyStr;

    public int Health=100;
    
    
    private SpeechControl SpeechCtrlScript;
    private InstancesControl InstancesControlScript;

    Job myJob;

   // private PhilosopherOrPerson PersonScript;

	// Use this for initialization
	void Start () {

     //   PersonScript = Lego.GetComponent<PhilosopherOrPerson>();

       // PersonScript.
        //Lego.transform.rotation = Rotatio;

        SetPortrait();
        try
        {
            SetQuests();        
        }
        catch
        {
        } 

        try
        {
            SetBiography();    
        }
        catch
        {
        } 

		try
        {
            SetEnemy();        
        }
        catch
        {
        } 

       

	}

	void SetEnemy()
	{
		string PhilosophersFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string EnemyFile = PhilosophersFolder + SpeakerName + "Enemy" + ".txt";

        //string[]
        IsEnemyStr = System.IO.File.ReadAllText(EnemyFile);

        if (IsEnemyStr == "Enemy")
        {
            EnemyRedLight.gameObject.SetActive(true);
            //EnemyRedLight.SetActive(false);

        }

	}
	
    void SetBiography() 
    {
        string PhilosophersFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string BioFile = PhilosophersFolder + SpeakerName + "Bio" + ".txt";

        //string[]
        Bio = System.IO.File.ReadAllText(BioFile);
    }
    void SetQuests() 
    {
        string PhilosophersFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string QuestsFile = PhilosophersFolder + SpeakerName + ".txt";

        //string[]
        Quests = System.IO.File.ReadAllLines(QuestsFile);


    }
	
	// Update is called once per frame
	void Update () {
        //Quaternion Rotatio = Lego.transform.rotation;

        //Rotatio.y = Rotatio.y +1;

        //Lego.transform.rotation = Rotatio;

	}


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "LEGO")
        {
            SpeechCtrlScript = other.GetComponent<SpeechControl>();
            SpeechCtrlScript.SpeechPanel.gameObject.SetActive(true);

            //SpeechCtrlScript.PortraitPlane

            SetPortraitOnSpeechPanel();

            CreateSpriteFromFile();

            //  SpeechCtrlScript.SpeakerPortrait.sprite = PortraitPicture;
            SpeechCtrlScript.SpeakerName.text = SpeakerName;
            
            // int QuestsCount;
            string QuestsString="";
            //string BringMeScrolls;

            int HaveScrollsTotal = 0;


            string Msg = "";

            InstancesControlScript = other.GetComponent<InstancesControl>();

            if (Quests.Length == 0)
            {             

                for (int i = 0; i < InstancesControlScript.PhilosophersList.Length; i++)
                {
                    if (InstancesControlScript.PhilosophersList[i] == SpeakerName) 
                    {
                        InstancesControlScript.ScrollsListInInventory[i] = true;
                        break;
                    }
                }

                Msg = "I don't have any quest for you.  Please, take my scroll";

                //BringMeScrolls = "";// "I don't have any quest for you.";// Please, take my scroll";
            }
            else 
            {
                for (int i = 0; i < Quests.Length; i++)
                {
                    //find completed quests
                    for (int p = 0; p < InstancesControlScript.PhilosophersList.Length; p++)
                    {
                        if (InstancesControlScript.PhilosophersList[p] == Quests[i])
                        {
                            if (InstancesControlScript.ScrollsListInInventory[p] == true) 
                            {                                
                                HaveScrollsTotal = HaveScrollsTotal + 1;
                            }
                            else if (InstancesControlScript.ScrollsListInInventory[p] == false)
                            {
                                {
                                    if (QuestsString == "") 
                                    {
                                        QuestsString = Quests[i];

                                    }
                                    else if (QuestsString != "") 
                                    {
                                        QuestsString = QuestsString + ", " + Quests[i];                                    
                                    }


                                }
                            }
                        
                        }

                    
                    }
                    
                    if ((Quests.Length - HaveScrollsTotal)==0)
                    {

                        for (int p = 0; p < InstancesControlScript.PhilosophersList.Length; p++)
                        {
                            if (InstancesControlScript.PhilosophersList[p] == SpeakerName)
                            {
                                
                                if (InstancesControlScript.ScrollsListInInventory[p] == false) 
                                {
                                    Msg = "You have completed all my quests. Please, take my scroll";

                                    InstancesControlScript.ScrollsListInInventory[p] = true;
                                }
                                else //if (InstancesControlScript.ScrollsListInInventory[p] == true)
                                {
                                    Msg = "I already gave you a scroll. Go away!";
                                }
                                                                
                                break;
                            }
                        }


                    }
                    else if (HaveScrollsTotal < Quests.Length) 
                    {
                        string BringPart = "";

                        string ScrollsCountPart = "";

                        if ((Quests.Length - HaveScrollsTotal) > 1)
                        {
                            ScrollsCountPart = "Bring me scrolls from the following ";
                            BringPart = " philosophers: ";
                        }
                        else if ((Quests.Length - HaveScrollsTotal) == 1)
                        {
                            ScrollsCountPart = "Bring me a scroll from ";
                            BringPart = " philosopher. His name is: ";
                        }
                        Msg = ScrollsCountPart + (Quests.Length - HaveScrollsTotal) + BringPart + QuestsString;
                    
                    }    
                }



                

 
            }

            SpeechCtrlScript.Speech.text = "Hello, my name is " + SpeakerName + ". " + "\n" + Bio + "\n" + Msg;// Found + "\n" + BringMeScrolls;

            //SpVoice voice;
            //voice = new SpVoice();
            //voice.Speak("Hello, my name is " + SpeakerName + ". " + "\n" + Bio + "\n" + Msg);


            if (SpeechCtrlScript.EnableSpeech == true) 
            {
                myJob = new Job();
                myJob.MSG = "Hello, my name is " + SpeakerName + ". " + "\n" + Bio + "\n" + Msg;
                myJob.Start();
            }
            else if (SpeechCtrlScript.EnableSpeech == false) 
            {
            
            } 


           


            //voice.Speak(txt.text);
            
            //System.Threading.Thread

            //System.Threading.ThreadStart.

           // SpeechCtrlScript.Speech.text = "Hello, my name is " + SpeakerName + ". " + "\n" + Bio + "\n" + Found +"\n"+ BringMeScrolls;


            //SpeechCtrlScript.Speech.text = "Hello, my name is " + SpeakerName + ". " + "\n" + Bio + "\n" + BringMeScrolls;

        }
    }

    public void CreateSpriteFromFile()
    {

        Renderer renderer = PortraitPlane.GetComponent<MeshRenderer>();
        
        string PhilosophersPortraitsFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string PortraitFile = PhilosophersPortraitsFolder + SpeakerName + ".jpg";

        byte[] data = File.ReadAllBytes(PortraitFile);

        Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);

        texture.LoadImage(data);

        renderer.material.mainTexture = texture;

       texture.name = Path.GetFileNameWithoutExtension(PortraitFile);
    
        float RectX = 0f;// -113;// SpeechCtrlScript.SpeakerPortrait.rectTransform.position.x;
        float RectY = 0f;// 20;// SpeechCtrlScript.SpeakerPortrait.rectTransform.position.y;
        float Width = 64f;// SpeechCtrlScript.SpeakerPortrait.rectTransform.sizeDelta.x;
        float Height = 64f;// SpeechCtrlScript.SpeakerPortrait.rectTransform.sizeDelta.y;
        
        Sprite PhilosopherSprite = Sprite.Create(texture, new Rect(RectX, RectY, Width, Height), new Vector2(0.5f, 0.5f), 1000);
      
        SpeechCtrlScript.SpeakerPortrait.sprite = PhilosopherSprite;

    }



    void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "LEGO")
        {
            SpeechCtrlScript = other.GetComponent<SpeechControl>();
            SpeechCtrlScript.CloseSpeechPanel();
            if (SpeechCtrlScript.EnableSpeech == true)
            {
                myJob.Abort();
               
                myJob.voice.Pause();
            }
           
        }
    }


    public void SetPortraitOnSpeechPanel() 
    {

        Renderer renderer = SpeechCtrlScript.PortraitPlane.GetComponent<MeshRenderer>();

        string PhilosophersPortraitsFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string PortraitFile = PhilosophersPortraitsFolder + SpeakerName + ".jpg";

        byte[] data = File.ReadAllBytes(PortraitFile);

        Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);

        texture.LoadImage(data);

        renderer.material.mainTexture = texture;
    }

    public void SetPortrait()
    {
        Renderer renderer = PortraitPlane.GetComponent<MeshRenderer>();

        string PhilosophersPortraitsFolder = "C:\\!GoodGenius Unity\\Philosophers\\";

        string PortraitFile = PhilosophersPortraitsFolder + SpeakerName + ".jpg";

        byte[] data = File.ReadAllBytes(PortraitFile);

        Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);

        texture.LoadImage(data);

        renderer.material.mainTexture = texture;
    }


}


//public class Job : ThreadedJob
//{
//    //public Vector3[] InData;  // arbitary job data
//    //public Vector3[] OutData; // arbitary job data

//    public string MSG = "";

//    public SpVoice voice;

//    protected override void ThreadFunction()
//    {
//        // Do your threaded task. DON'T use the Unity API here
//        //SpVoice voice;
//        voice = new SpVoice();
//       voice.Speak(MSG);
//       // voice.SpeakAsync(MSG);
        


//    }
//    protected override void OnFinished()
//    {
//        // This is executed by the Unity main thread when the job is finished
//        //for (int i = 0; i < InData.Length; i++)
//        //{
//        //    Debug.Log("Results(" + i + "): " + InData[i]);
//        //}
//    }
//}



//public class ThreadedJob
//{
//    private bool m_IsDone = false;
//    private object m_Handle = new object();
//    private System.Threading.Thread m_Thread = null;
//    public bool IsDone
//    {
//        get
//        {
//            bool tmp;
//            lock (m_Handle)
//            {
//                tmp = m_IsDone;
//            }
//            return tmp;
//        }
//        set
//        {
//            lock (m_Handle)
//            {
//                m_IsDone = value;
//            }
//        }
//    }

//    public virtual void Start()
//    {
//        m_Thread = new System.Threading.Thread(Run);
//        m_Thread.Start();
//    }
//    public virtual void Abort()
//    {
//        m_Thread.Abort();
//    }

//    protected virtual void ThreadFunction() { }

//    protected virtual void OnFinished() { }

//    public virtual bool Update()
//    {
//        if (IsDone)
//        {
//            OnFinished();
//            return true;
//        }
//        return false;
//    }
//    public IEnumerator WaitFor()
//    {
//        while (!Update())
//        {
//            yield return null;
//        }
//    }
//    private void Run()
//    {
//        ThreadFunction();
//        IsDone = true;
//    }
//}