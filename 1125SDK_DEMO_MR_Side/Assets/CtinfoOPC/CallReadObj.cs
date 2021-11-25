using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CallReadObj : MonoBehaviour
{
    public string[] ReadWhat;
    public string[] ReadResult;
    public bool Read;

    public float BufferTime;
    public float NowTime;
    public float LastTime;

    public string[] WriteWhat;
    public bool Write;

    MultiObjGetLevel RequestValues;
    // Start is called before the first frame update
    void Start()
    {
        RequestValues = GetComponent<MultiObjGetLevel>();
        WriteWhat = new string[3];
        WriteWhat[2] = "Write";  //It was very IMPORTANT!! 

        LastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        NowTime = Time.time;
        if (Read)
        {
            if(NowTime - LastTime > BufferTime)
            {
                ReadResult = RequestValues.ReadValues(ReadWhat);
                LastTime = Time.time;
            }
        }
        if (Write)
        {
            RequestValues.WriteValues(WriteWhat);
        }
    }
}
