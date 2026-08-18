
// Type: Intermech.Controls.StandartLineThicknessesEditor
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.Controls;

public class StandartLineThicknessesEditor(Type type) : CollectionEditor(type)
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    object obj = base.EditValue(context, provider, value);
    if (!(value is SelectLineThicknessUserControl.StandartLineThicknessesCollection))
      return obj;
    ((SelectLineThicknessUserControl.StandartLineThicknessesCollection) value).Owner.AfterStandartLineThicknessesChanged();
    return obj;
  }
}
