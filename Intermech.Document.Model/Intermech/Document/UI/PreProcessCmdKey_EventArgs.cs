// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PreProcessCmdKey_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Аргументы события PreProcessCmdKey</summary>
public class PreProcessCmdKey_EventArgs
{
  public bool Cancel;
  public Message Msg;
  public Keys KeyData;
  public PageElementUI FocusedElement;

  public PreProcessCmdKey_EventArgs(Message msg, Keys keyData, PageElementUI focusedElement)
  {
    this.Cancel = false;
    this.Msg = msg;
    this.KeyData = keyData;
    this.FocusedElement = focusedElement;
  }
}
