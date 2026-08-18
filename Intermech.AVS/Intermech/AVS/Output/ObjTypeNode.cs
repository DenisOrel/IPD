// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.ObjTypeNode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

internal class ObjTypeNode : TreeNode, IComparable
{
  public ObjTypeNode()
  {
  }

  public ObjTypeNode(Guid objectTypeGuid)
  {
    this.ObjectTypeGuid = objectTypeGuid.ToString();
    this.Name = MetaDataHelper.GetObjectName(objectTypeGuid);
    this.Text = "Тип объекта: " + this.Name;
  }

  public ObjTypeNode(Guid objectTypeGuid, string name)
  {
    this.ObjectTypeGuid = objectTypeGuid.ToString();
    this.Name = name ?? string.Empty;
    this.Text = "Тип объекта: " + this.Name;
  }

  internal string ObjectTypeGuid { get; set; } = string.Empty;

  internal static ObjTypeNode Default => new ObjTypeNode(Guid.Empty, "* Все типы");

  internal bool IsDefault
  {
    get => this.ObjectTypeGuid.Equals(Guid.Empty.ToString(), StringComparison.Ordinal);
  }

  public int CompareTo(object obj)
  {
    return obj is ObjTypeNode objTypeNode ? string.Compare(this.Text, objTypeNode.Text, StringComparison.CurrentCulture) : 0;
  }
}
