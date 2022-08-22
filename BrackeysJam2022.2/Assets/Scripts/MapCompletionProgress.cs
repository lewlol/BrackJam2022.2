using UnityEngine;
using UnityEngine.UI;

public class MapCompletionProgress : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform earth;
    public Text EarthDistance; 

    public float distance;
    private void Update()
    {
        DistanceToEarth();
    }
    void DistanceToEarth()
    {
        distance = Vector3.Distance(player.position, earth.position);
        EarthDistance.text ="Home: " + distance.ToString("F1") + "KM";
    }
}