
// Type: Intermech.Navigator.Controls.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Localization;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator.Controls;

internal sealed class Services
{
  public static void Start()
  {
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("ToggleTree.ico"))
    {
      using (Image image = Image.FromStream(resourceStream))
        Holder.NamedImageList.Add(image, "icoToggleTree");
    }
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("ListView.ico"))
    {
      using (Image image = Image.FromStream(resourceStream))
        Holder.NamedImageList.Add(image, "icoListView");
    }
    ButtonItem buttonItem = new ButtonItem();
    buttonItem.BeginGroup = true;
    buttonItem.CommandName = "FetchTree";
    buttonItem.Text = LocalizationHolder.rm.GetString("Client.Core_603");
    buttonItem.ImageIndex = Holder.NamedImageList.ImageIndex("imgFetchData");
    buttonItem.Visible = true;
    buttonItem.Enabled = false;
    Holder.CommandManager.Add((ButtonItemBase) buttonItem);
    Guid guid = new Guid("f34da14a-091a-4f96-934f-3e5ba2a5dc08");
    Holder.BarManager.FindToolbar(guid)?.Items.Add((ToolbarItemBase) buttonItem);
    Holder.ContentProvider.ContentCallback += new GetContentCallback(RecentObjectsWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback += new GetContentCallback(WellKnownNavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback += new GetContentCallback(NavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback += new GetContentCallback(VersionsNavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback += new GetContentCallback(CompositionsAutosortRulesWindow.RestoreWindowCallback);
    ClassifyingControl.Start();
    UpdateLock.Start();
  }

  public static void Stop()
  {
    Holder.ContentProvider.ContentCallback -= new GetContentCallback(RecentObjectsWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback -= new GetContentCallback(WellKnownNavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback -= new GetContentCallback(NavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback -= new GetContentCallback(VersionsNavWindow.RestoreWindowCallback);
    Holder.ContentProvider.ContentCallback -= new GetContentCallback(CompositionsAutosortRulesWindow.RestoreWindowCallback);
    ClassifyingControl.Stop();
    UpdateLock.Stop();
  }
}
