using MoreMountains.Feedbacks;
using UnityEngine;

    public class TouchEnviroment : MonoBehaviour
    {
    [SerializeField] MMFeedbacks feedBacks;
    float time;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saw")&& time < Time.time)
        {
            feedBacks.PlayFeedbacks();
            time += 2f;
        }
    }
}
