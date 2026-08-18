// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ParticipantsEditor
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>редактор исполнителей</summary>
internal class ParticipantsEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    ParticipantsEditorForm participantsEditorForm = new ParticipantsEditorForm();
    if (value != null)
    {
      if (value is ParticipantsPropertyClass)
        participantsEditorForm.Data = (value as ParticipantsPropertyClass).Value;
      else if (value is string)
        participantsEditorForm.Data = value.ToString();
    }
    return participantsEditorForm.ShowDialog() == DialogResult.OK ? (object) new ParticipantsPropertyClass(participantsEditorForm.Data) : value;
  }
}
