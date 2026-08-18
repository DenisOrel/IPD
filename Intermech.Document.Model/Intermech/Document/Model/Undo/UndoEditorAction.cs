// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoEditorAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoEditorAction : IUndoAction
{
  private IUndoManager manager;
  private int action = 638;

  public UndoEditorAction(IUndoManager manager) => this.manager = manager;

  public bool DoAction()
  {
    int num = 0;
    if (!(this.manager.Form is ImDocumentEditorFormBase))
      return num != 0;
    if ((this.manager.Form as ImDocumentEditorFormBase).DocumentControl == null)
      return num != 0;
    ImRtfEditor ternEditorBuffer = (this.manager.Form as ImDocumentEditorFormBase).DocumentControl.TernEditorBuffer;
    if (ternEditorBuffer == null)
      return num != 0;
    if (!ternEditorBuffer.Visible)
      return num != 0;
    ternEditorBuffer.TerCommand(this.action);
    return num != 0;
  }

  public string Caption => LocalizationHolder.rm.GetString("Document.Model_562");

  public void IdChanged(string oldValue, string newValue)
  {
  }

  public IUndoAction CreateRedoAction()
  {
    return (IUndoAction) new UndoEditorAction(this.manager)
    {
      action = 747
    };
  }
}
