using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    Vector2 screenInp;
    bool fire = false;
    bool fire_prev = false;
    bool fire_down = false;
    bool fire_up = false;
    public LineRenderer trail;
    Vector2 start, end;
    Vector3[] trailPositions=new Vector3[10];
    int index;
    float lineTimer = 1.0f;
    int linePart = 0;
    public int points;
    float trail_alpha = 0f;
    int raycastCount = 10;
    bool started=false;
    //水果汁预制品
    public GameObject[] splashPrefab;
    public GameObject[] splashFlatPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void BlowObject(RaycastHit hit)
    {
        if(hit.collider.gameObject.tag!="destroyed")
        {
            //生成切开水果的部分
            hit.collider.gameObject.GetComponent<ObjectKill>().OnKill();

            //删除切掉的水果
            Destroy(hit.collider.gameObject);

     
            //准备破开的水果的资源
            if (hit.collider.tag == "red") index = 0;
            if (hit.collider.tag == "yellow") index = 1;
            if (hit.collider.tag == "green") index = 2;
            //水果汁效果
            Vector3 splashPoint = hit.point;
            splashPoint.z = 4;
            Instantiate(splashPrefab[index], splashPoint, Quaternion.identity);
            splashPoint.z += 4;
            Instantiate(splashFlatPrefab[index], splashPoint, Quaternion.identity);

            //切到炸弹
            if (hit.collider.gameObject.tag != "bomb") points++; else points -= 5;
            points = points < 0 ? 0 : points;
            hit.collider.gameObject.tag = "destroyed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Mouse;
        screenInp.x = Input.mousePosition.x;
        screenInp.y = Input.mousePosition.y;
        fire_down = false;
        fire_up = false;
        fire = Input.GetMouseButton(0);
        if (fire && !fire_prev)
            fire_down = true;
        if (!fire && fire_prev)
            fire_up = true;
        fire_prev = fire;
        //控制画线
        Control();
        //设置线段颜色
        Color c1 = new Color(1, 1, 0, trail_alpha);
        Color c2 = new Color(1, 0, 0, trail_alpha);
        trail.SetColors(c1, c2);
        if(trail_alpha>0)
            trail_alpha -= Time.deltaTime;
    }
    void Control()
    {
        if(fire_down)
        {
            trail_alpha = 1.0f;
            start = screenInp;
            end = screenInp;
            started = true;
            linePart = 0;
            lineTimer = 0.25f;
            AddTrailPosition();
        }
        if(fire&&started)
        {
            start = screenInp;
            var a = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
            var b = Camera.main.ScreenToWorldPoint(new Vector3(end.x, end.y, 10));
            //判断用户鼠标
            if(Vector3.Distance(a,b)>0.1f)
            {
                linePart++;
                lineTimer = 0.25f;
                AddTrailPosition();
            }
            trail_alpha = 0.75f;
            end = screenInp;

        }
        if(trail_alpha>0.5f)
        {
            for(var p=0;p<8;p++)
            {
                for(var i=0;i<raycastCount;i++)
                {
                    Vector3 s = Camera.main.WorldToScreenPoint(trailPositions[p]);
                    Vector3 e= Camera.main.WorldToScreenPoint(trailPositions[p+1]);
                    Ray ray = Camera.main.ScreenPointToRay(Vector3.Lerp(s, e, i / raycastCount));
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit,100,1<<LayerMask.NameToLayer("fruit")))
                    {
                        BlowObject(hit);
                    }
                }
            }
        }
        if (trail_alpha <= 0) linePart = 0;
        lineTimer -= Time.deltaTime;
        if(lineTimer<=0f)
        {
            linePart++;
            AddTrailPosition();
            lineTimer = 0.01f;
        }
        if (fire_up && started)
            started = false;
        SendTrailPosition();
    }
    void AddTrailPosition()
    {
        if (linePart <= 9)
        {
            for (int i = linePart; i <= 9; i++)
            {
                trailPositions[i] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));

            }
        }
        else
        {
            for (int ii = 0; ii <= 0; ii++)

            {
                trailPositions[ii] = trailPositions[ii + 1];
            }
            trailPositions[9] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
        }

    }
    void SendTrailPosition()
    {
        var index = 0;
        foreach(Vector3 v in trailPositions)
        {
            trail.SetPosition(index, v);
            index++;
        }
    }
}
