// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.TemplateHolder
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Document.Model.ImportBlanks;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Document.Client;

public class TemplateHolder : TemplateHolderBase
{
  public override void LoadTemplates()
  {
    this.groups.Clear();
    this.templates.Clear();
    DocumentSection documentSection = (DocumentSection) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(new Guid(TemplateHolderBase.guidSpecTemplates));
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      });
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        documentSection = (DocumentSection) null;
        DocumentSection parent = (DocumentSection) null;
        long int64 = Convert.ToInt64(row[0]);
        try
        {
          ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(int64);
          if (imDocument != null)
          {
            if (imDocument.Nodes != null)
            {
              DocumentSection root = new DocumentSection();
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
              root.Name = objectInfo.Caption;
              parent = root;
              int index = 0;
              while (index < imDocument.NodesCount)
              {
                if (!(imDocument.Nodes[index] is DocumentSection node1) || !this.ReadDocumentSection((DocumentSection) null, node1))
                {
                  if (imDocument.Nodes[index] is Page node)
                  {
                    string attributeValue = node.GetAttributeValue(PrimitiveBase.AttributeGroupName, false);
                    if (attributeValue != null && root.Name != attributeValue)
                    {
                      root = new DocumentSection((DocumentTreeNode) parent);
                      root.Name = attributeValue;
                    }
                    if (this.ReadPage(root, node))
                      continue;
                  }
                  ++index;
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          LogManager.AddLine("TemplateHolder.LoadTemplates: " + ex.Message, true);
          continue;
        }
        if (parent != null && parent.NodesCount > 0)
          this.groups.Add(parent);
      }
    }
  }

  internal bool ReadDocumentSection(DocumentSection root, DocumentSection sect)
  {
    if (sect.Nodes == null || sect.NodesCount == 0)
      return false;
    DocumentSection documentSection = new DocumentSection();
    documentSection.Name = sect.Name;
    documentSection.Id = sect.Id;
    if (root == null)
      this.groups.Add(documentSection);
    else
      root.AddChildNode((DocumentTreeNode) documentSection, false, false);
    int index = 0;
    while (index < sect.NodesCount)
    {
      if ((!(sect.Nodes[index] is DocumentSection node1) || !this.ReadDocumentSection(documentSection, node1)) && (!(sect.Nodes[index] is Page node2) || !this.ReadPage(documentSection, node2)))
        ++index;
    }
    return true;
  }

  internal bool ReadPage(DocumentSection root, Page p)
  {
    if (p.Name == "")
      return false;
    if (this.templates.ContainsKey((object) p.Id.ToUpper()))
    {
      FormSearch template = this.templates[(object) p.Id.ToUpper()] as FormSearch;
      if (template.owner != root)
      {
        root.AddChildNode((DocumentTreeNode) p, false, false);
        template.owner = root;
        template.node = (DocumentTreeNode) p;
      }
      else
      {
        root.AddChildNode((DocumentTreeNode) p, false, false);
        template.node = (DocumentTreeNode) p;
      }
    }
    else
    {
      FormSearch formSearch = new FormSearch(p.Id.ToUpper(), (DocumentTreeNode) p, root);
      root.AddChildNode((DocumentTreeNode) p, false, false);
      this.templates.Add((object) p.Id.ToUpper(), (object) formSearch);
    }
    return true;
  }

  public override void ReloadTemplates()
  {
    this.docTemplates.Clear();
    this.LoadTemplates();
  }

  public override void SetTemplatesForDoc(ImDocument doc)
  {
    if (doc.FormulaList == null)
      return;
    foreach (Page node in doc.FormulaList.Nodes)
    {
      if (!(node.Id == ""))
      {
        string upper = node.Id.ToUpper();
        if (this.templates.ContainsKey((object) upper))
        {
          FormSearch template = this.templates[(object) upper] as FormSearch;
          if (template.owner != null)
          {
            template.owner.RemoveChildNode(template.node, false, false);
            this.docTemplates.Add(node);
            template.owner = (DocumentSection) null;
            template.node = (DocumentTreeNode) node;
          }
          else
          {
            this.docTemplates.Remove(template.node as Page);
            this.docTemplates.Add(node);
            template.node = (DocumentTreeNode) node;
          }
        }
        else
        {
          FormSearch formSearch = new FormSearch(upper, (DocumentTreeNode) node, (DocumentSection) null);
          this.docTemplates.Add(node);
          this.templates.Add((object) upper, (object) formSearch);
        }
      }
    }
  }
}
