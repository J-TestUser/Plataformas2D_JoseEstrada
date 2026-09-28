using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _puaseCanvas;

    public static CanvasManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    public void ChangeCanvasStatus()
    {
        if (_puaseCanvas.activeInHierarchy)
        {
            _puaseCanvas.SetActive(false);
        }
        else
        {
            _puaseCanvas.SetActive(true);
        }

    }
}
