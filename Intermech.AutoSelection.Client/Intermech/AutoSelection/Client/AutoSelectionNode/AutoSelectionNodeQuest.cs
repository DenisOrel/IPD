// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeQuest
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeQuest : AutoSelectionNodeCommon
{
  private string _question = "";

  private void InitializeData() => this._type = AutoSelectionNodeType.Question;

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    XmlAttribute attribute = doc.CreateAttribute("Question");
    attribute.Value = this._question;
    xmlNode.Attributes.Append(attribute);
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    XmlAttribute attribute = node.Attributes["Question"];
    if (attribute != null)
      this._question = attribute.Value;
    return (AutoSelectionNodeCommon) this;
  }

  public AutoSelectionNodeQuest(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this.InitializeData();
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_44")]
  [Editor(typeof (SimpleMemoEditor), typeof (UITypeEditor))]
  public string Question
  {
    get => this._question;
    set => this._question = value;
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied || !MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_14"), (object) this.Question), LocalizationHolder.rm.GetString("AutoSelection.Client_15"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.No))
      return selExecuteStatus;
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, LocalizationHolder.rm.GetString("AutoSelection.Client_16"));
    return AutoSelExecuteStatus.AbortAll;
  }
}
