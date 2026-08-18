
// Type: Intermech.PropertyEditors.CompositionObjectGuidEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class CompositionObjectGuidEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    long result = -1;
    if (value != null)
      long.TryParse(value.ToString(), out result);
    IDescriptor rootDescriptor = (IDescriptor) new Descriptor(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("FormDesigner_104"), string.Empty, rootDescriptor, SelectionOptions.Default | SelectionOptions.HideTree);
    return numArray == null || numArray.Length == 0 || result == numArray[0] ? value : (object) numArray[0];
  }
}
