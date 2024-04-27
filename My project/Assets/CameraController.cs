using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] players;
    public float minZoom = 5f;
    public float maxZoom = 10f;
    public float zoomSpeed = 5f;
    public float followSpeed = 5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (players.Length == 0)
            return;

        Vector3 centerPoint = GetCenterPoint();

        MoveCamera(centerPoint);

        ZoomCamera();

    }

    Vector3 GetCenterPoint()
    {
        if (players.Length == 1)
            return players[0].position;

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 1; i < players.Length; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    }

    void MoveCamera(Vector3 centerPoint)
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, centerPoint, followSpeed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, -10f);
    }

    void ZoomCamera()
    {
        float distance = GetGreatestDistance();

        float newSize = Mathf.Lerp(minZoom, maxZoom, distance / 10f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, zoomSpeed * Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        if (players.Length == 1)
            return 0f;

        float maxDistance = 0f;
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                float distance = Vector3.Distance(players[i].position, players[j].position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }
        return maxDistance;
    }
}
