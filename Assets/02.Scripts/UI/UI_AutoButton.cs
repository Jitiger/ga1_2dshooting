using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격
    [Header("on/off스프라이트")]
    [SerializeField] private Sprite _onSprite;

    [SerializeField] private Sprite _offSprite;

    private Image _myImage;

    private bool _autoMode = false;
    private Player _player;

    private void Start()
    {
        _myImage = GetComponent<Image>();
        _player = FindAnyObjectByType<Player>();

        // 처음에는 오토 모드를 끈 상태로 시작한다.
        _autoMode = false;
        ApplyAutoMode();
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;
        ApplyAutoMode();
    }

    private void ApplyAutoMode()
    {
        if (_player == null || _myImage == null) return;

        _player.GetComponent<PlayerFire>().SetAuto(_autoMode);
        _player.GetComponent<PlayerMove>().enabled = !_autoMode;
        _player.GetComponent<PlayerAutoMove>().enabled = _autoMode;

        // 오토 모드에 따라 보여지는 이미지 스프라이트 교체
        // 변수 = 조건 ? true일때의 값 : false일때의 값
        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}
