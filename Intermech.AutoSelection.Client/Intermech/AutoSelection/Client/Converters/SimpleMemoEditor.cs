// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters.SimpleMemoEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Extensions.WinForms;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters;

public class SimpleMemoEditor : UITypeEditor
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
    if (value == null)
      return (object) null;
    MemoForm form = new MemoForm()
    {
      Memo = value.ToString()
    };
    return form.ShowTopDialog() == DialogResult.OK ? (object) form.Memo ?? (object) string.Empty : value;
  }
}
