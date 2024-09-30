using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class ZoomState : AttackState
{
    private Camera _camera;
    private PlayerData _playerData;
    private PlayerStateMachine _player;
    private CinemachineVirtualCamera _virtualCamera;
    private Animator _anim;
    public ZoomState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _camera = _playerData.Camera;
        _virtualCamera = _playerData.PlayerVirtualCamera;
    }

    public override void Enter()
    {
        Debug.Log("현재 Zoom State에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }

    public override void Update()
    {
        ZoomInAim();
        // 줌상태에서 firestate에서 다시 돌아올때 오른쪽 마우스클릭을 먼저때면, ZommState에서 계속 유지되므로, 한번더 줌버튼눌렀는지 체크.
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButton(1) == false)
        {
            ZoomOutAim();
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
        if (Input.GetMouseButtonDown(0))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Fire]);
        }
    }
    public override void Exit()
    {
        Debug.Log("Zoom State에서 나감");
    }

    void ZoomInAim()
    {
        _playerData.IsZoom = EZoom.ZoomIn;
        _playerData.Aims[(int)EZoom.ZoomOut].SetActive(false);
        _anim.SetBool("isZoom", true);
        IZoomable zoomable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IZoomable>();
        zoomable.ZoomIn(_virtualCamera);
        _playerData.Aims[(int)EZoom.ZoomIn].SetActive(true);


    }

    void ZoomOutAim()
    {
        _playerData.IsZoom = EZoom.ZoomOut;
        _playerData.Aims[(int)EZoom.ZoomIn].SetActive(false);
        _anim.SetBool("isZoom", false);
        IZoomable zoomable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IZoomable>();
        zoomable.ZoomOut(_virtualCamera);
        _playerData.Aims[(int)EZoom.ZoomOut].SetActive(true);
    }

}
