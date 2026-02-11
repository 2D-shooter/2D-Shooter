//using UnityEngine;
//using System.Collections;

//public class PunchEffect : MonoBehaviour
//{
//    [SerializeField] private float duration = 0.1f;

//    public void Show()
//    {
//        StopAllCoroutines();
//        StartCoroutine(FlashRoutine());
//    }

//    private IEnumerator FlashRoutine()
//    {
//        gameObject.SetActive(true);
//        yield return new WaitForSeconds(duration);
//        gameObject.SetActive(false);
//    }
//}

using UnityEngine;
using System.Collections;

public class PunchEffect : MonoBehaviour
{
    [SerializeField] private GameObject circleVisual; // Drag the PunchCircle child here
    [SerializeField] private float duration = 0.1f;

    public void Show()
    {
        // Now this runs on the active parent!
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        if (circleVisual != null)
        {
            circleVisual.SetActive(true);
            yield return new WaitForSeconds(duration);
            circleVisual.SetActive(false);
        }
    }
}