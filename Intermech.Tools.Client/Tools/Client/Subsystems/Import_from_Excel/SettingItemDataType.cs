// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.SettingItemDataType
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[TypeConverter(typeof (EnumDescConverter))]
public enum SettingItemDataType
{
  [CustomDescription("Tools.Client_278")] TypeName,
  [CustomDescription("Tools.Client_279")] TypeId,
  [CustomDescription("Tools.Client_280")] TypeGuid,
}
