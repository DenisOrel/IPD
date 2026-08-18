// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.CRYPT_SIGN_MESSAGE_PARA
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct CRYPT_SIGN_MESSAGE_PARA
{
  public uint cbSize;
  public uint dwMsgEncodingType;
  public IntPtr pSigningCert;
  public CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;
  public IntPtr pvHashAuxInfo;
  public uint cMsgCert;
  public IntPtr rgpMsgCert;
  public uint cMsgCrl;
  public IntPtr rgpMsgCrl;
  public uint cAuthAttr;
  public IntPtr rgAuthAttr;
  public uint cUnauthAttr;
  public IntPtr rgUnauthAttr;
  public uint dwFlags;
  public uint dwInnerContentType;
  public CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;
  public IntPtr pvHashEncryptionAuxInfo;
}
