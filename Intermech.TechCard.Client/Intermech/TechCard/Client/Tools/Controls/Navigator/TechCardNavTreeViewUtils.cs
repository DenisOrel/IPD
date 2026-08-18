// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavTreeViewUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>TechCard navigator tree view utilities</summary>
public class TechCardNavTreeViewUtils
{
  /// <summary>Merge column's collections</summary>
  public static void MergeColumnCollections(
    NodeColumnCollection source,
    NodeColumnCollection destination)
  {
    if (source == null || destination == null)
      return;
    foreach (NodeColumn nodeColumn in (List<NodeColumn>) source)
    {
      if (!destination.ColumnIDExists(nodeColumn.ID))
        destination.Add(nodeColumn);
    }
  }

  /// <summary>Get supported columns for object type</summary>
  /// <param name="objTypeId"></param>
  public static NodeColumnCollection GetObjectSupportedColumns(int objTypeId)
  {
    NodeColumnCollection destination = new NodeColumnCollection();
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    NodeColumn column = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false).CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION");
    columnCollection.Add(column, 250);
    if (objTypeId != -1)
      Helper.AddObjectTypeColumns(columnCollection, objTypeId);
    Helper.AddObligatoryColumns(columnCollection, true, true);
    Helper.AddObligatoryColumnsAdv(columnCollection);
    Helper.AddAllColumns(columnCollection);
    TechCardNavTreeViewUtils.MergeColumnCollections(columnCollection, destination);
    return destination;
  }

  /// <summary>Get supported columns for object and relation types</summary>
  /// <param name="objTypeId"></param>
  /// <param name="relTypeId"></param>
  /// <returns></returns>
  public static NodeColumnCollection GetObjAndRelSupportedColumns(int objTypeId, int relTypeId)
  {
    NodeColumnCollection destination = new NodeColumnCollection();
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    NodeColumn column = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false).CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION");
    columnCollection.Add(column, 250);
    if (objTypeId != -1 && objTypeId != 0)
      Helper.AddObjectTypeColumns(columnCollection, objTypeId);
    if (relTypeId != -1)
      Helper.AddRelationTypeColumns(columnCollection, relTypeId);
    Helper.AddObligatoryColumns(columnCollection, true, true);
    Helper.AddObligatoryColumnsAdv(columnCollection);
    Helper.AddObligatoryColumnsRelation(columnCollection);
    Helper.AddObligatoryColumnsRelationAdv(columnCollection);
    Helper.AddAllColumns(columnCollection);
    Helper.AddAllColumnsRelation(columnCollection);
    TechCardNavTreeViewUtils.MergeColumnCollections(columnCollection, destination);
    return destination;
  }

  /// <summary>Get object columns only</summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public static NodeColumnCollection GetObjectColumnsOnly(object sender)
  {
    int objTypeId = -1;
    if (sender is NavigatorTreeView navigatorTreeView)
    {
      if (navigatorTreeView.RootDescriptor is TechObjectListDescriptor rootDescriptor1)
        return TechCardNavTreeViewUtils.GetObjectSupportedColumns(rootDescriptor1.TypeID);
      if (navigatorTreeView.RootDescriptor is TechCompositionBaseDescriptor rootDescriptor2)
        return TechCardNavTreeViewUtils.GetObjAndRelSupportedColumns(rootDescriptor2.CompObjTypeID, rootDescriptor2.CompRelTypeIDs == null || rootDescriptor2.CompRelTypeIDs.Count<int>() != 1 ? -1 : rootDescriptor2.CompRelTypeIDs.First<int>());
    }
    return TechCardNavTreeViewUtils.GetObjectSupportedColumns(objTypeId);
  }

  /// <summary>Загрузка параметров TechCardNavTreeViewControl</summary>
  /// <param name="config"></param>
  /// <param name="treeView"></param>
  public static void LoadSettings(IConfiguration config, NavigatorTreeView treeView)
  {
    if (config == null || !config.HasProperty(treeView.Name + "_CollumnsLayout"))
      return;
    string property = config.GetProperty(treeView.Name + "_CollumnsLayout");
    TechCardNavTreeViewUtils.SetCollumnsState(treeView, property);
  }

  /// <summary>Сохранение параметров TechCardNavTreeViewControl</summary>
  /// <param name="config"></param>
  /// <param name="treeView"></param>
  public static void SaveSettings(IConfiguration config, NavigatorTreeView treeView)
  {
    if (config == null)
      return;
    string collumnsState = TechCardNavTreeViewUtils.GetCollumnsState(treeView);
    config.SetProperty(treeView.Name + "_CollumnsLayout", collumnsState);
  }

  /// <summary>
  /// Получить строку с состоянием колонок (размеров, порядка следования и т.п.).
  /// </summary>
  /// <param name="treeView"></param>
  /// <returns></returns>
  public static string GetCollumnsState(NavigatorTreeView treeView)
  {
    NodeColumnCollection columnCollection = treeView?.ReflectTreeColumsChanges();
    if (columnCollection != null)
    {
      if (columnCollection.Count != 0)
      {
        try
        {
          XmlDocument xmlDocument = new XmlDocument();
          XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Settings");
          xmlDocument.AppendChild(element1);
          XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Columns");
          columnCollection.SaveData(element2);
          element1.AppendChild(element2);
          using (TextWriter w1 = (TextWriter) new StringWriter())
          {
            XmlWriter w2 = (XmlWriter) new XmlTextWriter(w1);
            w2.WriteStartDocument();
            xmlDocument.WriteTo(w2);
            w2.WriteEndDocument();
            w2.Flush();
            w2.Close();
            return w1.ToString();
          }
        }
        catch
        {
          return string.Empty;
        }
      }
    }
    return string.Empty;
  }

  /// <summary>
  /// Восстановить состояние колонок (размеров, порядка следования и т.п.).
  /// </summary>
  /// <param name="treeView"></param>
  /// <param name="columnsState"></param>
  /// <returns></returns>
  public static bool SetCollumnsState(NavigatorTreeView treeView, string columnsState)
  {
    if (treeView == null || columnsState == string.Empty)
      return false;
    XmlDocument xmlDocument = new XmlDocument();
    try
    {
      xmlDocument.LoadXml(columnsState);
    }
    catch (XmlException ex)
    {
      return false;
    }
    XmlNode xmlNode1 = xmlDocument.SelectSingleNode("Settings");
    if (xmlNode1 != null)
    {
      XmlNode xmlNode2 = xmlNode1.SelectSingleNode("Columns");
      NodeColumnCollection nodeColumnCollection = (NodeColumnCollection) null;
      if (xmlNode2 != null)
      {
        nodeColumnCollection = new NodeColumnCollection();
        nodeColumnCollection.LoadData(xmlNode2);
      }
      if (nodeColumnCollection != null && nodeColumnCollection.Count > 0)
      {
        treeView.SetColumns(nodeColumnCollection, false);
        return true;
      }
    }
    return false;
  }
}
