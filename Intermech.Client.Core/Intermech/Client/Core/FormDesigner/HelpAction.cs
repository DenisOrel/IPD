
// Type: Intermech.Client.Core.FormDesigner.HelpAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Вызов справки пользователя.</summary>
internal class HelpAction : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form)
  {
    return form is DesForm desForm && !string.IsNullOrEmpty(desForm.HelpPathToFile);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    if (!(form is DesForm parent))
      return;
    string str = parent.HelpPathToFile;
    if (!File.Exists(str))
      str = Path.Combine(Path.GetDirectoryName(HelpProvidersClass.HelpPath), Path.GetFileName(str));
    if (!File.Exists(str))
    {
      int num = (int) MessageBox.Show($"{LocalizationHolder.rm.GetString("FormDesigner_HelpAction_FileNotFound")}: {str}", LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (string.IsNullOrEmpty(parent.HelpPartLabel))
      Help.ShowHelp((Control) parent, str, HelpNavigator.TableOfContents);
    else
      Help.ShowHelp((Control) parent, str, HelpNavigator.TopicId, (object) parent.HelpPartLabel);
  }
}
