// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepPropDescriptor
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

public class LCStepPropDescriptor(
  int propID,
  object component,
  string name,
  object value,
  Type type,
  TypeConverter converter,
  object editor,
  string category,
  string description,
  bool readOnly,
  bool browsable,
  bool reset) : PropDescriptor(propID, component, name, value, type, converter, editor, category, description, readOnly, browsable, reset)
{
  public override void ResetValue(object component)
  {
    (this.GetValue((object) null) as LCStepScriptValue).NewScriptId = new long?(-1L);
    this.ValueChanged = true;
  }
}
