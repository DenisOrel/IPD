// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportDocEventHandler
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Делегат задачи генератора комплекта документов</summary>
/// <param name="sender"></param>
/// <param name="e"></param>
/// <returns></returns>
public delegate void ReportDocEventHandler(object sender, ReportDocBaseEvent e);
