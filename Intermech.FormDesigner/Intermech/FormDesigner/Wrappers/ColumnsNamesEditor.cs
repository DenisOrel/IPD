// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ColumnsNamesEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Едитор для свойства "Выборка".</summary>
public class ColumnsNamesEditor : UITypeEditor
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
    IObjectsListSupport objectsListSupport = (IObjectsListSupport) null;
    if (context != null && context.Instance != null && context.Instance is IWrapper instance)
      objectsListSupport = instance.BaseClass as IObjectsListSupport;
    if (objectsListSupport != null)
    {
      using (ColumnsNamesEditorForm columnsNamesEditorForm = new ColumnsNamesEditorForm())
      {
        columnsNamesEditorForm.ColumnsAliases = objectsListSupport.ColumnsAliases;
        NodeColumnCollection columnCollection = objectsListSupport.ColumnCollection;
        if (columnCollection == null)
        {
          int objectsTypeId = objectsListSupport.ObjectsTypeID;
          columnCollection = (objectsTypeId == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(objectsTypeId, AccessRights.Enabled)).GetDefaultColumns(ContentType.NonFolders);
        }
        columnsNamesEditorForm.Columns = columnCollection;
        if (columnsNamesEditorForm.ShowDialog() == DialogResult.OK)
          value = columnsNamesEditorForm.ColumnsAliases.Count > 0 ? (object) columnsNamesEditorForm.ColumnsAliases : (object) (Dictionary<Guid, string>) null;
      }
    }
    return value;
  }
}
