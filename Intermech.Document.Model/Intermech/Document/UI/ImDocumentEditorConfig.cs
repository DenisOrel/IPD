// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ImDocumentEditorConfig
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Document.UI;

public class ImDocumentEditorConfig : IPropertyPage, IPropertyPageSearchOptionEvents, IConfigurable
{
  public const string ConfigModuleName = "ImDocEditor";
  public const string DocPrinterSection = "DocPrinterSettings";
  public const string RecentSymbolSection = "RecentSymbols";
  private static ImDocumentEditorConfig instance = (ImDocumentEditorConfig) null;
  /// <summary>Это конфигурация клиентского плагина. Иначе это конфигурация автономного приложения редактора документов</summary>
  public bool IsClientPluginConfig;
  private bool? newShowInvisibleLines;
  private bool showInvisibleLines = true;
  private bool? newSpellCheck;
  private bool spellCheck = true;
  private float? newSnapSize;
  private float snapSize = 2f;
  private float? newGridSize;
  private float gridSize = 1f;
  private bool showPopupBarOnResize = true;
  private bool? newShowPopupBarOnResize;
  private PageCoorSystem? newCoorSystem;
  private PageCoorSystem coorSystem;
  private PointF? newCustomCoorSystemPosition;
  private PointF customCoorSystemPosition = new PointF(0.0f, 0.0f);
  private bool? newShowGeometryDlgOnCreate;
  private bool showGeometryDlgOnCreate;
  private bool? newCorrectDecimalSeparator;
  private bool showSingleCellInTemplate;
  private bool? newShowSingleCellInTemplate;
  private bool? newAllowDebugMode;
  private bool allowDebugMode;
  public bool? newHorizontalRuler;
  public bool horizontalRuler;
  public bool? newCreateLog;
  public bool? newVerticalRuler;
  public bool verticalRuler;
  public bool? neweditOleAsFiles;
  public bool editOleAsFiles;
  public CharFormat newDefaultCharFormat;
  public CharFormat defaultCharFormat = TextData.DefaultCharFormat.Clone();
  public ParagraphFormat newDefaultParagraphFormat;
  public ParagraphFormat defaultParagraphFormat = TextData.DefaultParagraphFormat.Clone();
  private DefaultFileNameSource? newDefaultFileNameSource;
  private DefaultFileNameSource defaultFileNameSource = DefaultFileNameSource.ObjectCaption;
  /// <summary>Количество последних отображаемых символов</summary>
  private static int recentSymbolsMaxCount = 15;
  /// <summary>Список последних использованных спецсимволов</summary>
  public static List<SpecSymbol> RecentSpecSymbols = new List<SpecSymbol>(ImDocumentEditorConfig.recentSymbolsMaxCount);
  private bool? newShowDebugInfo;
  /// <summary>Настройки документов под конкретные принтеры</summary>
  public Dictionary<string, DocumentPrinterSettings> DocumentPrintersSettings_Global = new Dictionary<string, DocumentPrinterSettings>();
  /// <summary>Настройки документов под конкретные принтеры</summary>
  public Dictionary<string, DocumentPrinterSettings> DocumentPrintersSettings_User = new Dictionary<string, DocumentPrinterSettings>();

  public static int RecentSymbolsMaxCount => ImDocumentEditorConfig.recentSymbolsMaxCount;

  public static ImDocumentEditorConfig Instance
  {
    [DebuggerStepThrough] get
    {
      if (ImDocumentEditorConfig.instance == null)
        ImDocumentEditorConfig.instance = new ImDocumentEditorConfig();
      return ImDocumentEditorConfig.instance;
    }
  }

