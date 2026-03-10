using UnityEngine;
using TopDown.UI;

namespace TopDown.Systems
{
    public class LevelExit : MonoBehaviour
    {
        [SerializeField] private LevelFinishUI finishUI;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the player walked into the exit
            if (other.CompareTag("Player"))
            {
                if (finishUI != null)
                {
                    finishUI.ShowFinishScreen();
                }
                else
                {
                    Debug.Log("LEVEL DONE! (Assign FinishUI to see the screen)");
                }

                // Optional: Disable player movement here
                other.enabled = false;
            }
        }
    }
}