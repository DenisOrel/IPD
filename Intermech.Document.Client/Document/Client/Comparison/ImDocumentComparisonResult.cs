// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Comparison.ImDocumentComparisonResult
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Document.Client.Comparison;

internal class ImDocumentComparisonResult
{
  internal IEnumerable<DocumentTreeNode> MissingDocument1Nodes { get; }

  internal IEnumerable<DocumentTreeNode> MissingDocument2Nodes { get; }

  internal IEnumerable<DocumentTreeNode> DifferentNodes { get; }

  internal ImDocumentComparisonResult(
    IEnumerable<DocumentTreeNode> missingDoc1Nodes,
    IEnumerable<DocumentTreeNode> missingDoc2Nodes,
    IEnumerable<DocumentTreeNode> diffNodes)
  {
    this.MissingDocument1Nodes = missingDoc1Nodes;
    this.MissingDocument2Nodes = missingDoc2Nodes;
    this.DifferentNodes = diffNodes;
  }

  internal IEnumerable<string> Report
  {
    get
    {
      List<string> report = new List<string>();
      report.Add("Результат сравнения:");
      report.Add("====================");
      report.Add(" ");
      IEnumerable<DocumentTreeNode> missingDocument1Nodes = this.MissingDocument1Nodes;
      if ((missingDocument1Nodes != null ? (missingDocument1Nodes.Any<DocumentTreeNode>() ? 1 : 0) : 0) != 0)
      {
        report.Add("Элементы, отсутствующие в документе №1:");
        report.Add("-----------------------------------");
        report.AddRange(this.MissingDocument1Nodes.Select<DocumentTreeNode, string>((Func<DocumentTreeNode, string>) (m1 => $"{m1.NodeTypeCaption} [{m1.Id}]")));
        report.Add(" ");
      }
      IEnumerable<DocumentTreeNode> missingDocument2Nodes = this.MissingDocument2Nodes;
      if ((missingDocument2Nodes != null ? (missingDocument2Nodes.Any<DocumentTreeNode>() ? 1 : 0) : 0) != 0)
      {
        report.Add("Элементы, отсутствующие в документе №2:");
        report.Add("-----------------------------------");
        report.AddRange(this.MissingDocument2Nodes.Select<DocumentTreeNode, string>((Func<DocumentTreeNode, string>) (m2 => $"{m2.NodeTypeCaption} [{m2.Id}]")));
        report.Add(" ");
      }
      IEnumerable<DocumentTreeNode> differentNodes = this.DifferentNodes;
      if ((differentNodes != null ? (differentNodes.Any<DocumentTreeNode>() ? 1 : 0) : 0) != 0)
      {
        report.Add("Элементы обоих документов, имеющие отличия в геометрии и/или содержимом:");
        report.Add("-----------------------------------");
        report.AddRange(this.DifferentNodes.Select<DocumentTreeNode, string>((Func<DocumentTreeNode, string>) (dif => $"{dif.NodeTypeCaption} [{dif.Id}]")));
        report.Add(" ");
      }
      if (report.Count == 0)
        report.Add("Различий не найдено.");
      return (IEnumerable<string>) report;
    }
  }

  internal List<ComparisonTreeNode> DifferenceTreeModel
  {
    get
    {
      List<ComparisonTreeNode> model = new List<ComparisonTreeNode>();
      this.BuildDifferenceTreeModel(model, this.MissingDocument2Nodes, ComparisonVerdict.AbsentInDoc2);
      this.BuildDifferenceTreeModel(model, this.MissingDocument1Nodes, ComparisonVerdict.AbsentInDocOne);
      this.BuildDifferenceTreeModel(model, this.DifferentNodes, ComparisonVerdict.HasDifferentContentOrGeometry);
      return model;
    }
  }

  /// <summary>
  /// Добавить в модель дерева различий информацию о коллекции узлов документа
  /// </summary>
  /// <param name="model">модель дерева различий</param>
  /// <param name="nodesWithDifferences">коллекция узлов документа</param>
  /// <param name="diffType">вид различия</param>
  private void BuildDifferenceTreeModel(
    List<ComparisonTreeNode> model,
    IEnumerable<DocumentTreeNode> nodesWithDifferences,
    ComparisonVerdict diffType)
  {
    foreach (DocumentTreeNode nodesWithDifference in nodesWithDifferences)
    {
      ComparisonTreeNode diffNode = new ComparisonTreeNode(nodesWithDifference, diffType);
      ComparisonTreeNode treeRoot = model.FirstOrDefault<ComparisonTreeNode>((Func<ComparisonTreeNode, bool>) (m => m == diffNode.SuperParent));
      if (treeRoot == (ComparisonTreeNode) null)
      {
        model.Add(diffNode.SuperParent);
      }
      else
      {
        for (ComparisonTreeNode comparisonTreeNode = diffNode; comparisonTreeNode != (ComparisonTreeNode) null; comparisonTreeNode = comparisonTreeNode.Parent)
        {
          ComparisonTreeNode parentInTree = comparisonTreeNode.FindParentInTree(treeRoot);
          if (parentInTree != (ComparisonTreeNode) null)
          {
            comparisonTreeNode.Parent = parentInTree;
            break;
          }
        }
      }
    }
  }
}
