// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.IConfigEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

public interface IConfigEditor
{
  void LoadConfigInObject(long idObjectConfig);

  string LoadConfigInFile(string pathFile);

  Image GetTabImage();

  void SaveConfigInObject();

  bool SaveConfigInFile(string pathFile);

  void UpdateTreeView(object sender, EventArgs e);

  void EnterEditorWindow(object sender, EventArgs e);

  void Menu_Opening(object sender, CancelEventArgs e, TreeNode selectedNode);
}
