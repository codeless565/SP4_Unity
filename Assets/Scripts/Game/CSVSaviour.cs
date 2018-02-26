using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEditor;

public class CSVSaviour {
    private List<string[]> rowData = new List<string[]>();
    private static CSVSaviour instance;
    public static CSVSaviour Instance
    {
        get
        {
            if (instance == null)
                instance = new CSVSaviour();
            return instance;
        }
    }
    private CSVSaviour()
    {

    }
        

    public void Save(List<Item> playerinv)
    {
        rowData.Clear();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[14];
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Type";
        rowDataTemp[2] = "Level";
        rowDataTemp[3] = "Cost";
        rowDataTemp[4] = "Experience";
        rowDataTemp[5] = "Health";
        rowDataTemp[6] = "MaxHealth";
        rowDataTemp[7] = "Stamina";
        rowDataTemp[8] = "MaxStamina";
        rowDataTemp[9] = "Attack";
        rowDataTemp[10] = "Defence";
        rowDataTemp[11] = "Movespeed";
        rowDataTemp[12] = "Sprite name";
        rowDataTemp[13] = "Rarity";
        rowData.Add(rowDataTemp);
        
        // You can add up the values in as many cells as you want.
        for (int i = 0; i < playerinv.Count; ++i)
        {
            rowDataTemp = new string[14];
            rowDataTemp[0] = playerinv[i].Name;
            rowDataTemp[1] = playerinv[i].ItemType;
            rowDataTemp[2] = playerinv[i].Level.ToString();
            rowDataTemp[3] = playerinv[i].ItemCost.ToString();
            rowDataTemp[4] = playerinv[i].EXP.ToString();
            rowDataTemp[5] = playerinv[i].Health.ToString();
            rowDataTemp[6] = playerinv[i].MaxHealth.ToString();
            rowDataTemp[7] = playerinv[i].Stamina.ToString();
            rowDataTemp[8] = playerinv[i].MaxStamina.ToString();
            rowDataTemp[9] = playerinv[i].Attack.ToString();
            rowDataTemp[10] = playerinv[i].Defense.ToString();
            rowDataTemp[11] = playerinv[i].MoveSpeed.ToString();
            rowDataTemp[12] = playerinv[i]._spritename;
            rowDataTemp[13] = playerinv[i].ItemRarity;
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath("PlayerItem");

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    public List<Item> LoadInv()
    {
        

        List<Item> tempInv = new List<Item>();

        TextAsset PlayerInv = Resources.Load<TextAsset>("PlayerItem");
        string[] rowdata = PlayerInv.text.Split(new char[] { '\n' });
        

        for (int i = 0; i < rowdata.Length - 2; ++i)
        {
            string[] linedata = rowdata[i].Split(new char[] { ',' });

            if (linedata[0] == "")
                continue;

            if (linedata[0] == "Name")
                continue;

            Item newItem = new Item();
            newItem.Name = linedata[0];
            newItem.ItemType = linedata[1];

            float temp2 = 0.0f;

            float.TryParse(linedata[2], out temp2);
            newItem.Level = (int)temp2;
            float.TryParse(linedata[3], out temp2);
            newItem.ItemCost = (int)temp2;
            float.TryParse(linedata[4], out temp2);
            newItem.EXP = temp2;
            float.TryParse(linedata[5], out temp2);
            newItem.Health = temp2;
            float.TryParse(linedata[6], out temp2);
            newItem.MaxHealth = temp2;
            float.TryParse(linedata[7], out temp2);
            newItem.Stamina = temp2;
            float.TryParse(linedata[8], out temp2);
            newItem.MaxStamina = temp2;
            float.TryParse(linedata[9], out temp2);
            newItem.Attack = temp2;
            float.TryParse(linedata[10], out temp2);
            newItem.Defense = temp2;
            float.TryParse(linedata[11], out temp2);
            newItem.MoveSpeed = temp2;
            newItem.ItemRarity = linedata[13];
            newItem.ItemRarity = newItem.ItemRarity.Trim();
            newItem._spritename = linedata[12];
            newItem.getImage();

            tempInv.Add(newItem);
        }

            return tempInv;
    }


    private string getPath(string pathname)
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/Resources/" + pathname + ".csv";
        #elif UNITY_ANDROID
            return Application.persistentDataPath + pathname + ".csv";
        #elif UNITY_IPHONE
            return Application.persistentDataPath + "/" + pathname + ".csv";
        #else
            return Application.dataPath + "/" + pathname + ".csv";
        #endif
    }
}
