using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniVRM10;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class CutScene : MonoBehaviour
{
    [SerializeField] TutorialStart leveltTrigger;
    [SerializeField] string sceneToLoad;
    public GameManager gameManager;
    //public PathGuide PathGuide;
    [SerializeField] GameObject player;
    Animator animator;
    [SerializeField] GameObject wayPoint;
    [SerializeField] GameObject wayPoint2;
    [SerializeField] GameObject cameraWayPoint;
    [SerializeField] GameObject planeModel;
    [SerializeField] float angle;
    [SerializeField] GameObject scaleingImage;
    private ObbyController obbyController;
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public Camera cutsceneCamera2;
    float speed = 5f;
    float cameraSpeed = 0.5f;

    bool hasPlayedSound = false;

    private GameObject planeInstance;  // ���������� ��� �������� ������ �� ���������������� ������

    void OnEnable()
    {

        //PathGuide.enabled = false;
        Transform skinHolder = GameObject.Find("SkinHolder")?.transform; // ���� ������ SkinHolder
        if (skinHolder != null)
        {
            animator = skinHolder.GetComponentInChildren<Animator>(); // ���� Animator ������ SkinHolder
        }
        else 
        {
            Debug.LogError("SkinHolder not found!");
        }
        planeModel = GameManager.Instance.GetSelectedModelPrefab();
        // ��������, ��� planeModel ����� � ����������
       

        // ������������� ObbyController
        obbyController = player.GetComponent<ObbyController>();
        obbyController.enabled = false;

        // ������������ �����
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        // ������������ ������ ������� � ����� ����� �������
        if (planeModel != null)
        {
            planeInstance = Instantiate(planeModel, wayPoint.transform.position, Quaternion.Euler(0, angle, 0));
            Debug.Log("Plane created at position: " + wayPoint.transform.position);
            
        }
        else
        {
            Debug.LogError("Plane model is missing!");
        }

        Debug.Log("=============DEBUG==============");
        Debug.Log(GameManager.Instance.GetSelectedModelPrefab());
        Debug.Log(GameManager.Instance.GetSelectedSkin());

    }

    public void Initialize(string level)
    {
        sceneToLoad = level;
    }

    void Update()
    {
        // �������� �������� ������
       
        animator.SetBool("IsWalking", true);

        // ������� ������
        MovePlayer();

        // ������� ������
        MoveCamera();

        // ��������� �������� ������������ ������ � �������� �������
        StartCoroutine(sceneCorrutine());
    }

    void MovePlayer()
    {
        float step = speed * Time.deltaTime;
        player.transform.position = Vector3.MoveTowards(player.transform.position, wayPoint.transform.position, step);

        Vector3 direction = wayPoint.transform.position - player.transform.position;
        if (direction.magnitude > 0.1f) // ����� �������� �������� ��� ���������� �����
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, step * 0.1f);
        }
    }

    void MoveCamera()
    {
        float cameraStep = cameraSpeed * Time.deltaTime;
        cutsceneCamera.transform.position = Vector3.MoveTowards(cutsceneCamera.transform.position, cameraWayPoint.transform.position, cameraStep);
    }

    IEnumerator sceneCorrutine()
    {
        
        yield return new WaitForSeconds(3);
        SkinnedMeshRenderer meshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
        EndCutScene(player);
        meshRenderer.enabled = false; // ���������� ������
        cutsceneCamera.enabled = false; // ��������� ������ ������
        cutsceneCamera2.enabled = true;
        // �������� ������ ������
        StartCoroutine(MovePlaneForward()); // �������� �������� �������
    }

    IEnumerator MovePlaneForward()
    {
        float step = speed * Time.deltaTime;
        // ��������, ��� ������ ��� ������
        if (planeInstance == null)
        {
            Debug.LogError("Plane instance is missing! It wasn't created properly.");
            yield break;  // ����� �� ��������, ���� ������ �� ��� ������
        }
        
        float moveSpeed = 10f; // �������� �������� �������
        Vector3 startPosition = planeInstance.transform.position; // ��������� ������� �������

        while (true)
        {
            // ������� ������ ������ �� ��� Z, ������������ ��� ������� �������
            planeInstance.transform.position = Vector3.MoveTowards(planeInstance.transform.position, wayPoint2.transform.position, step);

            yield return null; // ��� ���� ����

            // ������������� ������� ������ �� ��������, ��������, ����� ������ ��������� ����� wayPoint2
            if (Vector3.Distance(planeInstance.transform.position, wayPoint2.transform.position) < 0.1f)
            {
                StartCoroutine(sceneSwitch());
                scaleingImage.SetActive(true);
                YG2.InterstitialAdvShow();
                break;  // ������ ������ ����, ������� �� ��������
            }
        }

        // ������ ������ ���� (wayPoint2), ����� ���������� ���������� ��������
        Debug.Log("Plane reached the destination!");
    }

    IEnumerator sceneSwitch()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneToLoad);
    }

    void EndCutScene(GameObject player)
    {
        if(!hasPlayedSound) 
        { 
            hasPlayedSound = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.fly);

        }
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.useGravity = true; // �������� ���������� �������
        }
        // �������� �������� ����� ������� � ���������� �������
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerLayer"), LayerMask.NameToLayer("EnvironmentLayer"), false);
    }

}
