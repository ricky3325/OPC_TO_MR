using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class HolSendSocket : MonoBehaviour
{
    public bool Pos;
    public bool Mode;

    HolGetSocket GetValues;

    void Start()
    {
        GetValues = GetComponent<HolGetSocket>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void EMS_MR(bool write, bool values)
    {
        //EMS Code Here
        if (write)
        {
            SendInfoToPC(System.Text.Encoding.UTF8.GetBytes("EMS" + ":" + values));
        }
        else if (!write)
        {
            SendInfoToPC(System.Text.Encoding.UTF8.GetBytes("EMS" + ":" + "ReadOnly"));
        }
    }
    public void ConvertStringArrToByteAndSendToPc(string[] Info)
    {
        string SendData = null;
        for(int i = 0; i <　Info.Length; i++)
        {
            SendData = SendData + Info[i] +":";
        }
        SendData = SendData + "Read";
        SendInfoToPC(System.Text.Encoding.UTF8.GetBytes(SendData));
    }
    public void WriteValuesToPc(string[] Info)
    {
        string SendData = Info[0] + ":" + Info[1] + ":" + Info[2];
        Debug.Log("333333" + SendData);
        SendInfoToPC(System.Text.Encoding.UTF8.GetBytes(SendData));
    }
    public void SendInfoToPC(byte[] b)
    {
        #region //socketConnect
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.2.160"), 6666);//發佈改.2.160
        UdpClient uc = new UdpClient();
        #endregion
        uc.Send(b, b.Length, ipep);
    }
}
