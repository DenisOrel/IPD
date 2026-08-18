
// Type: Intermech.Security.CycleControlClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;


namespace Intermech.Security;

internal class CycleControlClass
{
  private TreeListNode treeListNode;
  private TreeListColumn column;

  public TreeListNode TreeListNode => this.treeListNode;

  public TreeListColumn Column => this.column;

  public CycleControlClass(TreeListNode aTreeListNode, TreeListColumn aColumn)
  {
    this.treeListNode = aTreeListNode;
    this.column = aColumn;
  }

  public override bool Equals(object obj)
  {
    if (obj == null || obj.GetType() != typeof (CycleControlClass))
      return base.Equals(obj);
    return this.treeListNode.Equals((object) ((CycleControlClass) obj).TreeListNode) && this.column.Equals((object) ((CycleControlClass) obj).Column);
  }

  public override int GetHashCode() => base.GetHashCode();
}
