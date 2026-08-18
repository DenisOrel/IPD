// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UpdateViewFieldValues
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;


namespace Intermech.Kernel;

internal class UpdateViewFieldValues
{
  public List<UpdateViewFieldValue> Values;

  public UpdateViewFieldValues(object value, string fldName)
  {
    this.Values = new List<UpdateViewFieldValue>();
    this.Values.Add(new UpdateViewFieldValue(value, fldName));
  }

  public void Add(object value, string fldName)
  {
    bool flag = true;
    foreach (UpdateViewFieldValue updateViewFieldValue in this.Values)
    {
      if (updateViewFieldValue.FieldName.Equals(fldName))
      {
        updateViewFieldValue.Value = value;
        flag = false;
        break;
      }
    }
    if (!flag)
      return;
    this.Values.Add(new UpdateViewFieldValue(value, fldName));
  }
}
