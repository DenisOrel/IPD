// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCNode
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Map;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.PropertyEditors;

[Serializable]
public class LCNode : MapIconicNode
{
  private bool inPalette;
  private int levelId = -1;
  private LCStepObject lcStepObject;
  private string caption = string.Empty;
  private string note = string.Empty;
  private System.Drawing.Icon iconImage;

  public bool InPalette => this.inPalette;

  public void SetNotInPalette(LCStepObject lcSPD)
  {
    this.inPalette = false;
    this.lcStepObject = lcSPD;
  }

  public int LevelId => this.levelId;

  public LCStepObject LCStepObject => this.lcStepObject;

  public string Caption
  {
    get => this.caption;
    set
    {
      if (!(this.caption != value))
        return;
      this.caption = value;
      this.RebuildNodeView();
    }
  }

  public string Note
  {
    get => this.note;
    set
    {
      if (!(this.note != value))
        return;
      this.note = value;
      this.RebuildNodeView();
    }
  }

  public System.Drawing.Icon IconImage
  {
    get => this.iconImage;
    set
    {
      if (this.iconImage == value)
        return;
      this.iconImage = value;
      this.RebuildNodeView();
    }
  }

  public void ComplexInit(string capt, string not, System.Drawing.Icon ic)
  {
    this.ComplexInit(capt, not, ic, false);
  }

  public void ComplexInit(string capt, string not, System.Drawing.Icon ic, bool isFirstHardly)
  {
    this.caption = capt;
    this.note = not;
    this.iconImage = ic;
    this.RebuildNodeView(isFirstHardly);
  }

  public void ComplexInit() => this.ComplexInit(this.Caption, this.Note, this.IconImage);

  public void ComplexInit(bool isFirstHardly)
  {
    this.ComplexInit(this.Caption, this.Note, this.IconImage, isFirstHardly);
  }

  public LCNode(int aLevelId)
  {
    this.inPalette = true;
    this.levelId = aLevelId;
    this.InitNode();
  }

  public LCNode(LCStepObject lpd)
  {
    this.inPalette = false;
    this.lcStepObject = lpd;
    this.levelId = this.lcStepObject.LCStepProperties.LevelID;
    this.InitNode();
  }

  private void InitNode()
  {
    string str1 = string.Empty;
    string str2 = string.Empty;
    System.Drawing.Icon icon = (System.Drawing.Icon) null;
    try
    {
      if (Statics.IconSrv != null)
        icon = Statics.IconSrv.GetIcon(8, this.levelId);
      if (this.inPalette)
      {
        DataHolders.LevelsHolder.LoadData(false);
        str1 = DataHolders.LevelsHolder.GetNamebyID(this.levelId);
      }
      else
      {
        str1 = this.lcStepObject.LCStepProperties.LCName;
        str2 = this.lcStepObject.LCStepProperties.Note;
      }
      this.Initialize(string.Empty);
      this.Icon.Resizable = false;
      this.Icon.Selectable = false;
    }
    finally
    {
      this.iconImage = icon;
      this.caption = str1;
      this.note = str2;
      this.RebuildNodeView();
    }
  }

  private void SetIcon(System.Drawing.Icon icon)
  {
    MapImage mapImage = new MapImage();
    if (icon != null)
      mapImage.Image = (System.Drawing.Image) icon.ToBitmap();
    this.Icon = (MapObject) mapImage;
  }

  private void SetIcon(System.Drawing.Image lImage)
  {
    MapImage mapImage = new MapImage();
    if (lImage != null)
      mapImage.Image = lImage;
    this.Icon = (MapObject) mapImage;
  }

  private void RebuildNodeView(bool isFirstHardly)
  {
    this.SetIcon(this.iconImage != null ? LCStepPainter.PaintStep((System.Drawing.Image) this.iconImage.ToBitmap(), this.caption, this.note, this.GetPaintData(isFirstHardly), 150) : (System.Drawing.Image) null);
    RectangleF bounds = this.Bounds;
    bounds.Height += 0.01f;
    this.Bounds = bounds;
  }

  private void RebuildNodeView() => this.RebuildNodeView(false);

  private LCStepPaintData GetPaintData(bool isFirstHardly)
  {
    return isFirstHardly || !this.inPalette && this.lcStepObject.LCStepProperties.FirstStep ? LCStepPaintData.Pink : LCStepPaintData.Blue;
  }

  private LCStepPaintData GetPaintData() => this.GetPaintData(false);

  public override bool CanDelete()
  {
    bool flag = base.CanDelete();
    if (!this.inPalette)
      flag = flag && !this.lcStepObject.LCStepProperties.FirstStep;
    return flag;
  }
}
