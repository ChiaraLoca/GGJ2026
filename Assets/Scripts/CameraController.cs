using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Players")]
    public Transform player1;
    public Transform player2;
    
    [Header("Camera Bounds")]
    [Tooltip("Limiti della camera basati sullo sfondo")]
    public float minX = -10f;
    public float maxX = 10f;
    
    [Header("Screen Edge Margin")]
    [Tooltip("Margine dal bordo dello schermo per i player (in unità world)")]
    public float screenEdgeMargin = 1f;
    
    [Header("Smoothing")]
    public float smoothSpeed = 5f;
    
    private Camera cam;
    private float camHalfWidth;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
            
        UpdateCameraHalfWidth();
    }
    
    void UpdateCameraHalfWidth()
    {
        if (cam.orthographic)
        {
            camHalfWidth = cam.orthographicSize * cam.aspect;
        }
        else
        {
            // Per camera prospettica, calcola la larghezza a distanza z=0
            float distance = Mathf.Abs(transform.position.z);
            camHalfWidth = distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * cam.aspect;
        }
    }
    
    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;
            
        UpdateCameraHalfWidth();
        
        // Calcola il punto medio tra i due player
        float midPointX = (player1.position.x + player2.position.x) / 2f;
        
        // Calcola la posizione target della camera
        float targetX = midPointX;
        
        // Limita la camera ai bounds dello sfondo
        // La camera non può andare oltre i limiti considerando la sua larghezza
        float clampedX = Mathf.Clamp(targetX, minX + camHalfWidth, maxX - camHalfWidth);
        
        // Smooth movement
        Vector3 targetPos = new Vector3(clampedX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        
        // Blocca i player ai bordi dello schermo
        ClampPlayerToScreen(player1);
        ClampPlayerToScreen(player2);
    }
    
    void ClampPlayerToScreen(Transform player)
    {
        // Calcola i bordi dello schermo in world space
        float leftEdge = transform.position.x - camHalfWidth + screenEdgeMargin;
        float rightEdge = transform.position.x + camHalfWidth - screenEdgeMargin;
        
        // Inoltre, non permettere ai player di uscire dai bounds globali
        leftEdge = Mathf.Max(leftEdge, minX);
        rightEdge = Mathf.Min(rightEdge, maxX);
        
        Vector3 pos = player.position;
        pos.x = Mathf.Clamp(pos.x, leftEdge, rightEdge);
        player.position = pos;
    }
    
    // Metodo pubblico per ottenere i limiti attuali dello schermo
    public float GetScreenLeftEdge()
    {
        return transform.position.x - camHalfWidth + screenEdgeMargin;
    }
    
    public float GetScreenRightEdge()
    {
        return transform.position.x + camHalfWidth - screenEdgeMargin;
    }
}
