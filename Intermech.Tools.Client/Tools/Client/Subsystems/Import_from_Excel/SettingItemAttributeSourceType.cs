// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.SettingItemAttributeSourceType
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[TypeConverter(typeof (EnumDescConverter))]
public enum SettingItemAttributeSourceType
{
  [CustomDescription("Tools.Client_281")] Object,
  [CustomDescription("Tools.Client_282")] Relation,
  [CustomDescription("Tools.Client_283")] Entrancy,
}
