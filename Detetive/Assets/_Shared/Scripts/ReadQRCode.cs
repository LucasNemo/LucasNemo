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

    public void ReadQR(WebCamTexture camTexture, Action<string> callback, bool zip = true)
    {
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            // decode the current frame
            //var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
         
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null)
            {
                var descompressedQr = result.Text;

                if(zip)
                    descompressedQr = Helper.DecompressString(result.Text);

                if (callback != null)
                    callback.Invoke(descompressedQr);

                Debug.Log("DECODED TEXT FROM QR:" + descompressedQr);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

}
