using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSmooth : MonoBehaviour
{
    // Start is called before the first frame update 
    //End Game Panel
    public GameObject endgameStatus;
    public GameObject replayButton;
    public bool IsEndGamePanel;
    void Start()
    {
            endgameStatus.transform.localScale = new Vector3(0, 0, 0);
            replayButton.transform.localScale = new Vector3(0, 0, 0);

            StartCoroutine(0.2f.Tweeng((p) => endgameStatus.transform.localScale = p,
                endgameStatus.transform.localScale,
                new Vector3(1, 1, 1)));
            StartCoroutine(WiteToScale(1f));
    }
    public IEnumerator WiteToScale(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(0.2f.Tweeng( (p)=>replayButton.transform.localScale=p,
            replayButton.transform.localScale,
            new Vector3(1f, 1f, 1f)) );
        StartCoroutine(WiteToScale2(0.2f));
    }
    public IEnumerator WiteToScale2(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(0.2f.Tweeng( (p)=>replayButton.transform.localScale=p,
            replayButton.transform.localScale,
            new Vector3(0.75f, 0.75f, 0.75f)) );
    }
}
