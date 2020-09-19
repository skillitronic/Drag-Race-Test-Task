using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }

    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private GameObject speedParticles = null;
    [HideInInspector] public Transform playerCar = null;

    [Space(10f)]
    [Header("CameraFovSettings")]

    [SerializeField] private float cameraFOV = 60;
    [SerializeField] private float cameraFOVIncreaser = 0;
    [SerializeField] private float FOVTimeAnimation = 0;
    [SerializeField] private float FOVTimeDelay = 0 ;
    [SerializeField] private Ease easeType = Ease.InOutQuad;

    [Space(10f)]
    [Header("CameraMoveSettings")]

    [SerializeField] private Vector3 cameraOffset =  new Vector3(0,0,0);
    [SerializeField] private float moveTimeAnimation = 0;

    private Vector3 cameraStartPosition;
    private Tween tween;

    private void Awake()
    {
        Instance = this;
        cameraStartPosition = transform.position;
        gameCamera.fieldOfView = cameraFOV;

        tween = gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay)).SetAutoKill(false);
        tween.Pause();
    }

    private void Start()
    {
        Events.Instance.WinEvent.AddListener(() => transform.position = cameraStartPosition);

        Events.Instance.StartGameEvent.AddListener(MoveCameraToCar);

        Events.Instance.GreenZoneClickEvent += ChangeCameraFOVAnimation;
        Events.Instance.BlueZoneClickEvent += () => speedParticles.SetActive(true);
    }

    private void OnDisable()
    {
        Events.Instance.StartGameEvent.RemoveListener(MoveCameraToCar);

        Events.Instance.ZoneClickEvent -= () => speedParticles.SetActive(true);
        Events.Instance.ZoneClickEvent += ChangeCameraFOVAnimation;
    }

    public void ChangeCameraFOVAnimation()
    {   
        if (tween.IsPlaying())
        {
            return;
        }
        tween.Play();

        if (tween.IsPlaying() == false)
        {
            tween.Restart();
        }
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
