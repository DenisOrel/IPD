// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PdfGenerator.ExcelConverter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Document.Model.PdfGenerator;

/// <summary>Класс для конвертации документа в excel</summary>
public class ExcelConverter
{
  /// <summary>Метод сохранения</summary>
  /// <param name="docs">Список документов для сохранения</param>
  /// <param name="outputStream">Поток куда сохранять</param>
  /// <param name="fileName">Файл куда сохранять если не задан поток</param>
  /// <param name="autoStart">Запустить excel после сохранения</param>
  /// <param name="showProgress">Показывать прогресс сохранения</param>
  public static void Save(
    ImDocumentData[] docs,
    Stream outputStream,
    string fileName,
    bool autoStart,
    bool showProgress)
  {
    if (showProgress)
    {
      BackgroundWorker worker = new BackgroundWorker();
      worker.WorkerReportsProgress = true;
      worker.WorkerSupportsCancellation = true;
      worker.DoWork += new DoWorkEventHandler(ExcelConverter.bw_DoWork);
      worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExcelConverter.bw_RunWorkerCompleted);
      object obj = (object) new object[4]
      {
        (object) docs,
        (object) outputStream,
        (object) fileName,
        (object) autoStart
      };
      int totalpages = ((IEnumerable<ImDocumentData>) docs).Sum<ImDocumentData>((Func<ImDocumentData, int>) (d => d.Count<PageData>()));
      int num = (int) new ProgressPdfForm(worker, totalpages, obj).ShowDialog();
    }
    else
      ExcelConverter.SaveInThread((BackgroundWorker) null, docs, outputStream, fileName, autoStart);
  }

  private static void bw_DoWork(object sender, DoWorkEventArgs e)
  {
    Array array = e.Argument as Array;
    ImDocumentData[] docs = array.GetValue(0) as ImDocumentData[];
    Stream outputStream = array.GetValue(1) as Stream;
    string fileName = array.GetValue(2) as string;
    bool flag = (bool) array.GetValue(3);
    object[] objArray = new object[2]
    {
      (object) (ExcelConverter.SaveInThread(sender as BackgroundWorker, docs, outputStream, fileName, false) & flag),
      (object) fileName
    };
    e.Result = (object) objArray;
  }

  private static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    Array result = e.Result as Array;
    bool flag = (bool) result.GetValue(0);
    string fileName = result.GetValue(1) as string;
    if (!flag)
      return;
    Process.Start(fileName);
  }

  private static bool SaveInThread(
    BackgroundWorker bw,
    ImDocumentData[] docs,
    Stream outputStream,
    string fileName,
    bool autoStart)
  {
    List<PageData> pageDataList = new List<PageData>();
    foreach (ImDocumentData doc in docs)
      pageDataList.AddRange((IEnumerable<PageData>) doc.GetAllPages());
    ExcelPackage excelPackage = new ExcelPackage();
    foreach (PageData node in pageDataList)
    {
      ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add(node.ComplectPageNumber.ToString());
      List<Decimal> coordinatesX = new List<Decimal>();
      sheet.Cells["A:XFD"].Style.Font.Name = node.OwnerDocument.DefaultCharFormat.FontFamily;
      sheet.Cells["A:XFD"].Style.Font.Size = node.OwnerDocument.DefaultCharFormat.FontSize.HasValue ? node.OwnerDocument.DefaultCharFormat.FontSize.Value : 11f;
      List<Decimal> snapPointsD1 = ExcelConverter.GetSnapPointsD((DocumentTreeNode) node, true);
      if (!snapPointsD1.Contains(0M))
        snapPointsD1.Insert(0, 0M);
      foreach (Decimal num in snapPointsD1)
      {
        if (!coordinatesX.Contains(num))
          coordinatesX.Add(num);
      }
      int num1 = 1;
      for (int index = 0; index < coordinatesX.Count; ++index)
      {
        Decimal mm = coordinatesX[index];
        if (!(mm == 0M))
        {
          if (index > 0)
            mm -= coordinatesX[index - 1];
          double width = (double) Converter.MmToWidth((float) mm, 96f);
          double num2 = (double) Converter.RoundD(Converter.PixelsToCharacters(Converter.MmToPixels((float) mm, 96f)) / 1.08f);
          sheet.Cells[1, num1, 1, num1].Value = (object) " ";
          sheet.Column(num1).Width = num2;
          ++num1;
        }
      }
      int index1 = 0;
      int num3 = 1;
      List<Decimal> snapPointsD2 = ExcelConverter.GetSnapPointsD((DocumentTreeNode) node, false);
      if (!snapPointsD2.Contains(0M))
        snapPointsD2.Insert(0, 0M);
      List<Decimal> numList = new List<Decimal>();
      for (int index2 = 0; index2 < snapPointsD2.Count; ++index2)
      {
        Decimal num4 = snapPointsD2[index2];
        if (index2 > 0)
          num4 -= snapPointsD2[index2 - 1];
        if (num4 > 100M)
        {
          Decimal num5 = num4;
          Decimal num6 = 0M;
          if (index2 > 0)
            num6 = snapPointsD2[index2 - 1];
          for (; num5 > 100M; num5 -= 100M)
          {
            num6 += 100M;
            if (num6 < snapPointsD2[index2])
              numList.Add(num6);
          }
        }
        numList.Add(snapPointsD2[index2]);
      }
      List<Decimal> coordinatesY = numList;
      for (int index3 = 0; index3 < coordinatesY.Count; ++index3)
      {
        Decimal mm = coordinatesY[index3];
        if (!(mm == 0M))
        {
          if (index3 > 0)
            mm -= coordinatesY[index3 - 1];
          Decimal points = (Decimal) Converter.MmToPoints((float) mm);
          sheet.Cells[num3, 1, num3, 1].Value = (object) " ";
          sheet.Row(num3).Height = (double) (int) points;
          ++num3;
        }
      }
      if (node.Landscape)
        sheet.PrinterSettings.Orientation = eOrientation.Landscape;
      ExcelConverter.ExportItem(sheet, (DocumentTreeNode) node, coordinatesX, coordinatesY, index1);
      int num7 = num3 - 1;
    }
    FileStream OutputStream = (FileStream) null;
    if (outputStream == null)
    {
      OutputStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
      outputStream = (Stream) OutputStream;
    }
    excelPackage.SaveAs((Stream) OutputStream);
    if (OutputStream != null)
    {
      OutputStream.Dispose();
      if (autoStart)
        Process.Start(fileName);
    }
    return true;
  }

  /// <summary>экспорт элемента</summary>
  /// <param name="sheet">Страница</param>
  /// <param name="item">Элемент</param>
  /// <param name="coordinatesX">Список горизонтальных координат</param>
  /// <param name="coordinatesY">Список вертикальных координат</param>
  /// <param name="index">Координата с которой начинается отсчет текущей страницы, если несколько страниц на листе</param>
  private static void ExportItem(
    ExcelWorksheet sheet,
    DocumentTreeNode item,
    List<Decimal> coordinatesX,
    List<Decimal> coordinatesY,
    int index)
  {
    if (item.Nodes == null)
      return;
    foreach (DocumentTreeNode node in item.Nodes)
    {
      if (node is RectangleElement rect1)
      {
        if (rect1.IsVisibleNow)
        {
          RectangleF bounds = rect1.Bounds;
          if ((double) bounds.Height >= 1.0)
          {
            bounds = rect1.Bounds;
            if ((double) bounds.Width >= 1.0)
            {
              if (rect1 is TableElement && (rect1 as TableElement).IsRow)
                ExcelConverter.SetSkipLines(sheet, rect1, coordinatesX, coordinatesY, index);
              List<Decimal> numList1 = coordinatesX;
              bounds = rect1.Bounds;
              Decimal num1 = Converter.RoundD(bounds.Left);
              int num2 = numList1.IndexOf(num1) + 1;
              List<Decimal> numList2 = coordinatesY;
              bounds = rect1.Bounds;
              Decimal num3 = Converter.RoundD(bounds.Top);
              int num4 = numList2.IndexOf(num3) + 1 + index;
              List<Decimal> numList3 = coordinatesX;
              bounds = rect1.Bounds;
              Decimal num5 = Converter.RoundD(bounds.Right);
              int num6 = numList3.IndexOf(num5);
              List<Decimal> numList4 = coordinatesY;
              bounds = rect1.Bounds;
              Decimal num7 = Converter.RoundD(bounds.Bottom);
              int num8 = numList4.IndexOf(num7) + index;
              bounds = rect1.Bounds;
              Converter.RoundD(bounds.Bottom);
              try
              {
                if (num2 >= 1)
                {
                  if (num6 >= 1)
                  {
                    if (num4 >= 1)
                    {
                      if (num8 >= 1)
                      {
                        if (num6 >= num2)
                        {
                          if (num8 >= num4)
                          {
                            ExcelRange cell1 = sheet.Cells[num4, num2, num8, num6];
                            TextData text = rect1 as TextData;
                            if ((num4 - num8 != 0 || num6 - num2 != 0) && (rect1.NodesCount == 0 && rect1.IsCellInDataFlowTable || text != null && text.Text != null && text.Text != string.Empty))
                            {
                              try
                              {
                                bool flag = true;
                                int num9 = rect1.Id == "5119" ? 1 : 0;
                                List<VisualNode> elements = new List<VisualNode>();
                                RectangleF rect;
                                ref RectangleF local = ref rect;
                                bounds = rect1.Bounds;
                                double x = (double) bounds.Left + 1.0;
                                bounds = rect1.Bounds;
                                double y = (double) bounds.Top + 1.0;
                                bounds = rect1.Bounds;
                                double width = (double) bounds.Width - 2.0;
                                bounds = rect1.Bounds;
                                double height = (double) bounds.Height - 2.0;
                                local = new RectangleF((float) x, (float) y, (float) width, (float) height);
                                rect1.Page.FindPageElementsInRectangle(rect, elements, false, true);
                                foreach (VisualNode visualNode in elements)
                                {
                                  if (visualNode is TextData textData && textData != rect1)
                                    flag &= string.IsNullOrWhiteSpace(textData.Text);
                                }
                                if (flag)
                                  cell1.Merge = true;
                              }
                              catch (Exception ex)
                              {
                              }
                            }
                            ExcelRange cell2 = sheet.Cells[num4, num2];
                            if (text != null)
                              ExcelConverter.SetText(text, cell1, cell2);
                            if (rect1 != null)
                            {
                              Rectangle rect = Rectangle.FromLTRB(num2, num4, num6, num8);
                              ExcelConverter.SetBorder(rect1, cell1, ExcelConverter.BorderType.Left, rect);
                              ExcelConverter.SetBorder(rect1, cell1, ExcelConverter.BorderType.Right, rect);
                              ExcelConverter.SetBorder(rect1, cell1, ExcelConverter.BorderType.Top, rect);
                              ExcelConverter.SetBorder(rect1, cell1, ExcelConverter.BorderType.Bottom, rect);
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
              catch
              {
              }
            }
            else
              continue;
          }
          else
            continue;
        }
        else
          continue;
      }
      ExcelConverter.ExportItem(sheet, node, coordinatesX, coordinatesY, index);
    }
  }

  /// <summary>Установка пропусков строк</summary>
  /// <param name="sheet">Страница</param>
  /// <param name="rect">Элемент с пропусками</param>
  /// <param name="coordinatesX">Список горизонтальных координат</param>
  /// <param name="coordinatesY">Список вертикальных координат</param>
  /// <param name="index"></param>
  public static void SetSkipLines(
    ExcelWorksheet sheet,
    RectangleElement rect,
    List<Decimal> coordinatesX,
    List<Decimal> coordinatesY,
    int index)
  {
    float oneSkipSize = rect.OneSkipSize;
    RectangleElement rectangleElement1 = (RectangleElement) null;
    if (rect.NodesCount > 0)
      rectangleElement1 = rect.Nodes[0] as RectangleElement;
    if ((double) oneSkipSize == 0.0 || (double) rect.Bounds.Height <= (double) oneSkipSize || rectangleElement1 == null)
      return;
    float num1 = rect.Bounds.Top;
    float num2 = num1 + oneSkipSize;
    while (true)
    {
      double num3 = (double) num2;
      RectangleF bounds = rect.Bounds;
      double bottom1 = (double) bounds.Bottom;
      if (num3 <= bottom1)
      {
        bounds = rectangleElement1.Bounds;
        if ((double) bounds.Top != (double) num1)
        {
          bounds = rectangleElement1.Bounds;
          if ((double) bounds.Bottom != (double) num2)
          {
            foreach (object node in rect.Nodes)
            {
              if (node is RectangleElement rectangleElement2 && rectangleElement2.NodesCount == 0)
              {
                bounds = rectangleElement2.Bounds;
                double left = (double) bounds.Left;
                double top = (double) num1;
                bounds = rectangleElement2.Bounds;
                double right = (double) bounds.Right;
                double bottom2 = (double) num2;
                RectangleF rectangleF = RectangleF.FromLTRB((float) left, (float) top, (float) right, (float) bottom2);
                int num4 = coordinatesX.IndexOf(Converter.RoundD(rectangleF.Left)) + 1;
                int num5 = coordinatesY.IndexOf(Converter.RoundD(rectangleF.Top)) + 1 + index;
                int num6 = coordinatesX.IndexOf(Converter.RoundD(rectangleF.Right));
                int num7 = coordinatesY.IndexOf(Converter.RoundD(rectangleF.Bottom)) + index;
                Converter.RoundD(rectangleF.Bottom);
                try
                {
                  if (num4 >= 1)
                  {
                    if (num6 >= 1)
                    {
                      if (num5 >= 1)
                      {
                        if (num7 >= 1)
                        {
                          if (num6 >= num4)
                          {
                            if (num7 >= num5)
                            {
                              ExcelRange cell = sheet.Cells[num5, num4, num7, num6];
                              if (num5 - num7 != 0 || num6 - num4 != 0)
                              {
                                try
                                {
                                  cell.Merge = true;
                                }
                                catch (Exception ex)
                                {
                                }
                              }
                              if (rect != null)
                              {
                                Rectangle rect1 = Rectangle.FromLTRB(num4, num5, num6, num7);
                                ExcelConverter.SetBorder(rectangleElement2, cell, ExcelConverter.BorderType.InnerLeft, rect1);
                                ExcelConverter.SetBorder(rectangleElement2, cell, ExcelConverter.BorderType.InnerRight, rect1);
                                ExcelConverter.SetBorder(rectangleElement2, cell, ExcelConverter.BorderType.InnerTop, rect1);
                                ExcelConverter.SetBorder(rectangleElement2, cell, ExcelConverter.BorderType.InnerBottom, rect1);
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
        num1 = num2;
        num2 += oneSkipSize;
      }
      else
        break;
    }
  }

  public static bool TryParse(string value, out double res)
  {
    return double.TryParse(ExcelConverter.CorrectDecimal(value, (CultureInfo) null), out res);
  }

  /// <summary>Исправить десятичный разделитель ',' или '.' на системный</summary>
  public static string CorrectDecimal(string value, CultureInfo culture)
  {
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    if (value != null && value != "")
    {
      if (culture.NumberFormat.NumberDecimalSeparator != ",")
        value = value.Replace(",", culture.NumberFormat.NumberDecimalSeparator);
      if (culture.NumberFormat.NumberDecimalSeparator != ".")
        value = value.Replace(".", culture.NumberFormat.NumberDecimalSeparator);
    }
    return value;
  }

  /// <summary>Установить текст в ячейку</summary>
  /// <param name="text">Элемент документа</param>
  /// <param name="range">Ячейка куда установить</param>
  /// <param name="ecell">Верхняя левая ячейка</param>
  private static void SetText(TextData text, ExcelRange range, ExcelRange ecell)
  {
    ExcelStyle style = ecell.Style;
    if (text.Text == null || text.Text == string.Empty)
      return;
    int num1 = text.Id == "21" ? 1 : 0;
    if (text.CharFormat.GetFont().Bold)
      style.Font.Bold = true;
    if (text.CharFormat.GetFont().Italic)
      style.Font.Italic = true;
    if (text.CharFormat.GetFont().Strikeout)
      style.Font.Strike = true;
    style.Font.Size = (float) (int) text.CharFormat.GetFont().Size;
    ExcelHorizontalAlignment horizontalAlignment = ExcelHorizontalAlignment.Left;
    HorzAlignment? horzAlignment1 = text.ParagraphFormat.HorzAlignment;
    HorzAlignment horzAlignment2 = HorzAlignment.Center;
    if (horzAlignment1.GetValueOrDefault() == horzAlignment2 & horzAlignment1.HasValue)
      horizontalAlignment = ExcelHorizontalAlignment.Center;
    horzAlignment1 = text.ParagraphFormat.HorzAlignment;
    HorzAlignment horzAlignment3 = HorzAlignment.Right;
    if (horzAlignment1.GetValueOrDefault() == horzAlignment3 & horzAlignment1.HasValue)
      horizontalAlignment = ExcelHorizontalAlignment.Right;
    ExcelVerticalAlignment verticalAlignment = ExcelVerticalAlignment.Bottom;
    VertAlignment? vertAlignment1 = text.ParagraphFormat.VertAlignment;
    VertAlignment vertAlignment2 = VertAlignment.Center;
    if (vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue)
      verticalAlignment = ExcelVerticalAlignment.Center;
    vertAlignment1 = text.ParagraphFormat.VertAlignment;
    VertAlignment vertAlignment3 = VertAlignment.Top;
    if (vertAlignment1.GetValueOrDefault() == vertAlignment3 & vertAlignment1.HasValue)
      verticalAlignment = ExcelVerticalAlignment.Top;
    if (text.ParagraphFormat.IdentLeft.HasValue)
    {
      double num2 = (double) Converter.RoundD(Converter.PixelsToCharacters(Converter.MmToPixels(text.ParagraphFormat.IdentLeft.Value, 96f)) / 1.08f) * 8.5;
      style.Indent = (int) num2;
    }
    style.WrapText = !text.ParagraphFormat.DisableWordWrap.Value;
    double res = 0.0;
    if (range.Merge)
    {
      if (ExcelConverter.TryParse(text.Text, out res))
        range.Value = (object) res;
      else
        range.Value = (object) text.Text;
    }
    else if (ExcelConverter.TryParse(text.Text, out res))
      ecell.Value = (object) res;
    else
      ecell.Value = (object) text.Text;
    switch (text.Orientation)
    {
      case TextOrientation.DownTop:
        style.TextRotation = 90;
        switch (verticalAlignment)
        {
          case ExcelVerticalAlignment.Top:
            verticalAlignment = ExcelVerticalAlignment.Bottom;
            break;
          case ExcelVerticalAlignment.Bottom:
            verticalAlignment = ExcelVerticalAlignment.Top;
            break;
        }
        break;
      case TextOrientation.UpsideDown:
        switch (verticalAlignment)
        {
          case ExcelVerticalAlignment.Top:
            verticalAlignment = ExcelVerticalAlignment.Bottom;
            break;
          case ExcelVerticalAlignment.Bottom:
            verticalAlignment = ExcelVerticalAlignment.Top;
            break;
        }
        switch (horizontalAlignment)
        {
          case ExcelHorizontalAlignment.Left:
            horizontalAlignment = ExcelHorizontalAlignment.Right;
            break;
          case ExcelHorizontalAlignment.Right:
            horizontalAlignment = ExcelHorizontalAlignment.Left;
            break;
        }
        break;
      case TextOrientation.TopDown:
        style.TextRotation = 180;
        switch (horizontalAlignment)
        {
          case ExcelHorizontalAlignment.Left:
            horizontalAlignment = ExcelHorizontalAlignment.Right;
            break;
          case ExcelHorizontalAlignment.Right:
            horizontalAlignment = ExcelHorizontalAlignment.Left;
            break;
        }
        break;
    }
    style.HorizontalAlignment = horizontalAlignment;
    style.VerticalAlignment = verticalAlignment;
    string name = text.CharFormat.GetFont().FontFamily.Name;
    style.Font.Name = name;
    style.Font.Color.SetColor(text.ForeColor);
  }

  /// <summary>Установка границ</summary>
  /// <param name="item">Элемент документа</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="type">Тип границы</param>
  /// <param name="rect">Прямоугольник координат</param>
  private static void SetBorder(
    RectangleElement item,
    ExcelRange cell,
    ExcelConverter.BorderType type,
    Rectangle rect)
  {
    BorderLine borderLine = (BorderLine) null;
    switch (type)
    {
      case ExcelConverter.BorderType.Left:
        borderLine = item.Borders.Left;
        break;
      case ExcelConverter.BorderType.Right:
        borderLine = item.Borders.Right;
        break;
      case ExcelConverter.BorderType.Top:
        borderLine = item.Borders.Top;
        break;
      case ExcelConverter.BorderType.Bottom:
        borderLine = item.Borders.Bottom;
        break;
      case ExcelConverter.BorderType.InnerLeft:
        borderLine = item.Borders.Left;
        break;
      case ExcelConverter.BorderType.InnerRight:
        borderLine = item.Borders.Right;
        break;
      case ExcelConverter.BorderType.InnerTop:
        borderLine = item.Borders.Top;
        break;
      case ExcelConverter.BorderType.InnerBottom:
        borderLine = item.Borders.Bottom;
        break;
    }
    ExcelBorderStyle style = ExcelBorderStyle.Thin;
    if ((double) borderLine.Width > 0.5)
      style = ExcelBorderStyle.Medium;
    if ((double) borderLine.Width > 1.0)
      style = ExcelBorderStyle.Thick;
    if (borderLine.Style == BorderStyles.Dash)
      style = ExcelBorderStyle.Dashed;
    if (borderLine.Style == BorderStyles.DashDot)
      style = ExcelBorderStyle.DashDot;
    if (borderLine.Style == BorderStyles.DashDotDot)
      style = ExcelBorderStyle.DashDotDot;
    if (borderLine.Style == BorderStyles.Dot)
      style = ExcelBorderStyle.Dotted;
    if (borderLine.Style == BorderStyles.None)
      style = ExcelBorderStyle.None;
    if (type == ExcelConverter.BorderType.Top || type == ExcelConverter.BorderType.InnerTop)
    {
      for (int left = rect.Left; left <= rect.Right; ++left)
        ExcelConverter.SetBorder(cell[rect.Top, left], type, style, borderLine.Color);
    }
    if (type == ExcelConverter.BorderType.Bottom || type == ExcelConverter.BorderType.InnerBottom)
    {
      for (int left = rect.Left; left <= rect.Right; ++left)
        ExcelConverter.SetBorder(cell[rect.Bottom, left], type, style, borderLine.Color);
    }
    if (type == ExcelConverter.BorderType.Left || type == ExcelConverter.BorderType.InnerLeft)
    {
      for (int top = rect.Top; top <= rect.Bottom; ++top)
        ExcelConverter.SetBorder(cell[top, rect.Left], type, style, borderLine.Color);
    }
    if (type != ExcelConverter.BorderType.Right && type != ExcelConverter.BorderType.InnerRight)
      return;
    for (int top = rect.Top; top <= rect.Bottom; ++top)
      ExcelConverter.SetBorder(cell[top, rect.Right], type, style, borderLine.Color);
  }

  /// <summary>Установка границы</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="type">Тип границы</param>
  /// <param name="style">Стиль границы</param>
  /// <param name="color">Цвет границы</param>
  private static void SetBorder(
    ExcelRange cell,
    ExcelConverter.BorderType type,
    ExcelBorderStyle style,
    Color color)
  {
    ExcelBorderItem excelBorderItem = (ExcelBorderItem) null;
    switch (type)
    {
      case ExcelConverter.BorderType.Left:
      case ExcelConverter.BorderType.InnerLeft:
        excelBorderItem = cell.Style.Border.Left;
        break;
      case ExcelConverter.BorderType.Right:
      case ExcelConverter.BorderType.InnerRight:
        excelBorderItem = cell.Style.Border.Right;
        break;
      case ExcelConverter.BorderType.Top:
      case ExcelConverter.BorderType.InnerTop:
        excelBorderItem = cell.Style.Border.Top;
        break;
      case ExcelConverter.BorderType.Bottom:
      case ExcelConverter.BorderType.InnerBottom:
        excelBorderItem = cell.Style.Border.Bottom;
        break;
    }
    ExcelBorderStyle style1 = excelBorderItem.Style;
    if (style1 == ExcelBorderStyle.Thick && (style == ExcelBorderStyle.None || style == ExcelBorderStyle.Thin || style == ExcelBorderStyle.Medium))
      style = style1;
    if (style1 == ExcelBorderStyle.Medium && (style == ExcelBorderStyle.None || style == ExcelBorderStyle.Thin))
      style = style1;
    if (style1 == ExcelBorderStyle.Thin && style == ExcelBorderStyle.None)
      style = style1;
    if (style != ExcelBorderStyle.None)
    {
      excelBorderItem.Style = style;
      excelBorderItem.Color.SetColor(color);
    }
    else
      excelBorderItem.Style = style;
  }

  /// <summary>Получить список координат сетки</summary>
  /// <param name="node">Корневой узел</param>
  /// <param name="coordinates">Список координат  ширин или высот</param>
  /// <returns></returns>
  internal static List<Decimal> GetSnapPointsD(DocumentTreeNode node, bool xcoordinates)
  {
    List<float> snapPoints1 = ExcelConverter.GetSnapPoints1(node, xcoordinates);
    List<Decimal> snapPointsD = new List<Decimal>();
    foreach (float point in snapPoints1)
      snapPointsD.Add(Converter.RoundD(point));
    return snapPointsD;
  }

  /// <summary>Получить список координат сетки</summary>
  /// <param name="node">Элемент</param>
  /// <param name="xcoordinates">Список координат ширин или высот</param>
  /// <returns></returns>
  private static List<float> GetSnapPoints(DocumentTreeNode node, bool xcoordinates)
  {
    List<float> snapPoints = new List<float>();
    if (node.Nodes != null)
    {
      foreach (DocumentTreeNode node1 in node.Nodes)
        snapPoints.AddRange((IEnumerable<float>) ExcelConverter.GetSnapPoints(node1, xcoordinates));
    }
    if (node is PageData)
    {
      if (xcoordinates)
        snapPoints.Add((node as PageData).Size.Width);
      else
        snapPoints.Add((node as PageData).Size.Height);
    }
    if (node is RectangleElement rectangleElement && (double) rectangleElement.Bounds.Height >= 1.0 && (double) rectangleElement.Bounds.Width >= 1.0)
    {
      if (xcoordinates)
      {
        List<float> floatList1 = snapPoints;
        RectangleF bounds = rectangleElement.Bounds;
        double left = (double) bounds.Left;
        floatList1.Add((float) left);
        List<float> floatList2 = snapPoints;
        bounds = rectangleElement.Bounds;
        double right = (double) bounds.Right;
        floatList2.Add((float) right);
      }
      else
      {
        List<float> floatList3 = snapPoints;
        RectangleF bounds = rectangleElement.Bounds;
        double top = (double) bounds.Top;
        floatList3.Add((float) top);
        List<float> floatList4 = snapPoints;
        bounds = rectangleElement.Bounds;
        double bottom1 = (double) bounds.Bottom;
        floatList4.Add((float) bottom1);
        if (node is TableElement)
        {
          TableElement tableElement = node as TableElement;
          if (tableElement.IsRow)
          {
            float oneSkipSize = tableElement.OneSkipSize;
            if ((double) oneSkipSize != 0.0)
            {
              bounds = rectangleElement.Bounds;
              float num1 = bounds.Top + oneSkipSize;
              while (true)
              {
                double num2 = (double) num1;
                bounds = rectangleElement.Bounds;
                double bottom2 = (double) bounds.Bottom;
                if (num2 < bottom2)
                {
                  snapPoints.Add(num1);
                  num1 += oneSkipSize;
                }
                else
                  break;
              }
            }
          }
        }
      }
    }
    return snapPoints;
  }

  /// <summary>Получить список координат сетки</summary>
  /// <param name="node">Элемент</param>
  /// <param name="xcoordinates">Список координат ширин или высот</param>
  /// <returns></returns>
  internal static List<float> GetSnapPoints1(DocumentTreeNode node, bool xcoordinates)
  {
    List<float> floatList = new List<float>();
    floatList.AddRange((IEnumerable<float>) ExcelConverter.GetSnapPoints(node, xcoordinates));
    foreach (DocumentTreeNode node1 in node.Nodes)
      floatList.AddRange((IEnumerable<float>) ExcelConverter.GetSnapPoints(node1, xcoordinates));
    floatList.Sort();
    List<float> snapPoints1 = new List<float>();
    foreach (float num in floatList)
    {
      if (!snapPoints1.Contains(num))
        snapPoints1.Add(num);
    }
    return snapPoints1;
  }

  private enum BorderType
  {
    Left,
    Right,
    Top,
    Bottom,
    InnerLeft,
    InnerRight,
    InnerTop,
    InnerBottom,
  }
}
