// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.CryptoApi
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Native;

internal sealed class CryptoApi
{
  private CryptoApi()
  {
  }

  [DllImport("CRYPT32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool CertCloseStore(IntPtr storeProvider, int flags);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern IntPtr CertDuplicateCertificateContext(IntPtr pCertContext);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern IntPtr CertEnumCertificatesInStore(
    IntPtr storeProvider,
    IntPtr prevCertContext);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern IntPtr CertFindCertificateInStore(
    IntPtr hCertStore,
    uint dwCertEncodingType,
    uint dwFindFlags,
    uint dwFindType,
    [MarshalAs(UnmanagedType.LPWStr), In] string pszFindString,
    IntPtr pPrevCertCntxt);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern bool CertFreeCertificateContext(IntPtr pCertContext);

  [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr CertOpenStore(
    [MarshalAs(UnmanagedType.LPStr)] string storeProvider,
    uint dwMsgAndCertEncodingType,
    IntPtr hCryptProv,
    uint dwFlags,
    string cchNameString);

  [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern IntPtr CertOpenSystemStore(IntPtr hCryptProv, string storename);

  [DllImport("crypt32.dll")]
  public static extern bool CryptDecodeObject(
    uint CertEncodingType,
    uint lpszStructType,
    IntPtr pbEncoded,
    int cbEncoded,
    uint flags,
    IntPtr pvStructInfo,
    ref int cbStructInfo);

  [DllImport("Crypt32.dll", CharSet = CharSet.Ansi)]
  public static extern bool CryptSignMessage(
    ref CRYPT_SIGN_MESSAGE_PARA pSignPara,
    bool fDetachedSignature,
    uint cToBeSigned,
    IntPtr[] rgpbToBeSigned,
    int[] rgcbToBeSigned,
    IntPtr pbSignedBlob,
    ref uint pcbSignedBlob);

  [DllImport("Cryptdll.dll", CharSet = CharSet.Ansi)]
  public static extern void MD5Final(ref Md5_Ctx context);

  [DllImport("Cryptdll.dll", CharSet = CharSet.Ansi)]
  public static extern void MD5Init(ref Md5_Ctx context);

  [DllImport("Cryptdll.dll", CharSet = CharSet.Ansi)]
  public static extern void MD5Update(ref Md5_Ctx context, byte[] input, int inlen);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern IntPtr PFXImportCertStore(
    ref CRYPT_DATA_BLOB pPfx,
    [MarshalAs(UnmanagedType.LPWStr)] string szPassword,
    uint dwFlags);

  [DllImport("crypt32.dll", SetLastError = true)]
  public static extern bool PFXIsPFXBlob(ref CRYPT_DATA_BLOB pPfx);
}
