
// Type: Intermech.Client.Core.BrowseForServerFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Класс для выбора серверной папки</summary>
public class BrowseForServerFolder
{
  /// <summary>Выбрать серверную папку.</summary>
  /// <param name="browser"></param>
  /// <returns>Если выбор отменен, то null</returns>
  public static string SelectFolder(IBrowseFolder browser)
  {
    string str = (string) null;
    using (BrowseFolderDialog browseFolderDialog = new BrowseFolderDialog(browser, BrowseFolderDialogOptions.CreateFolderEnable))
    {
      if (browseFolderDialog.ShowDialog() == DialogResult.OK)
        str = browseFolderDialog.Path.Replace("\\\\", "\\");
    }
    return str;
  }
}
