using UnityEngine;
using UnityEngine.Networking; 
using System.Collections;

public class Builder : NetworkBehaviour
{
    public bool isBuilding;

    public GameObject[] objectGuides;
    public GameObject[] objects;

    public int selectedObject = 0;
    public int buildDistance = 7;

    private PlaceableObject placeableObject;

    void Update()
    {
        if (!isBuilding)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, buildDistance))
        {
            if (placeableObject == null)
            {
                GameObject obj = Instantiate(objectGuides[selectedObject], hit.point, Quaternion.identity) as GameObject;
                placeableObject = obj.GetComponent<PlaceableObject>();
                placeableObject.placed = false;
            }
            else
            {
                placeableObject.transform.position = hit.point + placeableObject.posOffSet;
                placeableObject.transform.rotation = transform.rotation;

                if (Input.GetMouseButtonDown(1))
                {
                    PlaceObject();
                }
            }
        }
        else
        {
            if (placeableObject != null)
            {
                Destroy(placeableObject.gameObject);
            }
        }
    }

    public void PlaceObject()
    {

        Vector3 pos = placeableObject.transform.position;
        Quaternion rot = placeableObject.transform.rotation;
        Destroy(placeableObject.gameObject);

        CmdSpawn(selectedObject, pos, rot);
    }

    [Command]
    void CmdSpawn(int objId, Vector3 pos, Quaternion rot)
    {
        GameObject obj = Instantiate(objects[objId], pos, rot) as GameObject;
        NetworkServer.Spawn(obj);
    }
}
