
// Type: Intermech.PropertyEditors.IConfigPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Интерфейс работы с формой вставленной в TabPage</summary>
public interface IConfigPage
{
  IFolder Folder { set; get; }

  void DockToPanel(Panel panel);

  void Undock();

  void SetChangedStatus(bool status);

  bool Changed { get; }

  GridControl GridControl { get; }

  PropertyGrid PropertyGrid { get; }

  IBaseTabPage LastTabPage { get; }

  TabControl TabControl { get; }

  void OpenTabPage(TabPage tabpage);

  void DefaultsOnLoad();

  bool DefaultsOnSave();

  void DefaultsOnLostFocus(IFolder folder);
}
