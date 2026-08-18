// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportTaskOptions
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

[Flags]
public enum ReportTaskOptions
{
  /// <summary>
  /// 
  /// </summary>
  None = 0,
  /// <summary>Не отображать окно с документами</summary>
  HideDocWindow = 1,
  /// <summary>Не отображать окно вывода</summary>
  HideOutputWindow = 2,
  /// <summary>Не отображать окно трассировки</summary>
  HideTraceWindow = 4,
  /// <summary>
  /// 
  /// </summary>
  SilentMode = HideTraceWindow | HideOutputWindow | HideDocWindow, // 0x00000007
}
