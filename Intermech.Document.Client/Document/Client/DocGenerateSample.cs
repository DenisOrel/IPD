// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocGenerateSample
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Пример генерации документа</summary>
public class DocGenerateSample
{
  /// <summary>Создать и открыть новый тестовый документ</summary>
  public static void GenerateAndOpenNewTestDocument()
  {
    ImDocument imDocument = DocGenerateSample.GenerateImDocument(new Guid("cad0144e-306c-11d8-b4e9-00304f19f545"), "Таблица Ведомость покупных", "193", "246");
    imDocument.Designation = "DOC.TST." + (object) Guid.NewGuid();
    imDocument.DocumentName = "Тестовый документ";
    DocumentEditorPlugin.Instance.OpenImDocument(imDocument, callDialogWithObjectParamsBeforeSave: false, defaultDocumentDbObjectType: MetaDataHelper.GetObjectTypeID("cad00293-306c-11d8-b4e9-00304f19f545"));
  }

  /// <summary>Создать новый тестовый документ в БД и сохранить его</summary>
  public static void GenerateAndSaveNewTestDocument()
  {
    ImDocument imDocument = DocGenerateSample.GenerateImDocument(new Guid("cad0144e-306c-11d8-b4e9-00304f19f545"), "Таблица Ведомость покупных", "193", "246");
    imDocument.Designation = "DOC.TST." + (object) Guid.NewGuid();
    imDocument.DocumentName = "Тестовый документ";
    DocumentEditorPlugin.SaveDocumentInNewDBObject(imDocument, MetaDataHelper.GetObjectTypeID("cad00293-306c-11d8-b4e9-00304f19f545"));
    imDocument.Modified = false;
    DocumentEditorPlugin.Instance.OpenImDocument(imDocument, callDialogWithObjectParamsBeforeSave: false, defaultDocumentDbObjectType: MetaDataHelper.GetObjectTypeID("cad00293-306c-11d8-b4e9-00304f19f545"));
  }

  /// <summary>Сгенерировать документ</summary>
  /// <param name="documentTemplateGuid">Идентификатор шаблона в БД</param>
  /// <param name="mainTableID">Идентификатор шаблона главной таблицы в документе</param>
  /// <param name="mainTableRowID">Идентификатор шаблона записи в главной таблице</param>
  /// <param name="internalRowID">Идентификатор шаблона подстроки в записи</param>
  /// <returns></returns>
  public static ImDocument GenerateImDocument(
    Guid documentTemplateGuid,
    string mainTableID,
    string mainTableRowID,
    string internalRowID = null)
  {
    ImDocument documentFromTemplate = DocumentEditorPlugin.CreateDocumentFromTemplate(documentTemplateGuid);
    TableData tableData = (TableData) null;
    if (!string.IsNullOrWhiteSpace(internalRowID))
      tableData = documentFromTemplate.Template.FindNode(internalRowID) as TableData;
    TableData node1 = documentFromTemplate.FindNode(mainTableID) as TableData;
    for (int index1 = 1; index1 < 40; ++index1)
    {
      TableData elementFromTemplate1 = documentFromTemplate.CreateDocumentElementFromTemplate(mainTableRowID) as TableData;
      if (tableData != null)
      {
        TableData templateRecursive = elementFromTemplate1.FindFirstNodeFromTemplate_Recursive(tableData.Parent.Id) as TableData;
        for (int index2 = 0; index2 < 2; ++index2)
        {
          TableData elementFromTemplate2 = documentFromTemplate.CreateDocumentElementFromTemplate(internalRowID) as TableData;
          foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(elementFromTemplate2))
            textData.AssignText($"{"in"}: {index1}.{index2}", false, false, false);
          templateRecursive?.AddChildNode((DocumentTreeNode) elementFromTemplate2, false, false);
        }
      }
      for (int index3 = 0; index3 < elementFromTemplate1.Nodes.Count; ++index3)
      {
        if (index3 != 0 || !(mainTableID == "Таблица Ведомость покупных"))
        {
          if (elementFromTemplate1.Nodes[index3] is TextData node4)
          {
            string str = $"{node4.Name}: {index1}";
            node4.AssignText(str, false, false, false);
          }
          else if (elementFromTemplate1.Nodes[index3] is TableData node3)
          {
            for (int index4 = 0; index4 < node3.Nodes.Count; ++index4)
            {
              if (node3.Nodes[index4] is TableData node2)
              {
                foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(node2))
                  textData.AssignText(textData.Text + "*", false, false, false);
              }
            }
          }
        }
      }
      node1.AddChildNode((DocumentTreeNode) elementFromTemplate1, false, false);
    }
    documentFromTemplate.UpdateLayout(false, false);
    return documentFromTemplate;
  }
}
