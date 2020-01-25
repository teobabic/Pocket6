using UnityEngine;
using UnityEngine.Networking;

// this script receives the "player name" and touch data from the client (smartphone app) 

public class NetworkIDManager : NetworkBehaviour
{
    [Command]
    public void CmdSerPlayerName(string recievedName)
    {
        if (isServer)
        {
            name = recievedName;
            print("Network found and renamed: " + recievedName);
        }
    }

    [Command]
    public void CmdTouchData(Vector3 td)
    {
        if (name == "PhonePocket6")
        {
            GameObject.Find("Interaction Logic").GetComponent<TouchManager>().touchData = td;
        }
    }
}