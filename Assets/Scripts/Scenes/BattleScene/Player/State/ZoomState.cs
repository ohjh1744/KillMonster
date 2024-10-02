using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class ZoomState : AttackState
{
    private PlayerData _playerData;

    private PlayerStateMachine _player;

    private Animator _anim;
    public ZoomState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
    }

    public override void Enter()
    {
        Debug.Log("���� Zoom State�� ����!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }

    public override void Update()
    {
        ZoomInAim();
        // �ܻ��¿��� firestate���� �ٽ� ���ƿö� ������ ���콺Ŭ���� ��������, ZommState���� ��� �����ǹǷ�, �ѹ��� �ܹ�ư�������� üũ.
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
        Debug.Log("Zoom State���� ����");
    }

    private void ZoomInAim()
    {
        _playerData.IsZoom = EZoom.ZoomIn;
        _player.Aims[(int)EZoom.ZoomOut].SetActive(false);
        _anim.SetBool("isZoom", true);
        IZoomable zoomable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IZoomable>();
        zoomable.ZoomIn();
        _player.Aims[(int)EZoom.ZoomIn].SetActive(true);


    }

    private void ZoomOutAim()
    {
        _playerData.IsZoom = EZoom.ZoomOut;
        _player.Aims[(int)EZoom.ZoomIn].SetActive(false);
        _anim.SetBool("isZoom", false);
        IZoomable zoomable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IZoomable>();
        zoomable.ZoomOut();
        _player.Aims[(int)EZoom.ZoomOut].SetActive(true);
    }

}
