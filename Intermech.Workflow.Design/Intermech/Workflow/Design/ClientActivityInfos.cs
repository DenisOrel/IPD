// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ClientActivityInfos
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class ClientActivityInfos : ActivityInfos
{
  private static ImageList _imageList;

  public static ImageList ImageList
  {
    get
    {
      if (ClientActivityInfos._imageList == null)
      {
        ClientActivityInfos._imageList = new ImageList();
        ClientActivityInfos._imageList.ColorDepth = ColorDepth.Depth32Bit;
        ClientActivityInfos._imageList.ImageSize = new Size(32 /*0x20*/, 32 /*0x20*/);
        ICategoryTypeIconService service = (ICategoryTypeIconService) ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService));
        foreach (Intermech.Workflow.ActivityInfo activityInfo in (List<Intermech.Workflow.ActivityInfo>) ActivityInfos.Items)
        {
          Icon icon = service.GetIcon(4, activityInfo.Type);
          if (icon != null)
          {
            ClientActivityInfos._imageList.Images.Add(icon);
            activityInfo.ImageIndex = ClientActivityInfos._imageList.Images.Count - 1;
          }
          else
            activityInfo.ImageIndex = -1;
        }
      }
      return ClientActivityInfos._imageList;
    }
  }
}
