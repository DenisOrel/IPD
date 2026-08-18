// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.CERT_INFO
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct CERT_INFO
{
  public int dwVersion;
  public CRYPTOAPI_BLOB SerialNumber;
  public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
  public CRYPTOAPI_BLOB Issuer;
  public FILETIME NotBefore;
  public FILETIME NotAfter;
  public CRYPTOAPI_BLOB Subject;
  public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
  public CRYPTOAPI_BLOB IssuerUniqueId;
  public CRYPTOAPI_BLOB SubjectUniqueId;
  public int cExtension;
  public PCERT_EXTENSION rgExtension;
}
