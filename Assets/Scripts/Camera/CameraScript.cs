using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }

    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private GameObject speedParticles = null;
    [HideInInspector]public Transform playerCar;

    [Space(10f)]
    [Header("CameraFovSettings")]

    [SerializeField] private float cameraFOV = 60;
    [SerializeField] private float cameraFOVIncreaser = 0;
    [SerializeField] private float FOVTimeAnimation = 0;
    [SerializeField] private float FOVTimeDelay = 0;
    [SerializeField] private Ease easeType = Ease.InOutQuad;

    [Space(10f)]
    [Header("CameraMoveSettings")]

    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, 0);
    [SerializeField] private float moveTimeAnimation = 0;

    private Vector3 cameraStartPosition;
    private bool startTrack;

    private void Awake()
    {
        Instance = this;
        cameraStartPosition = transform.position;
        gameCamera.fieldOfView = cameraFOV;

    }

    private void Start()
    {

        Events.Instance.StartGameEvent.AddListener(() => startTrack = true);

        Events.Instance.GreenZoneClickEvent += () => gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay)).SetAutoKill(false); ;

        Events.Instance.BlueZoneClickEvent += () => gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser * 2f, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay)).SetAutoKill(false);
        Events.Instance.BlueZoneClickEvent += () => speedParticles.SetActive(true);

        Events.Instance.UpgradeEvent += () => startTrack = false;

    }

    private void OnDisable()
    {

        Events.Instance.StartGameEvent.RemoveListener(() => startTrack = true);

        Events.Instance.GreenZoneClickEvent -= () => gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay)).SetAutoKill(false); ;

        Events.Instance.BlueZoneClickEvent -= () => gameCamera.DOFieldOfView(cameraFOV + cameraFOVIncreaser * 2f, FOVTimeAnimation).SetEase(easeType).OnComplete(() => gameCamera.DOFieldOfView(cameraFOV, FOVTimeAnimation).SetEase(easeType).SetDelay(FOVTimeDelay)).SetAutoKill(false);
        Events.Instance.BlueZoneClickEvent -= () => speedParticles.SetActive(true);

        Events.Instance.UpgradeEvent += () => startTrack = false;

        playerCar = null;

    }

    private void Update()
    {
        if (startTrack == false)
        {
            transform.position = cameraStartPosition;
        }
    }

    private void FixedUpdate()
    {

        if (startTrack && playerCar != null)
        {
            MoveCameraToCar();
        }

    }

    private void MoveCameraToCar()
    {
        Tween tween = transform.DOMove(
            new Vector3(
                playerCar.position.x,
                playerCar.position.y + cameraOffset.y,
                playerCar.position.z + cameraOffset.z),
            moveTimeAnimation);

        if (startTrack)
        {
            tween.Play();
        }
        else
        {
            tween.Kill();
        }
    }
}
