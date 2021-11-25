using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class HolGetSocket : MonoBehaviour
{
    private IPEndPoint ipEndPoint;
    private UdpClient udpClient;
    private Thread receiveThread;
    private byte[] receiveByte;
    private string receiveData = "";

    public string[] NowResult;
    public string NowReadValue;
    public string NowReadName;
    public string[] NowReadMultiTank;
    private float GyroPitch;
    private float GyroRoll;

    public bool EMS_MR;

    HolSendSocket RequestValue;

    void Start()
    {
        ipEndPoint = new IPEndPoint(IPAddress.Any, 5555);
        udpClient = new UdpClient(ipEndPoint.Port);

        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        RequestValue = GetComponent<HolSendSocket>();
    }

    void ReceiveData()
    {
        while (true)
        {
            receiveByte = udpClient.Receive(ref ipEndPoint);
            receiveData = System.Text.Encoding.UTF8.GetString(receiveByte);
            string st = receiveData;
            Debug.Log(st);
            string[] sArray = st.Split(':');
            NowResult = new string[sArray.Length - 1];
            for (int i = 0; i < sArray.Length - 1; i++)
            {
                NowResult[i] = sArray[i];
            }
            //Can Read EMS
            /*if (sArray[0] == "EMS" && sArray[1] == "True")
            {
                EMS_MR = true;
            }
            else if (sArray.Length == 3 && sArray[0] != "MultiTankLevel")
            {
                ProcessUdpValus(sArray[0], sArray[1], sArray[2]);
            }
            else if (sArray.Length == 4 && sArray[0] != "MultiTankLevel")
            {
                ProcessUdpTwoValus(sArray[0], sArray[1], sArray[2]);
            }
            else if (sArray[0] == "MultiTankLevel")
            {
                Debug.Log(sArray[0] + sArray[1] + "_" + sArray[2] + "MMMMMM");
                NowReadMultiTank = new string[sArray.Length - 1];
                for(int i = 1; i < sArray.Length; i++)
                {
                    NowReadMultiTank[i - 1] = sArray[i];
                }
            }*/
        }
    }

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

    private void ProcessUdpValus(string Type, string Name, string Valus)
    {
        if (Type == "Any")
        {
            Debug.Log("YourReadValues: " + Name + " : " + Valus);
            NowReadName = Name;
            NowReadValue = Valus;
        }        
    }

    private void ProcessUdpTwoValus(string Name, string Valus1, string Valus2)
    {
        if (Name == "Gyro")
        {
            Debug.Log("YourGyro: " + "Pitch: " + Valus1 + "Roll: " + Valus2);
            GyroPitch = float.Parse("0" + Valus1);
            GyroRoll = float.Parse("0" + Valus2);
        }
    }
    /*public float[] GetNowGyro()
    {
        RequestValue.ShipGyro();
        float[] Gyro;
        Gyro = new float[2];
        Gyro[0] = GyroPitch;
        Gyro[1] = GyroRoll;
        return Gyro;
    }*/
    public string GetNowReadValue()
    {
        return NowReadValue;
    }
    public string[] GetNowReadMultiTank()
    {
        return NowReadMultiTank;
    }
}
