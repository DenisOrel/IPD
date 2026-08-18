// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.IPageConfigEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal interface IPageConfigEditor
{
  void InitializeCustomComponent();

  void LoadData(object selectNode, bool readOnly);

  void SaveData(bool save, bool refresh);

  void UpdateView();

  bool EditData { get; }

  string PageName { get; }

  event EventHandler ModifyData;
}
