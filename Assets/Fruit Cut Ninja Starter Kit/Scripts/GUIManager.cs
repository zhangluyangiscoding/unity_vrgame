using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Text guiPoints;
    MouseControl mouseControl;
    bool gamePause = false;
    public Text pauseButtonText;
    public FruitDispenser fd;
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        mouseControl = GameObject.Find("Game").GetComponent<MouseControl>();
       // mouseControl = GameObject.Find("Steel_Dagger_512").GetComponent<knifeRotate>();
    }

    // Update is called once per frame
    void Update()
    {
        guiPoints.text = "Points:" + mouseControl.points;
    }
    public void Pause()
    {
        Rigidbody[] rs= GameObject.FindObjectsOfType<Rigidbody>();
        gamePause = !gamePause;
        if (gamePause)
        {
            foreach (Rigidbody r in rs)
            {
                r.Sleep();
                pauseButtonText.text = "Resume";
                fd.pause = true;
                timer.PauseTimer(gamePause);
            }
        }
        else
        {
            foreach(Rigidbody r in rs)
            {
                r.WakeUp();
                pauseButtonText.text = "Pause";
                fd.pause = false;
                timer.PauseTimer(gamePause);
            }
        }
    }
}
