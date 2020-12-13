using Sfs2X.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleConnect
{
    public static bool isConnected = false;
    public static void OnConnection(BaseEvent evt)
    {
        // Nếu kết nối server thành công
        if ((bool)evt.Params["success"])
        {
            Debug.Log("Kết nối server thành công!");
            isConnected = true;
        }
        // Nếu kết nối server thất bại
        else
        {
            Debug.LogWarning("Kết nối server thất bại!");

            // Kết nối lại
            SmartFoxConnection.Connect(); 
            isConnected = false;
        }
    }
    public static void OnConnectionLost(BaseEvent evt)
    {
        isConnected = false;
        Debug.LogWarning("Mất kết nối server!");

        SceneManager.LoadScene("LoginGame"); 
    }
}
