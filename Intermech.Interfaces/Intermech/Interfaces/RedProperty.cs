
// Type: Intermech.Interfaces.RedProperty
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Interfaces
{
    /// <summary>настройки пометок</summary>
    public class RedProperty : IRedProperty
    {
      /// <summary>цвет кривой</summary>
      public Rclass<Color> PenColor = new Rclass<Color>(Color.Red);
      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      public Rclass<int> PenAlpha = new Rclass<int>((int) byte.MaxValue);
      /// <summary>толщина линий(мм)</summary>
      public Rclass<float> PenThickness = new Rclass<float>();
      /// <summary>цвет заливки</summary>
      public Rclass<Color> BrushColor = new Rclass<Color>(Color.Red);
      /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
      public Rclass<int> BrushAlpha = new Rclass<int>((int) byte.MaxValue);
      /// <summary>имя фонта</summary>
      public Rclass<string> FontName = new Rclass<string>("Arial");
      /// <summary>высота текста</summary>
      public Rclass<float> FontSize = new Rclass<float>(15f);
      /// <summary>цвет текста</summary>
      public Rclass<Color> TextColor = new Rclass<Color>(Color.Black);
      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      public Rclass<int> TextAlpha = new Rclass<int>((int) byte.MaxValue);
      /// <summary>стиль фаски</summary>
      public Rclass<IRedNoteStyle> NoteStyle = new Rclass<IRedNoteStyle>(IRedNoteStyle.Box);
      /// <summary>размер фаски</summary>
      public Rclass<float> Facet = new Rclass<float>(4f);
      /// <summary>стиль стрелки</summary>
      public Rclass<IRedArrowStyle> NoteArrow = new Rclass<IRedArrowStyle>();
      /// <summary>размер стрелки</summary>
      public Rclass<float> ArrowSize = new Rclass<float>(4f);

      /// <summary>тип работающего MapTool</summary>
      public Type TypeTool { get; set; }

      /// <summary>цвет кривой</summary>
      Color IRedProperty.PenColor
      {
        [DebuggerStepThrough] get => this.PenColor.Value;
        set => this.PenColor.Value = value;
      }

      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      int IRedProperty.PenAlpha
      {
        [DebuggerStepThrough] get => this.PenAlpha.Value;
        set => this.PenAlpha.Value = value;
      }

      /// <summary>цвет кривой с прозрачностью</summary>
      public Color PenColorAlpha
      {
        get
        {
          return Color.FromArgb((int) this.PenAlpha < 0 ? 0 : ((int) this.PenAlpha > (int) byte.MaxValue ? (int) byte.MaxValue : (int) this.PenAlpha), (Color) this.PenColor);
        }
      }

      /// <summary>толщина(мм)</summary>
      float IRedProperty.PenThickness
      {
        [DebuggerStepThrough] get => this.PenThickness.Value;
        set => this.PenThickness.Value = value;
      }

      /// <summary>цвет заливки</summary>
      Color IRedProperty.BrushColor
      {
        [DebuggerStepThrough] get => this.BrushColor.Value;
        set => this.BrushColor.Value = value;
      }

      /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
      int IRedProperty.BrushAlpha
      {
        [DebuggerStepThrough] get => this.BrushAlpha.Value;
        set => this.BrushAlpha.Value = value;
      }

      /// <summary>цвет заливки с прозрачностью</summary>
      public Color BrushColorAlpha
      {
        get
        {
          return Color.FromArgb((int) this.BrushAlpha < 0 ? 0 : ((int) this.BrushAlpha > (int) byte.MaxValue ? (int) byte.MaxValue : (int) this.BrushAlpha), (Color) this.BrushColor);
        }
      }

      /// <summary>имя фонта</summary>
      string IRedProperty.FontName
      {
        [DebuggerStepThrough] get => this.FontName.Value;
        set => this.FontName.Value = value;
      }

      /// <summary>высота текста</summary>
      float IRedProperty.FontSize
      {
        [DebuggerStepThrough] get => this.FontSize.Value;
        set => this.FontSize.Value = value;
      }

      /// <summary>цвет текста</summary>
      Color IRedProperty.TextColor
      {
        [DebuggerStepThrough] get => this.TextColor.Value;
        set => this.TextColor.Value = value;
      }

      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      int IRedProperty.TextAlpha
      {
        [DebuggerStepThrough] get => this.TextAlpha.Value;
        set => this.TextAlpha.Value = value;
      }

      /// <summary>цвет заливки с прозрачностью</summary>
      public Color TextColorAlpha
      {
        get
        {
          return Color.FromArgb((int) this.TextAlpha < 0 ? 0 : ((int) this.TextAlpha > (int) byte.MaxValue ? (int) byte.MaxValue : (int) this.TextAlpha), (Color) this.TextColor);
        }
      }

      /// <summary>стиль фаски</summary>
      IRedNoteStyle IRedProperty.NoteStyle
      {
        [DebuggerStepThrough] get => this.NoteStyle.Value;
        set => this.NoteStyle.Value = value;
      }

      /// <summary>размер фаски</summary>
      float IRedProperty.Facet
      {
        [DebuggerStepThrough] get => this.Facet.Value;
        set => this.Facet.Value = value;
      }

      /// <summary>стиль стрелки</summary>
      IRedArrowStyle IRedProperty.NoteArrow
      {
        [DebuggerStepThrough] get => this.NoteArrow.Value;
        set => this.NoteArrow.Value = value;
      }

      /// <summary>размер стрелки</summary>
      float IRedProperty.ArrowSize
      {
        [DebuggerStepThrough] get => this.ArrowSize.Value;
        set => this.ArrowSize.Value = value;
      }

      /// <summary>конструктор по умолчанию</summary>
      public RedProperty() => this.TypeTool = (Type) null;

      /// <summary>конструктор по интерфейсу</summary>
      /// <param name="var">образец для копии</param>
      public RedProperty(IRedProperty var) => this.Copy(var);

      /// <summary>копирование по интерфейсу</summary>
      /// <param name="var">образец для копии</param>
      public void Copy(IRedProperty var)
      {
        this.TypeTool = var.TypeTool;
        this.PenThickness.Value = var.PenThickness;
        this.PenColor.Value = var.PenColor;
        this.PenAlpha.Value = var.PenAlpha;
        this.BrushAlpha.Value = var.BrushAlpha;
        this.BrushColor.Value = var.BrushColor;
        this.FontName.Value = var.FontName;
        this.FontSize.Value = var.FontSize;
        this.TextColor.Value = var.TextColor;
        this.TextAlpha.Value = var.TextAlpha;
        this.NoteStyle.Value = var.NoteStyle;
        this.Facet.Value = var.Facet;
        this.NoteArrow.Value = var.NoteArrow;
        this.ArrowSize.Value = var.ArrowSize;
      }
    }
}
