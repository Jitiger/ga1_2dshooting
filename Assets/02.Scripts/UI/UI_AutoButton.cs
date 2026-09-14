using UnityEngine;
using UnityEngine.UIElements;

public class UI_AutoButton : MonoBehaviour
{
    //버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격
    [Header("on/off 스프라이트")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private Image _myImage;
    private bool _automode = false;
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
        AutoToggle();
    }

    public void AutoToggle()
    {
        _automode = !_automode;

        _player.GetComponent<PlayerFire>().SetAuto(_automode);
        _player.GetComponent<PlayerMove>().enabled = !_automode;
        _player.GetComponent<PlayerAutoMove>().enabled = _automode;

        _myImage.sprite = _automode ? _onSprite : _offSprite;
    }
}