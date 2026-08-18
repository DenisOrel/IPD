
// Type: Intermech.Client.Core.ObjectTypeTreeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core;

internal sealed class ObjectTypeTreeNode
{
  public int ObjectTypeID { get; private set; }

  public int ParentTypeID { get; private set; }

  public bool Handled { get; set; }

  public bool Enabled { get; set; }

  public CheckState Checked { get; set; }

  public string Code { get; set; }

  public ObjectTypeTreeNode(int objectTypeID, int parentTypeID)
    : this(objectTypeID, parentTypeID, true, string.Empty)
  {
  }

  public ObjectTypeTreeNode(int objectTypeID, int parentTypeID, string code)
    : this(objectTypeID, parentTypeID, true, code)
  {
  }

  public ObjectTypeTreeNode(int objectTypeID, int parentTypeID, bool enable)
    : this(objectTypeID, parentTypeID, enable, string.Empty)
  {
  }

  public ObjectTypeTreeNode(int objectTypeID, int parentTypeID, bool enable, string code)
  {
    this.ObjectTypeID = objectTypeID;
    this.ParentTypeID = parentTypeID;
    this.Handled = false;
    this.Checked = CheckState.Unchecked;
    this.Enabled = enable;
    this.Code = code;
  }
}
