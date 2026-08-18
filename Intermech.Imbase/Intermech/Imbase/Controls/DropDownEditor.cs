// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.DropDownEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class DropDownEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }
}
