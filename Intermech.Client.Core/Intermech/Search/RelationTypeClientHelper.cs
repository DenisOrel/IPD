
// Type: Intermech.Search.RelationTypeClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public static class RelationTypeClientHelper
{
  public static int[] SelectRelationTypes(string formText, int[] checkedRelationTypes = null)
  {
    if (string.IsNullOrEmpty(formText))
      throw new ArgumentException();
    using (TreeViewWithButtonsForm viewWithButtonsForm = new TreeViewWithButtonsForm())
    {
      viewWithButtonsForm.Text = formText;
      viewWithButtonsForm.DisableGroupCheckedNodes = true;
      viewWithButtonsForm.Nodes.Add(RelationTypeClientHelper.CreateRelationTypesRootNode());
      viewWithButtonsForm.ImageList = ServiceLocator.Get<ICategoryTypeIconService>().ImageList;
      if (checkedRelationTypes != null)
        viewWithButtonsForm.CheckedTags = ((IEnumerable<int>) checkedRelationTypes).Distinct<int>().Cast<object>().ToList<object>();
      viewWithButtonsForm.Nodes[0].Expand();
      return viewWithButtonsForm.ShowDialog() == DialogResult.OK ? viewWithButtonsForm.CheckedTags.Cast<int>().ToArray<int>() : checkedRelationTypes ?? new int[0];
    }
  }

  private static TreeNode CreateRelationTypesRootNode()
  {
    TreeNode relationTypesRootNode = new TreeNode();
    relationTypesRootNode.Text = "Типы связей";
    foreach (IMSRelationType relationType in (IEnumerable<IMSRelationType>) MetaDataHelper.GetRelationTypesList().OrderBy<IMSRelationType, string>((Func<IMSRelationType, string>) (o => o.TypeName)))
      relationTypesRootNode.Nodes.Add(RelationTypeClientHelper.CreateRelationTypeNode(relationType));
    return relationTypesRootNode;
  }

  private static TreeNode CreateRelationTypeNode(IMSRelationType relationType)
  {
    TreeNode relationTypeNode = new TreeNode(relationType.Text);
    relationTypeNode.Tag = (object) relationType.RelationTypeID;
    int num1;
    int num2 = num1 = ServiceLocator.Get<ICategoryTypeIconService>().IndexOf(6, relationType.RelationTypeID);
    relationTypeNode.SelectedImageIndex = num1;
    relationTypeNode.ImageIndex = num2;
    return relationTypeNode;
  }
}
