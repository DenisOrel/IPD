// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DrawContext
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Контекст отрисовки. Данные необходимые для отрисовки</summary>
[Serializable]
public class DrawContext : CellContext
{
  /// <summary>Объект на котором нужно отобразить</summary>
  [NonSerialized]
  public ImGraphics Graphics;
  /// <summary>Отрисовка вызвана событием Paint</summary>
  public bool IsPaint;
  /// <summary>Отрисовка в метафайл</summary>
  public bool IsMetafile;
  /// <summary>Отрисовка в PDF</summary>
  public bool IsPdf;
  /// <summary>Элемент выбран</summary>
  public bool? IsSelected;
  /// <summary>Фокус принадлежит элементу</summary>
  public bool? IsFocused;
  /// <summary>Отрисовывать текст используя текущий редактор в RtfInSiteEditorWrapper</summary>
  public bool DrawInCurrentEditor;
  /// <summary>Область в которой нужно отрисовать. Все что вне области не рисуется</summary>
  public RectangleF ClipRectangle;
  /// <summary>Номер слоя. -1 невидимые линии (только для isPaint==true), 0 основной слой</summary>
  public int Layer;
  /// <summary>Не отображать данные, только границы и фон</summary>
  public bool WithoutData;
  /// <summary>Отрисовка пропущенных строк</summary>
  public bool IsSkipedSpace;
  /// <summary>Рисовать только один уровень дочерних узлов</summary>
  public bool FirstChildLevel;
  /// <summary>Размер для пропущенных строк</summary>
  public float SkipedSpaceSize;
  /// <summary>Рисовать, преобразуя координаты в пикселы самому</summary>
  public bool PixelMode;
  /// <summary>Заранее расчитанные границы в пикселах</summary>
  public Rectangle PixelBounds = Rectangle.Empty;
  /// <summary>Показывать невидимые линии</summary>
  public bool ShowInvisibleLines = true;
  /// <summary>Матрица преобразования координат</summary>
  public MatrixWrapper TransformMatrix;
  /// <summary>Только для внутреннего использования в AVS. Возвращает номера строк,
  /// для которых не нужно рисовать линию сетки (формула материала)</summary>
  public List<int> MaterialList;
  /// <summary>В текстбоксе имеются изображения</summary>
  public bool HasImages;
  /// <summary>Верхняя строка в таблице</summary>
  public bool IsTopRow = true;
  /// <summary>Нижняя строка в таблице</summary>
  public bool IsBottomRow = true;
  /// <summary>Границы</summary>
  public RectangleBorder Borders;
  /// <summary>Границы родительской таблицы</summary>
  public RectangleBorder ParentBorders;
  /// <summary>DPI монитора</summary>
  protected PointF displayDPI;
  /// <summary>Значение DisplayDPI по умолчанию</summary>
  protected static PointF defaultDisplayDPI;

  /// <summary>DPI монитора</summary>
  public PointF DisplayDPI
  {
    get
    {
      if (this.displayDPI.IsEmpty)
        this.displayDPI = this.DefaultDisplayDPI;
      return this.displayDPI;
    }
  }

  /// <summary>Значение DisplayDPI по умолчанию</summary>
  protected virtual PointF DefaultDisplayDPI
  {
    get => DrawContext.defaultDisplayDPI;
    set => DrawContext.defaultDisplayDPI = value;
  }

  /// <summary>DPI Graphics</summary>
  public PointF GraphicsDPI
  {
    get
    {
      return this.Graphics != null ? new PointF(this.Graphics.DpiX, this.Graphics.DpiY) : PointF.Empty;
    }
  }

  /// <summary>Масштаб на экране</summary>
  public virtual float Scale => 1f;

  /// <summary>Конструктор</summary>
  /// <param name="g">Объект на котором нужно отобразить</param>
  /// <param name="isPaint">Отрисовка вызвана событием Paint</param>
  /// <param name="clipRectangle">Область в которой нужно отрисовать. Все что вне области не рисуется</param>
  /// <param name="layer">Номер слоя. -1 невидимые линии (только для isPaint==true), 0 основной слой</param>
  /// <param name="withoutData">Не отображать данные, только границы и фон</param>
  /// <param name="showInvisibleLines">Отображать невидимые линии</param>
  /// <param name="transformMatrix">Матрица трансформации</param>
  public DrawContext(
    ImGraphics g,
    bool isPaint,
    RectangleF clipRectangle,
    int layer,
    bool withoutData,
    bool showInvisibleLines,
    MatrixWrapper transformMatrix)
  {
    this.Graphics = g;
    this.IsPaint = isPaint;
    this.ClipRectangle = clipRectangle;
    this.Layer = layer;
    this.WithoutData = withoutData;
    this.ShowInvisibleLines = showInvisibleLines;
    this.TransformMatrix = transformMatrix;
  }

  /// <summary>Конструктор копии контекста</summary>
  /// <param name="src">Оригинальный контекст</param>
  public DrawContext(DrawContext src)
    : base((CellContext) src)
  {
    this.Graphics = src.Graphics;
    this.IsPaint = src.IsPaint;
    this.IsSelected = src.IsSelected;
    this.IsFocused = src.IsFocused;
    this.ClipRectangle = src.ClipRectangle;
    this.Layer = src.Layer;
    this.WithoutData = src.WithoutData;
    this.PixelMode = src.PixelMode;
    this.PixelBounds = src.PixelBounds;
    this.ShowInvisibleLines = src.ShowInvisibleLines;
    this.TransformMatrix = src.TransformMatrix;
    this.Borders = src.Borders;
    this.ParentBorders = src.ParentBorders;
    this.displayDPI = src.DisplayDPI;
  }
}
