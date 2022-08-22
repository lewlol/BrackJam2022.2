using UnityEngine;
using UnityEngine.UI;

public class MapCompletionProgress : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform earth;

    public float distance;
    private void Update()
    {
        DistanceToEarth();
    }
    void DistanceToEarth()
    {
        distance = Vector3.Distance(player.position, earth.position);
    }
}