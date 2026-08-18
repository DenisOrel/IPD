
// Type: Intermech.PropertyEditors.PluginFileAttEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class PluginFileAttEditor : UITypeEditor
{
  /// <summary>у нас этого диалог выбора файлов</summary>
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
  /// <param name="sp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.RestoreDirectory = true;
      openFileDialog.InitialDirectory = Environment.CurrentDirectory;
      openFileDialog.Filter = LocalizationHolder.rm.GetString("Client.Core_648") + "(*.dll)|*.dll";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        return (object) Path.GetFileName(openFileDialog.FileName);
    }
    return value;
  }
}
