// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ImagesLibraryEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class ImagesLibraryEditor : ImageEditor
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
    IPicturesCache service = ServiceUtils.GetService<IPicturesCache>((object) ApplicationServices.Container, false);
    IDescriptor rootDescriptor = (IDescriptor) new ImageLibraryRootNodeDescriptor();
    if (service != null && rootDescriptor != null)
    {
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("FormDesigner_107"), string.Empty, rootDescriptor, SelectionOptions.Default);
      if (numArray != null && numArray.Length != 0)
      {
        long objectId = numArray[0];
        object picture = service.GetPicture(objectId);
        if (picture is Image)
          return picture;
      }
    }
    return value;
  }
}
