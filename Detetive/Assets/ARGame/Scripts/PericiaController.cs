using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PericiaController : MonoBehaviour {

    public GameObject periciaButton;
    private string status;
    public AudioClip sirene;

    private void Start()
    {
        if (DetectiveManager.Instance.PericiaAlreadyRequested((Enums.Places)Manager.Instance.ActiveRoom.P.MP))
        {
            periciaButton.SetActive(false);
            if (DetectiveManager.Instance.AlreadGotAResultToThisPlace())
            {
                if (DetectiveManager.Instance.PericiaResultToThisPlace())
                {
                    status = Manager.Instance.CORRECT_PLACE;
                    AudioController.Instance.Play(sirene, AudioController.SoundType.SoundEffect2D, 1f, false, true);
                }
                else
                    status = Manager.Instance.WRONG_PLACE;
            }
            else
            {
                status = Manager.Instance.NOT_READY_YET;
            }

            ShowPericiaModal(status);
            return;
        }


        if (DetectiveManager.Instance.PericiasToCheck.Count > 0)
        {
            if (!DetectiveManager.Instance.PericiasToCheck.Contains((Enums.Places)Manager.Instance.ActiveRoom.P.MP))
            {
                var x = DetectiveManager.Instance.PericiasToCheck.First(t=>t != (Enums.Places)Manager.Instance.ActiveRoom.P.MP);
                ShowPericiaModal( string.Format(Manager.Instance.SOME_RESULTS, DetectiveManager.Instance.PericiasToCheck, (Enums.Places)x));
            }
            else
            {
                DetectiveManager.Instance.PericiasToCheck.Remove((Enums.Places)Manager.Instance.ActiveRoom.P.MP);
            }
        }
    }

    private void ShowPericiaModal(string text)
    {
        GenericModal.Instance.OpenAlertMode(Manager.Instance.SOME_RESULTS, "Ok", null);
    }

    public void RequestPericia()
    {
        DetectiveManager.Instance.RequestPericiaToThisPlace();
        GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_START, "Ok", null);
    }


}
