using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private SimpleAnimancer _animancer;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private string _idleAnimName = "Idle";
    [SerializeField] private float _idleAnimSpeed = 1f;
    [SerializeField] private string _runAnimName = "Running";
    [SerializeField] private float _runAnimSpeed = 2f;
    [SerializeField] private string _climbAnimName = "Climb";
    [SerializeField] private float _climbAnimSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 10f;

    private bool _isClimbingUpward = false;
    private bool _isClimbingDownward = false;
    private bool _isClimbed = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fixedJoystick.Vertical != 0 || fixedJoystick.Horizontal != 0)
        {
            RunAnimation();
            //transform.DORotate((Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal),1f);
        }
        else if (_isClimbingUpward && !_isClimbed)
        {
            Debug.Log("1");
            ClimbAnimation();
            transform.DOMoveY(7f, 0.07f).SetRelative();
        }
        else if (_isClimbed)
        {
            Debug.Log("2");
            _audio.Stop();
            IdleAnimation();
        }
        else if (_isClimbingDownward)
        {
            Debug.Log("3");
            _audio.Stop();
            IdleAnimation();
            //ClimbAnimation();
            //transform.DOMoveY(-7f, 0.07f).SetRelative();
        }
        else
            IdleAnimation();

       
        transform.DOMove((Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal), 0.02f).SetRelative();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal)), Time.deltaTime * _rotateSpeed);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LadderStart")
        {
            _isClimbingUpward = true;
            _isClimbingDownward = false;
            _isClimbed = false;
            _audio.Play();
        }
        else if (other.gameObject.tag == "LadderEnd")
        {
            _isClimbingUpward = false;
            _isClimbingDownward = true;
            _isClimbed = true;
        }
       
    }

    public void IdleAnimation()
    {
        PlayAnimation(_idleAnimName, _idleAnimSpeed);
    }

    public void RunAnimation()
    {
        PlayAnimation(_runAnimName, _runAnimSpeed);
    }

    public void ClimbAnimation()
    {
        PlayAnimation(_climbAnimName, _climbAnimSpeed);
    }
    public void PlayAnimation(string animName, float animSpeed)
    {
        _animancer.PlayAnimation(animName);
        _animancer.SetStateSpeed(animSpeed);
    }
}
