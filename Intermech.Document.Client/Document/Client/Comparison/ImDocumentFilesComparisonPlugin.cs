// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Comparison.ImDocumentFilesComparisonPlugin
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client.UI;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Comparison;

internal class ImDocumentFilesComparisonPlugin : ICanCompareObjectsFiles
{
  private readonly List<int> typeIdsList;

  public CompareFilesService CompareService { get; set; }

  /// <summary>
  /// <inheritdoc cref="P:Intermech.Client.Core.ICanCompareObjectsFiles.UniqueName" />
  /// </summary>
  public string UniqueName => nameof (ImDocumentFilesComparisonPlugin);

  /// <summary>
  /// <inheritdoc cref="P:Intermech.Client.Core.ICanCompareObjectsFiles.NameInMessages" />
  /// </summary>
  public string NameInMessages => "Плагин для сравнения документов внутреннего формата IPS";

  /// <summary>
  /// <inheritdoc cref="P:Intermech.Client.Core.ICanCompareObjectsFiles.TypeIds" />
  /// </summary>
  public ReadOnlyCollection<int> TypeIds { get; }

  public ImDocumentFilesComparisonPlugin()
  {
    this.typeIdsList = new List<int>();
    this.TypeIds = new ReadOnlyCollection<int>((IList<int>) this.typeIdsList);
  }

  public void SetTypeIds(List<int> typeIds)
  {
    if (typeIds == null)
      throw new ArgumentNullException(nameof (typeIds));
    this.typeIdsList.Clear();
    this.typeIdsList.AddRange((IEnumerable<int>) typeIds);
  }

  /// <summary>
  /// <inheritdoc cref="M:Intermech.Client.Core.ICanCompareObjectsFiles.CompareFilesFor(Intermech.DataFormats.DBObjectToCompare,Intermech.DataFormats.DBObjectToCompare,Intermech.FileTypes)" />
  /// </summary>
  public void CompareFilesFor(
    DBObjectToCompare object1,
    DBObjectToCompare object2,
    FileTypes fileType)
  {
    if (object1 == null)
      throw new ArgumentNullException(nameof (object1));
    if (object2 == null)
      throw new ArgumentNullException(nameof (object2));
    if (object1.ObjectTypeID != object2.ObjectTypeID || !this.TypeIds.Contains(object1.ObjectTypeID) || this.CompareService == null)
      return;
    if (fileType == FileTypes.ftAuthentical)
    {
      this.CompareService.CompareFilesWithCommonRules(object1, object2, fileType);
    }
    else
    {
      ImDocument imDocument1 = (ImDocument) null;
      ImDocument imDocument2 = (ImDocument) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        imDocument1 = DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(object1.ObjectID, true), -1, Guid.Empty, false, false, true, false) as ImDocument;
        imDocument2 = DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObject(object2.ObjectID, true), -1, Guid.Empty, false, false, true, false) as ImDocument;
      }
      ImDocument documentTemplate1 = imDocument1?.DocumentTemplate as ImDocument;
      ImDocument documentTemplate2 = imDocument2?.DocumentTemplate as ImDocument;
      ObjectFileInfo fileData1 = this.CompareService.GetFileData(object1, FileTypes.ftNormal);
      if (fileData1 == null || fileData1.FileIndex < 0)
        return;
      ObjectFileInfo fileData2 = this.CompareService.GetFileData(object2, FileTypes.ftNormal);
      if (fileData2 == null || fileData2.FileIndex < 0)
        return;
      if ((documentTemplate1?.Id ?? "") == (documentTemplate2?.Id ?? "") || ImDocument.AreCompatibleTemplates(documentTemplate1, documentTemplate2, out string _, true))
        this.OpenDocumentComparisonWindow(fileData1, fileData2, imDocument1, imDocument2, ImDocumentFilesComparisonPlugin.CompareDocuments(imDocument1, imDocument2));
      else
        CompareFilesService.ShowCommonCompareForm(fileData1, fileData2);
    }
  }

  private static ImDocumentComparisonResult CompareDocuments(ImDocument doc1, ImDocument doc2)
  {
    if (doc1 == null)
      throw new ArgumentException(nameof (doc1));
    if (doc2 == null)
      throw new ArgumentException(nameof (doc2));
    List<DocumentTreeNode> doc1Nodes = doc1.EnumerateDocumentTreeNodes().Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (e => !string.IsNullOrWhiteSpace(e.Id))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> doc2Nodes = doc2.EnumerateDocumentTreeNodes().Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (e => !string.IsNullOrWhiteSpace(e.Id))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> list1 = doc1Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => !doc2Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn => fn.Id == sn.Id && fn.NodeClass == sn.NodeClass && (fn.Parent?.Id ?? "") == (sn.Parent?.Id ?? ""))))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> list2 = doc2Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn => !doc1Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => sn.Id == fn.Id && sn.NodeClass == fn.NodeClass && (sn.Parent?.Id ?? "") == (fn.Parent?.Id ?? ""))))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> list3 = doc1Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => doc2Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn =>
    {
      if (!(fn.Id == sn.Id) || !(fn.NodeClass == sn.NodeClass) || !((fn.Parent?.Id ?? "") == (sn.Parent?.Id ?? "")) || !(fn is TextData textData3) || !(sn is TextData textData4))
        return false;
      return textData3.Text != textData4.Text || textData3.Size != textData4.Size;
    })))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> missingDoc2Nodes = list1;
    List<DocumentTreeNode> diffNodes = list3;
    return new ImDocumentComparisonResult((IEnumerable<DocumentTreeNode>) list2, (IEnumerable<DocumentTreeNode>) missingDoc2Nodes, (IEnumerable<DocumentTreeNode>) diffNodes);
  }

  private static void ShowDocumentComparisonForm(
    ObjectFileInfo fileData1,
    ObjectFileInfo fileData2,
    ImDocument docOne,
    ImDocument docTwo,
    ImDocumentComparisonResult result)
  {
    using (ImDocumentComparisonForm documentComparisonForm = new ImDocumentComparisonForm())
    {
      documentComparisonForm.Init(fileData1, fileData2);
      documentComparisonForm.DocumentOne = docOne;
      documentComparisonForm.DocumentTwo = docTwo;
      documentComparisonForm.ComparisonTreeDataSource = result.DifferenceTreeModel;
      documentComparisonForm.WindowState = FormWindowState.Maximized;
      int num = (int) documentComparisonForm.ShowDialog();
    }
  }

  /// <summary>
  /// Открыть результат сравнения в интегрированном окне IPS
  /// </summary>
  private void OpenDocumentComparisonWindow(
    ObjectFileInfo fileData1,
    ObjectFileInfo fileData2,
    ImDocument docOne,
    ImDocument docTwo,
    ImDocumentComparisonResult result)
  {
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    if (service == null)
      return;
    ImDocumentComparisonWindow comparisonWindow = new ImDocumentComparisonWindow(fileData1, fileData2);
    comparisonWindow.DocumentOne = docOne;
    comparisonWindow.DocumentTwo = docTwo;
    comparisonWindow.ComparisonTreeDataSource = result.DifferenceTreeModel;
    comparisonWindow.Show(service, DockState.Document);
    comparisonWindow.Select();
  }

  /// <summary>
  /// <inheritdoc cref="M:Intermech.Client.Core.ICanCompareObjectsFiles.RemoveTypeId(System.Int32)" />
  /// </summary>
  public void RemoveTypeId(int typeId)
  {
    if (this.typeIdsList == null)
      throw new ArgumentNullException("typeIdsList");
    this.typeIdsList.Remove(typeId);
  }
}
