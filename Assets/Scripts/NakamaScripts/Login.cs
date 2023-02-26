using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PassData.client = new Client("http", "63.32.46.59", 7350, "defaultkey");
        Debug.Log("device id " + System.Guid.NewGuid().ToString());
    }

    public async void GuestLogin()
    {
 
        var deviceId = System.Guid.NewGuid().ToString();
        PassData.session = await PassData.client.AuthenticateDeviceAsync(deviceId);
        PassData.socket = PassData.client.NewSocket();

        bool appearOnline = true;
        int connectionTimeout = 30;
        await PassData.socket.ConnectAsync(PassData.session, appearOnline, connectionTimeout);
        SceneManager.LoadScene("Matchmaking");
        Debug.Log("session " + PassData.session);
        
    }
}
