
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrCheckBoxControlDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class AttrCheckBoxControlDesigner : ControlDesigner
{
  /// <summary>
  /// 
  /// </summary>
  public override SelectionRules SelectionRules
  {
    get
    {
      SelectionRules selectionRules = base.SelectionRules;
      object component = (object) this.Component;
      PropertyDescriptor property = TypeDescriptor.GetProperties(component)["AutoSize"];
      if (property != null && (bool) property.GetValue(component))
        selectionRules &= ~SelectionRules.AllSizeable;
      return selectionRules;
    }
  }
}
