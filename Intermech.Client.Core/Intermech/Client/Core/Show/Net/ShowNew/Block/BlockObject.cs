
// Type: Intermech.Client.Core.Show.Net.ShowNew.Block.BlockObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.DwgLayer;
using Intermech.Client.Core.Show.Net.ShowNew.Shape;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.Block;

/// <summary>Блок  чертеж </summary>
[DebuggerDisplay("{Name} {Index,d}")]
public class BlockObject : IBlock, IDllIndex
{
  /// <summary>графика блока</summary>
  private ShapeList _shapes;

  /// <summary>объект DWG</summary>
  internal IShowDwgWork Work { get; }

  /// <summary>имя блока</summary>
  public override string ToString() => this.Name;

  private DwgLayerTable LayerTable { get; }

  /// <summary>создать блок</summary>
  /// <param name="index">метка блока в DLL</param>
  /// <param name="name">имя блока</param>
  /// <param name="work"></param>
  internal BlockObject(int index, string name, IShowDwgWork work)
  {
    this.Index = index;
    this.Name = name;
    this.Work = work;
    this.LayerTable = this.Work.Layers as DwgLayerTable;
  }

  /// <summary>положение блока в DLL </summary>
  public int Index { get; }

  /// <summary>имя блока</summary>
  public string Name { get; }

  /// <summary>пересчитать границы включённых слоёв для блока</summary>
  public RectangleD Bounds
  {
    get
    {
      lock (this)
      {
        this.ReCalculationBounds();
        return this.LayerTable.Bounds;
      }
    }
  }

  /// <summary>габариты блока при всех включённых слоях</summary>
  public RectangleD BoundsAll
  {
    get
    {
      lock (this)
      {
        this.ReCalculationBounds();
        return this.LayerTable.BoundsAll;
      }
    }
  }

  /// <summary>Рисует изображение блока в указанных границах</summary>
  /// <param name="graphics">Graphics для рисования</param>
  /// <param name="clipBox">Границы для рисования</param>
  /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
  public void Paint(System.Drawing.Graphics graphics, RectangleD clipBox, double epsilon)
  {
    lock (this)
    {
      this.ReCalculationBounds();
      this._shapes.Draw(graphics, clipBox, epsilon);
    }
  }

  /// <summary>рисовать графику блока</summary>
  /// <param name="graphics">Graphics для рисования</param>
  public void PaintCurrentUnit(System.Drawing.Graphics graphics)
  {
    lock (this)
    {
      this.ReCalculationBounds();
      this._shapes.Draw(graphics);
    }
  }

  /// <summary>Рисует изображение блока в указанных границах PDF</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
  public void Paint(PdfGraphics graphics, RectangleD clipBox, double epsilon)
  {
    lock (this)
    {
      this.ReCalculationBounds();
      this._shapes.Draw(graphics, clipBox, epsilon);
    }
  }

  /// <summary>рисовать графику блока</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="matrixD">матрица преобразования для графики</param>
  /// <param name="clipBox">Границы для рисования</param>
  public void PaintCurrentUnit(PdfGraphics graphics, MatrixD matrixD, RectangleD clipBox)
  {
    lock (this)
    {
      this.ReCalculationBounds();
      this._shapes.Draw(graphics, clipBox);
    }
  }

  /// <summary>пересчёт размеров для слоёв</summary>
  private void ReCalculationBounds()
  {
    if (this != this.LayerTable.CurrentObject)
      this.LayerTable.CurrentObject = (object) null;
    if (this._shapes == null)
    {
      this._shapes = this.Work.SubReadDataShowBlock((IDllIndex) this);
      this.LayerTable.CurrentObject = (object) null;
    }
    if (this.Work.CheckColorToBlack())
      this.LayerTable.CurrentObject = (object) null;
    if (this.LayerTable.CurrentObject != null)
      return;
    this.LayerTable.ClearBoundsAll();
    this._shapes.ReCalculationBounds();
    this.LayerTable.CurrentObject = (object) this;
  }
}
