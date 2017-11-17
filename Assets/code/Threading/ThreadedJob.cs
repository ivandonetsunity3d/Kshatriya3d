using System;
using System.Collections;
using SpeechLib;
using System.Threading;

public class Job : ThreadedJob
{
    //public Vector3[] InData;  // arbitary job data
    //public Vector3[] OutData; // arbitary job data

    public string MSG = "";

    public SpVoice voice;

    protected override void ThreadFunction()
    {
        // Do your threaded task. DON'T use the Unity API here
        //SpVoice voice;
        voice = new SpVoice();
        voice.Speak(MSG);
        // voice.SpeakAsync(MSG);



    }
    protected override void OnFinished()
    {
        // This is executed by the Unity main thread when the job is finished
        //for (int i = 0; i < InData.Length; i++)
        //{
        //    Debug.Log("Results(" + i + "): " + InData[i]);
        //}
    }
}



public class ThreadedJob
{
    private bool m_IsDone = false;
    private object m_Handle = new object();
    private System.Threading.Thread m_Thread = null;
    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (m_Handle)
            {
                tmp = m_IsDone;
            }
            return tmp;
        }
        set
        {
            lock (m_Handle)
            {
                m_IsDone = value;
            }
        }
    }

    public virtual void Start()
    {
        m_Thread = new System.Threading.Thread(Run);
        m_Thread.Start();
    }
    public virtual void Abort()
    {
        m_Thread.Abort();
    }

    protected virtual void ThreadFunction() { }

    protected virtual void OnFinished() { }

    public virtual bool Update()
    {
        if (IsDone)
        {
            OnFinished();
            return true;
        }
        return false;
    }
    public IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}
