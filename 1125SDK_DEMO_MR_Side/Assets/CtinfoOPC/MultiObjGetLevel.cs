using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MultiObjGetLevel : MonoBehaviour
{
    HolGetSocket GetValues;
    HolSendSocket RequestValues;

    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        GetValues = GetComponent<HolGetSocket>();
        RequestValues = GetComponent<HolSendSocket>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (test)
        {
            Debug.Log(ReadShipRotation()[0] + "__" + ReadShipRotation()[1]);
        }*/
        //
    }
    #region//TANK
    /*public float ReadTankWaterLevel(string Name)//float
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        Debug.Log("ReadWater:" + GetValues.GetNowReadValue());
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }
    public float[] ReadBMultiTankWaterLevel(string[] Name)//float
    {
        RequestValues.MultiTankLevel(Name);
        Thread.Sleep(20);
        float[] A = new float[GetValues.GetNowReadMultiTank().Length];
        for(int i = 0; i < A.Length; i++)
        {
            A[i] = float.Parse(GetValues.GetNowReadMultiTank()[i]);
        }
        //Debug.Log(A[0] + "_FFFF_" + A[1]);
        return A;
    }
    public float ReadTankWaterTarget(string Name)//float
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }
    public void WriteTankWaterTarget(string Name, float Value)//float
    {
        RequestValues.ReadWriteOpcData(Name, false, false, true, Value, false, 0, false, 0);
    }*/
    #endregion
    #region//PUMP
    /*public void WritePumpMode(string Name, bool Value)//bit
    {
        RequestValues.ReadWriteOpcData(Name, true, Value, false, 0, false, 0, false, 0);
    }
    public float ReadPumpMode(string Name)//bit to Float
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }
    public float ReadPumpTrip(string Name)//bit
    {
        //需要一直讀取  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>注意！！！
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }
    public void WritePumpRun(string Name, int Value)//bit
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, true, Value);
    }
    public float ReadPumpRun(string Name)//bit to Float
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }*/
    #endregion
    #region//Valve
    /*public float ReadValveMode(string Name)//word
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }*/
    /*public void WriteValveMode(string Name, float Value)//word
    {
        RequestValues.ReadWriteOpcData(Name, false, false, true, Value, false, 0, false, 0);
    }
    public float ReadValvePosition(string Name)//word
    {
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }
    public void WriteValvePosition(string Name, float Value)//word
    {
        RequestValues.ReadWriteOpcData(Name, false, false, true, Value, false, 0, false, 0);
    }
    public float ReadValveError(string Name)//word
    {
        //需要一直讀取  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>注意！！！
        RequestValues.ReadWriteOpcData(Name, false, false, false, 0, false, 0, false, 0);
        Thread.Sleep(20);
        float A = float.Parse(GetValues.GetNowReadValue());
        return A;
    }*/
    #endregion
    /*public float[] ReadShipRotation()//float
    {
        float[] A = new float[2];
        A[0] = GetValues.GetNowGyro()[0];
        A[1] = GetValues.GetNowGyro()[1];
        return A;
    }*/
    /*public void WriteShipError(bool EMS)//bool
    {
        if (EMS)
        {
            RequestValues.EMS_MR(true, true);
        }
        else if (!EMS)
        {
            RequestValues.EMS_MR(true, false);
        }
    }*/
    /*public bool ReadShipError()//bool
    {
        //RequestValues.EMS_MR(false, true);
        if (GetValues.EMS_MR)
        {
            return true;
        }
        else
            return false;
    }*/

    public string[] ReadValues(string[] Name)
    {
        RequestValues.ConvertStringArrToByteAndSendToPc(Name);
        string[] A = GetValues.NowResult;
        return A;
    }
    public void WriteValues(string[] Name)
    {
        RequestValues.WriteValuesToPc(Name);
    }
}
