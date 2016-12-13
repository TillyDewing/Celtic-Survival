using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HostDisplay : MonoBehaviour
{
    public Text serverName;
    public Image passwordProtected;
    public Text playerCount;


    public void setInfo(MasterMsgTypes.Room h)
    {
        serverName.text = h.name;
        playerCount.text = h.players + "/" + h.playerLimit;
    }

    
}
