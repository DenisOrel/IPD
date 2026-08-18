// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSExternalEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Client;
using Intermech.Document.Model.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

internal class AVSExternalEditor : DocumentExternalEditor
{
  private AVSWindow avsWindow;

  public AVSExternalEditor(AVSWindow window) => this.avsWindow = window;

  public override bool CallEditor(DocumentTreeNode[] nodes)
  {
    if (this.CallLiteraEditor(nodes))
      return true;
    bool flag = base.CallEditor(nodes);
    if (!flag)
      return flag;
    if (nodes.Length != 0 && nodes[0] is INodeWithReference node && this.avsWindow.AVSDocument.IsProductKodOrLitera(nodes[0]) && node.Reference is ReferenceToDBObjectAttribute reference)
      reference.SaveChangesToDB();
    return false;
  }

  public override bool CanEditDBObjectAttribute(
    ReferenceToDBObjectAttribute reference,
    INodeWithReference node)
  {
    return reference.AttributeID != AvsIDCache.Attr_Litera && base.CanEditDBObjectAttribute(reference, node);
  }

  public override bool EditDBObjectAttribute(
    ReferenceToDBObjectAttribute reference,
    INodeWithReference node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    bool flag = false;
    TextData textData = node as TextData;
    if (reference.AttributeID == AvsIDCache.Attr_ProductConventionalName && textData != null)
    {
      reference.UpdateAttributeInfo();
      if (reference.IsConnectedObjectRef)
      {
        flag = true;
        string attributeName = reference.AttributeName;
        EditTextDlg editTextDlg = new EditTextDlg(reference.Text, attributeName);
        if (editTextDlg.ShowDialog() == DialogResult.OK)
        {
          reference.SetDBAttributeValue(editTextDlg.AttributeText, true);
          textData.UpdateLayout(true);
        }
      }
    }
    UITypeEditor editor = DocumentExternalEditor.GetEditor(reference);
    if (!flag && editor != null)
    {
      AvsRowAttributeInfo attrInfo = (AvsRowAttributeInfo) null;
      AVSRow avsDocRow = this.avsWindow.AVSDocument.GetAvsDocRow(node as DocumentTreeNode);
      if (avsDocRow != null)
      {
        foreach (AvsRowAttributeInfo docRowField in avsDocRow.DocRowFields)
        {
          if (docRowField.AttributeId == reference.AttributeID)
          {
            attrInfo = docRowField;
            break;
          }
        }
        object text = (object) reference.Text;
        if (attrInfo != null)
        {
          flag = true;
          object fieldValue = avsDocRow.GetFieldValue(attrInfo, -1, -1, true, false);
          object obj = editor.EditValue((System.IServiceProvider) null, fieldValue);
          avsDocRow.SetFieldValue(attrInfo, -1, -1, obj, true, false, true, true, false, false);
        }
      }
    }
    if (!flag)
      flag = base.EditDBObjectAttribute(reference, node);
    return flag;
  }

  public override bool CanCallEditor(DocumentTreeNode[] nodes)
  {
    if (this.avsWindow.ReadOnly)
      return false;
    return this.CanCallLiteraEditor(nodes) || base.CanCallEditor(nodes);
  }

