// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.PrintReportCreator
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools;
using Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools
{
    internal static class PrintReportCreator
    {
        private static readonly string LastRowCellClassName = "last-row-cell";
        private static readonly int FontSize = 14;
        private static readonly int HeaderFontSize = PrintReportCreator.FontSize + 6;
        private static readonly string CheckMark = "✔";

        public static string CreateHtmlReport(List<PrinterNode> nodes)
        {
            Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>> reportData = PrintReportCreator.GroupNodes(nodes);
            HtmlWriter writer = new HtmlWriter();
            writer.AddBeginTag(HtmlTags.Html);
            writer.AddHeader();
            writer.AddBody(reportData);
            writer.AddEndTag();
            string htmlReport = writer.ToString();
            writer.Close();
            return htmlReport;
        }

        private static void AddHeader(this HtmlWriter writer)
        {
            writer.AddBeginTag(HtmlTags.Head);
            writer.AddUnpairedTag(HtmlTags.Meta, new HtmlProperty(HtmlAttributes.Charset, "utf-8"));
            writer.AddStyle();
            writer.AddEndTag();
        }

        private static void AddStyle(this HtmlWriter writer)
        {
            writer.AddBeginTag(HtmlTags.Style, new HtmlProperty(HtmlAttributes.Type, "text/css"));
            writer.AddTagCssStyle("*", new CssProperty("font-size", $"{PrintReportCreator.FontSize}px"));
            writer.AddTagCssStyle(HtmlTags.H1, new CssProperty("font-size", $"{PrintReportCreator.HeaderFontSize}px"));
            writer.AddTagCssStyle(HtmlTags.Thead, new CssProperty("text-align", "left"));
            writer.AddTagCssStyle(HtmlTags.Td, new CssProperty("vertical-align", "top"), new CssProperty("padding-bottom", "0.3em"));
            writer.AddTagCssStyle(HtmlTags.Img, new CssProperty("display", "block"), new CssProperty("margin-left", "auto"), new CssProperty("margin-right", "auto"));
            writer.AddTagCssStyle("td." + PrintReportCreator.LastRowCellClassName, new CssProperty("padding-bottom", "1em"));
            writer.AddEndTag();
        }

        private static void AddBody(
          this HtmlWriter writer,
          Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>> reportData)
        {
            writer.AddBeginTag(HtmlTags.Body);
            writer.AddTextWithinTag(HtmlTags.H1, "Отчёт центра печати PDF");
            writer.AddTextWithinTag(HtmlTags.P, "Дата создания: " + DateTime.Today.ToString("dd/MM/yyyy"));
            writer.AddUnpairedTag(HtmlTags.Br);
            foreach (KeyValuePair<string, Dictionary<string, List<PrintQueuePagesNode>>> keyValuePair in reportData)
            {
                string key = keyValuePair.Key;
                Dictionary<string, List<PrintQueuePagesNode>> objectNames2Nodes = keyValuePair.Value;
                writer.AddText("Принтер:");
                writer.AddTextWithinTag(HtmlTags.B, key);
                writer.AddUnpairedTag(HtmlTags.Br);
                writer.AddUnpairedTag(HtmlTags.Br);
                writer.AddTable(objectNames2Nodes, PrintReportCreator.GetObjectNamesFromDifferentPrinters(reportData));
                writer.AddUnpairedTag(HtmlTags.Hr);
                writer.AddUnpairedTag(HtmlTags.Br);
            }
            writer.AddEndTag();
        }

        private static void AddTable(
          this HtmlWriter writer,
          Dictionary<string, List<PrintQueuePagesNode>> objectNames2Nodes,
          HashSet<string> objectsFromDifferentPrinters)
        {
            writer.AddBeginTag(HtmlTags.Table, new HtmlProperty(HtmlAttributes.Width, "100%"));
            writer.AddTableColumnsWidth();
            writer.AddTableHeader();
            writer.AddTableBody(objectNames2Nodes, objectsFromDifferentPrinters);
            writer.AddEndTag();
        }

        private static void AddTableColumnsWidth(this HtmlWriter writer)
        {
            writer.AddTableColumnWidth("2%");
            writer.AddTableColumnWidth("30%");
            writer.AddTableColumnWidth("27%");
            writer.AddTableColumnWidth("15%");
            writer.AddTableColumnWidth("8%");
            writer.AddTableColumnWidth("5%");
            writer.AddTableColumnWidth("13%");
        }

        private static void AddTableHeader(this HtmlWriter writer)
        {
            writer.AddBeginTag(HtmlTags.Thead);
            writer.AddBeginTag(HtmlTags.Tr);
            writer.AddTableHeaderCell("Объект", new HtmlProperty(HtmlAttributes.Colspan, "2"));
            writer.AddTableHeaderCell("Файл");
            writer.AddTableHeaderCell("Макет");
            writer.AddTableHeaderCell("Копии");
            writer.AddTableHeaderCell("");
            writer.AddTableHeaderCell("Вписать");
            writer.AddTableHeaderCell("Страницы");
            writer.AddEndTag();
            writer.AddEndTag();
        }

        private static void AddTableBody(
          this HtmlWriter writer,
          Dictionary<string, List<PrintQueuePagesNode>> objectNames2Nodes,
          HashSet<string> filenamesFromDifferentPrinters)
        {
            string temp = PrintReportCreator.SaveAttentionImageToTemp();
            int currentFileNumber = 1;
            foreach (KeyValuePair<string, List<PrintQueuePagesNode>> objectNames2Node in objectNames2Nodes)
            {
                string key = objectNames2Node.Key;
                List<PrintQueuePagesNode> nodes = objectNames2Node.Value;
                bool onManyPrinters = filenamesFromDifferentPrinters.Contains(key);
                writer.AddFilesInfo(key, onManyPrinters, nodes, temp, currentFileNumber);
                ++currentFileNumber;
            }
        }

        private static void AddFilesInfo(
          this HtmlWriter writer,
          string objectName,
          bool onManyPrinters,
          List<PrintQueuePagesNode> nodes,
          string tempPathToImage,
          int currentFileNumber)
        {
            string str = nodes.Count.ToString();
            writer.AddBeginTag(HtmlTags.Tr);
            HtmlProperty htmlProperty1 = new HtmlProperty(HtmlAttributes.Class, PrintReportCreator.LastRowCellClassName);
            HtmlProperty htmlProperty2 = nodes.Count == 1 ? htmlProperty1 : (HtmlProperty)null;
            writer.AddTableCell(currentFileNumber.ToString() + ". ", new HtmlProperty(HtmlAttributes.Rowspan, str), htmlProperty2);
            writer.AddTableCell(objectName, new HtmlProperty(HtmlAttributes.Rowspan, str), htmlProperty2);
            Dictionary<string, List<PrintQueuePagesNode>> dictionary = nodes.GroupBy<PrintQueuePagesNode, string>((Func<PrintQueuePagesNode, string>)(node => node.FileName)).ToDictionary<IGrouping<string, PrintQueuePagesNode>, string, List<PrintQueuePagesNode>>((Func<IGrouping<string, PrintQueuePagesNode>, string>)(x => x.Key), (Func<IGrouping<string, PrintQueuePagesNode>, List<PrintQueuePagesNode>>)(x => x.ToList<PrintQueuePagesNode>()));
            foreach (string key in dictionary.Keys)
            {
                HtmlProperty htmlProperty3 = (HtmlProperty)null;
                if (key == dictionary.Keys.Last<string>() && dictionary[key].Count == 1)
                    htmlProperty3 = htmlProperty1;
                writer.AddTableCell(key, new HtmlProperty(HtmlAttributes.Rowspan, dictionary[key].Count.ToString()), htmlProperty3);
                List<PrintQueuePagesNode> printQueuePagesNodeList = dictionary[key];
                for (int index = 0; index < printQueuePagesNodeList.Count; ++index)
                {
                    HtmlProperty htmlProperty4 = (HtmlProperty)null;
                    if (key == dictionary.Keys.Last<string>() && index == printQueuePagesNodeList.Count - 1)
                        htmlProperty4 = htmlProperty1;
                    writer.AddTableCell(printQueuePagesNodeList[index].Parent.MainColumnCaption, htmlProperty4);
                    writer.AddTableCell(printQueuePagesNodeList[index].Copies.ToString(), htmlProperty4);
                    if (onManyPrinters)
                    {
                        writer.AddBeginTag(HtmlTags.Td, htmlProperty4);
                        writer.AddUnpairedTag(HtmlTags.Img, new HtmlProperty(HtmlAttributes.Src, tempPathToImage), new HtmlProperty(HtmlAttributes.Alt, "Документ печатается на разных принтерах"));
                        writer.AddEndTag();
                    }
                    else
                        writer.AddTableCell("", htmlProperty4);
                    writer.AddTableCell(printQueuePagesNodeList[index].FitToPage ? PrintReportCreator.CheckMark : " ", htmlProperty4);
                    writer.AddTableCell(printQueuePagesNodeList[index].Pages, htmlProperty4);
                    writer.AddEndTag();
                    if (key != dictionary.Keys.Last<string>() || index < printQueuePagesNodeList.Count - 1)
                        writer.AddBeginTag(HtmlTags.Tr);
                }
            }
        }

        private static void AddTableCell(
          this HtmlWriter writer,
          string caption,
          params HtmlProperty[] attributes)
        {
            IEnumerable<HtmlProperty> source = attributes.OfType<HtmlProperty>();
            writer.AddTextWithinTag(HtmlTags.Td, caption, source.ToArray<HtmlProperty>());
        }

        private static void AddTableColumnWidth(this HtmlWriter writer, string widthInPercent)
        {
            Regex regex = new Regex("^\\d{1,3}%$");
            writer.AddUnpairedTag(HtmlTags.Col, new HtmlProperty(HtmlAttributes.Style, "width:" + widthInPercent));
        }

        private static void AddTableHeaderCell(
          this HtmlWriter writer,
          string caption,
          params HtmlProperty[] attributes)
        {
            writer.AddTextWithinTag(HtmlTags.Th, caption, attributes);
        }

        private static HashSet<string> GetObjectNamesFromDifferentPrinters(
          Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>> reportData)
        {
            HashSet<string> hashSet = new HashSet<string>().Concat<string>((IEnumerable<string>)reportData.First<KeyValuePair<string, Dictionary<string, List<PrintQueuePagesNode>>>>().Value.Keys).ToHashSet<string>();
            HashSet<string> first = new HashSet<string>();
            foreach (KeyValuePair<string, Dictionary<string, List<PrintQueuePagesNode>>> keyValuePair in reportData.Skip<KeyValuePair<string, Dictionary<string, List<PrintQueuePagesNode>>>>(1))
            {
                string key = keyValuePair.Key;
                Dictionary<string, List<PrintQueuePagesNode>>.KeyCollection keys = keyValuePair.Value.Keys;
                IEnumerable<string> second = keys.Intersect<string>((IEnumerable<string>)hashSet);
                first = first.Concat<string>(second).ToHashSet<string>();
                hashSet = hashSet.Concat<string>((IEnumerable<string>)keys).ToHashSet<string>();
            }
            return first;
        }

        private static Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>> GroupNodes(
          List<PrinterNode> nodes)
        {
            Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>> dictionary = new Dictionary<string, Dictionary<string, List<PrintQueuePagesNode>>>();
            foreach (PrinterNode node in nodes)
            {
                string printerName = node.PrinterName;
                if (!dictionary.ContainsKey(printerName))
                    dictionary.Add(printerName, new Dictionary<string, List<PrintQueuePagesNode>>());
                foreach (PrintCenterNode printCenterNode in node.Children.OfType<LayoutNode>())
                {
                    foreach (PrintQueuePagesNode printQueuePagesNode in printCenterNode.Children.OfType<PrintQueuePagesNode>())
                    {
                        string objectName = printQueuePagesNode.ObjectName;
                        if (!dictionary[printerName].ContainsKey(objectName))
                            dictionary[printerName].Add(objectName, new List<PrintQueuePagesNode>());
                        dictionary[printerName][objectName].Add(printQueuePagesNode);
                    }
                }
            }
            return dictionary;
        }

        private static string SaveAttentionImageToTemp()
        {
            string randomFileName = Path.GetRandomFileName();
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), randomFileName));
            string filename = Path.Combine(Path.GetTempPath(), randomFileName, "attention_image.png");
            Resources.PNG_Attention.Save(filename);
            return filename;
        }
    }
}
