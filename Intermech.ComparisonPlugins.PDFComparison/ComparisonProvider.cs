// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.ComparisonProvider
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.ComparisonPlugins.PDFComparison.Common;
using Intermech.ComparisonPlugins.PDFComparison.UI;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison;

public class ComparisonProvider
{
  protected long _firstComparedVersion;
  protected long _secondComparedVersion;

  protected virtual void SetComparedVersions(long firstItem, long secondItem)
  {
    this._firstComparedVersion = firstItem;
    this._secondComparedVersion = secondItem;
  }

  public void ShowСomparisonWindow(IEnumerable<long> objectIDs)
  {
    List<long> longList = new List<long>();
    this.SetComparedVersions(objectIDs.ElementAtOrDefault<long>(0), objectIDs.ElementAtOrDefault<long>(1));
    using (MainView mainView = new MainView(this))
    {
      int num = (int) mainView.ShowDialog();
    }
  }

  public virtual FileDescription SelectFirstComparedFile()
  {
    return ClientUtils.FindObjectFile(this._firstComparedVersion);
  }

  public virtual FileDescription SelectSecondComparedFile()
  {
    return ClientUtils.FindObjectFile(this._secondComparedVersion);
  }

  public FileDescription SelectComparedVersion() => ClientUtils.ShowObjectSelectionDialog();
}
