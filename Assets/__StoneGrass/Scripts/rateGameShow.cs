using UnityEngine;

    public class rateGameShow : MonoBehaviour
    {
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UiController.Ins.rateGame.Show();
            Destroy(this.gameObject);   
        }
    }   
}
