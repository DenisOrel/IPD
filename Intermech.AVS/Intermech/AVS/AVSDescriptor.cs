// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.Interfaces;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.AVS;

/// <summary>Дескриптор для составных атрибутов с фильтрацией</summary>
internal class AVSDescriptor : Descriptor, IDisposable
{
  private int catID;
  private ICategoryTypeIconService iconService;

  public AVSDescriptor(
    int categoryID,
    int typeID,
    string caption,
    DescriptorCollection descriptors)
    : base(Intermech.Navigator.Consts.CategoryCustomNode, typeID, caption, descriptors)
  {
    this.catID = categoryID;
    this.iconService = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    this.iconService.FindIcon += new FindIconEventHandler(this.iconService_FindIcon);
  }

  private Icon iconService_FindIcon(int category, int type, object data)
  {
    return category == Intermech.Navigator.Consts.CategoryCustomNode && type == this._typeID ? this.iconService.GetIcon(this.catID, this._typeID) : (Icon) null;
  }

  public override INode GetChild(INodeID nodeID) => base.GetChild(nodeID);

  public void Dispose()
  {
    this.iconService.FindIcon -= new FindIconEventHandler(this.iconService_FindIcon);
    this.iconService = (ICategoryTypeIconService) null;
  }
}
