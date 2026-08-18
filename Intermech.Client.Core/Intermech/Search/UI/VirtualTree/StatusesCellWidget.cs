
// Type: Intermech.Search.UI.VirtualTree.StatusesCellWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Search.Statuses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.UI.VirtualTree;

public sealed class StatusesCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  CellWidgetBase(rowWidget, column)
{
  private const int ImageLeftMargin = 5;

  public override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    StatusSite statusSite = ((IEnumerable<StatusSite>) this.GetStatusSites(this.OnGetStatuses())).FirstOrDefault<StatusSite>((Func<StatusSite, bool>) (o => o.Rectangle.Contains(e.Location)));
    if (statusSite != null)
      this.Tree.ShowToolTip(statusSite.Status.Hint);
    else
      this.Tree.HideToolTip();
  }

  public override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this.Tree.HideToolTip();
  }

  protected override void PaintBackground(
    Graphics graphics,
    Style rowStyle,
    Style cellStyle,
    bool printing)
  {
    foreach (StatusSite statusSite in this.GetStatusSites(this.OnGetStatuses()))
    {
      if (statusSite.Status.ImageList != null)
        graphics.DrawImage(statusSite.Status.ImageList.Images[statusSite.Status.ImageIndex], statusSite.Rectangle);
      else
        graphics.DrawImage(statusSite.Status.Image, statusSite.Rectangle);
    }
  }

  private Status[] OnGetStatuses() => this.CellData.Value as Status[];

  private StatusSite[] GetStatusSites(Status[] statuses)
  {
    List<StatusSite> statusSiteList = new List<StatusSite>();
    if (statuses.Length != 0)
    {
      Size minimalImageSize = this.GetMinimalImageSize(statuses);
      int x = 5;
      foreach (Status statuse in statuses)
      {
        int width = Math.Min(statuse.ImageList.ImageSize.Width, minimalImageSize.Width);
        int height = Math.Min(statuse.ImageList.ImageSize.Height, minimalImageSize.Height);
        StatusSite statusSite = new StatusSite(statuse, new Rectangle(new Point(x, this.Bounds.Height / 2 - height / 2), new Size(width, height)));
        statusSiteList.Add(statusSite);
        x += 5 + width;
      }
    }
    return statusSiteList.ToArray();
  }

  private Size GetMinimalImageSize(Status[] statuses)
  {
    Size imageSize = statuses[0].ImageList.ImageSize;
    foreach (Status status in ((IEnumerable<Status>) statuses).Skip<Status>(1))
    {
      if (status.ImageList != null && (status.ImageList.ImageSize.Height < imageSize.Height || status.ImageList.ImageSize.Width < imageSize.Width) || status.Image != null && (status.Image.Height < imageSize.Height || status.Image.Width < imageSize.Width))
        imageSize = status.ImageList.ImageSize;
    }
    return imageSize;
  }
}
