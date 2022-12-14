using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;

/// <summary>
/// Created by sweetSD. (static class)
/// 
/// AES256을 사용하는 보안 스크립트입니다.
/// _securityKey와 _securityIv를 직접 설정하여 사용해주세요.
/// 
/// </summary>
public static class SDSecurityManager
{
    private static readonly string _securityKey = "fiwkdu876au6w5uj";
    private static readonly byte[] _securityIv = new byte[] { 45, 12, 89, 47, 32, 59, 12, 48, 65, 45, 12, 50, 94, 32, 12, 54 };

    private static RijndaelManaged _aes = new RijndaelManaged()
    {
        KeySize = 256,
        BlockSize = 128,
        Mode = CipherMode.CBC,
        Padding = PaddingMode.PKCS7,
        Key = Encoding.UTF8.GetBytes(_securityKey),
        IV = _securityIv,
    };
    
    /// <summary>
    /// 주어진 데이터를 AES256으로 암호화(Encrypt) 합니다.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Encrypt(string data)
    {
        if (data.IsEmpty()) return string.Empty;
        
        byte[] src = Encoding.UTF8.GetBytes(data);

        using (ICryptoTransform encrypt = _aes.CreateEncryptor(_aes.Key, _aes.IV))
        {
            byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
            return Convert.ToBase64String(dest);
        }
    }

    /// <summary>
    /// 주어진 데이터를 AES256으로 복호화(Decrypt) 합니다.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Decrypt(string data)
    {
        if (data.IsEmpty()) return string.Empty;

            byte[] src = Convert.FromBase64String(data);

        using (ICryptoTransform decrypt = _aes.CreateDecryptor(_aes.Key, _aes.IV))
        {
            byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
            return Encoding.UTF8.GetString(dest);
        }
    }
}
