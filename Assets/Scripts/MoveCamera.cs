using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]private float _mouseSensitive; // 마우스 민감도
    [SerializeField]private float _recoilResetSpeed; // 반동이 사라지는 속도
    [SerializeField] private float _yMinAxis;
    [SerializeField] private float _yMaxAxis;



    private float _recoil; // 반동의 크기
    private float _currentRecoil; // 현재 반동 값을 저장하는 변수
    private float _eulerAngleY;
    private float _eulerAngleX;
    private float _mouseX;
    private float _mouseY;


    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        InputMousePos();
        RotateCamera();
        ResetRecoil();
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // 마우스 위치 입력
    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    // 카메라 회전
    private void RotateCamera()
    {
        // 마우스 입력에 따라 카메라 회전
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        // 현재 반동 값을 포함한 회전 계산
        Quaternion rotation = Quaternion.Euler(_eulerAngleX + _currentRecoil, _eulerAngleY, 0);
        transform.rotation = rotation;
    }

    public void ApplyRecoil(float recoil)
    {
        // 반동을 Y축 회전 값에 추가
        _recoil = recoil;
        _currentRecoil = -_recoil;
    }

    private void ResetRecoil()
    {
        // 반동을 점진적으로 사라지게 함
        _currentRecoil = Mathf.Lerp(_currentRecoil, 0f, _recoilResetSpeed);
    }
}

