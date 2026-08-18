// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PKIStatus
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    [Flags]
    internal enum PKIStatus
    {
      Granted = 0,
      GrantedWithMods = 1,
      Rejection = 2,
      Waiting = Rejection | GrantedWithMods, // 0x00000003
      RevocationWarning = 4,
      RevocationNotification = RevocationWarning | GrantedWithMods, // 0x00000005
    }
}
