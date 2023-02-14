using UnityEngine;
public class HumanController : MonoBehaviour
{
    #region singleton
    public static HumanController Ins;
    private void Awake()
    {
        Ins = this;
    }

    #endregion


    #region Declare

    public Animator _ani;

    #endregion


    public void SetState(bool state)
    {
        _ani.SetBool("isDriver", state);
    }

    public void SetRunning(bool isRun)
    {
        _ani.SetBool("isRunning", isRun);
    }

    public void SetDriveType(float index)
    {
        _ani.SetFloat("type", index);
    }

}
