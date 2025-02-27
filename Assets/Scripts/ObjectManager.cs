using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation.Samples;

public class ObjectManager : MonoBehaviour
{
    public PlaceOnPlane placeOnPlane;
    private List<GameObject> placedObjects = new List<GameObject>();
    private GameObject selectedObject = null; // Выбранный объект для удаления

    public void LockObject()
    {
        GameObject spawnedObject = placeOnPlane.GetSpawnedObject();
        if (spawnedObject != null)
        {
            placedObjects.Add(spawnedObject);
            placeOnPlane.spawnedObject = null; // Готовим место для нового объекта
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие
        {
            SelectObject();
        }
    }

    private void SelectObject()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // Игнорируем UI
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (placedObjects.Contains(hit.collider.gameObject))
            {
                selectedObject = hit.collider.gameObject;
                Debug.Log("Выбран объект в ObjectManager (для удаления): " + selectedObject.name);
            }
        }
    }

    public void DeleteSelectedObject()
    {
        if (selectedObject != null)
        {
            //placedObjects.Remove(selectedObject);
            Destroy(selectedObject);
            selectedObject = null;
            Debug.Log("Объект удален.");
        }
    }
}
