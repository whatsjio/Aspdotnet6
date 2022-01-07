using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using System.Text;

namespace Tool.SecretHelp
{
    /// <summary>
    /// 非对称加密
    /// </summary>
    public static class RSAHelp
    {

        #region RSA算法
        public const string RSA_NONE_NoPadding = "RSA/NONE/NoPadding";
        public const string RSA_NONE_PKCS1Padding = "RSA/NONE/PKCS1Padding";
        public const string RSA_NONE_OAEPPadding = "RSA/NONE/OAEPPadding";
        public const string RSA_NONE_OAEPWithSHA1AndMGF1Padding = "RSA/NONE/OAEPWithSHA1AndMGF1Padding";
        public const string RSA_NONE_OAEPWithSHA224AndMGF1Padding = "RSA/NONE/OAEPWithSHA224AndMGF1Padding";
        public const string RSA_NONE_OAEPWithSHA256AndMGF1Padding = "RSA/NONE/OAEPWithSHA256AndMGF1Padding";
        public const string RSA_NONE_OAEPWithSHA384AndMGF1Padding = "RSA/NONE/OAEPWithSHA384AndMGF1Padding";
        public const string RSA_NONE_OAEPWithMD5AndMGF1Padding = "RSA/NONE/OAEPWithMD5AndMGF1Padding";

        public const string RSA_ECB_NoPadding = "RSA/ECB/NoPadding";
        public const string RSA_ECB_PKCS1Padding = "RSA/ECB/PKCS1Padding";
        public const string RSA_ECB_OAEPPadding = "RSA/ECB/OAEPPadding";
        public const string RSA_ECB_OAEPWithSHA1AndMGF1Padding = "RSA/ECB/OAEPWithSHA1AndMGF1Padding";
        public const string RSA_ECB_OAEPWithSHA224AndMGF1Padding = "RSA/ECB/OAEPWithSHA224AndMGF1Padding";
        public const string RSA_ECB_OAEPWithSHA256AndMGF1Padding = "RSA/ECB/OAEPWithSHA256AndMGF1Padding";
        public const string RSA_ECB_OAEPWithSHA384AndMGF1Padding = "RSA/ECB/OAEPWithSHA384AndMGF1Padding";
        public const string RSA_ECB_OAEPWithMD5AndMGF1Padding = "RSA/ECB/OAEPWithMD5AndMGF1Padding"; 
        #endregion


        /// <summary>
        /// rsa密钥参数
        /// </summary>
        /// <param name="PrivateKey"></param>
        /// <param name="PublicKey"></param>
        public record struct RsaKeyParameter(string PrivateKey,string PublicKey);


        #region Pkcs1
        /// <summary>
        /// Pkcs1
        /// </summary>
        /// <param name="keySize">密钥长度”一般只是指模值的位长度。目前主流可选值：1024、2048、3072、4096...</param>
        /// <param name="format">是否为PEM格式</param>
        /// <returns></returns>
        public static RsaKeyParameter Pkcs1(int keySize = 1024, bool format = false)
        {
            var keyGenerator = GeneratorUtilities.GetKeyPairGenerator("RSA");
            keyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyPair = keyGenerator.GenerateKeyPair();
            var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);
            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
            if (!format)
            {
                return new RsaKeyParameter()
                {
                    PrivateKey = Base64.ToBase64String(privateKeyInfo.ParsePrivateKey().GetEncoded()),
                    PublicKey = Base64.ToBase64String(subjectPublicKeyInfo.GetEncoded())
                };
            }
            var rsaKey = new RsaKeyParameter();
            using (var sw = new StringWriter())
            {
                var pWrt = new PemWriter(sw);
                pWrt.WriteObject(keyPair.Private);
                pWrt.Writer.Close();
                rsaKey.PrivateKey = sw.ToString();
            }

            using (var sw = new StringWriter())
            {
                var pWrt = new PemWriter(sw);
                pWrt.WriteObject(keyPair.Public);
                pWrt.Writer.Close();
                rsaKey.PublicKey = sw.ToString();
            }
            return rsaKey;
        }
        #endregion

