using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick Joystick;
    private Vector3 _dir;
    Animator _anim;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _speed;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        _dir = new Vector3(Joystick.Horizontal, 0, Joystick.Vertical);
        moveCharacter(_dir);
    }
    private void FixedUpdate()
    {
    }
    private void moveCharacter(Vector3 MoveDirection)
    {
        if (MoveDirection == Vector3.zero)
        {
            _anim.SetBool("isRunning", false);
            _dir = transform.rotation.eulerAngles;
        }
        else
        {
            _anim.SetBool("isRunning", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 100);
        }
        _rb.velocity = MoveDirection * _speed;
    }
}