// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ColumnCollectionEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для свойства "Отображаемые атрибуты".</summary>
public class ColumnCollectionEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    int objTypeID = -1;
    int relTypeID = -1;
    IObjectsListSupport objectsListSupport = (IObjectsListSupport) null;
    if (context != null && context.Instance != null && context.Instance is IWrapper instance)
      objectsListSupport = instance.BaseClass as IObjectsListSupport;
    if (objectsListSupport != null)
    {
      objTypeID = objectsListSupport.ObjectsTypeID;
      relTypeID = objectsListSupport.RelationsTypeID;
    }
    INode node = objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(objTypeID, AccessRights.Enabled);
    NodeColumnCollection supportedColumns = node.GetSupportedColumns(ContentType.NonFolders, string.Empty);
    if (!(value is NodeColumnCollection columns))
      columns = node.GetDefaultColumns(ContentType.NonFolders);
    if (relTypeID != -1)
    {
      Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsRelationAdv(supportedColumns);
      IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        foreach (IMSAttributeType imsAttributeType in Intermech.Navigator.DBObjects.Helper.GetAttributesForRelationType(relTypeID))
        {
          if (!columns.ColumnIDExists((object) imsAttributeType.AttributeID, Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid))
          {
            NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid, (object) imsAttributeType.AttributeID);
            if (!supportedColumns.Contains(column))
              supportedColumns.Add(column);
          }
        }
      }
    }
    if (AppearanceTuningForm.Execute(node, ContentType.NonFolders, supportedColumns, columns) == DialogResult.OK)
      value = (object) new NodeColumnCollection((IEnumerable<NodeColumn>) columns.ToArray());
    return columns.Count <= 0 ? (object) null : value;
  }
}
