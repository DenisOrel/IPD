// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.SelectionTreeViewUtils
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

internal static class SelectionTreeViewUtils
{
  private static int GetNodeImageIndex(AutoSelectionNodeBase node)
  {
    int nodeImageIndex = -1;
    ICategoryTypeIconService categoryTypeIconService = AutoSelectionUtils.ServiceKeeper.GetCategoryTypeIconService();
    if (categoryTypeIconService?.ImageList == null)
      return nodeImageIndex;
    if (node is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule)
      nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 103);
    else if (node is AutoSelectionNodeFolder selectionNodeFolder)
    {
      switch (selectionNodeFolder.FolderType)
      {
        case AutoSelectionFolderType.SimpleFolder:
          nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 2);
          break;
        case AutoSelectionFolderType.SelectFolder:
          nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 104);
          break;
        case AutoSelectionFolderType.DialogFolder:
          nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 100);
          break;
        case AutoSelectionFolderType.MultiSelectFolder:
          nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 102);
          break;
        case AutoSelectionFolderType.SlideFolder:
          nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 105);
          break;
      }
    }
    else if (node is AutoSelectionNodeItemCommon selectionNodeItemCommon)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(selectionNodeItemCommon.ObjTypeGuid.Value);
      if (objectTypeId != -1)
        nodeImageIndex = categoryTypeIconService.IndexOf(4, objectTypeId);
      if (nodeImageIndex != -1)
        return nodeImageIndex;
      if (selectionNodeItemCommon is AutoSelectionNodeItemImbase)
        nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 0);
      else if (selectionNodeItemCommon is AutoSelectionNodeItemObject)
        nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 1);
    }
    else if (node is AutoSelectionNodeProc)
      nodeImageIndex = AutoSelectionConsts.objTypeRuleID != -1 ? categoryTypeIconService.IndexOf(4, AutoSelectionConsts.objTypeRuleID) : categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 4);
    if (node is AutoSelectionNodeScript)
      nodeImageIndex = AutoSelectionConsts.objTypeScriptID != -1 ? categoryTypeIconService.IndexOf(4, AutoSelectionConsts.objTypeScriptID) : categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 5);
    else if (node is AutoSelectionNodeQuest)
      nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 3);
    else if (node is AutoSelectionNodeFillAttributes)
      nodeImageIndex = categoryTypeIconService.IndexOf(AutosSelectConsts.CategoryAutoSelectionTypeNode, 6);
    return nodeImageIndex;
  }

  internal static TreeNode AddSelectionRule(TreeView treeView, Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule)
  {
    TreeNode ruleNode = rule != null ? treeView.Nodes.Add(rule.Name) : (TreeNode) null;
    SelectionTreeViewUtils.UpdateSelectionRule(ruleNode, rule, true);
    return ruleNode;
  }

  internal static void UpdateSelectionRule(
    TreeNode ruleNode,
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule,
    bool updateChild)
  {
    if (ruleNode == null)
      return;
    ruleNode.TreeView.BeginUpdate();
    try
    {
      if (ruleNode.Text != rule.Name)
        ruleNode.Text = rule.Name;
      ruleNode.Tag = (object) rule;
      ruleNode.ImageIndex = ruleNode.SelectedImageIndex = SelectionTreeViewUtils.GetNodeImageIndex((AutoSelectionNodeBase) rule);
      if (!updateChild)
        return;
      ruleNode.Nodes.Clear();
      foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) rule.ChildsNodes)
        SelectionTreeViewUtils.AddSelectionNode(ruleNode, childsNode);
      ruleNode.TreeView.Sort();
    }
    finally
    {
      ruleNode.TreeView.EndUpdate();
    }
  }

  private static TreeNode AddSelectionNode(TreeNode ownerNode, AutoSelectionNodeCommon selNode)
  {
    if (ownerNode == null)
      return (TreeNode) null;
    TreeNode treeNode = ownerNode.Nodes.Add(selNode.ToString());
    SelectionTreeViewUtils.UpdateSelectionNode(treeNode, selNode, true);
    return treeNode;
  }

  internal static void UpdateSelectionNode(
    TreeNode treeNode,
    AutoSelectionNodeCommon selNode,
    bool updateChild)
  {
    if (treeNode == null)
      return;
    string str = selNode.ToString();
    if (treeNode.Text != str)
      treeNode.Text = str;
    treeNode.Tag = (object) selNode;
    treeNode.ImageIndex = treeNode.SelectedImageIndex = SelectionTreeViewUtils.GetNodeImageIndex((AutoSelectionNodeBase) selNode);
    if (!updateChild)
      return;
    treeNode.Nodes.Clear();
    foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) selNode.ChildsNodes)
      SelectionTreeViewUtils.AddSelectionNode(treeNode, childsNode);
  }
}
