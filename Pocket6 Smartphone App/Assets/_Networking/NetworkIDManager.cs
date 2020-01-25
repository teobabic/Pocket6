using UnityEngine;
using UnityEngine.Networking;

// this script sends the "player name" and touch data to the server (distant display app) 
public class NetworkIDManager : NetworkBehaviour
{
	public string _playerName;
	public Vector3 _touchData;

	[Command] //Executed only on server
	public void CmdSerPlayerName(string recievedName)
	{
		_playerName = recievedName;
		name = recievedName;
	}

	[Command]
	public void CmdTouchData(Vector3 td)
	{
		_touchData = td;
		//print(_touchData.ToString("F4"));
	}

	void Start()
	{
		if (isLocalPlayer)
		{
			CmdSerPlayerName(_playerName);
			name = _playerName;
		}
	}

	public void SendTouchData(Vector3 touchData)
	{
		if (isLocalPlayer)
		{
			CmdTouchData(touchData);
		}
	}
}