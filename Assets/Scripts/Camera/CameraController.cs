using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private DayNight m_dayNight = null;

    private const float DURATION = 2f;

    private bool m_isPlay = false;
    private Transform m_player = null;

    private Vector3 m_initialPosition = new Vector3(-2.5f, 12f, -7f);
    private Vector3 m_initialAngle = new Vector3(55f, 25f, 0f);
    
    private void Awake()
    {
        m_player = GameObject.FindWithTag(Const.TAG_PLAYER).transform;
        transform.position = m_initialPosition;
        transform.eulerAngles = m_initialAngle;
    }

    private void LateUpdate()
    {
        if (!m_isPlay)
        {
            transform.position = m_player.position + m_initialPosition;
            transform.eulerAngles = m_initialAngle;
        }
    }

    public void Play2(Transform _target)
    {
        m_isPlay = true;

        InputManager.Instance.OnInput = null;
        m_player.GetComponent<Player>().CancelCurrentAction();

        Sequence mySequence = DOTween.Sequence();

        Vector3 position = Vector3.zero;
        Vector3 angle = Vector3.zero;

        angle.x = 25f;

        position = _target.position;
        position.y = 3f;
        position.z -= 12f;

        mySequence.Append(transform.DORotate(angle, DURATION));
        mySequence.Insert(0f * DURATION, transform.DOMove(position, DURATION));
        angle.y = 100f;

        position = _target.position;
        position.x -= 12f;
        position.y = 3f;

        mySequence.Append(transform.DORotate(angle, DURATION));
        mySequence.Insert(1f * DURATION, transform.DOMove(position, DURATION));

        angle.y = 190f;

        position = _target.position;
        position.y = 3f;
        position.z += 12f;

        mySequence.Append(transform.DORotate(angle, DURATION));
        mySequence.Insert(2f * DURATION, transform.DOMove(position, DURATION));

        angle.y = 280f;

        position = _target.position;
        position.x += 12f;
        position.y = 3f;

        mySequence.Append(transform.DORotate(angle, DURATION));
        mySequence.Insert(3f * DURATION, transform.DOMove(position, DURATION));

        mySequence.Append(transform.DORotate(m_initialAngle, DURATION));
        mySequence.Insert(4f * DURATION, transform.DOMove(m_player.position + m_initialPosition, DURATION))
            .AppendCallback(() =>
            {
                m_dayNight.ChangeDay();
                InputManager.Instance.OnInput = m_player.GetComponent<Player>().OnInput;
                m_isPlay = false;
            });
    }

    public void Play(Transform _target)
    {
        m_isPlay = true;

        InputManager.Instance.OnInput = null;
        m_player.GetComponent<Player>().CancelCurrentAction();

        Sequence mySequence = DOTween.Sequence();

        Vector3 position = Vector3.zero;
        Vector3 angle = Vector3.zero;

        angle.x = 25f;

        position = _target.position;
        position.y = 5f;
        position.z -= 12f;

        transform.DOMove(position, DURATION).OnStart(() => { transform.DORotate(angle, DURATION); }).OnComplete(() => {
            angle.y = 100f;

            position = _target.position;
            position.y = 5f;
            position.x -= 12f;

            transform.DOMove(position, DURATION).OnStart(() => { transform.DORotate(angle, DURATION); }).OnComplete(() => {
                angle.y = 190f;

                position = _target.position;
                position.y = 5f;
                position.z += 12f;

                transform.DOMove(position, DURATION).OnStart(() => { transform.DORotate(angle, DURATION); }).OnComplete(() => {
                    angle.y = 280f;

                    position = _target.position;
                    position.y = 5f;
                    position.x += 12f;

                    transform.DOMove(position, DURATION).OnStart(() => { transform.DORotate(angle, DURATION); }).OnComplete(() => {

                        transform.DOMove(m_player.position + m_initialPosition, DURATION).OnStart(() => { transform.DORotate(m_initialAngle, DURATION); }).OnComplete(() => {

                            m_dayNight.ChangeDay();
                            InputManager.Instance.OnInput = m_player.GetComponent<Player>().OnInput;
                            m_isPlay = false;

                        });
                    });
                });
            });
        });


    }
}
