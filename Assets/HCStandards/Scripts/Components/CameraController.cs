using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [HideInInspector] public Camera mainCamera;
    public float followSpeed = 1f;
    public float lookSpeed = 1f;
    public float xSpeed = 1f;
    public Transform followTarget;
    public Transform lookTarget;
    public Vector3 offset;
    public Vector3 rotation;
    public bool useTargetRotation = true;
    Quaternion quat;
    Vector3 newlook;
    Vector3 curPos;
    Vector3 vec;
    float x;

    protected override void Awake()
    {
        base.Awake();
        offset *= HCStandards.Settings.CameraScale();
        mainCamera = GetComponent<Camera>();
    }


    public void SetCameraFollow(Transform tr)
    {
        followTarget = tr;
        transform.position = followTarget.position + offset;
        mainCamera = Camera.main;
    }
    
    public void SetCameraLook(Transform lookAt)
    {
        lookTarget = lookAt;
        newlook = lookTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation((newlook + rotation));
    }

    void FixedUpdate()
    {
        if (!HCStandards.Game.IsGameStarted)
            return;

        if (!HCStandards.Game.IsGameStarted)
            return;

        if (useTargetRotation)
        {
            vec = followTarget.position + (followTarget.rotation * offset);
        }
        else
        {
            vec = followTarget.position + (offset);
            x = transform.position.x;
            x = Mathf.Lerp(transform.position.x, vec.x, xSpeed * Time.deltaTime);
            vec.x = 0f;
        }

        curPos = Vector3.Lerp(transform.position, vec + Vector3.right * x, followSpeed * Time.deltaTime);
        transform.position = curPos;

        newlook = lookTarget.position - transform.position;
        quat = Quaternion.LookRotation(newlook + rotation, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, quat, lookSpeed * Time.deltaTime);
    }


    public void StopCamera()
    {
        enabled = false;
        lookTarget = null;
        followTarget = null;
    }

    

    public void UpdateValues()
    {
        if (!followTarget)
            return;
        transform.position = followTarget.position + offset;
        newlook = lookTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation((newlook + rotation));
    }

    private void OnValidate()
    {
        if (!followTarget)
            return;
        transform.position = followTarget.position + offset;
        newlook = lookTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation((newlook + rotation));
    }
}
