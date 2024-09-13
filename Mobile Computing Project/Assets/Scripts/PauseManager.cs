using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Canvas GameCanvas;
    [SerializeField] Canvas PauseCanvas;

    //Hide Game UI and Show PauseMenu UI
    public void LoadPausePanel()
    {
        GameCanvas.enabled = false;
        PauseCanvas.enabled = true;
    }

    //Hide PauseMenu UI and Show Game UI
    public void LoadGamePanel()
    {
        PauseCanvas.enabled = false;
        GameCanvas.enabled = true;
    }
}
