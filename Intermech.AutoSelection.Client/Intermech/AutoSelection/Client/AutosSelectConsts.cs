// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutosSelectConsts
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Forms;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces.Client;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client;

public static class AutosSelectConsts
{
  internal static int CategoryAutoSelectionTypesNode = -1;
  internal static int CategoryAutoSelectionTypeNode = -1;
  internal static readonly Guid CategoryAutoSelectionTypesNodeGuid = new Guid("{6A744B63-D9E0-452f-B3DE-85ECA8A142F1}");
  internal static readonly Guid CategoryAutoSelectionTypeNodeGuid = new Guid("{6A744B64-D9E0-452f-B3DE-85ECA8A142F1}");
  public static Guid ImbaseObjectLinkAttrGuid = new Guid("cad00209-306c-11d8-b4e9-00304f19f545");
  public static Guid AutoSelectionModeAttrGuid = new Guid("cad009bb-306c-11d8-b4e9-00304f19f545");
  public static Guid AutoSelectionFormDockGuid = new Guid("cad009bb-306c-11d8-b4e9-00304f134645");

  public static void AutoSelectionSetupClick(object sender, EventArgs e)
  {
    using (Form form = (Form) new AutoSelectionTreeSetupForm())
    {
      int num = (int) form.ShowTopDialog();
    }
  }

  internal static class Config
  {
    public static readonly bool DelayedObjectCreation;
  }

  public static class Images
  {
    public const int ImageMaxNo = 105;
    public const int ImageFolderWithRuleIdx = 8;
    public static readonly string AutoSelectionResourceString = "Intermech.AutoSelection.Client.Resources.{0}.bmp";

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool DestroyIcon(IntPtr handle);

    public static void LoadImages(ref ImageList ilTree)
    {
      ICategoryTypeIconService categoryTypeIconService = AutoSelectionUtils.ServiceKeeper.GetCategoryTypeIconService();
      if (categoryTypeIconService?.ImageList == null)
        return;
      ImageList imList = new ImageList();
      for (int type = 0; type <= 105; ++type)
      {
        if (categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, type) <= 0)
        {
          imList.Images.Clear();
          AutosSelectConsts.Images.LoadImage(string.Format(AutosSelectConsts.Images.AutoSelectionResourceString, (object) ("i" + (object) type)), imList);
          if (imList.Images.Count > 0 && imList.Images[0] is Bitmap image)
          {
            using (Icon icon = Icon.FromHandle(image.GetHicon()))
            {
              categoryTypeIconService.AddIcon(icon, AutosSelectConsts.CategoryAutoSelectionTypeNode, type);
              AutosSelectConsts.Images.DestroyIcon(icon.Handle);
            }
          }
        }
      }
      ilTree = categoryTypeIconService.ImageList;
    }

    public static void LoadBaseImages(ImageList ilTree)
    {
      if (ilTree == null)
        return;
      ilTree.Images.Clear();
      for (int index = 0; index <= 105; ++index)
        AutosSelectConsts.Images.LoadImage(string.Format(AutosSelectConsts.Images.AutoSelectionResourceString, (object) ("i" + (object) index)), ilTree);
    }

    public static void LoadImage(string manifestString, ImageList imList)
    {
      Stream manifestResourceStream = typeof (AutosSelectConsts).Assembly.GetManifestResourceStream(manifestString);
      if (manifestResourceStream == null)
        return;
      Bitmap bitmap = new Bitmap(manifestResourceStream);
      imList.Images.Add((Image) bitmap, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
    }
  }
}
