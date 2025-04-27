using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public class ShowPlane : MonoBehaviour
{
    private PlaceOnPlane placeOnPlane;  // Компонент, который управляет PlacedPrefab
    [SerializeField] GameObject XROrigin;
    private GameObject Plane;
    private ARPlaneManager arPlaneManager;
    void Start()
    {
        // Найдем компонент PlaceOnPlane в нужном объекте
        if (XROrigin.TryGetComponent<PlaceOnPlane>(out placeOnPlane))
        {
            Debug.Log("Компонент PlaceOnPlane успешно получен, можно с ним работать");
            placeOnPlane.OnFirstObjectPlaced += DeactivatePlane;
        }
        else
        {
            Debug.LogWarning("Компонент PlaceOnPlane не найден на объекте XROrigin");
        }
        if (XROrigin.TryGetComponent<ARPlaneManager>(out arPlaneManager))
        {
            Debug.Log("Компонент ARPlaneManager успешно получен, можно с ним работать");
        }
        else
        {
            Debug.LogWarning("Компонент ARPlaneManager не найден на объекте XROrigin");
        }
    }

    void OnDisable()
    {
        if (placeOnPlane != null)
            placeOnPlane.OnFirstObjectPlaced -= DeactivatePlane;
    }

    void DeactivatePlane()
    {
        arPlaneManager.enabled = false;
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
    
    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var added in args.added)
        {
            HidePlane(added);
        }
    }

    void HidePlane(ARPlane plane)
    {
        // Отключаем визуализацию
        var meshRenderer = plane.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.enabled = false;

        // Иногда используется ARPlaneMeshVisualizer — его тоже можно отключить
        var visualizer = plane.GetComponent<ARPlaneMeshVisualizer>();
        if (visualizer != null)
            visualizer.enabled = false;
    }
}