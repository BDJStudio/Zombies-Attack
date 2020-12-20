using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovGun : MonoBehaviour, IDragHandler
{
    public GameObject gun;
    
    public float speedGun;
    public bool trig;

    private Image panel;
    private Quaternion posit;
    private Camera cam;

    private void Start()
    {
        panel = GetComponent<Image>();
        cam = Camera.main;
    }

    // управление пушкой по нажатию на экран
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && trig)
        {
            Plane playerPlane = new Plane(Vector3.up, gun.transform.position);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                
                Vector3 targetPoint = ray.GetPoint(hitdist);

                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - gun.transform.position);

                gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, targetRotation, speedGun * Time.deltaTime);
            }
        }
    }

    // альтернативное управление с помощью панели
    public void OnDrag(PointerEventData eventData)
    {
        if (!trig)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panel.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                posit = Quaternion.Euler(0, pos.x / 3, 0);
                gun.transform.rotation = posit;
            }
        }
    }
}
