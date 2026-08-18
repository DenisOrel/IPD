
// Type: Intermech.WindowsDll.TokenAccessRights
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.WindowsDll
{
    [Flags]
    public enum TokenAccessRights
    {
      AdjustDefault = 128, // 0x00000080
      AdjustGroups = 64, // 0x00000040
      AdjustPrivileges = 32, // 0x00000020
      AdjustSessionId = 256, // 0x00000100
      AssignPrimary = 1,
      Duplicate = 2,
      Impersonate = 4,
      Query = 8,
      QuerySource = 16, // 0x00000010
      Read = 131080, // 0x00020008
    }
}
