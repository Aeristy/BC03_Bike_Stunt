using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Utility
{
    public static PlayerModel LoadGameData()
    {
        if (PlayerPrefs.HasKey(Application.identifier))
        {
            string @string = PlayerPrefs.GetString(Application.identifier);
            PlayerModel t = (PlayerModel)(Utility.FromByteArray(Convert.FromBase64String(@string)));
            
            return t;
        }
        else
        {
            PlayerModel t = new PlayerModel();
            
            PlayerPrefs.SetString(Application.identifier,
                Convert.ToBase64String(Utility.ToByteArray(t)));
            return t;
        }

    }

    public static void SaveGameData(object saveData)
    {
        PlayerPrefs.SetString(Application.identifier,
            Convert.ToBase64String(Utility.ToByteArray(saveData)));
    }

    static object FromByteArray(byte[] array)
    {
        try
        {
            object result;
            MemoryStream memoryStream = new MemoryStream(array);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            result = binaryFormatter.Deserialize(memoryStream);
            return result;
        }
        catch (Exception ex)
        {
            Debug.Log("exception: " + ex.Message);
            return new object();
        }
    }

    static byte[] ToByteArray(object data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        byte[] result;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, data);
            result = memoryStream.ToArray();
        }
        return result;
    }





    public static Color ColorGradient(float fadeFraction, Color startColor, Color endColor)
    {
        var w1 = fadeFraction;
        var w2 = 1 - w1;

        Color gradientColor = new Color
        {
            r = startColor.r * w2 + endColor.r * w1,
            g = startColor.g * w2 + endColor.g * w1,
            b = startColor.b * w2 + endColor.b * w1,
            a = 1
        };

        return gradientColor;
    }

    public static Color ColorGradient(float fadeFraction, Color startColor, Color endColor, Color betweenColor)
    {
        var color1 = endColor;
        var color2 = startColor;
        var fade = fadeFraction * 2f;

        if (fade >= 1)
        {
            fade -= 1;
            color1 = endColor;
            color2 = betweenColor;
        }


        var diffRed = color2.r - color1.r;
        var diffGreen = color2.g - color1.g;
        var diffBlue = color2.b - color1.b;

        Color gradientColor = new Color
        {
            r = color1.r + (diffRed * fade),
            g = color1.g + (diffGreen * fade),
            b = color1.b + (diffBlue * fade),
            a = 1
        };

        return gradientColor;
    }
}
