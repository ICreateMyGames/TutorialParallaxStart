using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public static class StringExtentions {
    /// <summary>
    /// Returns a determininistic hash code unlinke the usual GetHashCode function
    /// </summary>
    /// <param name="str"></param>
    public static int GetDeterministicHashCode(this string str) {
        unchecked {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2) {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }

    }

    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    /// <param name="str"></param>
    public static void ToClipboard(this string str) {
        var textEditor = new UnityEngine.TextEditor();
        textEditor.text = str;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    public static void LogToConsole(this string str, DebugLogLvl lvl = DebugLogLvl.Info) {
        switch (lvl) {
            case DebugLogLvl.Info:
                UnityEngine.Debug.Log(str);
                return;
            case DebugLogLvl.Warning:
                UnityEngine.Debug.LogWarning(str);
                return;
            case DebugLogLvl.Error:
                UnityEngine.Debug.LogError(str);
                return;
        }
    }


    /// <summary>
    /// Counts the occurances of the needle
    /// </summary>
    /// <param name="needle"> string your looking for</param>
    public static int CountOccurances(this string toCheck, string needle) {
        if (needle.Length == 0) return toCheck.Length;
        return (toCheck.Length - toCheck.Replace(needle, "").Length) / needle.Length;
    }


    public static bool IsNullOrEmpty(this string value) {
        return string.IsNullOrEmpty(value);
    }
    /// <summary>
    /// Count the words in a string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitOn"> null = split on {' ', '.', '?', '!'}</param>
    /// <returns></returns>
    public static int WordCount(this string str, char[] splitOn = null) {
        return str.Split(splitOn ?? new char[] { ' ', '.', '?', '!' },
                         System.StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static string Repeat(this string str, int times) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < times; i++)
            sb.Append(str);

        return sb.ToString();
    }



    #region ConvertToX

    public static T ToEnum<T>(this string value, bool ignorecase = true) where T : System.Enum {
        if (value == null)
            throw new System.ArgumentNullException("Value");

        value = value.Trim();

        if (value.Length == 0)
            throw new System.ArgumentNullException("Must specify valid information for parsing in the string.", "value");

        return (T)System.Enum.Parse(typeof(T), value, ignorecase);

    }

    public static float ToFloat(this string value, float defaultValue = 0) {
        if (float.TryParse(value, out float result)) {
            return result;
        } else return defaultValue;
    }
    public static int ToInt(this string value, int defaultValue = 0) {
        if (int.TryParse(value, out int result)) {
            return result;
        } else return defaultValue;
    }

    /// <summary>
    /// converts to boolean, extended by 0 = false, 1 = true, T = true, F = false
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool ToBool(this string value, bool defaultValue = false) {
        string trimmed = value.Trim();
        if (trimmed == "0") return false;
        if (trimmed == "1") return true;
        if (trimmed.ToUpper() == "F") return false;
        if (trimmed.ToUpper() == "T") return true;
        if (bool.TryParse(value, out bool result)) {
            return result;
        } else return defaultValue;
    }
    #endregion

    #region Encrypt Decrypt
    /// <summary>
    /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
    /// </summary>
    /// <param name="stringToEncrypt">String that must be encrypted.</param>
    /// <param name="key">Encryptionkey.</param>
    /// <returns>A string representing a byte array separated by a minus sign.</returns>
    /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
    public static string Encrypt(this string stringToEncrypt, string key) {
        if (string.IsNullOrEmpty(stringToEncrypt)) {
            throw new System.ArgumentException("An empty string value cannot be encrypted.");
        }

        if (string.IsNullOrEmpty(key)) {
            throw new System.ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
        }

        System.Security.Cryptography.CspParameters cspp = new System.Security.Cryptography.CspParameters();
        cspp.KeyContainerName = key;

        System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cspp);
        rsa.PersistKeyInCsp = true;

        byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(stringToEncrypt), true);

        return System.BitConverter.ToString(bytes);
    }

    /// <summary>
    /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
    /// </summary>
    /// <param name="key">Decryptionkey.</param>
    /// <returns>The decrypted string or null if decryption failed.</returns>
    /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
    public static string Decrypt(this string stringToDecrypt, string key) {
        string result = null;

        if (string.IsNullOrEmpty(stringToDecrypt)) {
            throw new System.ArgumentException("An empty string value cannot be encrypted.");
        }

        if (string.IsNullOrEmpty(key)) {
            throw new System.ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
        }

        try {
            System.Security.Cryptography.CspParameters cspp = new System.Security.Cryptography.CspParameters();
            cspp.KeyContainerName = key;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            string[] decryptArray = stringToDecrypt.Split(new string[] { "-" }, System.StringSplitOptions.None);
            byte[] decryptByteArray = System.Array.ConvertAll<string, byte>(decryptArray, (s => System.Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


            byte[] bytes = rsa.Decrypt(decryptByteArray, true);

            result = System.Text.UTF8Encoding.UTF8.GetString(bytes);
        } catch (System.Exception) {
            return "Wrong Key";

        }

        return result;
    }
    #endregion
}
public enum DebugLogLvl {
    Info,
    Warning,
    Error
}