// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfEncryptor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class PdfEncryptor
{
  private const int c_128RevisionNumber = 3;
  private const int c_40RevisionNumber = 2;
  private const int c_bytesAmount = 256 /*0x0100*/;
  private const byte c_flagNum = 255 /*0xFF*/;
  private const int c_key128 = 16 /*0x10*/;
  private const int c_key256 = 32 /*0x20*/;
  private const int c_key40 = 5;
  private const int c_newKeyOffset = 5;
  internal const byte c_numBits = 8;
  private const int c_ownerLoopNum = 50;
  private const int c_ownerLoopNum2 = 20;
  private const int c_permissionCleared = -4;
  private const int c_permissionRevisionTwoMask = 4095 /*0x0FFF*/;
  private const int c_permissionSet = -3904;
  private const int c_randomBytesAmount = 16 /*0x10*/;
  private const int c_stringLength = 32 /*0x20*/;
  private bool m_bChanged;
  private byte[] m_customArray;
  private bool m_encrypt;
  private PdfEncryptionAlgorithm m_encryptionAlgorithm = PdfEncryptionAlgorithm.RC4;
  private byte[] m_encryptionKey;
  private bool m_encryptMetadata = true;
  private byte[] m_fileEncryptionKey;
  private bool m_hasComputedPasswordValues;
  private SHA256Managed m_hashComputer;
  private PdfEncryptionKeySize m_keyLength = PdfEncryptionKeySize.Key128Bit;
  private byte[] m_ownerEncryptionKeyOut;
  private string m_ownerPassword = string.Empty;
  private byte[] m_ownerPasswordOut;
  private byte[] m_ownerRandomBytes;
  private PdfPermissionsFlags m_permission;
  private byte[] m_permissionFlag;
  private int m_permissionValue;
  private MD5CryptoServiceProvider m_provider;
  private Random m_randomArray = new Random();
  private byte[] m_randomBytes;
  private int m_revision;
  private int m_revisionNumberOut;
  private byte[] m_userEncryptionKeyOut;
  private string m_userPassword = string.Empty;
  private byte[] m_userPasswordOut;
  private byte[] m_userRandomBytes;
  private int m_versionNumberOut;
  private static object s_lockObject = new object();
  private static byte[] s_paddingString;
  private static readonly byte[] salt = new byte[4]
  {
    (byte) 115,
    (byte) 65,
    (byte) 108,
    (byte) 84
  };

  internal PdfEncryptor()
  {
    PdfEncryptor.PaddingString = new byte[32 /*0x20*/]
    {
      (byte) 40,
      (byte) 191,
      (byte) 78,
      (byte) 94,
      (byte) 78,
      (byte) 117,
      (byte) 138,
      (byte) 65,
      (byte) 100,
      (byte) 0,
      (byte) 78,
      (byte) 86,
      byte.MaxValue,
      (byte) 250,
      (byte) 1,
      (byte) 8,
      (byte) 46,
      (byte) 46,
      (byte) 0,
      (byte) 182,
      (byte) 208 /*0xD0*/,
      (byte) 104,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 47,
      (byte) 12,
      (byte) 169,
      (byte) 254,
      (byte) 100,
      (byte) 83,
      (byte) 105,
      (byte) 122
    };
    this.CustomArray = new byte[256 /*0x0100*/];
    this.Encrypt = true;
    this.Permissions = PdfPermissionsFlags.Default;
    try
    {
      this.m_provider = new MD5CryptoServiceProvider();
    }
    catch
    {
    }
  }

  private byte[] AESDecrypt(byte[] data, byte[] key)
  {
    MemoryStream memoryStream = new MemoryStream();
    byte[] numArray1 = new byte[16 /*0x10*/];
    int length1 = data.Length;
    int destinationIndex = 0;
    int length2 = Math.Min(numArray1.Length - destinationIndex, length1);
    Array.Copy((Array) data, 0, (Array) numArray1, destinationIndex, length2);
    int length3 = length1 - length2;
    int inOff = destinationIndex + length2;
    if (inOff != numArray1.Length)
      return data;
    AesEncryptor aesEncryptor = new AesEncryptor(key, numArray1, false);
    byte[] numArray2 = new byte[aesEncryptor.GetBlockSize(length3)];
    aesEncryptor.ProcessBytes(data, inOff, length3, numArray2, 0);
    memoryStream.Write(numArray2, 0, numArray2.Length);
    byte[] numArray3 = new byte[aesEncryptor.CalculateOutputSize()];
    int length4 = aesEncryptor.Finalize(numArray3);
    if (numArray3.Length != length4)
    {
      byte[] numArray4 = new byte[length4];
      Array.Copy((Array) numArray3, 0, (Array) numArray4, 0, length4);
      memoryStream.Write(numArray4, 0, numArray4.Length);
    }
    else
      memoryStream.Write(numArray3, 0, numArray3.Length);
    memoryStream.Dispose();
    return memoryStream.ToArray();
  }

  private PdfDictionary AESDictionary()
  {
    PdfDictionary pdfDictionary1 = new PdfDictionary();
    PdfDictionary pdfDictionary2 = new PdfDictionary();
    if (!pdfDictionary2.ContainsKey(new PdfName("CFM")))
    {
      if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
        pdfDictionary2[new PdfName("CFM")] = (IPdfPrimitive) new PdfName("AESV3");
      else
        pdfDictionary2[new PdfName("CFM")] = (IPdfPrimitive) new PdfName("AESV2");
    }
    if (!pdfDictionary2.ContainsKey(new PdfName("AuthEvent")))
      pdfDictionary2[new PdfName("AuthEvent")] = (IPdfPrimitive) new PdfName("DocOpen");
    if (!pdfDictionary2.ContainsKey(new PdfName("Length")))
    {
      if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
        pdfDictionary2[new PdfName("Length")] = (IPdfPrimitive) new PdfNumber(32 /*0x20*/);
      else
        pdfDictionary2[new PdfName("Length")] = (IPdfPrimitive) new PdfNumber(128 /*0x80*/);
    }
    if (!pdfDictionary1.ContainsKey(new PdfName("StdCF")))
      pdfDictionary1[new PdfName("StdCF")] = (IPdfPrimitive) pdfDictionary2;
    return pdfDictionary1;
  }

  private byte[] AESEncrypt(byte[] data, byte[] key)
  {
    MemoryStream memoryStream = new MemoryStream();
    byte[] iv = this.GenerateIV();
    AesEncryptor aesEncryptor = new AesEncryptor(key, iv, true);
    byte[] numArray1 = new byte[aesEncryptor.GetBlockSize(data.Length)];
    aesEncryptor.ProcessBytes(data, 0, data.Length, numArray1, 0);
    memoryStream.Write(numArray1, 0, numArray1.Length);
    byte[] numArray2 = new byte[aesEncryptor.CalculateOutputSize()];
    aesEncryptor.Finalize(numArray2);
    memoryStream.Write(numArray2, 0, numArray2.Length);
    memoryStream.Dispose();
    return memoryStream.ToArray();
  }

  private bool Authenticate256BitOwnerPassword(string password)
  {
    byte[] numArray1 = new byte[8];
    byte[] destinationArray1 = new byte[8];
    byte[] destinationArray2 = new byte[32 /*0x20*/];
    this.m_ownerRandomBytes = new byte[16 /*0x10*/];
    byte[] numArray2 = new byte[48 /*0x30*/];
    Array.Copy((Array) this.m_userPasswordOut, 0, (Array) numArray2, 0, 48 /*0x30*/);
    byte[] bytes = Encoding.UTF8.GetBytes(password);
    Array.Copy((Array) this.m_ownerPasswordOut, 0, (Array) destinationArray2, 0, destinationArray2.Length);
    Array.Copy((Array) this.m_ownerPasswordOut, 32 /*0x20*/, (Array) this.m_ownerRandomBytes, 0, 16 /*0x10*/);
    Array.Copy((Array) this.m_ownerRandomBytes, 0, (Array) numArray1, 0, numArray1.Length);
    Array.Copy((Array) this.m_ownerRandomBytes, numArray1.Length, (Array) destinationArray1, 0, destinationArray1.Length);
    byte[] numArray3 = new byte[bytes.Length + numArray1.Length + numArray2.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray3, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray3, bytes.Length, numArray1.Length);
    Array.Copy((Array) numArray2, 0, (Array) numArray3, bytes.Length + numArray1.Length, numArray2.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray3);
    bool flag = false;
    if (hash.Length == destinationArray2.Length)
    {
      int index = 0;
      while (index < hash.Length && (int) hash[index] == (int) destinationArray2[index])
        ++index;
      if (index == hash.Length)
        flag = true;
    }
    this.FindFileEncryptionKey(password);
    return flag;
  }

  private bool Authenticate256BitUserPassword(string password)
  {
    byte[] numArray1 = new byte[8];
    byte[] destinationArray1 = new byte[8];
    byte[] destinationArray2 = new byte[32 /*0x20*/];
    this.m_userRandomBytes = new byte[16 /*0x10*/];
    byte[] bytes = Encoding.UTF8.GetBytes(password);
    Array.Copy((Array) this.m_userPasswordOut, 0, (Array) destinationArray2, 0, destinationArray2.Length);
    Array.Copy((Array) this.m_userPasswordOut, 32 /*0x20*/, (Array) this.m_userRandomBytes, 0, 16 /*0x10*/);
    Array.Copy((Array) this.m_userRandomBytes, 0, (Array) numArray1, 0, numArray1.Length);
    Array.Copy((Array) this.m_userRandomBytes, numArray1.Length, (Array) destinationArray1, 0, destinationArray1.Length);
    byte[] numArray2 = new byte[bytes.Length + numArray1.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray2, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray2, bytes.Length, numArray1.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray2);
    bool flag = false;
    if (hash.Length == destinationArray2.Length)
    {
      int index = 0;
      while (index < hash.Length && (int) hash[index] == (int) destinationArray2[index])
        ++index;
      if (index == hash.Length)
        flag = true;
    }
    this.FindFileEncryptionKey(password);
    return flag;
  }

  private bool AuthenticateOwnerPassword(string password)
  {
    if (this.m_keyLength == PdfEncryptionKeySize.Key256Bit)
      return this.Authenticate256BitOwnerPassword(password);
    this.m_encryptionKey = this.GetKeyFromOwnerPass(password);
    byte[] numArray = this.m_ownerPasswordOut;
    if (this.RevisionNumber == 2)
      numArray = this.EncryptDataByCustom(numArray, this.m_encryptionKey);
    else if (this.RevisionNumber > 2)
    {
      numArray = this.m_ownerPasswordOut;
      for (int index = 0; index < 20; ++index)
      {
        byte[] forOwnerPassStep7 = this.GetKeyForOwnerPassStep7(this.m_encryptionKey, (byte) (20 - index - 1));
        numArray = this.EncryptDataByCustom(numArray, forOwnerPassStep7);
      }
    }
    this.m_encryptionKey = (byte[]) null;
    string password1 = this.ConvertToPassword(numArray);
    if (!this.AuthenticateUserPassword(password1))
      return false;
    this.m_userPassword = password1;
    this.m_ownerPassword = password;
    return true;
  }

  private bool AuthenticateUserPassword(string password)
  {
    if (this.m_keyLength == PdfEncryptionKeySize.Key256Bit)
      return this.Authenticate256BitUserPassword(password);
    this.m_encryptionKey = this.CreateEncryptionKey(password, this.m_ownerPasswordOut);
    byte[] userPassword = this.CreateUserPassword();
    return this.RevisionNumber == 2 ? this.CompareByteArrays(userPassword, this.m_userPasswordOut) : this.CompareByteArrays(userPassword, this.m_userPasswordOut, 16 /*0x10*/);
  }

  internal bool CheckPassword(string password, PdfString key)
  {
    if (password == null)
      throw new ArgumentNullException(nameof (password));
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    byte[] randomBytes = this.m_randomBytes;
    this.m_randomBytes = key.Bytes.Clone() as byte[];
    bool flag;
    if (this.AuthenticateUserPassword(password))
    {
      this.m_userPassword = password;
      flag = true;
    }
    else if (this.AuthenticateOwnerPassword(password))
    {
      this.m_ownerPassword = password;
      flag = true;
    }
    else
    {
      this.m_encryptionKey = (byte[]) null;
      flag = false;
    }
    if (!flag)
      this.m_randomBytes = randomBytes;
    return flag;
  }

  internal PdfEncryptor Clone()
  {
    PdfEncryptor pdfEncryptor = this.MemberwiseClone() as PdfEncryptor;
    pdfEncryptor.CryptographicAlgorithm = this.m_keyLength;
    pdfEncryptor.UserPassword = this.UserPassword;
    pdfEncryptor.OwnerPassword = this.OwnerPassword;
    pdfEncryptor.Permissions = this.Permissions;
    pdfEncryptor.m_randomBytes = this.m_randomBytes.Clone() as byte[];
    pdfEncryptor.m_customArray = this.m_customArray.Clone() as byte[];
    pdfEncryptor.m_revision = this.m_revision;
    if (this.m_encryptionKey != null)
      pdfEncryptor.m_encryptionKey = this.m_encryptionKey.Clone() as byte[];
    pdfEncryptor.m_customArray = this.m_customArray.Clone() as byte[];
    pdfEncryptor.m_ownerPasswordOut = this.m_ownerPasswordOut.Clone() as byte[];
    pdfEncryptor.m_userPasswordOut = this.m_userPasswordOut.Clone() as byte[];
    pdfEncryptor.m_hasComputedPasswordValues = this.m_hasComputedPasswordValues;
    pdfEncryptor.m_bChanged = this.m_bChanged;
    return pdfEncryptor;
  }

  private bool CompareByteArrays(byte[] array1, byte[] array2)
  {
    if (array1 == null || array2 == null)
      return array1 == array2;
    if (array1.Length != array2.Length)
      return false;
    int index = 0;
    for (int length = array1.Length; index < length; ++index)
    {
      if ((int) array1[index] != (int) array2[index])
        return false;
    }
    return true;
  }

  private bool CompareByteArrays(byte[] array1, byte[] array2, int size)
  {
    if (array1 == null || array2 == null)
      return array1 == array2;
    if (array1.Length < size || array2.Length < size)
      throw new ArgumentException("Size of one of the arrays are less then requisted size.");
    if (array1.Length != array2.Length)
      return false;
    for (int index = 0; index < size; ++index)
    {
      if ((int) array1[index] != (int) array2[index])
        return false;
    }
    return true;
  }

  private string ConvertToPassword(byte[] array)
  {
    int length = array.Length;
    for (int index = 0; index < length; ++index)
    {
      if ((int) array[index] == (int) PdfEncryptor.s_paddingString[0] && index < length - 1 && (int) array[index + 1] == (int) PdfEncryptor.s_paddingString[1])
      {
        length = index;
        break;
      }
    }
    return PdfString.ByteToString(array, length);
  }

  private byte[] Create128BitUserPassword()
  {
    if (this.EncryptionKey == null)
      throw new ArgumentNullException("EncryptionKey");
    List<byte> byteList = new List<byte>();
    byteList.AddRange((IEnumerable<byte>) this.PadTrancateString(string.Empty));
    byteList.AddRange((IEnumerable<byte>) this.RandomBytes);
    byte[] hash = this.Provider.ComputeHash(byteList.ToArray());
    byte[] data = new byte[16 /*0x10*/];
    byte[] destinationArray = data;
    int length = data.Length;
    Array.Copy((Array) hash, 0, (Array) destinationArray, 0, length);
    byte[] numArray = this.EncryptDataByCustom(data, this.EncryptionKey);
    byte[] encryptionKey = this.EncryptionKey;
    for (byte index = 1; index < (byte) 20; ++index)
    {
      byte[] forOwnerPassStep7 = this.GetKeyForOwnerPassStep7(this.EncryptionKey, index);
      numArray = this.EncryptDataByCustom(numArray, forOwnerPassStep7, forOwnerPassStep7.Length);
    }
    return this.PadTrancateString(numArray);
  }

  private byte[] Create256BitOwnerPassword()
  {
    byte[] numArray1 = new byte[8];
    byte[] numArray2 = new byte[8];
    this.m_ownerRandomBytes = new byte[16 /*0x10*/];
    this.m_randomArray.NextBytes(this.m_ownerRandomBytes);
    byte[] bytes = Encoding.UTF8.GetBytes(this.m_ownerPassword);
    Array.Copy((Array) this.m_ownerRandomBytes, 0, (Array) numArray1, 0, 8);
    Array.Copy((Array) this.m_ownerRandomBytes, 8, (Array) numArray2, 0, 8);
    byte[] numArray3 = new byte[bytes.Length + numArray1.Length + this.m_userPasswordOut.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray3, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray3, bytes.Length, numArray1.Length);
    Array.Copy((Array) this.m_userPasswordOut, 0, (Array) numArray3, bytes.Length + numArray1.Length, this.m_userPasswordOut.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray3);
    byte[] destinationArray = new byte[hash.Length + numArray1.Length + numArray2.Length];
    Array.Copy((Array) hash, 0, (Array) destinationArray, 0, hash.Length);
    Array.Copy((Array) numArray1, 0, (Array) destinationArray, hash.Length, numArray1.Length);
    Array.Copy((Array) numArray2, 0, (Array) destinationArray, hash.Length + numArray1.Length, numArray2.Length);
    return destinationArray;
  }

  private byte[] Create256BitUserPassword()
  {
    byte[] numArray1 = new byte[8];
    byte[] numArray2 = new byte[8];
    this.m_userRandomBytes = new byte[16 /*0x10*/];
    this.m_randomArray.NextBytes(this.m_userRandomBytes);
    byte[] bytes = Encoding.UTF8.GetBytes(this.m_userPassword);
    Array.Copy((Array) this.m_userRandomBytes, 0, (Array) numArray1, 0, 8);
    Array.Copy((Array) this.m_userRandomBytes, 8, (Array) numArray2, 0, 8);
    byte[] numArray3 = new byte[this.UserPassword.Length + numArray1.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray3, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray3, this.UserPassword.Length, numArray1.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray3);
    byte[] destinationArray = new byte[hash.Length + numArray1.Length + numArray2.Length];
    Array.Copy((Array) hash, 0, (Array) destinationArray, 0, hash.Length);
    Array.Copy((Array) numArray1, 0, (Array) destinationArray, hash.Length, numArray1.Length);
    Array.Copy((Array) numArray2, 0, (Array) destinationArray, hash.Length + numArray1.Length, numArray2.Length);
    return destinationArray;
  }

  private byte[] Create40BitUserPassword()
  {
    return this.EncryptionKey != null ? this.EncryptDataByCustom(this.PadTrancateString(string.Empty), this.EncryptionKey) : throw new ArgumentNullException("EncryptionKey");
  }

  private byte[] CreateEncryptionKey(string inputPass, byte[] ownerPass)
  {
    if (inputPass == null)
      throw new ArgumentNullException(nameof (inputPass));
    if (ownerPass == null)
      throw new ArgumentNullException(nameof (ownerPass));
    byte[] collection1 = this.PadTrancateString(inputPass);
    List<byte> byteList = new List<byte>();
    byteList.AddRange((IEnumerable<byte>) collection1);
    byteList.AddRange((IEnumerable<byte>) ownerPass);
    byte[] collection2 = new byte[4]
    {
      (byte) this.m_permissionValue,
      (byte) (this.m_permissionValue >> 8),
      (byte) (this.m_permissionValue >> 16 /*0x10*/),
      (byte) (this.m_permissionValue >> 24)
    };
    byteList.AddRange((IEnumerable<byte>) collection2);
    byteList.AddRange((IEnumerable<byte>) this.RandomBytes);
    if (this.RevisionNumber > 2 && !this.EncryptMetaData)
    {
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
    }
    byte[] array = byteList.ToArray();
    byte[] numArray = this.Provider != null ? this.Provider.ComputeHash(array) : throw new NotSupportedException("Document encryption is not allowed in FIPS mode.");
    if (this.RevisionNumber > 2)
    {
      for (int index = 0; index < 50; ++index)
        numArray = this.Provider.ComputeHash(numArray);
    }
    this.EncryptionKey = new byte[this.GetKeyLength()];
    Array.Copy((Array) numArray, (Array) this.EncryptionKey, this.EncryptionKey.Length);
    return this.EncryptionKey;
  }

  private void CreateFileEncryptionKey()
  {
    this.m_fileEncryptionKey = new byte[32 /*0x20*/];
    this.m_randomArray.NextBytes(this.m_fileEncryptionKey);
  }

  private byte[] CreateOwnerEncryptionKey()
  {
    byte[] destinationArray = new byte[8];
    byte[] numArray1 = new byte[8];
    byte[] bytes = Encoding.UTF8.GetBytes(this.m_ownerPassword);
    Array.Copy((Array) this.m_ownerRandomBytes, 0, (Array) destinationArray, 0, 8);
    Array.Copy((Array) this.m_ownerRandomBytes, 8, (Array) numArray1, 0, 8);
    byte[] numArray2 = new byte[bytes.Length + destinationArray.Length + this.m_userPasswordOut.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray2, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray2, bytes.Length, destinationArray.Length);
    Array.Copy((Array) this.m_userPasswordOut, 0, (Array) numArray2, bytes.Length + destinationArray.Length, this.m_userPasswordOut.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray2);
    Rijndael rijndael = Rijndael.Create();
    rijndael.Mode = CipherMode.CBC;
    rijndael.KeySize = 256 /*0x0100*/;
    rijndael.Key = hash;
    rijndael.IV = new byte[16 /*0x10*/];
    rijndael.Padding = PaddingMode.None;
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
    cryptoStream.Write(this.m_fileEncryptionKey, 0, this.m_fileEncryptionKey.Length);
    cryptoStream.Close();
    memoryStream.Dispose();
    return memoryStream.ToArray();
  }

  private byte[] CreateOwnerPassword()
  {
    byte[] keyFromOwnerPass = this.GetKeyFromOwnerPass(this.OwnerPassword == null || this.OwnerPassword.Length == 0 ? this.UserPassword : this.OwnerPassword);
    byte[] data = this.EncryptDataByCustom(this.PadTrancateString(this.UserPassword), keyFromOwnerPass, keyFromOwnerPass.Length);
    if (this.RevisionNumber > 2)
    {
      for (byte index = 1; index < (byte) 20; ++index)
      {
        byte[] forOwnerPassStep7 = this.GetKeyForOwnerPassStep7(keyFromOwnerPass, index);
        data = this.EncryptDataByCustom(data, forOwnerPassStep7, forOwnerPassStep7.Length);
      }
    }
    return data;
  }

  private byte[] CreatePermissionFlag()
  {
    byte[] numArray1 = new byte[16 /*0x10*/];
    byte[] sourceArray = new byte[4]
    {
      (byte) this.m_permissionValue,
      (byte) (this.m_permissionValue >> 8),
      (byte) (this.m_permissionValue >> 16 /*0x10*/),
      (byte) (this.m_permissionValue >> 24)
    };
    Array.Copy((Array) sourceArray, 0, (Array) numArray1, 0, sourceArray.Length);
    int length = sourceArray.Length;
    byte[] numArray2 = numArray1;
    int index1 = length;
    int num1 = index1 + 1;
    numArray2[index1] = byte.MaxValue;
    byte[] numArray3 = numArray1;
    int index2 = num1;
    int num2 = index2 + 1;
    numArray3[index2] = byte.MaxValue;
    byte[] numArray4 = numArray1;
    int index3 = num2;
    int num3 = index3 + 1;
    numArray4[index3] = byte.MaxValue;
    byte[] numArray5 = numArray1;
    int index4 = num3;
    int num4 = index4 + 1;
    numArray5[index4] = byte.MaxValue;
    byte[] numArray6 = numArray1;
    int index5 = num4;
    int num5 = index5 + 1;
    numArray6[index5] = (byte) 70;
    byte[] numArray7 = numArray1;
    int index6 = num5;
    int num6 = index6 + 1;
    numArray7[index6] = (byte) 97;
    byte[] numArray8 = numArray1;
    int index7 = num6;
    int num7 = index7 + 1;
    numArray8[index7] = (byte) 100;
    byte[] numArray9 = numArray1;
    int index8 = num7;
    int num8 = index8 + 1;
    numArray9[index8] = (byte) 98;
    byte[] numArray10 = numArray1;
    int index9 = num8;
    int num9 = index9 + 1;
    numArray10[index9] = (byte) 98;
    byte[] numArray11 = numArray1;
    int index10 = num9;
    int num10 = index10 + 1;
    numArray11[index10] = (byte) 98;
    byte[] numArray12 = numArray1;
    int index11 = num10;
    int num11 = index11 + 1;
    numArray12[index11] = (byte) 98;
    byte[] numArray13 = numArray1;
    int index12 = num11;
    int num12 = index12 + 1;
    numArray13[index12] = (byte) 98;
    Rijndael rijndael = Rijndael.Create();
    rijndael.Mode = CipherMode.ECB;
    rijndael.KeySize = 256 /*0x0100*/;
    rijndael.Key = this.m_fileEncryptionKey;
    rijndael.IV = new byte[16 /*0x10*/];
    rijndael.Padding = PaddingMode.None;
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
    cryptoStream.Write(numArray1, 0, numArray1.Length);
    cryptoStream.Close();
    memoryStream.Close();
    return memoryStream.ToArray();
  }

  private byte[] CreateUserEncryptionKey()
  {
    byte[] destinationArray = new byte[8];
    byte[] numArray1 = new byte[8];
    byte[] bytes = Encoding.UTF8.GetBytes(this.m_userPassword);
    Array.Copy((Array) this.m_userRandomBytes, 0, (Array) destinationArray, 0, 8);
    Array.Copy((Array) this.m_userRandomBytes, 8, (Array) numArray1, 0, 8);
    byte[] numArray2 = new byte[bytes.Length + numArray1.Length];
    Array.Copy((Array) bytes, 0, (Array) numArray2, 0, bytes.Length);
    Array.Copy((Array) numArray1, 0, (Array) numArray2, bytes.Length, numArray1.Length);
    byte[] hash = this.HashComputer.ComputeHash(numArray2);
    Rijndael rijndael = Rijndael.Create();
    rijndael.Mode = CipherMode.CBC;
    rijndael.KeySize = 256 /*0x0100*/;
    rijndael.Key = hash;
    rijndael.IV = new byte[16 /*0x10*/];
    rijndael.Padding = PaddingMode.None;
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
    cryptoStream.Write(this.m_fileEncryptionKey, 0, this.m_fileEncryptionKey.Length);
    cryptoStream.Close();
    memoryStream.Dispose();
    return memoryStream.ToArray();
  }

  private byte[] CreateUserPassword()
  {
    return this.RevisionNumber == 2 ? this.Create40BitUserPassword() : this.Create128BitUserPassword();
  }

  private byte[] DecryptData256(byte[] data) => this.AESDecrypt(data, this.m_fileEncryptionKey);

  internal byte[] EncryptData(long currObjNumber, byte[] data, bool isEncryption)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
      return isEncryption ? this.EncryptData256(data) : this.DecryptData256(data);
    this.InitializeData();
    int num1 = 0;
    int length1;
    byte[] key;
    if (this.EncryptionKey.Length == 5)
    {
      byte[] originalKey = new byte[this.EncryptionKey.Length + 5];
      int index = 0;
      for (int length2 = this.EncryptionKey.Length; index < length2; ++index)
        originalKey[index] = this.EncryptionKey[index];
      int num2 = this.EncryptionKey.Length - 1;
      int num3;
      originalKey[num3 = num2 + 1] = (byte) currObjNumber;
      int num4;
      originalKey[num4 = num3 + 1] = (byte) (currObjNumber >> 8);
      int num5;
      originalKey[num5 = num4 + 1] = (byte) (currObjNumber >> 16 /*0x10*/);
      int num6;
      originalKey[num6 = num5 + 1] = (byte) num1;
      int num7;
      originalKey[num7 = num6 + 1] = (byte) (num1 >> 8);
      length1 = originalKey.Length;
      key = this.PrepareKeyForEncryption(originalKey);
    }
    else
    {
      byte[] numArray = this.EncryptionAlgorithm != PdfEncryptionAlgorithm.AES ? new byte[this.EncryptionKey.Length + 5] : new byte[this.EncryptionKey.Length + 9];
      Array.Copy((Array) this.EncryptionKey, (Array) numArray, this.EncryptionKey.Length);
      int num8 = this.EncryptionKey.Length - 1;
      int num9;
      numArray[num9 = num8 + 1] = (byte) currObjNumber;
      int num10;
      numArray[num10 = num9 + 1] = (byte) (currObjNumber >> 8);
      int num11;
      numArray[num11 = num10 + 1] = (byte) (currObjNumber >> 16 /*0x10*/);
      int num12;
      numArray[num12 = num11 + 1] = (byte) num1;
      int num13;
      numArray[num13 = num12 + 1] = (byte) (num1 >> 8);
      if (this.EncryptionAlgorithm == PdfEncryptionAlgorithm.AES)
      {
        int num14;
        numArray[num14 = num13 + 1] = PdfEncryptor.salt[0];
        int num15;
        numArray[num15 = num14 + 1] = PdfEncryptor.salt[1];
        int num16;
        numArray[num16 = num15 + 1] = PdfEncryptor.salt[2];
        int num17;
        numArray[num17 = num16 + 1] = PdfEncryptor.salt[3];
      }
      key = this.Provider.ComputeHash(numArray);
      length1 = key.Length;
    }
    int keyLen = Math.Min(length1, key.Length);
    if (this.EncryptionAlgorithm != PdfEncryptionAlgorithm.AES)
      return this.EncryptDataByCustom(data, key, keyLen);
    return isEncryption ? this.AESEncrypt(data, key) : this.AESDecrypt(data, key);
  }

  private byte[] EncryptData256(byte[] data) => this.AESEncrypt(data, this.m_fileEncryptionKey);

  private byte[] EncryptDataByCustom(byte[] data, byte[] key)
  {
    return this.EncryptDataByCustom(data, key, key.Length);
  }

  private byte[] EncryptDataByCustom(byte[] data, byte[] key, int keyLen)
  {
    byte[] numArray = new byte[data.Length];
    this.RecreateCustomArray(key, keyLen);
    keyLen = data.Length;
    int index1 = 0;
    int index2 = 0;
    for (int index3 = 0; index3 < keyLen; ++index3)
    {
      index1 = (index1 + 1) % 256 /*0x0100*/;
      index2 = (index2 + (int) this.CustomArray[index1]) % 256 /*0x0100*/;
      byte custom1 = this.CustomArray[index1];
      this.CustomArray[index1] = this.CustomArray[index2];
      this.CustomArray[index2] = custom1;
      byte custom2 = this.CustomArray[((int) this.CustomArray[index1] + (int) this.CustomArray[index2]) % 256 /*0x0100*/];
      numArray[index3] = (byte) ((uint) data[index3] ^ (uint) custom2);
    }
    return numArray;
  }

  private void FindFileEncryptionKey(string password)
  {
    byte[] numArray1 = (byte[]) null;
    byte[] buffer = (byte[]) null;
    if (this.m_ownerRandomBytes != null)
    {
      byte[] destinationArray = new byte[8];
      byte[] numArray2 = new byte[8];
      byte[] bytes = Encoding.UTF8.GetBytes(password);
      byte[] numArray3 = new byte[48 /*0x30*/];
      Array.Copy((Array) this.m_userPasswordOut, 0, (Array) numArray3, 0, 48 /*0x30*/);
      Array.Copy((Array) this.m_ownerRandomBytes, 0, (Array) destinationArray, 0, 8);
      Array.Copy((Array) this.m_ownerRandomBytes, 8, (Array) numArray2, 0, 8);
      byte[] numArray4 = new byte[bytes.Length + destinationArray.Length + numArray3.Length];
      Array.Copy((Array) bytes, 0, (Array) numArray4, 0, bytes.Length);
      Array.Copy((Array) numArray2, 0, (Array) numArray4, bytes.Length, numArray2.Length);
      Array.Copy((Array) numArray3, 0, (Array) numArray4, bytes.Length + destinationArray.Length, numArray3.Length);
      numArray1 = this.HashComputer.ComputeHash(numArray4);
      buffer = this.m_ownerEncryptionKeyOut;
    }
    else if (this.m_userRandomBytes != null)
    {
      byte[] destinationArray = new byte[8];
      byte[] numArray5 = new byte[8];
      byte[] bytes = Encoding.UTF8.GetBytes(password);
      Array.Copy((Array) this.m_userRandomBytes, 0, (Array) destinationArray, 0, 8);
      Array.Copy((Array) this.m_userRandomBytes, 8, (Array) numArray5, 0, 8);
      byte[] numArray6 = new byte[bytes.Length + numArray5.Length];
      Array.Copy((Array) bytes, 0, (Array) numArray6, 0, bytes.Length);
      Array.Copy((Array) numArray5, 0, (Array) numArray6, bytes.Length, numArray5.Length);
      numArray1 = this.HashComputer.ComputeHash(numArray6);
      buffer = this.m_userEncryptionKeyOut;
    }
    Rijndael rijndael = Rijndael.Create();
    rijndael.Mode = CipherMode.CBC;
    rijndael.KeySize = 256 /*0x0100*/;
    rijndael.Key = numArray1;
    rijndael.IV = new byte[16 /*0x10*/];
    rijndael.Padding = PaddingMode.None;
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
    cryptoStream.Write(buffer, 0, buffer.Length);
    cryptoStream.Close();
    memoryStream.Dispose();
    this.m_fileEncryptionKey = memoryStream.ToArray();
  }

  private byte[] GenerateIV()
  {
    byte[] buffer = new byte[16 /*0x10*/];
    this.m_randomArray.NextBytes(buffer);
    return buffer;
  }

  private byte[] GetKeyForOwnerPassStep7(byte[] originalKey, byte index)
  {
    byte[] forOwnerPassStep7 = originalKey != null ? new byte[originalKey.Length] : throw new ArgumentNullException(nameof (originalKey));
    int index1 = 0;
    for (int length = originalKey.Length; index1 < length; ++index1)
      forOwnerPassStep7[index1] = (byte) ((uint) originalKey[index1] ^ (uint) index);
    return forOwnerPassStep7;
  }

  private byte[] GetKeyFromOwnerPass(string password)
  {
    byte[] hash = this.Provider.ComputeHash(this.PadTrancateString(password));
    if (this.RevisionNumber > 2)
    {
      for (int index = 0; index < 50; ++index)
        hash = this.Provider.ComputeHash(hash);
    }
    byte[] destinationArray = new byte[this.GetKeyLength()];
    Array.Copy((Array) hash, (Array) destinationArray, destinationArray.Length);
    return destinationArray;
  }

  protected internal int GetKeyLength()
  {
    if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key40Bit)
      return 5;
    return this.CryptographicAlgorithm == PdfEncryptionKeySize.Key128Bit ? 16 /*0x10*/ : 32 /*0x20*/;
  }

  private void InitializeData()
  {
    if (this.m_hasComputedPasswordValues)
      return;
    if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
    {
      this.m_userPasswordOut = this.Create256BitUserPassword();
      this.m_ownerPasswordOut = this.Create256BitOwnerPassword();
      this.CreateFileEncryptionKey();
      this.m_userEncryptionKeyOut = this.CreateUserEncryptionKey();
      this.m_ownerEncryptionKeyOut = this.CreateOwnerEncryptionKey();
      this.m_permissionFlag = this.CreatePermissionFlag();
    }
    else
    {
      if (this.Provider == null)
        throw new NotSupportedException("Document encryption is not allowed in FIPS mode.");
      this.m_ownerPasswordOut = this.CreateOwnerPassword();
      this.m_encryptionKey = this.CreateEncryptionKey(this.UserPassword, this.m_ownerPasswordOut);
      this.m_userPasswordOut = this.CreateUserPassword();
    }
    this.m_hasComputedPasswordValues = true;
  }

  private byte[] PadTrancateString(string source)
  {
    return source != null ? this.PadTrancateString(this.SecurityEncoding.GetBytes(source)) : throw new ArgumentNullException(nameof (source));
  }

  private byte[] PadTrancateString(byte[] sourceBytes)
  {
    if (sourceBytes == null)
      throw new ArgumentNullException(nameof (sourceBytes));
    byte[] destinationArray = new byte[32 /*0x20*/];
    int length = sourceBytes.Length;
    if (length > 0)
      Array.Copy((Array) sourceBytes, 0, (Array) destinationArray, 0, Math.Min(length, 32 /*0x20*/));
    if (length < 32 /*0x20*/)
      Array.Copy((Array) PdfEncryptor.PaddingString, 0, (Array) destinationArray, length, 32 /*0x20*/ - length);
    return destinationArray;
  }

  private byte[] PrepareKeyForEncryption(byte[] originalKey)
  {
    int num = originalKey != null ? originalKey.Length : throw new ArgumentNullException(nameof (originalKey));
    byte[] hash = this.Provider.ComputeHash(originalKey);
    byte[] destinationArray = hash;
    if (num > 16 /*0x10*/)
    {
      int length = Math.Min(this.GetKeyLength() + 5, 16 /*0x10*/);
      destinationArray = new byte[length];
      Array.Copy((Array) hash, 0, (Array) destinationArray, 0, length);
    }
    return destinationArray;
  }

  internal void ReadFromDictionary(PdfDictionary dictionary)
  {
    PdfName pdfName = dictionary != null ? PdfCrossTable.Dereference(dictionary["Filter"]) as PdfName : throw new ArgumentNullException(nameof (dictionary));
    if (pdfName.Value != "Standard")
      throw new PdfDocumentException("Invalid Format: Unsupported security filter: " + pdfName.Value);
    this.m_permissionValue = dictionary.GetInt("P");
    this.m_permission = (PdfPermissionsFlags) (this.m_permissionValue & 3903);
    this.m_keyLength = (PdfEncryptionKeySize) dictionary.GetInt("V");
    this.m_revisionNumberOut = dictionary.GetInt("R");
    this.m_versionNumberOut = dictionary.GetInt("V");
    if (this.m_keyLength == (PdfEncryptionKeySize) 4 && this.m_keyLength != (PdfEncryptionKeySize) dictionary.GetInt("R"))
      throw new PdfDocumentException("Invalid Format: V and R entries of the Encryption dictionary doesn't match.");
    if (this.m_keyLength == (PdfEncryptionKeySize) 5)
    {
      this.m_userEncryptionKeyOut = dictionary.GetString("UE").Bytes;
      this.m_ownerEncryptionKeyOut = dictionary.GetString("OE").Bytes;
      this.m_permissionFlag = dictionary.GetString("Perms").Bytes;
    }
    this.m_userPasswordOut = dictionary.GetString("U").Bytes;
    this.m_ownerPasswordOut = dictionary.GetString("O").Bytes;
    int num = !dictionary.ContainsKey("Length") ? (this.m_keyLength != PdfEncryptionKeySize.Key40Bit ? (this.m_keyLength != PdfEncryptionKeySize.Key128Bit ? 256 /*0x0100*/ : 128 /*0x80*/) : 40) : dictionary.GetInt("Length");
    if (num == 128 /*0x80*/ && dictionary.GetInt("R") < 4)
    {
      this.m_keyLength = PdfEncryptionKeySize.Key128Bit;
      this.m_encryptionAlgorithm = PdfEncryptionAlgorithm.RC4;
    }
    else if (num == 128 /*0x80*/ && dictionary.GetInt("R") == 4)
    {
      this.m_keyLength = PdfEncryptionKeySize.Key128Bit;
      this.m_encryptionAlgorithm = !((((dictionary["CF"] as PdfDictionary)["StdCF"] as PdfDictionary)[new PdfName("CFM")] as PdfName).Value != "V2") ? PdfEncryptionAlgorithm.RC4 : PdfEncryptionAlgorithm.AES;
    }
    else
      this.m_keyLength = num != 40 ? PdfEncryptionKeySize.Key256Bit : PdfEncryptionKeySize.Key40Bit;
    if (num != 0 && (this.m_keyLength == PdfEncryptionKeySize.Key40Bit && num != 40 || this.m_keyLength == PdfEncryptionKeySize.Key128Bit && num != 128 /*0x80*/ || this.m_keyLength == PdfEncryptionKeySize.Key256Bit && num != 256 /*0x0100*/))
      throw new PdfDocumentException("Invalid format: Invalid/Unsupported security dictionary.");
    this.m_hasComputedPasswordValues = true;
  }

  private void RecreateCustomArray(byte[] key, int keyLen)
  {
    byte[] numArray = new byte[256 /*0x0100*/];
    for (int index = 0; index < 256 /*0x0100*/; ++index)
    {
      numArray[index] = key[index % keyLen];
      this.CustomArray[index] = (byte) index;
    }
    int index1 = 0;
    for (int index2 = 0; index2 < 256 /*0x0100*/; ++index2)
    {
      index1 = (index1 + (int) this.CustomArray[index2] + (int) numArray[index2]) % 256 /*0x0100*/;
      byte custom = this.CustomArray[index2];
      this.CustomArray[index2] = this.CustomArray[index1];
      this.CustomArray[index1] = custom;
    }
  }

  internal void SaveToDictionary(PdfDictionary dictionary)
  {
    dictionary.SetName("Filter", "Standard");
    dictionary.SetNumber("P", this.m_permissionValue);
    dictionary.SetProperty("U", (IPdfPrimitive) new PdfString(this.UserPasswordOut));
    dictionary.SetProperty("O", (IPdfPrimitive) new PdfString(this.OwnerPasswordOut));
    dictionary.SetNumber("Length", this.GetKeyLength() * 8);
    if (this.m_encryptionAlgorithm == PdfEncryptionAlgorithm.AES || this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
    {
      if (this.m_revisionNumberOut > 0)
        dictionary.SetNumber("R", this.m_revisionNumberOut);
      else
        dictionary.SetNumber("R", (int) (this.m_keyLength + 2));
      if (this.m_versionNumberOut > 0)
        dictionary.SetNumber("V", this.m_versionNumberOut);
      else
        dictionary.SetNumber("V", (int) (this.m_keyLength + 2));
      dictionary.SetName("StmF", "StdCF");
      dictionary.SetName("StrF", "StdCF");
      dictionary.SetProperty("CF", (IPdfPrimitive) new PdfDictionary(this.AESDictionary()));
      if (this.CryptographicAlgorithm == PdfEncryptionKeySize.Key256Bit)
      {
        dictionary.SetProperty("UE", (IPdfPrimitive) new PdfString(this.m_userEncryptionKeyOut));
        dictionary.SetProperty("OE", (IPdfPrimitive) new PdfString(this.m_ownerEncryptionKeyOut));
        dictionary.SetProperty("Perms", (IPdfPrimitive) new PdfString(this.m_permissionFlag));
      }
    }
    else
    {
      if (this.m_revisionNumberOut > 0)
        dictionary.SetNumber("R", this.m_revisionNumberOut);
      else
        dictionary.SetNumber("R", (int) (this.m_keyLength + 1));
      if (this.m_versionNumberOut > 0)
        dictionary.SetNumber("V", this.m_versionNumberOut);
      else
        dictionary.SetNumber("V", (int) this.m_keyLength);
    }
    dictionary.Archive = false;
  }

  internal bool Changed => this.m_bChanged;

  public PdfEncryptionKeySize CryptographicAlgorithm
  {
    get => this.m_keyLength;
    set
    {
      if (this.m_keyLength == value)
        return;
      this.m_keyLength = value;
      this.m_bChanged = true;
      this.m_hasComputedPasswordValues = false;
    }
  }

  protected byte[] CustomArray
  {
    get => this.m_customArray;
    set
    {
      if (this.m_customArray == value)
        return;
      this.m_customArray = value;
    }
  }

  internal bool Encrypt
  {
    get
    {
      bool flag = this.Permissions != PdfPermissionsFlags.Default || this.m_userPassword.Length > 0 || this.m_ownerPassword.Length > 0;
      return this.m_encrypt && flag;
    }
    set => this.m_encrypt = value;
  }

  public PdfEncryptionAlgorithm EncryptionAlgorithm
  {
    get => this.m_encryptionAlgorithm;
    set => this.m_encryptionAlgorithm = value;
  }

  protected byte[] EncryptionKey
  {
    get => this.m_encryptionKey;
    set
    {
      if (this.m_encryptionKey == value)
        return;
      this.m_encryptionKey = value;
    }
  }

  internal bool EncryptMetaData
  {
    get => this.m_encryptMetadata;
    set => this.m_encryptMetadata = value;
  }

  public PdfArray FileID
  {
    get
    {
      PdfString pdfString = new PdfString(this.RandomBytes);
      return new PdfArray()
      {
        (IPdfPrimitive) pdfString,
        (IPdfPrimitive) pdfString
      };
    }
  }

  public string Filter => SecurityHandlers.Standard.ToString();

  private SHA256Managed HashComputer
  {
    get
    {
      if (this.m_hashComputer == null)
        this.m_hashComputer = new SHA256Managed();
      return this.m_hashComputer;
    }
  }

  internal string OwnerPassword
  {
    get => this.m_ownerPassword;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (OwnerPassword));
      if (this.Provider == null)
        throw new NotSupportedException("Document encryption is not allowed in FIPS mode.");
      if (!(this.m_ownerPassword != value))
        return;
      this.m_bChanged = true;
      this.m_ownerPassword = value;
      this.m_hasComputedPasswordValues = false;
    }
  }

  internal byte[] OwnerPasswordOut
  {
    get
    {
      this.InitializeData();
      return this.m_ownerPasswordOut;
    }
  }

  protected static byte[] PaddingString
  {
    get => PdfEncryptor.s_paddingString;
    set
    {
      lock (PdfEncryptor.s_lockObject)
      {
        if (PdfEncryptor.s_paddingString == value)
          return;
        PdfEncryptor.s_paddingString = value;
      }
    }
  }

  internal PdfPermissionsFlags Permissions
  {
    get => this.m_permission;
    set
    {
      this.m_bChanged = true;
      this.m_permission = value;
      this.m_permissionValue = (int) ((this.m_permission | (PdfPermissionsFlags) -3904) & (PdfPermissionsFlags) -4);
      if (this.RevisionNumber > 2)
        this.m_permissionValue &= 4095 /*0x0FFF*/;
      this.m_hasComputedPasswordValues = false;
    }
  }

  protected MD5CryptoServiceProvider Provider => this.m_provider;

  protected byte[] RandomBytes
  {
    get
    {
      if (this.m_randomBytes == null)
      {
        this.m_randomBytes = new byte[16 /*0x10*/];
        this.m_randomArray.NextBytes(this.m_randomBytes);
      }
      return this.m_randomBytes;
    }
  }

  public int RevisionNumber
  {
    get
    {
      if (this.m_revision != 0)
        return this.m_revision;
      return this.CryptographicAlgorithm != PdfEncryptionKeySize.Key40Bit ? 3 : 2;
    }
  }

  protected Encoding SecurityEncoding => Encoding.Default;

  internal string UserPassword
  {
    get => this.m_userPassword;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (UserPassword));
      if (this.Provider == null)
        throw new NotSupportedException("Document encryption is not allowed in FIPS mode.");
      if (!(this.m_userPassword != value))
        return;
      this.m_bChanged = true;
      this.m_userPassword = value;
      this.m_hasComputedPasswordValues = false;
    }
  }

  internal byte[] UserPasswordOut
  {
    get
    {
      this.InitializeData();
      return this.m_userPasswordOut;
    }
  }
}
