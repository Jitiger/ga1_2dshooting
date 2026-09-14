using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [Header("자동 이동 속도")]
    [SerializeField] private float _speed;

    [Header("추적 중단 Y 위치")]
    [SerializeField] private int _stopTrackingY = 2;

    private GameObject _target = null;


    private void Update()
    {
        if (_target == null ||
            _target.transform.position.y < -_stopTrackingY)
        {
            FindNearestTarget();
        }

        Move();
    }


    private void Move()
    {
        if (_target == null)
        {
            return;
        }

        // 플레이어에서 적 방향 구하기
        Vector3 diff =
            _target.transform.position -
            transform.position;

        Vector3 direction = diff;

        // 적과 플레이어의 Y축 차이에 따라
        // 앞 / 뒤 이동 방향 결정
        if (diff.y >= 3f)
        {
            direction.y = 1f;
        }
        else
        {
            direction.y = -1f;
        }

        direction.Normalize();

        // 기본 이동속도 + 이동속도 업그레이드
        float finalSpeed =
            _speed +
            UpgradeManager.Instance
                .Upgrades[2]
                .CurrentValue;

        // 최종 이동
        transform.position += direction * finalSpeed * Time.deltaTime;
    }


    private void FindNearestTarget()
    {
        GameObject[] targets =
            GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length == 0)
        {
            _target = null;
            return;
        }

        _target = targets[0];

        float minDistance =
            float.MaxValue;

        foreach (GameObject enemy in targets)
        {
            if (enemy.transform.position.y < -_stopTrackingY)
            {
                continue;
            }

            float distance =
                Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;

                _target = enemy;
            }
        }
    }
}