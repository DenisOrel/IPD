
// Type: Intermech.Controls.BrushStyle
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;


namespace Intermech.Controls;

public class BrushStyle
{
  public static readonly BrushStyle Clear = new BrushStyle(true);
  public static readonly BrushStyle Solid = new BrushStyle(false);
  public static readonly BrushStyle.HatchesCollection Hatches;
  public static readonly int HatchesCount;
  public readonly BrushStyle.FillTypeEnum FillType;
  public readonly HatchStyle HatchStyle;

  /// <summary>Перечисление всех возможных стилей кистей</summary>
  public static IEnumerable<BrushStyle> PossibleBrushesStyles
  {
    get => BrushStyle.GetPossibleBrushesStyles();
  }

  private static IEnumerable<BrushStyle> GetPossibleBrushesStyles()
  {
    yield return BrushStyle.Clear;
    yield return BrushStyle.Solid;
    foreach (BrushStyle hatch in BrushStyle.Hatches)
      yield return hatch;
  }

  public static int BrushStylesCount => BrushStyle.HatchesCount + 2;

  static BrushStyle()
  {
    HatchStyle[] array = ((IEnumerable<HatchStyle>) Enum.GetValues(typeof (HatchStyle))).Select<HatchStyle, int>((Func<HatchStyle, int>) (hatchStyle => (int) hatchStyle)).Distinct<int>().Select<int, HatchStyle>((Func<int, HatchStyle>) (hatchStyleInt => (HatchStyle) hatchStyleInt)).ToArray<HatchStyle>();
    BrushStyle.HatchesCount = array.Length;
    BrushStyle.Hatches = new BrushStyle.HatchesCollection(array);
  }

  private BrushStyle()
  {
  }

  private BrushStyle(bool clear)
  {
    if (clear)
      this.FillType = BrushStyle.FillTypeEnum.Transparent;
    else
      this.FillType = BrushStyle.FillTypeEnum.Solid;
  }

  private BrushStyle(HatchStyle hatchStyle)
  {
    this.FillType = BrushStyle.FillTypeEnum.Hatch;
    this.HatchStyle = hatchStyle;
  }

  public Brush CreateBrush(Color foreColor) => this.CreateBrush(foreColor, Color.Transparent);

  public Brush CreateBrush(Color foreColor, Color bgColor)
  {
    switch (this.FillType)
    {
      case BrushStyle.FillTypeEnum.Solid:
        return (Brush) new SolidBrush(foreColor);
      case BrushStyle.FillTypeEnum.Hatch:
        return (Brush) new HatchBrush(this.HatchStyle, foreColor, bgColor);
      case BrushStyle.FillTypeEnum.Transparent:
        return (Brush) Brushes.Transparent.Clone();
      default:
        throw new Exception("Unknown FillType value");
    }
  }

  public override string ToString()
  {
    switch (this.FillType)
    {
      case BrushStyle.FillTypeEnum.Solid:
        return "Solid";
      case BrushStyle.FillTypeEnum.Hatch:
        return this.HatchStyle.ToString();
      case BrushStyle.FillTypeEnum.Transparent:
        return "Clear";
      default:
        throw new Exception("Unknown FillType value");
    }
  }

  public static BrushStyle Get(BrushStyle.FillTypeEnum fillType, HatchStyle hatchStyle = HatchStyle.Cross)
  {
    if (fillType == BrushStyle.FillTypeEnum.Transparent)
      return BrushStyle.Clear;
    return fillType != BrushStyle.FillTypeEnum.Solid ? BrushStyle.Hatches[hatchStyle] : BrushStyle.Solid;
  }

  public class HatchesCollection : IEnumerable<BrushStyle>, IEnumerable
  {
    private readonly Dictionary<int, BrushStyle> _uniqueHatches;

    public HatchesCollection(HatchStyle[] uniqueHatchStyles)
    {
      this._uniqueHatches = ((IEnumerable<HatchStyle>) uniqueHatchStyles).ToDictionary<HatchStyle, int, BrushStyle>((Func<HatchStyle, int>) (hatchStyle => (int) hatchStyle), (Func<HatchStyle, BrushStyle>) (hatchStyle => new BrushStyle(hatchStyle)));
    }

    public BrushStyle this[HatchStyle hatchStyle] => this._uniqueHatches[(int) hatchStyle];

    public IEnumerator<BrushStyle> GetEnumerator()
    {
      return (IEnumerator<BrushStyle>) this._uniqueHatches.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this._uniqueHatches.Values.GetEnumerator();
    }
  }

  public enum FillTypeEnum
  {
    Solid,
    Hatch,
    Transparent,
  }
}
