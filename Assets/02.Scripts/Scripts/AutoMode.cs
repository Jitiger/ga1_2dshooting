using UnityEngine;

public class AutoMode : MonoBehaviour
{
    public PlayerFire PlayerFire;
    public bool AutoFire;
    public bool cooltime;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFire = !AutoFire;
        }

        if (AutoFire)
        {
            PlayerFire.Fire();
            PlayerFire.SubFire();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerFire.Fire();
            PlayerFire.SubFire();
        }
    }
}
