using UnityEngine;
using UnityEngine.UI;

public class MapCompletionProgress : UnityEngine.MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform earth;
    public Text EarthDistance; 

    public float distance;

    Vector3 targetPos;
    RectTransform pointer;
    private void Update()
    {
        DistanceToEarth();



    }
    void DistanceToEarth()
    {
        distance = Vector3.Distance(player.position, earth.position);
        EarthDistance.text = "         " + distance.ToString("F1") + "KM";
    }
}