using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float _mouseSensitive; // ���콺 �ΰ���
    [SerializeField] private float _recoilResetSpeed; // �ݵ��� ������� �ӵ�
    [SerializeField] private float _yMinAxis;
    [SerializeField] private float _yMaxAxis;
    [SerializeField] private GameManager _gameManager;


    private float _recoil; // �ݵ��� ũ��
    private float _currentRecoil; // ���� �ݵ� ���� �����ϴ� ����
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
        if (_gameManager.GameState == GameState.����)
        {
            InputMousePos();
            RotateCamera();
            ResetRecoil();
        }
        else if(_gameManager.GameState == GameState.��)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ���콺 ��ġ �Է�
    private void InputMousePos()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    // ī�޶� ȸ��
    private void RotateCamera()
    {
        // ���콺 �Է¿� ���� ī�޶� ȸ��
        _eulerAngleY += _mouseX * _mouseSensitive;
        _eulerAngleX -= _mouseY * _mouseSensitive;

        _eulerAngleX = Mathf.Clamp(_eulerAngleX, -_yMinAxis, _yMaxAxis);

        // ���� �ݵ� ���� ������ ȸ�� ���
        Quaternion rotation = Quaternion.Euler(_eulerAngleX + _currentRecoil, _eulerAngleY, 0);
        transform.rotation = rotation;
    }

    public void ApplyRecoil(float recoil)
    {
        // �ݵ��� Y�� ȸ�� ���� �߰�
        _recoil = recoil;
        _currentRecoil = -_recoil;
    }

    private void ResetRecoil()
    {
        // �ݵ��� ���������� ������� ��
        _currentRecoil = Mathf.Lerp(_currentRecoil, 0f, _recoilResetSpeed);
    }
}

