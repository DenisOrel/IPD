// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentExternalEditor
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Client.UI;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

public class DocumentExternalEditor : IExternalEditor
{
  internal static IAttributePropertyDescriberService attributePropertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;

  public virtual bool CanEditDBObjectAttribute(
    ReferenceToDBObjectAttribute reference,
    INodeWithReference node)
  {
    return reference.CanCallEditor || DocumentExternalEditor.GetEditor(reference) != null || reference.GetEditorControl(UITypeEditorEditStyle.Modal) != null;
  }

  public virtual bool EditDBObjectAttribute(
    ReferenceToDBObjectAttribute reference,
    INodeWithReference node)
  {
    bool flag = false;
    if (reference.IsEmptyObjectRef)
      reference.GetParentDBObjectInfo();
    if (reference.IsEmptyObjectRef)
      return false;
    if (!reference.IsConnected)
      reference.UpdateDBObjectInfo();
    if (!reference.IsConnected)
      return false;
    UITypeEditor editor = DocumentExternalEditor.GetEditor(reference);
    if (editor != null)
    {
      object text = (object) reference.Text;
      object obj = editor.EditValue((System.IServiceProvider) null, text);
      if (obj is string)
        reference.SetDBAttributeValue((string) obj, true);
      flag = true;
    }
    else
    {
      IAttributeEditorControl editorControl = reference.GetEditorControl(UITypeEditorEditStyle.Modal);
      if (editorControl != null && editorControl is Form form && form.ShowDialog() == DialogResult.OK)
        reference.SaveChangesToDB();
    }
    return flag;
  }

  public virtual bool CallEditor(DocumentTreeNode[] nodes)
  {
    if (nodes.Length != 0 && nodes[0] is INodeWithReference node && node.Reference != null)
    {
      ReferenceBase reference1 = node.Reference;
      ReferenceToDBObjectAttribute reference2 = node.Reference as ReferenceToDBObjectAttribute;
      bool flag = reference1.CanCallEditor;
      if (flag && reference2 != null)
      {
        List<UITypeEditorEditStyle> editorStyles = reference2.GetEditorStyles();
        if (editorStyles == null || !editorStyles.Contains(UITypeEditorEditStyle.Modal))
          flag = false;
      }
      if (flag)
        return reference1.CallEditor();
      if (reference2 != null)
        return this.EditDBObjectAttribute(reference2, node);
      if (node.Reference is ReferenceToNodeAttribute reference3 && reference3.AttributeName == DocumentTreeNode.AttributeName_PageNumberMore1)
      {
        string attributeValue = reference3.GetAttributeValue();
        int result;
        if (attributeValue != string.Empty && int.TryParse(attributeValue, out result))
        {
          EditFirstPageNumberForm firstPageNumberForm = new EditFirstPageNumberForm(result);
          if (firstPageNumberForm.ShowDialog() == DialogResult.OK)
            reference3.SetAttributeValue(firstPageNumberForm.PageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture), true, true, true);
        }
        return true;
      }
    }
    return false;
  }

  public static IAttributePropertyDescriber GetDescriber(int AttrId)
  {
    return DocumentExternalEditor.attributePropertyDescriberService.GetDescriber(AttrId);
  }

  private static PropDescriptor GetPropDescriptorByID(
    PropertyDescriptorCollection collection,
    int aPropID)
  {
    for (int index = 0; index < collection.Count; ++index)
    {
      if (((PropDescriptor) collection[index]).PropID == aPropID)
        return (PropDescriptor) collection[index];
    }
    return (PropDescriptor) null;
  }

  public static UITypeEditor GetEditor(ReferenceToDBObjectAttribute reference)
  {
    IAttributePropertyDescriber describer = DocumentExternalEditor.GetDescriber(reference.AttributeID);
    UITypeEditor editor = (UITypeEditor) null;
    if (describer != null)
    {
      editor = describer.GetPropDescriptorEditor(reference.AttributeID) as UITypeEditor;
    }
    else
    {
      AttributableElements aElement = AttributableElements.Object;
      if (!reference.IsConnectedObjectRef)
        reference.UpdateDBObjectInfo();
      if (!reference.IsConnectedAttributeRef)
        reference.UpdateAttributeInfo();
      if (!reference.IsRelationAttribute && reference.DBObjectID != -1L)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(reference.DBObjectType, reference.AttributeID);
        if (attribute4ObjectType != null)
          editor = attribute4ObjectType.PossibleValues == null || attribute4ObjectType.PossibleValues.Count <= 0 ? (UITypeEditor) new HistoryEditor(reference.DBObjectID, aElement, reference.AttributeID) : (UITypeEditor) null;
      }
    }
    return editor;
  }

  public virtual bool CanCallEditor(DocumentTreeNode[] nodes)
  {
    if (nodes.Length != 0)
    {
      if (nodes[0] is TextData node1)
      {
        if (node1.ReadOnly)
          return false;
        if (node1.ReferenceToTextSource != null && node1.ReferenceToTextSource.CanCallEditor)
          return (!(node1.ReferenceToTextSource is ReferenceToDBObjectBase) || !(node1.ReferenceToTextSource as ReferenceToDBObjectBase).PassiveLink) && (!(node1.ReferenceToTextSource is ReferenceToDBObjectAttribute) || DocumentExternalEditor.GetEditor(node1.ReferenceToTextSource as ReferenceToDBObjectAttribute) != null || (node1.ReferenceToTextSource as ReferenceToDBObjectAttribute).GetEditorControl(UITypeEditorEditStyle.Modal) != null);
      }
      if (nodes[0] is INodeWithReference node2 && (node2.Reference is ReferenceToDBObjectAttribute reference1 && this.CanEditDBObjectAttribute(reference1, node2) || node2.Reference is ReferenceToNodeAttribute reference2 && reference2.AttributeName == DocumentTreeNode.AttributeName_PageNumberMore1 && reference2.GetAttributeValue() != string.Empty))
        return true;
    }
    return false;
  }
}
