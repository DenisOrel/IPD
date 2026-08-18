
// Type: Intermech.Search.ObjectTypeClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public static class ObjectTypeClientHelper
{
  public static int[] SelectObjectTypes(string formText, int[] checkedObjectTypes = null)
  {
    if (string.IsNullOrEmpty(formText))
      throw new ArgumentException();
    using (TreeViewWithButtonsForm viewWithButtonsForm = new TreeViewWithButtonsForm())
    {
      viewWithButtonsForm.Text = formText;
      viewWithButtonsForm.DisableGroupCheckedNodes = true;
      viewWithButtonsForm.Nodes.Add(ObjectTypeClientHelper.CreateObjectTypesRootNode());
      viewWithButtonsForm.ImageList = ServiceLocator.Get<ICategoryTypeIconService>().ImageList;
      if (checkedObjectTypes != null)
        viewWithButtonsForm.CheckedTags = ((IEnumerable<int>) checkedObjectTypes).Distinct<int>().Cast<object>().ToList<object>();
      viewWithButtonsForm.Nodes[0].Expand();
      viewWithButtonsForm.ShowCheckedNodes();
      return viewWithButtonsForm.ShowDialog() == DialogResult.OK ? viewWithButtonsForm.CheckedTags.Cast<int>().ToArray<int>() : checkedObjectTypes ?? new int[0];
    }
  }

  public static int SelectObjectType(int selectedObjectType, bool selectAbstractTypes)
  {
    using (TreeViewWithButtonsForm treeSelectDialog = new TreeViewWithButtonsForm())
    {
      treeSelectDialog.Text = "Выберите тип объекта";
      treeSelectDialog.ShowCheckBoxes = false;
      treeSelectDialog.Nodes.Add(ObjectTypeClientHelper.CreateObjectTypesRootNode());
      treeSelectDialog.ImageList = ServiceLocator.Get<ICategoryTypeIconService>().ImageList;
      if (!ObjectTypeHelper.IsUnknownObjectTypeID(selectedObjectType))
        treeSelectDialog.SelectedTag = (object) selectedObjectType;
      treeSelectDialog.Nodes[0].Expand();
      treeSelectDialog.ShowSelectedNode();
      treeSelectDialog.TreeView.AfterSelect += (TreeViewEventHandler) ((s, e) => treeSelectDialog.OKButton.Enabled = selectAbstractTypes || !ObjectTypeHelper.IsAbstract((int) e.Node.Tag));
      return treeSelectDialog.ShowDialog() == DialogResult.OK ? (int) treeSelectDialog.SelectedTag : selectedObjectType;
    }
  }

  private static TreeNode CreateObjectTypesRootNode()
  {
    TreeNode objectTypesRootNode = new TreeNode();
    objectTypesRootNode.Text = "Типы объектов";
    foreach (IMSObjectType objectType in (IEnumerable<IMSObjectType>) MetaDataHelper.GetTopObjectTypesIDs().Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)))
      objectTypesRootNode.Nodes.Add(ObjectTypeClientHelper.CreateObjectTypeNode(objectType));
    return objectTypesRootNode;
  }

  private static TreeNode CreateObjectTypeNode(IMSObjectType objectType)
  {
    TreeNode objectTypeNode = new TreeNode(objectType.ObjectTypeName)
    {
      Tag = (object) objectType.ObjectTypeID
    };
    if (objectType.VersionsMode == ObjectVersionModes.Abstract)
      objectTypeNode.ForeColor = SystemColors.GrayText;
    objectTypeNode.ImageIndex = objectTypeNode.SelectedImageIndex = ServiceLocator.Get<ICategoryTypeIconService>().IndexOf(4, objectType.ObjectTypeID);
    foreach (IMSObjectType objectType1 in (IEnumerable<IMSObjectType>) MetaDataHelper.GetObjectTypeChildrenID(objectType.ObjectTypeID).Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)))
      objectTypeNode.Nodes.Add(ObjectTypeClientHelper.CreateObjectTypeNode(objectType1));
    return objectTypeNode;
  }
}