  [CustomDisplayName("Attribute.Document.Model_7")]
  [CustomDescription("Attribute.Document.Model_8")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ShowInvisibleLinesVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newShowInvisibleLines.HasValue ? this.newShowInvisibleLines.Value : this.showInvisibleLines;
    }
    set
    {
      bool? showInvisibleLines = this.newShowInvisibleLines;
      bool flag = value;
      if (showInvisibleLines.GetValueOrDefault() == flag & showInvisibleLines.HasValue)
        return;
      this.newShowInvisibleLines = new bool?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_254")]
  [CustomDescription("Attribute.Document.Model_255")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool SpellCheckVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newSpellCheck.HasValue ? this.newSpellCheck.Value : this.spellCheck;
    }
    set
    {
      bool? newSpellCheck = this.newSpellCheck;
      bool flag = value;
      if (newSpellCheck.GetValueOrDefault() == flag & newSpellCheck.HasValue)
        return;
      this.newSpellCheck = new bool?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_7")]
  [CustomDescription("Attribute.Document.Model_8")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool ShowInvisibleLines
  {
    [DebuggerStepThrough] get => this.showInvisibleLines;
    set
    {
      if (this.showInvisibleLines == value)
        return;
      this.newShowInvisibleLines = new bool?();
      this.showInvisibleLines = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_254")]
  [CustomDescription("Attribute.Document.Model_255")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool SpellCheck
  {
    [DebuggerStepThrough] get => this.spellCheck;
    set
    {
      if (this.spellCheck == value)
        return;
      this.newSpellCheck = new bool?();
      this.spellCheck = value;
      this.OnChanged();
    }
  }

  /// <summary>Формат символов по умолчанию</summary>
  [Browsable(false)]
  public CharFormat DefaultCharFormat
  {
    [DebuggerStepThrough] get => this.defaultCharFormat;
    set
    {
      if (this.defaultCharFormat == value)
        return;
      this.newDefaultCharFormat = (CharFormat) null;
      this.defaultCharFormat = value;
      this.OnChanged();
    }
  }

  /// <summary>Формат символов по умолчанию. Свойство для PropertyGrid</summary>
  [CustomDisplayName("Attribute.Document.Model_295")]
  [CustomDescription("Attribute.Document.Model_296")]
  [CustomCategory("Attribute.Document.Model_253")]
  [TypeConverter(typeof (DefaultCharFormatConverter))]
  public CharFormat DefaultCharFormatVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newDefaultCharFormat != null ? this.newDefaultCharFormat : this.defaultCharFormat;
    }
    set
    {
      if (this.newDefaultCharFormat == value)
        return;
      this.newDefaultCharFormat = value;
    }
  }

  /// <summary>Формат абзаца по умолчанию</summary>
  [Browsable(false)]
  public ParagraphFormat DefaultParagraphFormat
  {
    [DebuggerStepThrough] get => this.defaultParagraphFormat;
    set
    {
      if (this.defaultParagraphFormat == value)
        return;
      this.newDefaultParagraphFormat = (ParagraphFormat) null;
      this.defaultParagraphFormat = value;
      this.OnChanged();
    }
  }

  /// <summary>Формат абзаца по умолчанию. Свойство для PropertyGrid</summary>
  [CustomDisplayName("Attribute.Document.Model_297")]
  [CustomDescription("Attribute.Document.Model_298")]
  [CustomCategory("Attribute.Document.Model_253")]
  [TypeConverter(typeof (DefaultParagraphFormatConverter))]
  public ParagraphFormat DefaultParagraphFormatVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newDefaultParagraphFormat != null ? this.newDefaultParagraphFormat : this.defaultParagraphFormat;
    }
    set
    {
      if (this.newDefaultParagraphFormat == value)
        return;
      this.newDefaultParagraphFormat = value;
    }
  }

  [CustomDisplayName("Attribute.Document.Model_246")]
  [CustomDescription("Attribute.Document.Model_247")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(2f)]
  [TypeConverter(typeof (FloatConverter))]
  public float SnapSizeVisual
  {
    [DebuggerStepThrough] get => this.newSnapSize.HasValue ? this.newSnapSize.Value : this.snapSize;
    set
    {
      float? newSnapSize = this.newSnapSize;
      float num = value;
      if ((double) newSnapSize.GetValueOrDefault() == (double) num & newSnapSize.HasValue)
        return;
      this.newSnapSize = new float?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_10")]
  [CustomDescription("Attribute.Document.Model_11")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(1f)]
  [TypeConverter(typeof (FloatConverter))]
  public float GridSizeVisual
  {
    [DebuggerStepThrough] get => this.newGridSize.HasValue ? this.newGridSize.Value : this.gridSize;
    set
    {
      float? newGridSize = this.newGridSize;
      float num = value;
      if ((double) newGridSize.GetValueOrDefault() == (double) num & newGridSize.HasValue)
        return;
      this.newGridSize = new float?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_10")]
  [CustomDescription("Attribute.Document.Model_11")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(1f)]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public float GridSize
  {
    [DebuggerStepThrough] get => this.gridSize;
    set
    {
      if ((double) this.gridSize == (double) value)
        return;
      this.newGridSize = new float?();
      this.gridSize = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_256")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [DependsOn("AllowDebugModeVisual")]
  public bool ShowDebugInfoVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newShowDebugInfo.HasValue ? this.newShowDebugInfo.Value : ImDocumentData.ShowDebugInfo;
    }
    set
    {
      bool? newShowDebugInfo = this.newShowDebugInfo;
      bool flag = value;
      if (newShowDebugInfo.GetValueOrDefault() == flag & newShowDebugInfo.HasValue)
        return;
      this.newShowDebugInfo = new bool?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_256")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool AllowDebugMode
  {
    [DebuggerStepThrough] get => this.allowDebugMode;
    set
    {
      if (this.allowDebugMode == value)
        return;
      this.newAllowDebugMode = new bool?();
      this.allowDebugMode = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_304")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [IsAdmin]
  public bool AllowDebugModeVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newAllowDebugMode.HasValue ? this.newAllowDebugMode.Value : this.allowDebugMode;
    }
    set
    {
      bool? newAllowDebugMode = this.newAllowDebugMode;
      bool flag = value;
      if (newAllowDebugMode.GetValueOrDefault() == flag & newAllowDebugMode.HasValue)
        return;
      this.newAllowDebugMode = new bool?(value);
    }
  }

  [CustomDisplayName("Attribute.Document.Model_304")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool ShowDebugInfo
  {
    [DebuggerStepThrough] get => ImDocumentData.ShowDebugInfo;
    set
    {
      if (ImDocumentData.ShowDebugInfo == value)
        return;
      this.newShowDebugInfo = new bool?();
      ImDocumentData.ShowDebugInfo = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_557")]
  [CustomDescription("Attribute.Document.Model_558")]
  [CustomCategory("Attribute.Document.Model_12")]
  [DefaultValue(2f)]
  [TypeConverter(typeof (FloatConverter))]
  [Browsable(false)]
  public float SnapSize
  {
    [DebuggerStepThrough] get => this.snapSize;
    set
    {
      if ((double) this.snapSize == (double) value)
        return;
      this.newSnapSize = new float?();
      this.snapSize = value;
      this.OnChanged();
    }
  }

  public void AssignGridSize(float value)
  {
    this.gridSize = value;
    this.newGridSize = new float?();
  }

  public void AssignSnapSize(float value)
  {
    this.snapSize = value;
    this.newSnapSize = new float?();
  }

  /// <summary>Система координат страницы</summary>
  [CustomDisplayName("Attribute.Document.Model_13")]
  [CustomDescription("Attribute.Document.Model_14")]
  [CustomCategory("Attribute.Document.Model_15")]
  [DefaultValue(PageCoorSystem.BottomLeft)]
  public PageCoorSystem CoorSystemVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newCoorSystem.HasValue ? this.newCoorSystem.Value : this.coorSystem;
    }
    set
    {
      PageCoorSystem? newCoorSystem = this.newCoorSystem;
      PageCoorSystem pageCoorSystem = value;
      if (newCoorSystem.GetValueOrDefault() == pageCoorSystem & newCoorSystem.HasValue)
        return;
      this.newCoorSystem = new PageCoorSystem?(value);
    }
  }

  /// <summary>Система координат страницы</summary>
  [CustomDisplayName("Attribute.Document.Model_13")]
  [CustomDescription("Attribute.Document.Model_14")]
  [CustomCategory("Attribute.Document.Model_15")]
  [DefaultValue(PageCoorSystem.BottomLeft)]
  [Browsable(false)]
  public PageCoorSystem CoorSystem
  {
    [DebuggerStepThrough] get => this.coorSystem;
    set
    {
      if (this.coorSystem == value)
        return;
      this.newCoorSystem = new PageCoorSystem?();
      this.coorSystem = value;
      DocumentControl.IsCoorSystemSelecting = false;
      this.OnChanged();
      this.OnCoorSystemChanged();
    }
  }

  public void AssignCoorSystem(PageCoorSystem value)
  {
    this.coorSystem = value;
    this.newCoorSystem = new PageCoorSystem?();
    this.OnCoorSystemChanged();
  }

  /// <summary>Положение пользовательской системы координат</summary>
  [CustomDisplayName("Attribute.Document.Model_16")]
  [CustomDescription("Attribute.Document.Model_17")]
  [CustomCategory("Attribute.Document.Model_18")]
  [TypeConverter(typeof (PointFConverter))]
  public PointF CustomCoorSystemPositionVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newCustomCoorSystemPosition.HasValue ? this.newCustomCoorSystemPosition.Value : this.customCoorSystemPosition;
    }
    set
    {
      PointF? coorSystemPosition = this.newCustomCoorSystemPosition;
      PointF pointF = value;
      if ((coorSystemPosition.HasValue ? (coorSystemPosition.HasValue ? (coorSystemPosition.GetValueOrDefault() != pointF ? 1 : 0) : 0) : 1) == 0)
        return;
      this.newCustomCoorSystemPosition = new PointF?(value);
    }
  }

  /// <summary>Положение пользовательской системы координат</summary>
  [CustomDisplayName("Attribute.Document.Model_16")]
  [CustomDescription("Attribute.Document.Model_17")]
  [CustomCategory("Attribute.Document.Model_18")]
  [TypeConverter(typeof (PointFConverter))]
  [Browsable(false)]
  public PointF CustomCoorSystemPosition
  {
    [DebuggerStepThrough] get => this.customCoorSystemPosition;
    set
    {
      if (!(this.customCoorSystemPosition != value))
        return;
      this.newCustomCoorSystemPosition = new PointF?();
      this.customCoorSystemPosition = value;
      this.OnChanged();
      this.OnCoorSystemPositionChanged();
    }
  }

  public void AssignСustomCoorSystemPosition(PointF value)
  {
    this.customCoorSystemPosition = value;
    this.newCustomCoorSystemPosition = new PointF?();
    this.OnCoorSystemPositionChanged();
  }

  /// <summary>Создавать  лог работы</summary>
  [CustomDisplayName("Attribute.Document.Model_257")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool CreateLogVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newCreateLog.HasValue ? this.newCreateLog.Value : this.CreateLog;
    }
    set
    {
      bool? newCreateLog = this.newCreateLog;
      bool flag = value;
      if (newCreateLog.GetValueOrDefault() == flag & newCreateLog.HasValue)
        return;
      this.newCreateLog = new bool?(value);
    }
  }

  /// <summary>Создавать  лог работы</summary>
  [CustomDisplayName("Attribute.Document.Model_2")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool CreateLog
  {
    [DebuggerStepThrough] get => LogManager.CreateLog;
    set
    {
      if (LogManager.CreateLog == value)
        return;
      this.newCreateLog = new bool?();
      LogManager.CreateLog = value;
      if (LogManager.FileName == "ImDocBase.log")
        LogManager.FileName = "ImDocument.log";
      this.OnChanged();
    }
  }

  /// <summary>Показывать диалог после создания элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_19")]
  [CustomDescription("Attribute.Document.Model_20")]
  [CustomCategory("Attribute.Document.Model_21")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ShowGeometryDlgOnCreateVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newShowGeometryDlgOnCreate.HasValue ? this.newShowGeometryDlgOnCreate.Value : this.showGeometryDlgOnCreate;
    }
    set
    {
      bool? geometryDlgOnCreate = this.newShowGeometryDlgOnCreate;
      bool flag = value;
      if (geometryDlgOnCreate.GetValueOrDefault() == flag & geometryDlgOnCreate.HasValue)
        return;
      this.newShowGeometryDlgOnCreate = new bool?(value);
    }
  }

  /// <summary>Показывать диалог после создания элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_19")]
  [CustomDescription("Attribute.Document.Model_20")]
  [CustomCategory("Attribute.Document.Model_21")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool ShowGeometryDlgOnCreate
  {
    [DebuggerStepThrough] get => this.showGeometryDlgOnCreate;
    set
    {
      if (this.showGeometryDlgOnCreate == value)
        return;
      this.newShowGeometryDlgOnCreate = new bool?();
      this.showGeometryDlgOnCreate = value;
      this.OnChanged();
    }
  }

  public void AssignShowGeometryDlgOnCreate(bool value)
  {
    this.showGeometryDlgOnCreate = value;
    this.newShowGeometryDlgOnCreate = new bool?();
  }

  /// <summary>Исправлять десятичный разделитель ',' или '.' на установленный в системе</summary>
  [CustomDisplayName("Attribute.Document.Model_22")]
  [CustomDescription("Attribute.Document.Model_23")]
  [CustomCategory("Attribute.Document.Model_24")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool CorrectDecimalSeparatorVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newCorrectDecimalSeparator.HasValue ? this.newCorrectDecimalSeparator.Value : FloatConverter.CorrectDecimalSeparator;
    }
    set
    {
      bool? decimalSeparator = this.newCorrectDecimalSeparator;
      bool flag = value;
      if (decimalSeparator.GetValueOrDefault() == flag & decimalSeparator.HasValue)
        return;
      this.newCorrectDecimalSeparator = new bool?(value);
    }
  }

  /// <summary>Исправлять десятичный разделитель ',' или '.' на установленный в системе</summary>
  [CustomDisplayName("Attribute.Document.Model_22")]
  [CustomDescription("Attribute.Document.Model_23")]
  [CustomCategory("Attribute.Document.Model_24")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool CorrectDecimalSeparator
  {
    [DebuggerStepThrough] get => FloatConverter.CorrectDecimalSeparator;
    set
    {
      if (FloatConverter.CorrectDecimalSeparator == value)
        return;
      this.newCorrectDecimalSeparator = new bool?();
      FloatConverter.CorrectDecimalSeparator = value;
      this.OnChanged();
    }
  }

  public void AssignCorrectDecimalSeparator(bool value)
  {
    FloatConverter.CorrectDecimalSeparator = value;
  }

  /// <summary>Показывать горизонтальную линейку</summary>
  [CustomDisplayName("Attribute.Document.Model_25")]
  [CustomDescription("Attribute.Document.Model_26")]
  [CustomCategory("Attribute.Document.Model_27")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool HorizontalRulerVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newHorizontalRuler.HasValue ? this.newHorizontalRuler.Value : this.horizontalRuler;
    }
    set
    {
      bool? newHorizontalRuler = this.newHorizontalRuler;
      bool flag = value;
      if (newHorizontalRuler.GetValueOrDefault() == flag & newHorizontalRuler.HasValue)
        return;
      this.newHorizontalRuler = new bool?(value);
    }
  }

  /// <summary>Показывать горизонтальную линейку</summary>
  [CustomDisplayName("Attribute.Document.Model_25")]
  [CustomDescription("Attribute.Document.Model_26")]
  [CustomCategory("Attribute.Document.Model_27")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool HorizontalRuler
  {
    [DebuggerStepThrough] get => this.horizontalRuler;
    set
    {
      if (this.horizontalRuler == value)
        return;
      this.newHorizontalRuler = new bool?();
      this.horizontalRuler = value;
      this.OnChanged();
    }
  }

  public void AssignHorizontalRuler(bool value)
  {
    this.horizontalRuler = value;
    this.newHorizontalRuler = new bool?();
  }

  /// <summary>Редактировать OLE объекты как файлы</summary>
  [CustomDisplayName("Attribute.Document.Model_299")]
  [CustomDescription("Attribute.Document.Model_300")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool EditOleAsFilesVisual
  {
    [DebuggerStepThrough] get
    {
      return this.neweditOleAsFiles.HasValue ? this.neweditOleAsFiles.Value : this.editOleAsFiles;
    }
    set
    {
      bool? neweditOleAsFiles = this.neweditOleAsFiles;
      bool flag = value;
      if (neweditOleAsFiles.GetValueOrDefault() == flag & neweditOleAsFiles.HasValue)
        return;
      this.neweditOleAsFiles = new bool?(value);
    }
  }

  /// <summary>Редактировать OLE объекты как файлы</summary>
  [CustomDisplayName("Attribute.Document.Model_299")]
  [CustomDescription("Attribute.Document.Model_300")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool EditOleAsFiles
  {
    [DebuggerStepThrough] get => this.editOleAsFiles;
    set
    {
      if (this.editOleAsFiles == value)
        return;
      this.neweditOleAsFiles = new bool?();
      this.editOleAsFiles = value;
      this.OnChanged();
    }
  }

  /// <summary>Показывать вертикальную линейку</summary>
  [CustomDisplayName("Attribute.Document.Model_28")]
  [CustomDescription("Attribute.Document.Model_29")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool VerticalRulerVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newVerticalRuler.HasValue ? this.newVerticalRuler.Value : this.verticalRuler;
    }
    set
    {
      bool? newVerticalRuler = this.newVerticalRuler;
      bool flag = value;
      if (newVerticalRuler.GetValueOrDefault() == flag & newVerticalRuler.HasValue)
        return;
      this.newVerticalRuler = new bool?(value);
    }
  }

  /// <summary>Показывать вертикальную линейку</summary>
  [CustomDisplayName("Attribute.Document.Model_28")]
  [CustomDescription("Attribute.Document.Model_29")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool VerticalRuler
  {
    [DebuggerStepThrough] get => this.verticalRuler;
    set
    {
      if (this.verticalRuler == value)
        return;
      this.newVerticalRuler = new bool?();
      this.verticalRuler = value;
      this.OnChanged();
    }
  }

  /// <summary>Отображать всплывающее окно при изменении размеров элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_238")]
  [CustomDescription("Attribute.Document.Model_239")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ShowPopupBarOnResizeVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newShowPopupBarOnResize.HasValue ? this.newShowPopupBarOnResize.Value : this.showPopupBarOnResize;
    }
    set
    {
      bool? popupBarOnResize = this.newShowPopupBarOnResize;
      bool flag = value;
      if (popupBarOnResize.GetValueOrDefault() == flag & popupBarOnResize.HasValue)
        return;
      this.newShowPopupBarOnResize = new bool?(value);
    }
  }

  /// <summary>Отображать всплывающее окно при изменении размеров элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_28")]
  [CustomDescription("Attribute.Document.Model_29")]
  [CustomCategory("Attribute.Document.Model_30")]
  [DefaultValue(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool ShowPopupBarOnResize
  {
    [DebuggerStepThrough] get => this.showPopupBarOnResize;
    set
    {
      if (this.showPopupBarOnResize == value)
        return;
      this.newShowPopupBarOnResize = new bool?();
      this.showPopupBarOnResize = value;
      this.OnChanged();
    }
  }

  /// <summary>Показывать только выбранную строку для таблиц с необязательными элементами</summary>
  [CustomDisplayName("Attribute.Document.Model_302")]
  [CustomDescription("Attribute.Document.Model_303")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public bool ShowSingleCellInTemplate
  {
    [DebuggerStepThrough] get => this.showSingleCellInTemplate;
    set
    {
      this.newShowSingleCellInTemplate = new bool?();
      this.showSingleCellInTemplate = value;
      this.OnChanged();
    }
  }

  /// <summary>Показывать только выбранную строку для таблиц с необязательными элементами</summary>
  [CustomDisplayName("Attribute.Document.Model_302")]
  [CustomDescription("Attribute.Document.Model_303")]
  [CustomCategory("Attribute.Document.Model_9")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool ShowSingleCellInTemplateVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newShowSingleCellInTemplate.HasValue ? this.newShowSingleCellInTemplate.Value : this.showSingleCellInTemplate;
    }
    set
    {
      bool? singleCellInTemplate = this.newShowSingleCellInTemplate;
      bool flag = value;
      if (singleCellInTemplate.GetValueOrDefault() == flag & singleCellInTemplate.HasValue)
        return;
      this.newShowSingleCellInTemplate = new bool?(value);
    }
  }

  public void AssignVerticalRuler(bool value)
  {
    this.verticalRuler = value;
    this.newVerticalRuler = new bool?();
  }

  /// <summary>Источник имени файла</summary>
  [CustomDisplayName("Attribute.Document.Model_308")]
  [CustomDescription("Attribute.Document.Model_309")]
  [CustomCategory("Attribute.Document.Model_310")]
  [DefaultValue(DefaultFileNameSource.ObjectCaption)]
  public DefaultFileNameSource DefaultFileNameSourceVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newDefaultFileNameSource.HasValue ? this.newDefaultFileNameSource.Value : this.defaultFileNameSource;
    }
    set
    {
      DefaultFileNameSource? defaultFileNameSource1 = this.newDefaultFileNameSource;
      DefaultFileNameSource defaultFileNameSource2 = value;
      if (defaultFileNameSource1.GetValueOrDefault() == defaultFileNameSource2 & defaultFileNameSource1.HasValue)
        return;
      this.newDefaultFileNameSource = new DefaultFileNameSource?(value);
    }
  }

  [Browsable(false)]
  public DefaultFileNameSource DefaultFileNameSource
  {
    [DebuggerStepThrough] get => this.defaultFileNameSource;
    set
    {
      if (this.defaultFileNameSource == value)
        return;
      this.newDefaultFileNameSource = new DefaultFileNameSource?();
      this.defaultFileNameSource = value;
      this.OnChanged();
    }
  }

  public void AssignDefaultFileNameSource(DefaultFileNameSource value)
  {
    this.defaultFileNameSource = value;
    this.newDefaultFileNameSource = new DefaultFileNameSource?();
  }

  /// <summary>Настройки смещения страниц для принтера</summary>
  [Browsable(false)]
  public PointF GetShiftPage(string printerName)
  {
    DocumentPrinterSettings documentPrinterSettings;
    if (!this.DocumentPrintersSettings_User.TryGetValue(printerName, out documentPrinterSettings) || documentPrinterSettings == null)
      this.DocumentPrintersSettings_Global.TryGetValue(printerName, out documentPrinterSettings);
    return documentPrinterSettings != null ? documentPrinterSettings.ShiftPage : (PointF) Point.Empty;
  }

  /// <summary>Сохранить последние используемые символы.</summary>
  public void SaveRecentSpecSymbols()
  {
    if (!this.IsClientPluginConfig)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      long userId = sessionKeeper.Session.UserID;
      int num = 0;
      foreach (SpecSymbol recentSpecSymbol in ImDocumentEditorConfig.RecentSpecSymbols)
      {
        configurations.WriteString("ImDocEditor", "RecentSymbols", "SymbolId" + (object) num, recentSpecSymbol.Id, userId);
        ++num;
      }
      configurations.WriteString("ImDocEditor", "RecentSymbols", "SymbolCount", num.ToString((IFormatProvider) CultureInfo.InvariantCulture), userId);
    }
  }

  /// <summary>Сохранить настройки документов под конкретные принтеры</summary>
  /// <param name="globalSettings">Сохранять в глобальные настройки, иначе в пользовательские</param>
  public void SaveDocumentPrintersSettings(bool globalSettings)
  {
    if (!this.IsClientPluginConfig)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      PointFConverter pointFconverter = new PointFConverter();
      Dictionary<string, DocumentPrinterSettings> dictionary = globalSettings ? this.DocumentPrintersSettings_Global : this.DocumentPrintersSettings_User;
      long userId = globalSettings ? 0L : sessionKeeper.Session.UserID;
      int num = 0;
      foreach (KeyValuePair<string, DocumentPrinterSettings> keyValuePair in dictionary)
      {
        configurations.WriteString("ImDocEditor", "DocPrinterSettings", "PrtName" + num.ToString(), keyValuePair.Key, userId);
        configurations.WriteString("ImDocEditor", "DocPrinterSettings", "DocPrtShift" + num.ToString(), pointFconverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) keyValuePair.Value.ShiftPage), userId);
        ++num;
      }
      if (num <= 0)
        return;
      configurations.WriteString("ImDocEditor", "DocPrinterSettings", "PrtCount", num.ToString((IFormatProvider) CultureInfo.InvariantCulture), userId);
    }
  }

  /// <summary>Загрузить настройки документов под конкретные принтеры</summary>
  public void LoadDocumentPrintersSettings()
  {
    this.LoadDocumentPrintersSettings(true);
    this.LoadDocumentPrintersSettings(false);
  }

  /// <summary>Загрузить настройки документов под конкретные принтеры</summary>
  /// <param name="globalSettings">Грузить глобальные настройки для всех пользователей. Если false, то настройки текущего пользователя</param>
  public void LoadDocumentPrintersSettings(bool globalSettings)
  {
    if (!this.IsClientPluginConfig)
      return;
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    Dictionary<string, DocumentPrinterSettings> dictionary = globalSettings ? this.DocumentPrintersSettings_Global : this.DocumentPrintersSettings_User;
    DBConfigMode configMode = globalSettings ? DBConfigMode.GlobalOnly : DBConfigMode.UserOnly;
    long num = service.ReadInteger("ImDocEditor", "DocPrinterSettings", "PrtCount", 0L, configMode);
    for (int index = 0; (long) index < num; ++index)
    {
      string key = service.ReadString("ImDocEditor", "DocPrinterSettings", "PrtName" + index.ToString(), (string) null, configMode);
      if (!string.IsNullOrEmpty(key))
      {
        string text = service.ReadString("ImDocEditor", "DocPrinterSettings", "DocPrtShift" + index.ToString(), (string) null, configMode);
        if (!string.IsNullOrEmpty(text))
        {
          PointF shiftPage = (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, text);
          DocumentPrinterSettings documentPrinterSettings;
          if (!dictionary.TryGetValue(key, out documentPrinterSettings))
            dictionary.Add(key, new DocumentPrinterSettings(shiftPage));
          else
            documentPrinterSettings.ShiftPage = shiftPage;
        }
      }
    }
  }

  /// <summary>
  /// Загрузить последние использованные пользователем спецсимволы.
  /// </summary>
  public void LoadRecentSymbolsSettings()
  {
    if (!this.IsClientPluginConfig)
      return;
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    long num = service.ReadInteger("ImDocEditor", "RecentSymbols", "SymbolCount", 0L, DBConfigMode.UserOnly);
    for (int index = 0; (long) index < num; ++index)
    {
      string id = service.ReadString("ImDocEditor", "RecentSymbols", "SymbolId" + index.ToString(), (string) null, DBConfigMode.UserOnly);
      if (!string.IsNullOrEmpty(id))
        ImDocumentEditorConfig.RecentSpecSymbols.Add(new SpecSymbol(id));
    }
  }

  /// <summary>Конструктор</summary>
  protected ImDocumentEditorConfig()
  {
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  [Browsable(false)]
  public string HelpTopicID => "1079";

  public event EventHandler CoorSystemPositionChanged;

  private void OnCoorSystemPositionChanged()
  {
    EventHandler systemPositionChanged = this.CoorSystemPositionChanged;
    if (systemPositionChanged == null)
      return;
    systemPositionChanged((object) this, new EventArgs());
  }

  public event EventHandler CoorSystemChanged;

  private void OnCoorSystemChanged()
  {
    EventHandler coorSystemChanged = this.CoorSystemChanged;
    if (coorSystemChanged == null)
      return;
    coorSystemChanged((object) this, new EventArgs());
  }

  public event EventHandler Changed;

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  [Browsable(false)]
  public PropertyPageType Type
  {
    [DebuggerStepThrough] get => PropertyPageType.Object;
  }

  [Browsable(false)]
  public object Control
  {
    [DebuggerStepThrough] get => (object) new ClassWrapperForPropertyGrid((object) this);
  }

  [Browsable(false)]
  public string PageName
  {
    [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("Document.Model_65");
  }

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  [Browsable(false)]
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this.newSpellCheck.HasValue)
      this.SpellCheck = this.newSpellCheck.Value;
    if (this.newShowInvisibleLines.HasValue)
      this.ShowInvisibleLines = this.newShowInvisibleLines.Value;
    if (this.newGridSize.HasValue)
      this.GridSize = this.newGridSize.Value;
    if (this.newSnapSize.HasValue)
      this.SnapSize = this.newSnapSize.Value;
    if (this.newCoorSystem.HasValue)
      this.CoorSystem = this.newCoorSystem.Value;
    if (this.newCustomCoorSystemPosition.HasValue)
      this.CustomCoorSystemPosition = this.newCustomCoorSystemPosition.Value;
    if (this.newShowGeometryDlgOnCreate.HasValue)
      this.ShowGeometryDlgOnCreate = this.newShowGeometryDlgOnCreate.Value;
    if (this.newCorrectDecimalSeparator.HasValue)
      this.CorrectDecimalSeparator = this.newCorrectDecimalSeparator.Value;
    if (this.newHorizontalRuler.HasValue)
      this.HorizontalRuler = this.newHorizontalRuler.Value;
    if (this.newVerticalRuler.HasValue)
      this.VerticalRuler = this.newVerticalRuler.Value;
    if (this.newShowPopupBarOnResize.HasValue)
      this.ShowPopupBarOnResize = this.newShowPopupBarOnResize.Value;
    if (this.newDefaultCharFormat != null)
      this.defaultCharFormat = this.newDefaultCharFormat;
    if (this.newDefaultParagraphFormat != null)
      this.defaultParagraphFormat = this.newDefaultParagraphFormat;
    if (this.newAllowDebugMode.HasValue)
      this.AllowDebugMode = this.newAllowDebugMode.Value;
    if (this.newShowDebugInfo.HasValue)
      this.ShowDebugInfo = this.newShowDebugInfo.Value;
    if (this.newCreateLog.HasValue)
      this.CreateLog = this.newCreateLog.Value;
    if (this.neweditOleAsFiles.HasValue)
      this.EditOleAsFiles = this.neweditOleAsFiles.Value;
    if (this.newShowSingleCellInTemplate.HasValue)
      this.ShowSingleCellInTemplate = this.newShowSingleCellInTemplate.Value;
    if (this.newDefaultFileNameSource.HasValue)
      this.DefaultFileNameSource = this.newDefaultFileNameSource.Value;
    this.newShowInvisibleLines = new bool?();
    this.newSpellCheck = new bool?();
    this.newGridSize = new float?();
    this.newSnapSize = new float?();
    this.newCoorSystem = new PageCoorSystem?();
    this.newCustomCoorSystemPosition = new PointF?();
    this.newShowGeometryDlgOnCreate = new bool?();
    this.newCorrectDecimalSeparator = new bool?();
    this.newHorizontalRuler = new bool?();
    this.newVerticalRuler = new bool?();
    this.newShowPopupBarOnResize = new bool?();
    this.newDefaultCharFormat = (CharFormat) null;
    this.newDefaultParagraphFormat = (ParagraphFormat) null;
    this.newShowDebugInfo = new bool?();
    this.newCreateLog = new bool?();
    this.neweditOleAsFiles = new bool?();
    this.newShowSingleCellInTemplate = new bool?();
    this.newDefaultFileNameSource = new DefaultFileNameSource?();
  }

  public void Cancel()
  {
    this.newShowInvisibleLines = new bool?();
    this.newSpellCheck = new bool?();
    this.newGridSize = new float?();
    this.newSnapSize = new float?();
    this.newCoorSystem = new PageCoorSystem?();
    this.newCustomCoorSystemPosition = new PointF?();
    this.newShowGeometryDlgOnCreate = new bool?();
    this.newCorrectDecimalSeparator = new bool?();
    this.newHorizontalRuler = new bool?();
    this.newVerticalRuler = new bool?();
    this.newShowPopupBarOnResize = new bool?();
    this.newDefaultParagraphFormat = (ParagraphFormat) null;
    this.newDefaultCharFormat = (CharFormat) null;
    this.newShowDebugInfo = new bool?();
    this.newCreateLog = new bool?();
    this.neweditOleAsFiles = new bool?();
    this.newShowSingleCellInTemplate = new bool?();
    this.newAllowDebugMode = new bool?();
    this.newDefaultFileNameSource = new DefaultFileNameSource?();
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    IConfiguration configuration = configurationManager.Open("ImDocEditor");
    if (configuration != null)
    {
      string property1 = configuration.GetProperty("ShowInvisibleLines");
      if (property1 != null && property1 != "")
        this.showInvisibleLines = bool.Parse(property1);
      string property2 = configuration.GetProperty("SpellCheck");
      if (property2 != null && property2 != "")
        this.spellCheck = bool.Parse(property2);
      string property3 = configuration.GetProperty("GridSize");
      if (property3 != null && property3 != "")
        this.gridSize = float.Parse(property3, (IFormatProvider) CultureInfo.InvariantCulture);
      string property4 = configuration.GetProperty("SnapSize");
      if (property4 != null && property4 != "")
        this.snapSize = float.Parse(property4, (IFormatProvider) CultureInfo.InvariantCulture);
      string property5 = configuration.GetProperty("CoorSystem");
      if (property5 != null && property5 != "")
        this.coorSystem = (PageCoorSystem) Enum.Parse(typeof (PageCoorSystem), property5);
      string property6 = configuration.GetProperty("customCoorSystemX");
      if (property6 != null && property6 != "")
        this.customCoorSystemPosition.X = float.Parse(property6);
      string property7 = configuration.GetProperty("customCoorSystemY");
      if (property7 != null && property7 != "")
        this.customCoorSystemPosition.Y = float.Parse(property7);
      string property8 = configuration.GetProperty("ShowGeometryDlgOnCreate");
      if (property8 != null && property8 != "")
        this.showGeometryDlgOnCreate = bool.Parse(property8);
      string property9 = configuration.GetProperty("CreateLog");
      if (property9 != null && property9 != "")
        this.CreateLog = bool.Parse(property9);
      string property10 = configuration.GetProperty("CorrectDecimalSeparator");
      if (property10 != null && property10 != "")
        FloatConverter.CorrectDecimalSeparator = bool.Parse(property10);
      string property11 = configuration.GetProperty("HorizontalRuler");
      if (property11 != null && property11 != "")
        this.horizontalRuler = bool.Parse(property11);
      string property12 = configuration.GetProperty("VerticalRuler");
      if (property12 != null && property12 != "")
        this.verticalRuler = bool.Parse(property12);
      string property13 = configuration.GetProperty("ShowPopupBarOnResize");
      if (property13 != null && property13 != "")
        this.showPopupBarOnResize = bool.Parse(property13);
      this.defaultParagraphFormat = TextData.DefaultParagraphFormat.Clone();
      string property14 = configuration.GetProperty("HorzAlignmentText");
      if (string.IsNullOrEmpty(property14))
        property14 = configuration.GetProperty("PF.HorzAlignment");
      if (!string.IsNullOrEmpty(property14))
        this.defaultParagraphFormat.HorzAlignment = new HorzAlignment?((HorzAlignment) int.Parse(property14, (IFormatProvider) CultureInfo.InvariantCulture));
      string property15 = configuration.GetProperty("VertAlignmentText");
      if (string.IsNullOrEmpty(property15))
        property15 = configuration.GetProperty("PF.VertAlignment");
      if (!string.IsNullOrEmpty(property15))
        this.defaultParagraphFormat.VertAlignment = new VertAlignment?((VertAlignment) int.Parse(property15, (IFormatProvider) CultureInfo.InvariantCulture));
      string property16 = configuration.GetProperty("PF.LineSpacingMethod");
      if (!string.IsNullOrEmpty(property16))
        this.defaultParagraphFormat.LineSpacingMethod = new LineSpacingMethod?((LineSpacingMethod) int.Parse(property16, (IFormatProvider) CultureInfo.InvariantCulture));
      string property17 = configuration.GetProperty("PF.SpaceBetweenLines");
      if (!string.IsNullOrEmpty(property17))
        this.defaultParagraphFormat.SpaceBetweenLines = new float?(float.Parse(property17, (IFormatProvider) CultureInfo.InvariantCulture));
      this.defaultCharFormat = TextData.DefaultCharFormat.Clone();
      string property18 = configuration.GetProperty("CF.FontFamily");
      if (!string.IsNullOrEmpty(property18))
        this.defaultCharFormat.FontFamily = property18;
      string property19 = configuration.GetProperty("CF.BoldItalic");
      if (!string.IsNullOrEmpty(property19))
        this.defaultCharFormat.BoldItalic = new BoldItalicStyle?((BoldItalicStyle) int.Parse(property19, (IFormatProvider) CultureInfo.InvariantCulture));
      string property20 = configuration.GetProperty("CF.FontSize");
      if (!string.IsNullOrEmpty(property20))
        this.defaultCharFormat.FontSize = new float?(float.Parse(property20, (IFormatProvider) CultureInfo.InvariantCulture));
      string property21 = configuration.GetProperty("CF.FontSizeMm");
      if (!string.IsNullOrEmpty(property21))
        this.defaultCharFormat.FontSizeMm = new float?(float.Parse(property21, (IFormatProvider) CultureInfo.InvariantCulture));
      string property22 = configuration.GetProperty("EditOleAsFiles");
      if (property22 != null && property22 != "")
        this.editOleAsFiles = bool.Parse(property22);
      string property23 = configuration.GetProperty("ShowSingleCellInTemplate");
      if (!string.IsNullOrEmpty(property23))
        this.showSingleCellInTemplate = bool.Parse(property23);
      string property24 = configuration.GetProperty("DefaultFileNameSource");
      if (!string.IsNullOrEmpty(property24))
        this.defaultFileNameSource = (DefaultFileNameSource) int.Parse(property24);
      this.newDefaultCharFormat = (CharFormat) null;
      this.newDefaultParagraphFormat = (ParagraphFormat) null;
      this.newShowInvisibleLines = new bool?();
      this.newGridSize = new float?();
      this.newCoorSystem = new PageCoorSystem?();
      this.newCustomCoorSystemPosition = new PointF?();
      this.newShowGeometryDlgOnCreate = new bool?();
      this.newCorrectDecimalSeparator = new bool?();
      this.newHorizontalRuler = new bool?();
      this.newVerticalRuler = new bool?();
      this.newShowPopupBarOnResize = new bool?();
      this.neweditOleAsFiles = new bool?();
      this.newShowSingleCellInTemplate = new bool?();
      this.newDefaultFileNameSource = new DefaultFileNameSource?();
    }
    this.LoadDocumentPrintersSettings();
    this.LoadRecentSymbolsSettings();
  }

  public void LoadFromBase()
  {
    this.allowDebugMode = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("ImDocEditor", "Global", "AllowDebugMode", false, DBConfigMode.GlobalOnly);
    bool flag = ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service && service.IsAdmin;
    if (!ImDocumentData.ShowDebugInfo || flag || this.allowDebugMode)
      return;
    ImDocumentData.ShowDebugInfo = false;
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    IConfiguration configuration1 = configurationManager.Open("ImDocEditor") ?? configurationManager.Create("ImDocEditor");
    configuration1.SetProperty("ShowInvisibleLines", this.showInvisibleLines.ToString());
    configuration1.SetProperty("SpellCheck", this.spellCheck.ToString());
    configuration1.SetProperty("GridSize", this.gridSize.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    configuration1.SetProperty("SnapSize", this.snapSize.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    configuration1.SetProperty("CoorSystem", this.coorSystem.ToString());
    configuration1.SetProperty("customCoorSystemX", this.customCoorSystemPosition.X.ToString());
    configuration1.SetProperty("customCoorSystemY", this.customCoorSystemPosition.Y.ToString());
    configuration1.SetProperty("ShowGeometryDlgOnCreate", this.showGeometryDlgOnCreate.ToString());
    configuration1.SetProperty("CreateLog", this.CreateLog.ToString());
    configuration1.SetProperty("CorrectDecimalSeparator", FloatConverter.CorrectDecimalSeparator.ToString());
    if (this.horizontalRuler)
      configuration1.SetProperty("HorizontalRuler", this.horizontalRuler.ToString());
    else
      configuration1.RemoveProperty("HorizontalRuler");
    if (this.verticalRuler)
      configuration1.SetProperty("VerticalRuler", this.verticalRuler.ToString());
    else
      configuration1.RemoveProperty("VerticalRuler");
    configuration1.SetProperty("ShowPopupBarOnResize", this.showPopupBarOnResize.ToString());
    IConfiguration configuration2 = configuration1;
    int defaultFileNameSource = (int) this.defaultParagraphFormat.HorzAlignment.Value;
    string str1 = defaultFileNameSource.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration2.SetProperty("PF.HorzAlignment", str1);
    IConfiguration configuration3 = configuration1;
    defaultFileNameSource = (int) this.defaultParagraphFormat.VertAlignment.Value;
    string str2 = defaultFileNameSource.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration3.SetProperty("PF.VertAlignment", str2);
    IConfiguration configuration4 = configuration1;
    defaultFileNameSource = (int) this.defaultParagraphFormat.LineSpacingMethod.Value;
    string str3 = defaultFileNameSource.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration4.SetProperty("PF.LineSpacingMethod", str3);
    float num;
    if (this.defaultParagraphFormat.SpaceBetweenLines.HasValue)
    {
      IConfiguration configuration5 = configuration1;
      num = this.defaultParagraphFormat.SpaceBetweenLines.Value;
      string str4 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      configuration5.SetProperty("PF.SpaceBetweenLines", str4);
    }
    else
      configuration1.RemoveProperty("PF.SpaceBetweenLines");
    configuration1.SetProperty("CF.FontFamily", this.defaultCharFormat.FontFamily);
    IConfiguration configuration6 = configuration1;
    defaultFileNameSource = (int) this.defaultCharFormat.BoldItalic.Value;
    string str5 = defaultFileNameSource.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    configuration6.SetProperty("CF.BoldItalic", str5);
    float? nullable = this.defaultCharFormat.FontSize;
    if (nullable.HasValue)
    {
      IConfiguration configuration7 = configuration1;
      nullable = this.defaultCharFormat.FontSize;
      num = nullable.Value;
      string str6 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      configuration7.SetProperty("CF.FontSize", str6);
    }
    else
      configuration1.RemoveProperty("CF.FontSize");
    nullable = this.defaultCharFormat.FontSizeMm;
    if (nullable.HasValue)
    {
      IConfiguration configuration8 = configuration1;
      nullable = this.defaultCharFormat.FontSizeMm;
      num = nullable.Value;
      string str7 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      configuration8.SetProperty("CF.FontSizeMm", str7);
    }
    else
      configuration1.RemoveProperty("CF.FontSizeMm");
    configuration1.SetProperty("ShowSingleCellInTemplate", this.showSingleCellInTemplate.ToString());
    this.SaveDocumentPrintersSettings(false);
    configuration1.SetProperty("EditOleAsFiles", this.editOleAsFiles.ToString());
    IConfiguration configuration9 = configuration1;
    defaultFileNameSource = (int) this.defaultFileNameSource;
    string str8 = defaultFileNameSource.ToString();
    configuration9.SetProperty("DefaultFileNameSource", str8);
    this.SaveRecentSpecSymbols();
  }

  public void SaveToBase()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      if ((!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) ? 0 : (service.IsAdmin ? 1 : 0)) == 0)
        return;
      configurations.WriteBool("ImDocEditor", "Global", "AllowDebugMode", this.AllowDebugMode, 0L);
    }
  }
}
