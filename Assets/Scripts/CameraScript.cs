using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }

    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameObject speedParticles;
    [HideInInspector] public Transform playerCar;

    [Space(10f)]
    [Header("CameraFovSettings")]

    [SerializeField] private float cameraFOV = 60;
    [SerializeField] private float cameraFOVIncreaser;
    [SerializeField] private float FOVTimeAnimation;
    [SerializeField] private float FOVTimeDelay;
    [SerializeField] private Ease easeType;

    [Space(10f)]
    [Header("CameraMoveSettings")]

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float moveTimeAnimation;

    private void Awake()
    {
        Instance = this;
        gameCamera.fieldOfView = cameraFOV;
    }

    private void Start()
    {
        Events.Instance.StartGameEvent.AddListener(MoveCameraToCar);
        Events.Instance.ZoneClickEvent += () => speedParticles.SetActive(true);
    }

    private void OnDisable()
    {
        Events.Instance.StartGameEvent.RemoveListener(MoveCameraToCar);
        Events.Instance.ZoneClickEvent -= () => speedParticles.SetActive(true);
    }

    public void ChangeCameraFOVAnimation()
    {
        gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay));
    }

    private void MoveCameraToCar()
    {
        gameObject.transform.DOMove(
                new Vector3(
                    playerCar.position.x,
                    playerCar.position.y + cameraOffset.y,
                    playerCar.position.z + cameraOffset.z),
            moveTimeAnimation);
    }
}
