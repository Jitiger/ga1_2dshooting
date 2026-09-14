using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격
    [Header("On/Off 스프라이트")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private Image _myImage;

    private bool _autoMode;
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private PlayerAutoMove _playerAutoMove;

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();

        if (_myImage == null)
        {
            _myImage = GetComponent<Image>();
        }

        if (player == null)
        {
            Debug.LogError("Scene에서 Player를 찾을 수 없습니다.");
            enabled = false;
            return;
        }

        _playerFire = player.GetComponent<PlayerFire>();
        _playerMove = player.GetComponent<PlayerMove>();
        _playerAutoMove = player.GetComponent<PlayerAutoMove>();

        if (_myImage == null ||
            _playerFire == null ||
            _playerMove == null ||
            _playerAutoMove == null)
        {
            Debug.LogError(
                "UI_AutoButton에 필요한 Image 또는 Player 컴포넌트가 없습니다."
            );

            enabled = false;
            return;
        }

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
        _playerFire.SetAuto(_autoMode);
        _playerMove.enabled = !_autoMode;
        _playerAutoMove.enabled = _autoMode;

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}