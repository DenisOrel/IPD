
// Type: Intermech.Navigator.SelectionView.SelectionConditionWrapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Navigator.SelectionView;

internal class SelectionConditionWrapper : SelectionWrapper
{
  private TreeList _treeList;

  public SelectionConditionWrapper(TreeList treeList)
    : base(true)
  {
    this._treeList = treeList;
  }

  private XmlNode TreeNodesToXmlNode(
    IUserSession session,
    XmlDocument aXmlDoc,
    TreeListNodes aNodes)
  {
    XmlNode element = (XmlNode) aXmlDoc.CreateElement("ChildNodes");
    foreach (TreeListNode aNode in aNodes)
    {
      ConditionStructure cs = aNode.Tag == null || !(aNode.Tag is ConditionStructureNode) ? new ConditionStructure((string) null, RelationalOperators.None, (object) null, (object) null, LogicalOperators.NONE, 0, false) : ((ConditionStructureNode) aNode.Tag).ConditionStruct;
      XmlNode newChild = this.PackCondition(session, cs, aXmlDoc);
      XmlAttribute attribute = aXmlDoc.CreateAttribute("ConditionText");
      attribute.Value = Convert.ToString(aNode.GetValue((object) 0));
      newChild.Attributes.Append(attribute);
      newChild.AppendChild(this.TreeNodesToXmlNode(session, aXmlDoc, aNode.Nodes));
      element.AppendChild(newChild);
    }
    return element;
  }

  private void LoadXmlNode(XmlNode aXmlNode, TreeListNode rootNode)
  {
    foreach (XmlNode childNode1 in aXmlNode.ChildNodes)
    {
      if (childNode1.Name == "Condition")
      {
        string str = childNode1.Attributes["ConditionText"].Value;
        ConditionStructure conditionStructure = new ConditionStructure((string) null, RelationalOperators.None, (object) null, (object) null, LogicalOperators.NONE, 0, false);
        TreeListNode rootNode1 = this._treeList.AppendNode((object) new object[2]
        {
          (object) str,
          null
        }, rootNode);
        rootNode1.Tag = (object) new ConditionStructureNode(this.UnpackCondition(childNode1));
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.Name == "ChildNodes")
            this.LoadXmlNode(childNode2, rootNode1);
        }
        rootNode1.StateImageIndex = conditionStructure.LogicalOperator == LogicalOperators.AND ? 0 : 1;
      }
    }
  }

  /// <summary>
  /// Обновление информации об условиях выборки в кэше локальной службы на клиенте
  /// </summary>
  /// <param name="selectionID">инентификатор выборки информацию для которой надо обновить</param>
  private void UpdateCasheInfo(long selectionID)
  {
    ISelectionsService service = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
    if (service == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      service.UpdateCashe((object) sessionKeeper.Session, selectionID);
  }

  public void ToBase(IUserSession session, IDBObject obj)
  {
    if (this._treeList.Nodes.Count > 0)
      this.SaveXML(session, obj, this.ToXmlDocument(session, this._treeList.Nodes));
    else
      this.SaveXML(session, obj, (XmlDocument) null);
    this.UpdateCasheInfo(obj.ObjectID);
  }

  public void FromConditionsArray(
    IUserSession userSession,
    ConditionStructure[] array,
    bool correct)
  {
    this.FromXmlDocument(this.SaveToXML(userSession, array, correct));
  }

  public void FromXmlDocument(XmlDocument xmlDoc)
  {
    XmlNode documentElement = (XmlNode) xmlDoc.DocumentElement;
    if (documentElement == null)
      return;
    foreach (XmlNode childNode in documentElement.ChildNodes)
    {
      if (childNode.Name == "ChildNodes")
        this.LoadXmlNode(childNode, (TreeListNode) null);
    }
  }

  public void FromBase(IUserSession session, long objectID)
  {
    this.UpdateCasheInfo(objectID);
    this._treeList.Nodes.Clear();
    this.FromXmlDocument(this.LoadXML(session, objectID));
  }

  private XmlDocument ToXmlDocument(IUserSession session, TreeListNodes nodes)
  {
    XmlDocument aXmlDoc = new XmlDocument();
    aXmlDoc.AppendChild((XmlNode) aXmlDoc.CreateXmlDeclaration("1.0", (string) null, (string) null));
    XmlNode element = (XmlNode) aXmlDoc.CreateElement("SelectionParameters");
    aXmlDoc.AppendChild(element);
    element.AppendChild(this.TreeNodesToXmlNode(session, aXmlDoc, nodes));
    return aXmlDoc;
  }

  public List<ConditionStructure> UnpackConditionTreeList(IUserSession session, TreeListNodes nodes)
  {
    return this.GetConditionStructures(this.ToXmlDocument(session, nodes));
  }
}
