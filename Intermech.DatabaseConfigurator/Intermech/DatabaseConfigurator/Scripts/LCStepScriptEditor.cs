// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepScriptEditor
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

internal class LCStepScriptEditor : UITypeEditor
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
    if (value is LCStepScriptValue lcStepScriptValue1)
    {
      object[] objArray = SelectionWindow.Select("Выбор скрипта", (IDescriptor) new Descriptor(LCStepScriptValue.LCScriptTypeId), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
      if (objArray != null && objArray.Length != 0 && objArray[0] is IDBTypedObjectID)
      {
        LCStepScriptValue lcStepScriptValue = lcStepScriptValue1.Clone();
        lcStepScriptValue.NewScriptId = new long?((objArray[0] as IDBTypedObjectID).ObjectID);
        return (object) lcStepScriptValue;
      }
    }
    return (object) lcStepScriptValue1;
  }
}
