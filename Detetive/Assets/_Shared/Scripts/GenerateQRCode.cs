using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class GenerateQRCode {

    private static Color32[] Encode(string textForEncoding, int width, int height, bool zip = true)
    {
        var newQr = textForEncoding;

        if(zip)
            newQr = Helper.CompressString(textForEncoding);

        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };

        return writer.Write(newQr);
    }

    /// <summary>
    /// Generate QRCode with the information 
    /// </summary>
    /// <param name="text">QRCode info</param>
    /// <returns></returns>
    public static Texture2D GenerateQR(string text, int size = 256)
    {
        var encoded = new Texture2D(size, size);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public static Sprite GenerateQRSprite(string text, int size = 256)
    {
        Texture2D qrCode = GenerateQR(text, 256);
        return Sprite.Create(qrCode, new Rect(0, 0, qrCode.width, qrCode.height), new Vector2(0.5f, 0.5f));
    }
}
