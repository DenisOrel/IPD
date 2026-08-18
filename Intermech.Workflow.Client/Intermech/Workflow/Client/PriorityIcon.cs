// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.PriorityIcon
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

public class PriorityIcon : IGridColumnImageList, IGridCellDrawing
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
    ProcessPriority processPriority = ProcessPriority.Normal;
    try
    {
      object obj = cell.Value;
      if (!obj.Equals((object) DBNull.Value))
        processPriority = (ProcessPriority) Convert.ToInt32(obj);
    }
    catch
    {
    }
    int num = -1;
    switch (processPriority)
    {
      case ProcessPriority.Low:
        num = Holder.LowImageIndex;
        break;
      case ProcessPriority.High:
        num = Holder.HighImageIndex;
        break;
    }
    return num;
  }
}
