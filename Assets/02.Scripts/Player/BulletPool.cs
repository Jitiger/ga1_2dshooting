using UnityEngine;

private Bullet GetBulletFromPool(Bullet[] pool)
{
    foreach (Bullet bullet in pool)
    {
        if (bullet == null)
        {
            continue;
        }

        if (bullet.gameObject.activeSelf == false)
        {
            bullet.gameObject.SetActive(true);

            return bullet;
        }
    }

    Debug.LogWarning("Bullet Pool에 사용할 수 있는 총알이 없습니다.");

    return null;
}