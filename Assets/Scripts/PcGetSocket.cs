using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class PcGetSocket : MonoBehaviour
{
    //接收由HOLOLENS過來PC的UDP訊息
    //這個腳本用在接收到HOLOLENS的值後
    //1.辨識屬於MODE還是POS的
    //2.辨識屬於第幾個Tank的
    //3.把辨識到要傳送的值丟到公用變數上讓OPC腳本抓
    private IPEndPoint ipEndPoint;
    private UdpClient udpClient;
    private Thread receiveThread;
    private byte[] receiveByte;
    //private byte[] sendByte;
    private string receiveData = "";

    OPCReadWrite_SendUDP GetValueOPC;

    public string Send;

    #region//DebugLog
    public GameObject[] DebugLogText;
    private static string[] ShowState;
    #endregion

    void Start()
    {
        ipEndPoint = new IPEndPoint(IPAddress.Any, 6666);
        udpClient = new UdpClient(ipEndPoint.Port);

        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        GetValueOPC = GetComponent<OPCReadWrite_SendUDP>();

        ShowState = new string[3];
    }

    void Update()
    {
        #region//SendAckIP
        GetValueOPC.NowAckIP = ShowState[0];
        #endregion
        #region//SHOW STATE
        DebugLogText[0].GetComponent<Text>().text = ShowState[0];//Now IP Addr
        DebugLogText[1].GetComponent<Text>().text = ShowState[1];//Now Values
        DebugLogText[2].GetComponent<Text>().text = ShowState[2];//Values use for... 
        #endregion
    }

    void ReceiveData()
    {
        while (true)
        {
            EndPoint Remote = (EndPoint)ipEndPoint;
            ShowState[0] = Remote.ToString();
            receiveByte = udpClient.Receive(ref ipEndPoint);
            receiveData = System.Text.Encoding.UTF8.GetString(receiveByte);
            string st = receiveData;
            ShowState[1] = st; // Show Values
            Debug.Log("Get: " + st);
            string[] sArray = st.Split(':');
            if(sArray[sArray.Length - 1] == "Read")
            {
                ShowState[2] = "ReadValues";
                ReadSthFromOpc(sArray);
            }
            else if (sArray[sArray.Length - 1] == "Write")
            {
                ShowState[2] = "WriteValues";
                WriteSthFromPc(sArray);
            }
        }
    }
    #region//UDP
    private void OnDisable()
    {
        udpClient.Close();
        receiveThread.Join();
        receiveThread.Abort();
    }

    private void OnApplicationQuit()
    {
        receiveThread.Abort();
    }
    #endregion
    public void ReadSthFromOpc(string[] Name)
    {
        GetValueOPC.ReturnReadSthFromOpc(Name);
    }
    public void WriteSthFromPc(string[] Name)
    {
        GetValueOPC.WriteSthFromPc(Name);
    }
}
