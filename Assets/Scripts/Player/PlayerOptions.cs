using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Xml.Serialization;
using System.IO;

public class PlayerOptions : MonoBehaviour
{
    public float sensitivityX = 70f;
    public float sensitivityY = 70f;

    public string playerName = "PlayerName";

    public string playerId;

    void Start()
    {
        //Atempt to grab uniquie player id from registry if not creates a new one used to identify players on server
        if (PlayerPrefs.HasKey("Id"))
        {
            playerId = PlayerPrefs.GetString("Id");
        }
        else
        {
            playerId = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("Id", playerId);
        }

        Debug.Log("Player Id: " + playerId);

        LoadOptions();
        //Debug.Log(new WebClient().DownloadString("http://icanhazip.com")); Gets Player public Ip

    }

    public void SaveOptions()
    {
        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Celtic Survival\");
        Options options = ToOptions();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Celtic Survival\options.cfg";
        XmlSerializer serilizer = new XmlSerializer(typeof(Options));
        FileStream stream = new FileStream(path, FileMode.Create);
        serilizer.Serialize(stream, options);
        stream.Close();
    }

    public void LoadOptions()
    {
        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Celtic Survival\options.cfg"))
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Celtic Survival\options.cfg";
            XmlSerializer serilizer = new XmlSerializer(typeof(Options));
            FileStream stream = new FileStream(path, FileMode.Open);
            Options options = serilizer.Deserialize(stream) as Options;
            stream.Close();
            SetOptions(options);
        }
    }

    private Options ToOptions()
    {
        Options options = new Options();
        options.sensitivityX = sensitivityX;
        options.sensitivityY = sensitivityY;
        options.playerName = playerName;
        return options;
    }

    private void SetOptions(Options options)
    {
        sensitivityX = options.sensitivityX;
        sensitivityY = options.sensitivityY;
        playerName = options.playerName;
    }

}
public class Options
{
    public float sensitivityX = 70f;
    public float sensitivityY = 70f;

    public string playerName = "PlayerName";
}
