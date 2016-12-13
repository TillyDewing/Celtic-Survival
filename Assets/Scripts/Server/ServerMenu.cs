using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerMenu : MonoBehaviour
{
    public NetworkMasterClient client;
    public GameObject hostDisplay;
    public GameObject container;

    public string serverName;

    private MasterMsgTypes.Room selectedHost;

    public void UpdateHosts()
    {
        client.ClearHostList();
        client.RequestHostList("Survival");
        StartCoroutine("DisplayHosts");
    }

    IEnumerator DisplayHosts()
    {
        while (client.PollHostList() == null)
        {
            yield return new WaitForSeconds(.5f);
        }

        foreach (var h in client.PollHostList())
        {
            HostDisplay display = Instantiate(hostDisplay, container.transform) as HostDisplay;

            display.setInfo(h);
        }
    }

    public void StartServer()
    {
        NetworkManager serve = NetworkManager.singleton;
        client.RegisterHost(serverName, "", false, serve.numPlayers, serve.maxConnections, serve.networkPort);
        serve.StartServer();
    }

    public void JoinSelected()
    {
        if (!(selectedHost.hostIp == ""))
        {
            NetworkManager serve = NetworkManager.singleton;
            serve.networkAddress = selectedHost.hostIp;
            serve.networkPort = selectedHost.hostPort;
            serve.StartClient();
        }
    }
}
