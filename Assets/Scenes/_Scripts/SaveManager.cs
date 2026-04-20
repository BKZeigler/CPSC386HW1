using UnityEngine;
using System.Collections.Generic;

public static class SaveManager
{
    //public static void SaveUnits(List<UnitSaveData> units)
    //{
    //    string json = JsonUtility.ToJson(new Wrapper<UnitSaveData>(units));
    //    PlayerPrefs.SetString("Units", json);
    //    PlayerPrefs.Save();
    //}

    //public static List<UnitSaveData> LoadUnits()
    //{
    //    if (!PlayerPrefs.HasKey("Units"))
    //        return new List<UnitSaveData>();

    //    string json = PlayerPrefs.GetString("Units");
    //    return JsonUtility.FromJson<Wrapper<UnitSaveData>>(json).items;
    //}

    //[System.Serializable]
    //private class Wrapper<T>
    //{
        //public List<T> items;
       // public Wrapper(List<T> items) { this.items = items; }
    //}
}