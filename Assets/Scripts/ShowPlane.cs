using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public class ShowPlane : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public float scanCompleteDelay = 5f;

    private float timer = 0f;
    private int lastPlaneCount = 0;
    private bool scanCompleted = false;

    void Update()
    {
        if (scanCompleted) return;

        int currentPlaneCount = arPlaneManager.trackables.count;

        if (currentPlaneCount == lastPlaneCount)
        {
            timer += Time.deltaTime;
            if (timer >= scanCompleteDelay)
            {
                scanCompleted = true;
                HidePlanes();
            }
        }
        else
        {
            timer = 0f;
            lastPlaneCount = currentPlaneCount;
        }
    }

    void HidePlanes()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            var meshRenderer = plane.GetComponent<MeshRenderer>();
            var visualizer = plane.GetComponent<ARPlaneMeshVisualizer>();

            if (meshRenderer != null)
                meshRenderer.enabled = false;

            if (visualizer != null)
                visualizer.enabled = false;
        }
    }
}
