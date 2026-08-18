// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.BlankSetupPage
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Setup;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual;

public class BlankSetupPage : BlankSetup
{
  private static readonly Guid PersistStateGuid = new Guid("DE107032-15DB-419D-91B4-D05EB0F3D25E");

  public BlankSetupPage()
  {
    this.Guid = BlankSetupPage.PersistStateGuid;
    this.OnSaveChanges = (Func<BlankSetup, bool, bool>) ((sender, closing) =>
    {
      if (this.ReadOnly)
        return true;
      DialogResult dialogResult = DialogResult.Yes;
      if (closing)
        dialogResult = MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Document_186"), LocalizationHolder.rm.GetString("TechCard.Document_187"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      switch (dialogResult)
      {
        case DialogResult.Cancel:
          return false;
        case DialogResult.Yes:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            DocumentConfigSerializer.Save(this.Rules, this.Rules.ObjectId, sessionKeeper.Session);
          ApplicationServices.Container.GetService<INotificationService>()?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.Rules.ObjectId));
          return true;
        default:
          return true;
      }
    });
  }

  protected override string GetPersistString()
  {
    try
    {
      XmlDocument state = this.GetState();
      using (TextWriter w1 = (TextWriter) new StringWriter())
      {
        XmlWriter w2 = (XmlWriter) new XmlTextWriter(w1);
        state.WriteTo(w2);
        w2.Flush();
        w2.Close();
        return w1.ToString();
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("TechCard.Document_191"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (string) null;
    }
  }

  private XmlDocument GetState()
  {
    XmlDocument state = new XmlDocument();
    XmlNode element = (XmlNode) state.CreateElement("TechCardDocumentSetup");
    state.AppendChild((XmlNode) state.CreateXmlDeclaration("1.0", (string) null, (string) null));
    state.AppendChild(element);
    XmlAttribute attribute1 = state.CreateAttribute("ObjectId");
    attribute1.Value = this.Rules.ObjectId.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = state.CreateAttribute("ReadOnly");
    attribute2.Value = this.ReadOnly ? "1" : "0";
    element.Attributes.Append(attribute2);
    return state;
  }

  public static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    if (guid != BlankSetupPage.PersistStateGuid)
      return (DockControl) null;
    if (string.IsNullOrEmpty(persistString))
      return (DockControl) null;
    try
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(persistString);
      if (xmlDocument.ChildNodes.Count == 0)
        return (DockControl) null;
      XmlAttributeCollection attributes = xmlDocument.ChildNodes[1].Attributes;
      if (attributes == null)
        return (DockControl) null;
      XmlAttribute xmlAttribute1 = attributes["ObjectId"];
      XmlAttribute xmlAttribute2 = attributes["ReadOnly"];
      long result;
      if (xmlAttribute1 == null || !long.TryParse(xmlAttribute1.Value, out result))
        return (DockControl) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(result, false);
        if (dbObject == null)
          return (DockControl) null;
        Rules rules = DocumentConfigLoader.Load(dbObject.ObjectID, sessionKeeper.Session);
        string str = !string.IsNullOrEmpty(dbObject.Caption) ? dbObject.Caption : LocalizationHolder.rm.GetString("TechCard.Document_187");
        BlankSetupPage blankSetupPage = new BlankSetupPage();
        blankSetupPage.Rules = rules;
        blankSetupPage.ReadOnly = xmlAttribute2.Value == "1";
        blankSetupPage.Text = str;
        blankSetupPage.TabText = str;
        return (DockControl) blankSetupPage;
      }
    }
    catch (Exception ex)
    {
      IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, true);
      service.WriteString("Navigator", LocalizationHolder.rm.GetString("TechCard.Document_192"));
      service.WriteString("Navigator", ex.Message);
      return (DockControl) null;
    }
  }
}
