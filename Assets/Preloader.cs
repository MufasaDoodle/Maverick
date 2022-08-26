using FishNet.Managing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string[] args = System.Environment.GetCommandLineArgs();

		for (int i = 0; i < args.Length; i++)
		{
            if (args[i] == "-launchserver")
			{
                StartServer();
            }
		}        
    }

    private void StartServer()
	{
        FindObjectOfType<NetworkManager>().ServerManager.StartConnection();
    }
}
