
// Type: Intermech.Client.Core.FlagsConditions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Client.Core;

/// <summary>Summary description for Commons.</summary>
[CustomDescription("Attribute.Client.Core_201")]
[Flags]
public enum FlagsConditions : uint
{
  [CustomDescription("Attribute.Client.Core_202")] NONE = 0,
  [CustomDescription("Attribute.Client.Core_203")] EQUAL = 1,
  [CustomDescription("Attribute.Client.Core_204")] NOTEQUAL = 2,
  [CustomDescription("Attribute.Client.Core_205")] LESS = 4,
  [CustomDescription("Attribute.Client.Core_206")] LESSEQUAL = 8,
  [CustomDescription("Attribute.Client.Core_207")] GREATER = 16, // 0x00000010
  [CustomDescription("Attribute.Client.Core_208")] GREATEREQUAL = 32, // 0x00000020
  [CustomDescription("Attribute.Client.Core_209")] SUBSTR = 64, // 0x00000040
}
