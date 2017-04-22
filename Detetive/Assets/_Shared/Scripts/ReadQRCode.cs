using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;

public class ReadQRCode
{
    private IBarcodeReader barcodeReader; 

   public ReadQRCode ()
    {
        barcodeReader = new BarcodeReader();
    }

    public void ReadQR(WebCamTexture camTexture, Action<string> callback)
    {
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null)
            {
                if (callback != null) callback.Invoke(result.Text);
                Debug.Log("DECODED TEXT FROM QR:" + result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

}
