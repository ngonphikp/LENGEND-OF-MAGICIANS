using UnityEngine;

public static class C_Util
{
    public static T Clone<T>(this T source)
    {
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(source);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serialized);
    }
    public static void GetDumpObject(object obj)
    {
        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
    }

    public static void ActiveGO(bool isActive, params GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(isActive);
        }
    }
}

