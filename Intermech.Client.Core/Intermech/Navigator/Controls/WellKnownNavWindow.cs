
// Type: Intermech.Navigator.Controls.WellKnownNavWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Controls;

public class WellKnownNavWindow : NavWindowBase
{
  private string _wellKnownName;
  public static readonly Guid _persistStateGuid = new Guid("{EB92B1A0-0762-437d-97C0-0D4F72D64417}");

  public WellKnownNavWindow()
  {
    this._wellKnownName = string.Empty;
    this.Guid = WellKnownNavWindow._persistStateGuid;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && Holder.WellKnownNavigators != null)
      Holder.WellKnownNavigators.Unregister((Control) this);
    base.Dispose(disposing);
  }

  [Category("Behavior")]
  [DefaultValue("")]
  public string WellKnownName
  {
    get => this._wellKnownName;
    set
    {
      if (value == null)
        throw new ArgumentException("Well known navigator window name cannot be null!", nameof (WellKnownName));
      if (!(this._wellKnownName != value))
        return;
      this._wellKnownName = value;
      if (Holder.WellKnownNavigators == null)
        return;
      if (value != string.Empty)
        Holder.WellKnownNavigators.Register(this._wellKnownName, (Control) this);
      else
        Holder.WellKnownNavigators.Unregister((Control) this);
    }
  }

  protected override string GetPersistString() => base.GetPersistString();

  protected override XmlNode GetPropertiesNode(XmlDocument xmlDoc)
  {
    XmlNode propertiesNode = base.GetPropertiesNode(xmlDoc);
    XmlNode element1 = (XmlNode) xmlDoc.CreateElement("WellKnownName");
    element1.AppendChild((XmlNode) xmlDoc.CreateTextNode(this._wellKnownName));
    propertiesNode.AppendChild(element1);
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("Text");
    element2.AppendChild((XmlNode) xmlDoc.CreateTextNode(this.Text));
    propertiesNode.AppendChild(element2);
    if (this.TabImageIndex >= 0)
    {
      XmlNode element3 = (XmlNode) xmlDoc.CreateElement("TabImageName");
      element3.AppendChild((XmlNode) xmlDoc.CreateTextNode(Holder.NamedImageList.ImageName(this.TabImageIndex)));
      propertiesNode.AppendChild(element3);
    }
    return propertiesNode;
  }

  protected override void RestoreProperties(XmlNode settingsNode)
  {
    base.RestoreProperties(settingsNode);
    XmlNode xmlNode1 = settingsNode.SelectSingleNode("Properties/WellKnownName");
    if (xmlNode1 != null)
      this.WellKnownName = xmlNode1.InnerText;
    XmlNode xmlNode2 = settingsNode.SelectSingleNode("Properties/Text");
    if (xmlNode2 != null)
      this.Text = xmlNode2.InnerText;
    if (Holder.NamedImageList == null)
      return;
    XmlNode xmlNode3 = settingsNode.SelectSingleNode("Properties/TabImageName");
    if (xmlNode3 == null)
      return;
    this.TabImageIndex = Holder.NamedImageList.ImageIndex(xmlNode3.InnerText);
  }

  public static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    if (guid != WellKnownNavWindow._persistStateGuid)
      return (DockControl) null;
    try
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(persistString);
      WellKnownNavWindow wellKnownNavWindow = new WellKnownNavWindow();
      try
      {
        wellKnownNavWindow.RestoreState(xmlDoc);
        if (wellKnownNavWindow.RootDescriptor == null)
        {
          wellKnownNavWindow.WellKnownName = string.Empty;
          wellKnownNavWindow.HideOnClose = false;
          wellKnownNavWindow.Close();
          return (DockControl) null;
        }
        bool flag1 = false;
        if (ServicesManager.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service1)
          flag1 = service1.EnableTreeMultiSelect(wellKnownNavWindow.RootDescriptor, (System.IServiceProvider) wellKnownNavWindow.Services);
        if (flag1)
          wellKnownNavWindow.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
        bool flag2 = true;
        if (ServicesManager.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service2)
          flag2 = service2.EnableTreeColumnsSorting(wellKnownNavWindow.RootDescriptor, (System.IServiceProvider) wellKnownNavWindow.Services);
        wellKnownNavWindow.TreeView.DisableColumnsSorting = !flag2;
        wellKnownNavWindow.btClearSorting.Enabled = flag2;
        if (!flag2)
          wellKnownNavWindow.btClearSorting.Checked = true;
        bool flag3 = false;
        if (ServicesManager.GetService(typeof (INavigatorTreeCollapseService)) is INavigatorTreeCollapseService service3)
          flag3 = service3.EnableTreeCollapse(wellKnownNavWindow.RootDescriptor, (System.IServiceProvider) wellKnownNavWindow.Services);
        if (flag3 && !wellKnownNavWindow.spTreeView.IsCollapsed)
          wellKnownNavWindow.spTreeView.ToggleState();
        return (DockControl) wellKnownNavWindow;
      }
      catch (Exception ex)
      {
        wellKnownNavWindow.WellKnownName = string.Empty;
        wellKnownNavWindow.HideOnClose = false;
        wellKnownNavWindow.Close();
        wellKnownNavWindow.Dispose();
      }
    }
    catch (Exception ex)
    {
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      service.WriteString("Navigator", LocalizationHolder.rm.GetString("Client.Core_326"));
      service.WriteString("Navigator", ex.Message);
      return (DockControl) null;
    }
    return (DockControl) null;
  }
}
