
// Type: Intermech.Client.Core.Show.Net.ShowNew.Layout.LayoutObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.DwgLayer;
using Intermech.Client.Core.Show.Net.ShowNew.Shape;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.Layout;

/// <summary>чертеж компоновки</summary>
[DebuggerDisplay("{Name} {Index,d}")]
internal class LayoutObject : ILayout, IDllIndex
{
  /// <summary>графика компоновки</summary>
  private ShapeList _shapes;

  /// <summary>объект DWG</summary>
  internal IShowDwgWork Work { get; }

  private DwgLayerTable LayerTable { get; }

  /// <summary>создать компоновку</summary>
  /// <param name="index">метка компоновки в DLL</param>
  /// <param name="name">имя компоновки</param>
  /// <param name="work"></param>
  internal LayoutObject(int index, string name, IShowDwgWork work)
  {
    this.Index = index;
    this.Name = name;
    this.Work = work;
    this.LayerTable = this.Work.Layers as DwgLayerTable;
  }

  /// <summary>положение компоновки в DLL</summary>
  public int Index { get; }

  /// <summary>имя компоновки</summary>
  public override string ToString() => this.Name;

  /// <summary>имя компоновки</summary>
  public string Name { get; }

  /// <summary>пересчитать границы включённых слоёв для компоновки</summary>
  public RectangleD Bounds
  {
    get
    {
      this.ReCalculationBounds();
      return this.LayerTable.Bounds;
    }
  }

  /// <summary>габариты компоновки при всех включённых слоях</summary>
  public RectangleD BoundsAll
  {
    get
    {
      this.ReCalculationBounds();
      return this.LayerTable.BoundsAll;
    }
  }

  /// <summary>Рисует изображение листа в указанных границах</summary>
  /// <param name="graphics">Graphics для рисования</param>
  public void Paint(System.Drawing.Graphics graphics)
  {
    this.ReCalculationBounds();
    this._shapes.Draw(graphics);
  }

  /// <summary>Рисует изображение листа в указанных границах</summary>
  /// <param name="graphics">Graphics для рисования</param>
  /// <param name="clipBox">Границы для рисования</param>
  /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
  public void Paint(System.Drawing.Graphics graphics, RectangleD clipBox, double epsilon)
  {
    this.ReCalculationBounds();
    this._shapes.Draw(graphics, clipBox, epsilon);
  }

  /// <summary>прочитать штамп (для видимых слоёв) компоновки</summary>
  /// <param name="fileCfgName">имя файла конфигурации штампа</param>
  /// <param name="cfgData">данные файла конфигурации штампа</param>
  /// <returns>список прочитанных данных из штампа; null -нет штампа</returns>
  public IStampField[] ScanStamp(string fileCfgName, byte[] cfgData)
  {
    return cfgData == null && fileCfgName == null ? (IStampField[]) null : this.Work.SubReadScanStamp((ILayout) this, fileCfgName, cfgData);
  }

  /// <summary>рисовать графику компоновки</summary>
  /// <param name="graphics">Graphics для рисования</param>
  public void PaintCurrentUnit(System.Drawing.Graphics graphics)
  {
    this.ReCalculationBounds();
    this._shapes.Draw(graphics);
  }

  /// <summary>Рисует изображение листа в указанных границах</summary>
  /// <param name="graphics">Graphics для рисования</param>
  /// <param name="clipBox">Границы для рисования</param>
  /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
  public void Paint(PdfGraphics graphics, RectangleD clipBox, double epsilon)
  {
    this.ReCalculationBounds();
    this._shapes.Draw(graphics, clipBox, epsilon);
  }

  /// <summary>рисовать графику компоновки</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="matrixD">матрица преобразования для графики</param>
  /// <param name="clipBox">Границы для рисования</param>
  public void PaintCurrentUnit(PdfGraphics graphics, MatrixD matrixD, RectangleD clipBox)
  {
    this.ReCalculationBounds();
    this._shapes.Draw(graphics, clipBox);
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