        #region  PKCS8（JAVA适用）
        /// <summary>
        /// PKCS8（JAVA适用）
        /// </summary>
        /// <param name="keySize">密钥长度”一般只是指模值的位长度。目前主流可选值：1024、2048、3072、4096...</param>
        /// <param name="format">PEM格式</param>
        /// <returns></returns>
        public static RsaKeyParameter Pkcs8(int keySize = 1024, bool format = false)
        {
            var keyGenerator = GeneratorUtilities.GetKeyPairGenerator("RSA");
            keyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyPair = keyGenerator.GenerateKeyPair();

            var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);
            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);

            if (!format)
            {
                return new RsaKeyParameter
                {
                    PrivateKey = Base64.ToBase64String(privateKeyInfo.GetEncoded()),
                    PublicKey = Base64.ToBase64String(subjectPublicKeyInfo.GetEncoded())
                };
            }

            var rsaKey = new RsaKeyParameter();
            using (var sw = new StringWriter())
            {
                var pWrt = new PemWriter(sw);
                var pkcs8 = new Pkcs8Generator(keyPair.Private);
                pWrt.WriteObject(pkcs8);
                pWrt.Writer.Close();
                rsaKey.PrivateKey = sw.ToString();
            }

            using (var sw = new StringWriter())
            {
                var pWrt = new PemWriter(sw);
                pWrt.WriteObject(keyPair.Public);
                pWrt.Writer.Close();
                rsaKey.PublicKey = sw.ToString();
            }

            return rsaKey;

        }
        #endregion

        #region 从Pkcs1私钥中提取公钥
        /// <summary>
        /// 从Pkcs1私钥中提取公钥
        /// </summary>
        /// <param name="privateKey">Pkcs1私钥</param>
        /// <returns></returns>
        public  static string GetPublicKeyFromPrivateKeyPkcs1(string privateKey)
        {
            var instance = RsaPrivateKeyStructure.GetInstance(Base64.Decode(privateKey));

            var publicParameter = (AsymmetricKeyParameter)new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);

            var privateParameter = (AsymmetricKeyParameter)new RsaPrivateCrtKeyParameters(instance.Modulus, instance.PublicExponent, instance.PrivateExponent, instance.Prime1, instance.Prime2, instance.Exponent1, instance.Exponent2, instance.Coefficient);

            var keyPair = new AsymmetricCipherKeyPair(publicParameter, privateParameter);
            var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);

            return Base64.ToBase64String(subjectPublicKeyInfo.GetEncoded());
        }
        #endregion

        #region 从Pkcs8私钥中提取公钥
        /// <summary>
        /// 从Pkcs8私钥中提取公钥
        /// </summary>
        /// <param name="privateKey">Pkcs8私钥</param>
        /// <returns></returns>
        public static string GetPublicKeyFromPrivateKeyPkcs8(string privateKey)
        {
            var privateKeyInfo = PrivateKeyInfo.GetInstance(Asn1Object.FromByteArray(Base64.Decode(privateKey)));
            privateKey = Base64.ToBase64String(privateKeyInfo.ParsePrivateKey().GetEncoded());

            var instance = RsaPrivateKeyStructure.GetInstance(Base64.Decode(privateKey));

            var publicParameter = (AsymmetricKeyParameter)new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);

            var privateParameter = (AsymmetricKeyParameter)new RsaPrivateCrtKeyParameters(instance.Modulus, instance.PublicExponent, instance.PrivateExponent, instance.Prime1, instance.Prime2, instance.Exponent1, instance.Exponent2, instance.Coefficient);

            var keyPair = new AsymmetricCipherKeyPair(publicParameter, privateParameter);
            var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);

            return Base64.ToBase64String(subjectPublicKeyInfo.GetEncoded());
        }
        #endregion

        #region pkcs1 PEM格式密钥读取
        /// <summary>
        ///pkcs1 PEM格式密钥读取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReadPkcs1PrivateKey(string text)
        {
            if (!text.StartsWith("-----BEGIN RSA PRIVATE KEY-----"))
            {
                return text;
            }

            using (var reader = new StringReader(text))
            {
                var pr = new PemReader(reader);
                var keyPair = pr.ReadObject() as AsymmetricCipherKeyPair;
                pr.Reader.Close();

                var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair?.Private);
                return Base64.ToBase64String(privateKeyInfo.ParsePrivateKey().GetEncoded());
            }
        }
        #endregion

        #region pkcs8  PEM格式密钥读取
        /// <summary>
        ///pkcs8  PEM格式密钥读取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReadPkcs8PrivateKey(string text)
        {
            if (!text.StartsWith("-----BEGIN PRIVATE KEY-----"))
            {
                return text;
            }

            using (var reader = new StringReader(text))
            {
                var pr = new PemReader(reader);
                var akp = pr.ReadObject() as AsymmetricKeyParameter; ;
                pr.Reader.Close();
                return Base64.ToBase64String(PrivateKeyInfoFactory.CreatePrivateKeyInfo(akp).GetEncoded());
            }
        }
        #endregion

        #region PEM公钥读取
        /// <summary>
        /// PEM公钥读取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReadPublicKey(string text)
        {
            if (!text.StartsWith("-----BEGIN PUBLIC KEY-----"))
            {
                return text;
            }
            using (var reader = new StringReader(text))
            {
                var pr = new PemReader(reader);
                var keyPair = pr.ReadObject() as AsymmetricCipherKeyPair;
                pr.Reader.Close();

                var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair?.Public);
                return Base64.ToBase64String(subjectPublicKeyInfo.GetEncoded());
            }
        }
        #endregion

        #region 获取Pkcs1非对称秘钥参数
        /// <summary>
        /// 获取Pkcs1非对称秘钥参数
        /// </summary>
        /// <param name="privateKey">Pkcs1格式私钥</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AsymmetricKeyParameter GetAsymmetricKeyParameterFormPrivateKey(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey));
            }

            var instance = RsaPrivateKeyStructure.GetInstance(Base64.Decode(privateKey));
            return new RsaPrivateCrtKeyParameters(instance.Modulus, instance.PublicExponent, instance.PrivateExponent, instance.Prime1, instance.Prime2, instance.Exponent1, instance.Exponent2, instance.Coefficient);
        }
        #endregion

        #region 获取Pkcs8非对称秘钥参数
        /// <summary>
        /// 获取Pkcs8非对称秘钥参数
        /// </summary>
        /// <param name="privateKey">Pkcs8格式私钥</param>
        /// <returns></returns>
        public static AsymmetricKeyParameter GetAsymmetricKeyParameterFormAsn1PrivateKey(string privateKey)
        {
            return PrivateKeyFactory.CreateKey(Base64.Decode(privateKey));
        }
        #endregion

        #region 获取公钥非对称秘钥参数
        /// <summary>
        /// 获取公钥非对称秘钥参数
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AsymmetricKeyParameter GetAsymmetricKeyParameterFormPublicKey(string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentNullException(nameof(publicKey));
            }

            return PublicKeyFactory.CreateKey(Base64.Decode(publicKey));
        }
        #endregion

        #region RSA加密
        /// <summary>
        ///  RSA加密
        /// </summary>
        /// <param name="data">未加密数据字节数组</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>已加密数据字节数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] Encrypt(byte[] data, AsymmetricKeyParameter parameters, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (string.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            var bufferedCipher = CipherUtilities.GetCipher(algorithm);
            bufferedCipher.Init(true, parameters);
            return bufferedCipher.DoFinal(data);
        }
        #endregion

        #region RSA解密
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="data">已加密数据字节数组</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>未加密数据字节数组</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] Decrypt(byte[] data, AsymmetricKeyParameter parameters, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (string.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException(nameof(algorithm));
            }
            var bufferedCipher = CipherUtilities.GetCipher(algorithm);
            bufferedCipher.Init(false, parameters);
            return bufferedCipher.DoFinal(data);
        }
        #endregion

        #region RSA加密——Base64
        /// <summary>
        /// RSA加密——Base64
        /// </summary>
        /// <param name="data">未加密字符串</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>已加密Base64字符串</returns>
        public static string EncryptToBase64(string data, AsymmetricKeyParameter parameters, string algorithm)
        {
            return Base64.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data), parameters, algorithm));
        }
        #endregion

        #region RSA解密——Base64
        /// <summary>
        /// RSA解密——Base64
        /// </summary>
        /// <param name="data">已加密Base64字符串</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>未加密字符串</returns>
        public static string DecryptFromBase64(string data, AsymmetricKeyParameter parameters, string algorithm)
        {
            return Encoding.UTF8.GetString(Decrypt(Base64.Decode(data), parameters, algorithm));
        }
        #endregion

        #region RSA加密——十六进制
        /// <summary>
        /// RSA加密——十六进制
        /// </summary>
        /// <param name="data">未加密字符串</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>已加密十六进制字符串</returns>
        public static string EncryptToHex(string data, AsymmetricKeyParameter parameters, string algorithm)
        {
            return Hex.ToHexString(Encrypt(Encoding.UTF8.GetBytes(data), parameters, algorithm));
        }
        #endregion 

        #region RSA解密——十六进制
        ///  <summary>
        /// RSA解密——十六进制
        /// </summary>
        /// <param name="data">已加密十六进制字符串</param>
        /// <param name="parameters">非对称密钥参数</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>未加密字符串</returns>
        public static string DecryptFromHex(string data, AsymmetricKeyParameter parameters, string algorithm)
        {
            return Encoding.UTF8.GetString(Decrypt(Hex.Decode(data), parameters, algorithm));
        } 
        #endregion

    }
}
