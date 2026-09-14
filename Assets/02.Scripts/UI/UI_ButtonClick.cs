using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    [Header("클릭시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;
    private float _scale = 1.0f;
    private bool _isBumping = false;
    private float _elapsedTime = 0;
    private const float BumpDuration = 0.6f;
    private const float BumpScale = 1.2f;
    private AudioSource _audioSource;
    private Button _button;

    public void PlayAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0;
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button.onClick.AddListener(PlayAnimation);
        _button.onClick.AddListener(PlaySound);
    }

    private void Update()
    {
        if (!_isBumping) return;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        //2. 누적시간과 애니메이션 커브에 따른 스케일 변경
        float time = _elapsedTime / BumpDuration; // 얼마나 지났는지 퍼센트 (0~1)
        float curveValue = _bumpCurve.Evaluate(time); //퍼센트에 따라 커브 애니메이션 값 추출
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * BumpScale, curveValue);
    }
}