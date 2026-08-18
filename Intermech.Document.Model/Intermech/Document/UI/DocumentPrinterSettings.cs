// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.DocumentPrinterSettings
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Настройки документа под конкретный принтер</summary>
public class DocumentPrinterSettings
{
  /// <summary>Смещение страниц</summary>
  public PointF ShiftPage;

  /// <summary>Конструктор</summary>
  /// <param name="shiftPage">Смещение страниц</param>
  public DocumentPrinterSettings(PointF shiftPage) => this.ShiftPage = shiftPage;
}