  private bool CallLiteraEditor(DocumentTreeNode[] nodes)
  {
    if (nodes == null || nodes.Length == 0)
      return false;
    AVSDocument avsDocument = (AVSDocument) null;
    if (AVSPlugin.Instance.ActiveAVSWindow != null)
      avsDocument = AVSPlugin.Instance.ActiveAVSWindow.AVSDocument;
    if (avsDocument == null || !(nodes[0] is TextData node1))
      return false;
    TextData textData = avsDocument.GetLiteraCellFromTitleBlock();
    if (avsDocument.IsSpecification)
    {
      TableData parentNode = (TableData) null;
      int num = -1;
      long aId = -1;
      if ((avsDocument.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V) && node1 != textData)
      {
        PageData page = node1.Page;
        if (avsDocument.productKodAndLiteraTemplate != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLiteraTemplate) as TableData;
        if (parentNode == null && avsDocument.productKodAndLitera2Template != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLitera2Template) as TableData;
        if (parentNode != null && node1.IsChildForNode((DocumentTreeNode) parentNode, false))
        {
          if (!(node1.Parent.Nodes[0] is TextData node2))
            return false;
          if (node2.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(referenceToTextSource.AttributeGuid);
            if (attributeType != null)
              referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, attributeType.AttributeID, referenceToTextSource.AttributeName);
          }
          if (referenceToTextSource == null || referenceToTextSource.AttributeID == -1)
            return false;
          num = referenceToTextSource.AttributeID;
          int index = avsDocument.GetFirstProductIndex(page) + node1.Index;
          if (index >= avsDocument.productsInfo.Count)
            return false;
          aId = avsDocument.productsInfo[index].Id;
          textData = node1;
        }
      }
      else if (textData != null && textData == node1)
      {
        aId = avsDocument.productsInfo[0].Id;
        num = AvsIDCache.Attr_Litera;
      }
      if (!this.avsWindow.AVSDocument.IsSpecification)
        aId = this.avsWindow.AVSDocument.DocumentID;
      if (aId != -1L && num != -1)
      {
        AttributeProcessor attributeProcessor = new AttributeProcessor(0L, AttributableElements.Object);
        attributeProcessor.Load(aId, AttributableElements.Object, GetAttributeValuesModes.None, false);
        if (attributeProcessor.FindAttributeValues(num) == null)
          attributeProcessor.ActualAttributeValues.Add(new AttributeValues(num, (object) DBNull.Value));
        IAttributeEditorControl editorControl = attributeProcessor.GetEditorControl(num, new int?(0), UITypeEditorEditStyle.Modal, true);
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(avsDocument.ProductType, num);
        if (!this.avsWindow.AVSDocument.IsSpecification)
          attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(avsDocument.DocumentDBObjectType, num);
        if (editorControl is Form form && attribute4ObjectType != null)
        {
          if (form.ShowDialog() == DialogResult.OK && num == AvsIDCache.Attr_Litera)
          {
            if (avsDocument.GetLiteraCellFromTitleBlock() == textData)
            {
              object initValue = (object) null;
              if (attributeProcessor.FindAttributeValues(num).Values.Length != 0)
                initValue = attributeProcessor.FindAttributeValues(num).Values[0];
              List<long> objectIDs = new List<long>();
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                foreach (ProductInfo productInfo in avsDocument.productsInfo)
                {
                  long id = productInfo.Id;
                  productInfo.Litera = Convert.ToString(initValue);
                  if (id != -1L && !objectIDs.Contains(id))
                  {
                    IDBObject dbObject = sessionKeeper.Session.GetObject(id, false);
                    if (dbObject != null)
                    {
                      AttributeValues[] valuesList = new AttributeValues[1]
                      {
                        new AttributeValues(num, initValue)
                      };
                      dbObject.SetAttributesValues(valuesList);
                      objectIDs.Add(id);
                    }
                  }
                }
                IDBObject dbObject1 = sessionKeeper.Session.GetObject(avsDocument.DocumentID, false);
                if (dbObject1 != null)
                {
                  AttributeValues[] valuesList = new AttributeValues[1]
                  {
                    new AttributeValues(num, initValue)
                  };
                  dbObject1.SetAttributesValues(valuesList);
                  objectIDs.Add(avsDocument.DocumentID);
                }
              }
              AVSPlugin.NotificationService.FireEvent((object) avsDocument.AVSWindow, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
            }
            else
              attributeProcessor.Save();
          }
          return true;
        }
      }
    }
    return false;
  }

  private bool CanCallLiteraEditor(DocumentTreeNode[] nodes)
  {
    if (nodes == null || nodes.Length == 0)
      return false;
    AVSDocument avsDocument = (AVSDocument) null;
    if (AVSPlugin.Instance.ActiveAVSWindow != null)
      avsDocument = AVSPlugin.Instance.ActiveAVSWindow.AVSDocument;
    if (avsDocument == null || !(nodes[0] is TextData node1))
      return false;
    TextData cellFromTitleBlock = avsDocument.GetLiteraCellFromTitleBlock();
    if (avsDocument.IsSpecification)
    {
      TableData parentNode = (TableData) null;
      int num = -1;
      if ((avsDocument.IsFormB || avsDocument.AvsDocumentForm == AVSDocumentForm.V) && node1 != cellFromTitleBlock)
      {
        PageData page = node1.Page;
        if (avsDocument.productKodAndLiteraTemplate != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLiteraTemplate) as TableData;
        if (parentNode == null && avsDocument.productKodAndLitera2Template != null)
          parentNode = page.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) avsDocument.productKodAndLitera2Template) as TableData;
        if (parentNode != null && node1.IsChildForNode((DocumentTreeNode) parentNode, false))
        {
          if (!(node1.Parent.Nodes[0] is TextData node2))
            return false;
          if (node2.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(referenceToTextSource.AttributeGuid);
            if (attributeType != null)
              referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, attributeType.AttributeID, referenceToTextSource.AttributeName);
          }
          if (referenceToTextSource == null || referenceToTextSource.AttributeID == -1)
            return false;
          num = referenceToTextSource.AttributeID;
          if (avsDocument.GetFirstProductIndex(page) + node1.Index >= avsDocument.productsInfo.Count)
            return false;
        }
      }
      else if (cellFromTitleBlock != null && cellFromTitleBlock == node1)
        num = AvsIDCache.Attr_Litera;
      if (num != -1)
        return true;
    }
    return false;
  }
}
