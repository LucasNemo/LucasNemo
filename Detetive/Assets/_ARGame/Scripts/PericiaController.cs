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
        var activeRoom = Manager.Instance.ActiveRoom;

        DetectiveManager.Instance.ProcessOthersPlacesPericia(activeRoom);

        //Already requyest pericia ?
        if (DetectiveManager.Instance.PericiaAlreadyRequested((Enums.Places)activeRoom.P.MP))
        {
            //Hide the button
            periciaButton.SetActive(false);

            //The is a result? 
            if (DetectiveManager.Instance.AlreadGotAResultToThisPlace())
            {
                var isThisCorrectPlace = DetectiveManager.Instance.PericiaResultToThisPlace();
                if (isThisCorrectPlace)
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

            //Show the message 
            ShowPericiaModal(status);
            return;
        }

        //any pericia to check
        HandlePericiastoCheck();
    }

    private void HandlePericiastoCheck()
    {
        if (DetectiveManager.Instance.PericiasToCheck.Count > 0)
        {
            if (!DetectiveManager.Instance.PericiasToCheck.Contains((Enums.Places)Manager.Instance.ActiveRoom.P.MP))
            {
                var x = DetectiveManager.Instance.PericiasToCheck.First(t => t != (Enums.Places)Manager.Instance.ActiveRoom.P.MP);
                ShowPericiaModal(string.Format(Manager.Instance.SOME_RESULTS, Manager.Instance.PlacesNames[x]));
            }
            else
            {
                DetectiveManager.Instance.PericiasToCheck.Remove((Enums.Places)Manager.Instance.ActiveRoom.P.MP);
            }
        }
    }

    private void ShowPericiaModal(string text)
    {
        GenericModal.Instance.OpenAlertMode(text, "Ok", null);
    }

    public void RequestPericia()
    {
        DetectiveManager.Instance.RequestPericiaToThisPlace();
        GenericModal.Instance.OpenAlertMode(Manager.Instance.PERICIA_START, "Ok", null);
    }


}
