// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AttachsIcon
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Client;

public class AttachsIcon : IGridColumnImageList, IGridCellDrawing
{
  public bool DrawOnlyIcon
  {
    get => true;
    set
    {
    }
  }

  public ImageList ImageList => BaseHolder.NamedList.ImageList;

  public int ImageIndex(INodeID nodeID, iGCell cell, NodeColumnCollection columns, iGrid control)
  {
    if (cell == null)
      return -1;
    bool flag = false;
    try
    {
      object obj = cell.Value;
      flag = !obj.Equals((object) DBNull.Value) && !obj.Equals((object) 0L);
    }
    catch
    {
    }
    return flag ? Holder.AttachsImageIndex : -1;
  }
}
