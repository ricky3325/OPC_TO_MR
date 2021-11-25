using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

[Serializable]
public class IO_list
{
    public string Name = null;
    public string Address = null;
    public string Type = null;
    public string LinerChk = null;
    public string Liner = null;
}
public class Addr : MonoBehaviour
{
    public List<IO_list> New_IO_list = new List<IO_list>();
    [Header("Bool = 1, Float = 3, Word = 5, Bit = 7")]
    public string InputName;
    public string ResultAddr;
    public string ResultType;
    public bool test;

    public GameObject DebResult;
    private static string addrDebug;

    void Start()
    {
        
        TextAsset List_data = Resources.Load<TextAsset>("S7-1500_Master");

        string[] data = List_data.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                IO_list New_IO = new IO_list();
                New_IO.Name = row[0];
                if(row[11] == "Double")
                {
                    New_IO.LinerChk = "UseTankLiner";
                    New_IO.Address = "ns=2;s=Ballast.CTL01.Tank." + row[0];
                }
                else
                {
                    New_IO.LinerChk = "NoLiner";
                    New_IO.Address = "ns=2;s=Ballast.CTL01." + row[0];
                }
                New_IO.Liner = row[8];
                
                switch (row[2])
                {
                    case "Boolean"  : New_IO.Type = "1"; break;
                    case "Float" : New_IO.Type = "3"; break;
                    case "Word"  : New_IO.Type = "5"; break;
                    case "Bit"   : New_IO.Type = "7"; break; 
                } 
                New_IO_list.Add(New_IO);
            }
        }
    }
    void Update()
    {
        DebResult.GetComponent<Text>().text = addrDebug;
    }
    public string[] SearchAddr(String SearchName)
    {
        try
        {
            InputName = SearchName;
            string[] Result = new string[3];
            foreach (IO_list list in New_IO_list.Where(n => n.Name == SearchName))
            {
                ResultAddr = Result[0] = list.Address;
                addrDebug = Result[0];
                ResultType = Result[1] = list.Type;
                if (list.LinerChk == "UseTankLiner")
                {
                    Result[2] = list.Liner;
                }
                else
                {
                    Result[2] = "NoLiner";
                }
                return Result;
            }
            return null;
        }
        catch (Exception Ex)
        {
            Debug.Log("Error Message :" + Ex);
            return null;
        }
    }
}
