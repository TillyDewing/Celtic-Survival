using UnityEngine;
using System.Collections;

public class PlaceableObject : MonoBehaviour
{
    public Material canPlaceMat;
    public Material cantPlaceMat;

    private Material originalMat;

    public bool canPlace = false;
    public bool placed = true;

    public Vector3 posOffSet; 

    private MeshRenderer renderer;
    private Collider objCollider;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        objCollider = GetComponent<Collider>();
        originalMat = renderer.material;
        SetPlaceable();
    }

    void OnTriggerStay(Collider other)
    {
        if (placed)
        {
            renderer.material = originalMat;
            return;
        }
        else if (canPlace)
        {
            SetUnplacaeable();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (placed)
        {
            renderer.material = originalMat;
            return;
        }
        SetPlaceable();
    }

    public void SetPlaceable()
    {
        canPlace = true;
        renderer.material = canPlaceMat;
        objCollider.isTrigger = true;
    }
    public void SetUnplacaeable()
    {
        canPlace = false;
        renderer.material = cantPlaceMat;
        objCollider.isTrigger = true;

    }
}
