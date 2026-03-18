using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > 1) pos.x = 0;
        if (pos.x < 0) pos.x = 1;
        if (pos.y > 1) pos.y = 0;
        if (pos.y < 0) pos.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}