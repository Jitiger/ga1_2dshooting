using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject GreenBullet;

    public Transform RightFirePoint;
    public Transform LeftFirePoint;

    public GameObject RedBullet;
    public Transform SubRightFirePoint;
    public Transform SubLeftFirePoint;

    public float CoolTime = 0.5f;
    public float CoolTimer = 0;

    public bool AutoFireMode = false;

    private void Start()
    {
        CoolTimer = CoolTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        CoolTimer -= Time.deltaTime;

        if (CoolTimer <= 0 &&
            (Input.GetKeyDown(KeyCode.Space) || AutoFireMode))
        {
            Fire();
            SubFire();

            CoolTimer = CoolTime;
        }
    }

    public void SubFire()
    {
        GameObject subRightBullet = Instantiate(RedBullet);
        subRightBullet.transform.position = SubRightFirePoint.position;

        GameObject subLeftBullet = Instantiate(RedBullet);
        subLeftBullet.transform.position = SubLeftFirePoint.position;
    }

    public void Fire()
    {
        GameObject rightBullet = Instantiate(GreenBullet);
        rightBullet.transform.position = RightFirePoint.position;

        GameObject leftBullet = Instantiate(GreenBullet);
        leftBullet.transform.position = LeftFirePoint.position;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        CoolTime -= amount;

        if (CoolTime < 0.1f)
        {
            CoolTime = 0.1f;
        }

        Debug.Log("Attack CoolTime : " + CoolTime);
    }
}