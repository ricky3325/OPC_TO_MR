using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class CheckIpv4Connect : MonoBehaviour
{
    public Text hintText;
    public GameObject Fail;

    private void Start()
    {
        GetLocalIPAddress();
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                hintText.text = ip.ToString();
                if (ip.ToString() != "192.168.2.160")
                {
                    Fail.SetActive(true);
                }
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Application.Quit();
        }
    }
}
