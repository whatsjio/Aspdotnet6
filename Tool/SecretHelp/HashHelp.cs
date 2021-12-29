using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Buffers.Text;
using System.Security.Cryptography;

namespace Tool.SecretHelp
{
    /// <summary>
    /// 哈希密码
    /// </summary>
    public static class HashHelp
    {

        #region Pbkdf2加密
        /// <summary>
        /// Pbkdf2加密
        /// </summary>
        /// <param name="passstr">加密字符</param>
        /// <param name="salt">随机salt base64字符串</param>
        /// <returns></returns>
        public static string GetPbkdf2(string passstr,string passsalt)
        {
          var saltarry=Convert.FromBase64String(passsalt);
          var getsertbyte = KeyDerivation.Pbkdf2(
          password: passstr,
          salt: saltarry,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 100000,
          numBytesRequested: 256 / 8);
            return Convert.ToBase64String(getsertbyte);
        }

        /// <summary>
        /// Pbkdf2加密
        /// </summary>
        /// <param name="passstr">加密字符</param>
        /// <param name="salt">随机salt数组</param>
        /// <returns></returns>
        public static string GetPbkdf2(string passstr, byte[] saltarry)
        {
            var getsertbyte = KeyDerivation.Pbkdf2(
            password: passstr,
            salt: saltarry,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);
            return Convert.ToBase64String(getsertbyte);
        }
        #endregion

        #region 获取加密型强随机值序列base64字符串
        /// <summary>
        /// 获取加密型强随机值序列base64字符串
        /// </summary>
        /// <param name="count">字节位数
        /// 默认16位
        /// </param>
        /// <returns></returns>
        public static string GetPbkdf2Salt(int count = 16)
        {
            byte[] randomsalt = RandomNumberGenerator.GetBytes(count);
            return Convert.ToBase64String(randomsalt);
        } 
        #endregion
    }
}
