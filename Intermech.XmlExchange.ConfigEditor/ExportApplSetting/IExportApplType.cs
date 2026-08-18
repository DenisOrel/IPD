// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ExportApplSetting.IExportApplType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ExportApplSetting;

internal interface IExportApplType
{
  string TypeName { get; set; }

  Guid TypeGuid { get; set; }

  int TypeId { get; set; }

  bool ExistInBase { get; }

  string ApplType { get; }

  void UpdateExportAppl();

  List<XmlExchangeExportAppl> GetCurrentApplList();

  void ResetValue();
}
