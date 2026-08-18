// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ValueObjChangedHandler
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Делегат на изменение object значения</summary>
/// <param name="sender"></param>
/// <param name="value"></param>
public delegate void ValueObjChangedHandler(object sender, object value);
