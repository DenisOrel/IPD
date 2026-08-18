// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.CustomDescription
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class CustomDescription(string description) : DescriptionAttribute(LocalizationHolder.rm.GetString(description) ?? string.Empty)
{
}
