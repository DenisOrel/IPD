
// Type: Intermech.Tools.ToolSecurityRights
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools
{
    [Flags]
    [Serializable]
    public enum ToolSecurityRights
    {
      None = 0,
      EditPublicSettings = 1,
      EditPersonalSettings = 2,
      OverridePersonalSettings = 4,
      All = OverridePersonalSettings | EditPersonalSettings | EditPublicSettings, // 0x00000007
    }
}
