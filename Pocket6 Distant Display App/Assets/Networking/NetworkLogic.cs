using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

// this script starts or stops the network server of the distant display app, and creates a reference to the client object (smartphone app)

public class NetworkLogic : MonoBehaviour
{
	public Text buttonStartStopServer;
	public Text labelNetworkStatus;
	public Text labelPhoneAppConnected;
	private NetworkManager networkManager;
	private GameObject serverNetworkObject; //Object which Transform etc. gets synced over network
	private bool clientFound;
	private string thisDeviceIp;

	void Awake()
	{
		networkManager = GetComponent<NetworkManager>();
	}

	void Start()
	{
		thisDeviceIp = GetLocalIPAddress();
		//StartOrStopNetwork(); // auto-start server on application start-up
	}

	void Update()
	{
		if (NetworkServer.active)
		{
			labelNetworkStatus.text = "Server running.\nIP of this device: " + thisDeviceIp + "\nNumber of clients: " + networkManager.numPlayers;
			if (!clientFound)
			{
				labelPhoneAppConnected.text = "Smartphone app: Not connected.";
				SearchForObjectsServer();
			}
		}
		else
		{
			labelNetworkStatus.text = "Server not running.";
		}
	}

	void SearchForObjectsServer()
	{
		clientFound = false;
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			if (go.name == "PhonePocket6" && !clientFound)
			{
				serverNetworkObject = go;
				clientFound = true;
				labelPhoneAppConnected.text = "Smartphone app: Connected.";
			}
		}
	}

	public void StartOrStopNetwork()
	{
		if (!NetworkServer.active)
		{
			networkManager.StartServer();
			buttonStartStopServer.text = "Stop server";
		}
		else
		{
			networkManager.StopHost();
			buttonStartStopServer.text = "Start server";
			if (GameObject.Find("PhonePocket6"))
			{
				GameObject.Destroy(GameObject.Find("PhonePocket6"));
			}
			clientFound = false;
		}
	}

	public static string GetLocalIPAddress()
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