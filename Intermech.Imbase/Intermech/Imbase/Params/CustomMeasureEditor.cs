// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.CustomMeasureEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.Params;

internal class CustomMeasureEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    long[] numArray = SelectionWindow.SelectObjects("Выберите единицу измерения по умолчанию", "Укажите единицу измерения, которая будет использоваться по умолчанию", MetaDataHelper.GetObjectTypeID("cad0000b-306c-11d8-b4e9-00304f19f545"), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    return numArray != null && numArray.Length != 0 ? (object) numArray[0] : value;
  }
}
