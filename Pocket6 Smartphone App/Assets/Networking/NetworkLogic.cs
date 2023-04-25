using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using Mirror;

// this script connects or disconnects the client with the server (distant display app)

public class NetworkLogic : MonoBehaviour
{
	private NetworkManager manager;
	public GameObject sceneNetworkPhone; // scene object ARKit/Pocket6 is moving 
	private GameObject networkClientPhone; // client object which transform gets synced over network
	private bool _foundClient;

	// gui
	public TMP_Text _buttonTextStartOrJoin;
	public TMP_Text labelConnectionInfo;
	public TMP_InputField ipInputField;
	public TouchLogic touchDataLogic;

	void Awake()
	{
		manager = GetComponent<NetworkManager>();
		ipInputField.text = manager.networkAddress;
	}

	void Update()
	{
		labelConnectionInfo.text = "IP of this phone: " + GetLocalIPAddress();

		if (NetworkClient.isConnected)//manager.IsClientConnected())
		{
			if (_foundClient)
			{
				networkClientPhone.transform.position = sceneNetworkPhone.transform.position;
				networkClientPhone.transform.rotation = sceneNetworkPhone.transform.rotation;

				networkClientPhone.GetComponent<NetworkIDManager>().SendTouchData(touchDataLogic.GetTouchData());
			}
			else
			{
				SearchForObjectsClient("PhonePocket6");
			}
		}
		else // there was a disconnect
		{
			_foundClient = false;
			manager.networkAddress = ipInputField.text;
		}

		// gui buttons
		if (NetworkClient.isConnected)//manager.IsClientConnected())
		{
			_buttonTextStartOrJoin.text = "Disconnect";
		}
		else
		{
			_buttonTextStartOrJoin.text = "Connect";
		}
	}

	void SearchForObjectsClient(string objectName)
	{
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			if (go.name == "PhonePocket6")
			{
				if (go.GetComponent<NetworkIDManager>()._playerName == objectName)
				{
					networkClientPhone = go;
					_foundClient = true;
				}
			}
		}
	}

	public void StartOrStopNetwork()
	{
		if (!NetworkClient.isConnected)//!manager.IsClientConnected())
		{
			manager.StartClient();
			GameObject.Find("Pocket6 Logic").GetComponent<Pocket6Logic>().ForceRemapControlSpace();
		}
		else
		{
			manager.StopClient();
		}
	}

	public string GetLocalIPAddress()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
		}
		return "No internet connection.";
	}
}