using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    float _speed = 10;

    Vector3 _destPos;

 
    float wait_run_ratio;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle, // ��ٸ���
    }
    PlayerState _state = PlayerState.Idle;

    void OnRunEvent()
    {
        Debug.Log("�ѹ�");
    }
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.00001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        //���ϸ��̼� ó��
        Animator anim = GetComponent<Animator>();
        //���� ���ӻ��¸� �Ѱ��ش�
        anim.SetFloat("speed", _speed);
    }
    void UpdateIdle()
    {
        //���ϸ��̼� ó��
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }
    void Start()
    {

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;


        //Temp
        //Managers.Resource.Instantiate("UI/UI_Button");



    }
    //GameObject (Player)
    //Transform
    // PlayerController 
    void Update()
    {
            switch (_state)
            {
                case PlayerState.Die:
                    UpdateDie();
                    break;
                case PlayerState.Moving:
                    UpdateMoving();
                    break;
                case PlayerState.Idle:
                    UpdateIdle();
                    break;
            }

    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;

        //layermask ��Ʈ �÷���
        //int mask = (1 << 6)| (1 << 7);
        LayerMask mask = LayerMask.GetMask("Wall");

        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }
    }
  /*  void OnKeyboard()
    {
        float _turnSpeed = 0.2f;

        //Local -> World
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _turnSpeed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _turnSpeed);
            transform.position += Vector3.back * Time.deltaTime * _speed;



        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _turnSpeed);
            transform.position += Vector3.left * Time.deltaTime * _speed;



        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _turnSpeed);
            transform.position += Vector3.right * Time.deltaTime * _speed;



        }
        _moveToDest = false;
    }*/
}
