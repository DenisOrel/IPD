// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.Processing_Ved_Static
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

/// <summary>КЛАСС для различных обработок</summary>
public static class Processing_Ved_Static
{
  public static string String_To_Print;

  /// <summary> ПРИМЕР прохождения всех строк документа </summary>
  /// <param name="document"></param>
  /// <param name="listError"></param>
  /// <returns></returns>
  public static ListError_OneError Check_Ved_Or_Tabl(
    ImDocument document,
    ListError_OneError listError)
  {
    if (listError == null)
    {
      ListError_OneError listErrorOneError = new ListError_OneError();
    }
    else
      listError.Clear();
    if (document == null)
      return listError;
    Check_In_One_DocRow check_One_DocRow1 = new Check_In_One_DocRow(Processing_Ved_Static.Check_ObjId_In_One_docRow);
    Processing_Ved_Static.Control1(document, listError, check_One_DocRow1, true);
    Check_In_One_DocRow check_One_DocRow2 = new Check_In_One_DocRow(Processing_Ved_Static.Check_Naim_In_One_docRow);
    Processing_Ved_Static.Control1(document, listError, check_One_DocRow2, true);
    Processing_Ved_Static.Flipping_Lines.Clean();
    Processing_Ved_Static.Flipping_Lines.document = document;
    while (!Processing_Ved_Static.Flipping_Lines._endDoc)
    {
      DocumentTreeNode nextDocRow = Processing_Ved_Static.Flipping_Lines.Get_Next_DocRow();
      if (nextDocRow != null && !(nextDocRow.Name == "Пустая строка"))
      {
        Processing_Ved_Static.Check_ObjId_In_One_docRow(nextDocRow, listError);
        Processing_Ved_Static.Check_Naim_In_One_docRow(nextDocRow, listError);
      }
    }
    return listError;
  }

  /// <summary> Контроль документа. Это только пример обращения к Control1 </summary>
  /// <param name="document"></param>
  /// <param name="listError"></param>
  public static void Control(ImDocument document, ListError_OneError listError)
  {
    Check_In_One_DocRow check_One_DocRow = new Check_In_One_DocRow(Processing_Ved_Static.Check_ObjId_In_One_docRow);
    Processing_Ved_Static.Control1(document, listError, check_One_DocRow, true);
  }

  /// <summary> Контроль ВСЕГО документа (ШАБЛОН) с ПЕРЕДАЧЕЙ ФУНКЦИИ (check_One_DocRow) как параметр </summary>
  /// <param name="document"></param>
  /// <param name="listError"></param>
  /// <param name="check_One_DocRow"></param>
  /// <param name="only_Info"></param>
  /// <returns></returns>
  public static bool Control1(
    ImDocument document,
    ListError_OneError listError,
    Check_In_One_DocRow check_One_DocRow,
    bool only_Info)
  {
    if (document == null)
      return false;
    bool flag = false;
    Processing_Ved_Static.Flipping_Lines.Clean();
    Processing_Ved_Static.Flipping_Lines.document = document;
    while (!Processing_Ved_Static.Flipping_Lines._endDoc)
    {
      DocumentTreeNode nextDocRow = Processing_Ved_Static.Flipping_Lines.Get_Next_DocRow();
      if (nextDocRow != null && (!only_Info || !(Vedomost_VB_Static.GetTypeRowS(nextDocRow) != "Info")) && !(nextDocRow.Name == "Пустая строка") && !(nextDocRow.Name == "Заголовок") && check_One_DocRow(nextDocRow, listError))
      {
        flag = true;
        OneError oneError = new OneError();
        oneError.Message();
        oneError._message = "Длинная ";
        oneError._message_kurc = "Короткая";
        listError._list.Add(oneError);
      }
    }
    return flag;
  }

  public static bool Check_ObjId_In_One_docRow(
    DocumentTreeNode docRow,
    ListError_OneError listError)
  {
    string.IsNullOrEmpty(docRow.GetAttributeValue("ObjectIdIzd", true));
    return true;
  }

  public static bool Check_Naim_In_One_docRow(DocumentTreeNode docRow, ListError_OneError listError)
  {
    return true;
  }

  /// <summary> Вывод из ListBox </summary>
  /// <param name="listBox"></param>
  public static void Print_ListBox(ListBox listBox)
  {
    string text = "";
    for (int index = 0; index < listBox.Items.Count; ++index)
    {
      if (!string.IsNullOrEmpty(text))
        text += "\n";
      text += (string) listBox.Items[index];
    }
    Processing_Ved_Static.Print_Text(text);
  }

  /// <summary> Вывод на принтер списка строк </summary>
  /// <param name="strings"></param>
  public static void Print_Strings(List<string> strings)
  {
    string text = "";
    for (int index = 0; index < strings.Count; ++index)
    {
      if (!string.IsNullOrEmpty(text))
        text += "\n";
      text += strings[index];
    }
    Processing_Ved_Static.Print_Text(text);
  }

  /// <summary> Печать дли-и-инной строки </summary>
  /// <param name="text"></param>
  public static void Print_Text(string text)
  {
    PrintDocument printDocument = new PrintDocument();
    Processing_Ved_Static.String_To_Print = text;
    printDocument.PrintPage += new PrintPageEventHandler(Processing_Ved_Static.PrintPageHandler);
    PrintDialog printDialog = new PrintDialog();
    printDialog.Document = printDocument;
    printDocument.DefaultPageSettings.Landscape = true;
    if (printDialog.ShowDialog() != DialogResult.OK)
      return;
    printDialog.Document.Print();
  }

  /// <summary>  Вывод в поток </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public static void PrintPageHandler(object sender, PrintPageEventArgs e)
  {
    e.Graphics.DrawString(Processing_Ved_Static.String_To_Print, new Font("Arial", 9f), Brushes.Black, 0.0f, 0.0f);
  }

  /// <summary> Сохранение в файл </summary>
  /// <param name="strings"></param>
  public static void SaveToFile(List<string> strings)
  {
    string contents = "";
    for (int index = 0; index < strings.Count; ++index)
    {
      if (!string.IsNullOrEmpty(contents))
        contents += "\r\n";
      contents += strings[index];
    }
    string name1 = "%IPS_VED_DUMP%";
    string name2 = "%text%";
    string path1 = Environment.ExpandEnvironmentVariables(name1);
    if (!Directory.Exists(path1))
      path1 = (string) null;
    string path2 = string.IsNullOrEmpty(path1) ? Environment.ExpandEnvironmentVariables(name2) : path1;
    if (!Directory.Exists(path2))
      path2 = (string) null;
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.DefaultExt = ".TXT";
    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
    string str = path2 + "\\Error.txt";
    saveFileDialog.FileName = str;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    File.WriteAllText(saveFileDialog.FileName, contents);
    string directoryName = Path.GetDirectoryName(saveFileDialog.FileName);
    if (MessageBox.Show($"Создан файл\r\n\r\n{str}\r\n\r\nОткрыть папку с этим файлом?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    Process.Start(directoryName);
  }

  /// <summary> Удалить одинаковые тексты в ДОКУМЕНТЕ </summary>
  /// <param name="document"></param>
  public static bool DeleteIdenticalTexts(ImDocument document)
  {
    bool flag = false;
    int nodesCount = document.NodesCount;
    for (int index = 0; index < document.NodesCount; ++index)
    {
      DocumentTreeNode node = document.Nodes[index];
      string nodeClass = node.NodeClass;
      if (!(node.Id == "TL") && !(node.Id == "Титульный лист"))
      {
        if (!(node.Id == "LRI") && !(node.Id == "Лист регистрации изменений"))
        {
          if (nodeClass == "Page" && Processing_Ved_Static.DeleteIdenticalTexts_Page(node))
            flag = true;
        }
        else
          break;
      }
    }
    if (flag)
    {
      document.UpdateLayout(false);
      document.SetAttributeValue(nameof (DeleteIdenticalTexts), "Yes");
      document.SetAttributeValue("RecoverIdenticalTexts", "");
      if (document.NodesCount != nodesCount)
        Processing_Ved_Static.DeleteIdenticalTexts_Second(document);
    }
    return flag;
  }

  /// <summary> Повторная обработка, если изменилось количество страниц </summary>
  /// <param name="document"></param>
  public static void DeleteIdenticalTexts_Second(ImDocument document)
  {
    bool flag = false;
    for (int index = 0; index < document.NodesCount; ++index)
    {
      DocumentTreeNode node = document.Nodes[index];
      string nodeClass = node.NodeClass;
      if (!(node.Id == "TL") && !(node.Id == "Титульный лист"))
      {
        if (!(node.Id == "LRI") && !(node.Id == "Лист регистрации изменений"))
        {
          if (nodeClass == "Page" && Processing_Ved_Static.DeleteIdenticalTexts_Page(node))
            flag = true;
        }
        else
          break;
      }
    }
    if (!flag)
      return;
    document.UpdateLayout(false);
  }

  /// <summary> Удалить одинаковые тексты на СТРАНИЦЕ </summary>
  /// <param name="page"></param>
  /// <returns></returns>
  public static bool DeleteIdenticalTexts_Page(DocumentTreeNode page)
  {
    bool flag1 = false;
    if (!(page.FindFirstChildNodeByName("Главная таблица") is TableData firstChildNodeByName1))
      return false;
    DocumentTreeNode firstChildNodeByName2 = firstChildNodeByName1.FindFirstChildNodeByName("Основная строка");
    if (firstChildNodeByName2 != null)
    {
      for (int index1 = 0; index1 < firstChildNodeByName2.NodesCount; ++index1)
      {
        DocumentTreeNode node1 = firstChildNodeByName2.Nodes[index1];
        if (!(node1.NodeClass != "TextBoxElement") && !(node1.Name == "№ строки") && !(node1.Name == "Наименование"))
        {
          bool flag2 = true;
          int num = 0;
          string str = "";
          for (int index2 = 0; index2 < firstChildNodeByName1.NodesCount; ++index2)
          {
            DocumentTreeNode node2 = firstChildNodeByName1.Nodes[index2];
            string typeRowS = Vedomost_VB_Static.GetTypeRowS(node2);
            if (typeRowS != "Info")
            {
              if (!(typeRowS == "Empty"))
              {
                flag2 = true;
                num = 0;
                str = "";
              }
            }
            else
            {
              DocumentTreeNode node3 = node2.Nodes[index1];
              if (!(node3.NodeClass != "TextBoxElement"))
              {
                TextBoxElement node4 = (TextBoxElement) node2.Nodes[index1];
                if (flag2)
                {
                  str = node4.Text;
                  if (str == "\"" || str == "То же")
                    str = "";
                  if (str == "")
                  {
                    flag2 = true;
                    num = 0;
                  }
                  else
                    flag2 = false;
                }
                else
                {
                  string text = node4.Text;
                  if (text == "")
                  {
                    flag2 = true;
                    num = 0;
                  }
                  else if (text == str)
                  {
                    if (num == 0)
                    {
                      node3.SetAttributeValue("DeletingText", node4.Text);
                      node4.AssignText("То же", false, false, false);
                      flag1 = true;
                      ++num;
                    }
                    else
                    {
                      node3.SetAttributeValue("DeletingText", node4.Text);
                      node4.AssignText("\"", false, false, false);
                      flag1 = true;
                    }
                    flag2 = false;
                  }
                  else if (text != "То же" && text != "\"")
                  {
                    str = text;
                    flag2 = false;
                    num = 0;
                  }
                  else
                  {
                    if (text == "То же" && num > 0)
                    {
                      node4.AssignText("\"", false, false, false);
                      flag1 = true;
                    }
                    num = 2;
                  }
                }
              }
            }
          }
        }
      }
    }
    return flag1;
  }

  /// <summary> Восстановить тексты в ДОКУМЕНТЕ </summary>
  /// <param name="document"></param>
  public static bool RecoverIdenticalTexts(ImDocument document)
  {
    bool flag = false;
    for (int index = 0; index < document.NodesCount; ++index)
    {
      DocumentTreeNode node = document.Nodes[index];
      string nodeClass = node.NodeClass;
      if (!(node.Id == "TL") && !(node.Id == "Титульный лист"))
      {
        if (!(node.Id == "LRI") && !(node.Id == "Лист регистрации изменений"))
        {
          if (nodeClass == "Page" && Processing_Ved_Static.RecoverIdenticalTexts_Page(node))
            flag = true;
        }
        else
          break;
      }
    }
    if (flag)
    {
      document.UpdateLayout(false);
      document.SetAttributeValue("DeleteIdenticalTexts", "");
      document.SetAttributeValue(nameof (RecoverIdenticalTexts), "Yes");
    }
    return flag;
  }

  /// <summary> Восстановить одинаковые тексты на СТРАНИЦЕ </summary>
  /// <param name="page"></param>
  /// <returns></returns>
  public static bool RecoverIdenticalTexts_Page(DocumentTreeNode page)
  {
    bool flag = false;
    if (!(page.FindFirstChildNodeByName("Главная таблица") is TableData firstChildNodeByName1))
      return false;
    DocumentTreeNode firstChildNodeByName2 = firstChildNodeByName1.FindFirstChildNodeByName("Основная строка");
    if (firstChildNodeByName2 != null)
    {
      for (int index1 = 0; index1 < firstChildNodeByName2.NodesCount; ++index1)
      {
        DocumentTreeNode node1 = firstChildNodeByName2.Nodes[index1];
        if (!(node1.NodeClass != "TextBoxElement") && !(node1.Name == "№ строки") && !(node1.Name == "Наименование"))
        {
          for (int index2 = 0; index2 < firstChildNodeByName1.NodesCount; ++index2)
          {
            DocumentTreeNode node2 = firstChildNodeByName1.Nodes[index2];
            if (!(Vedomost_VB_Static.GetTypeRowS(node2) != "Info"))
            {
              DocumentTreeNode node3 = node2.Nodes[index1];
              if (!(node3.NodeClass != "TextBoxElement"))
              {
                TextBoxElement node4 = (TextBoxElement) node2.Nodes[index1];
                if (node4.Text == "\"" || node4.Text == "То же")
                {
                  string attributeValue = node4.GetAttributeValue("DeletingText", true);
                  if (!string.IsNullOrEmpty(attributeValue))
                  {
                    node4.AssignText(attributeValue, false, false, false);
                    node3.SetAttributeValue("DeletingText", "");
                    flag = true;
                  }
                }
              }
            }
          }
        }
      }
    }
    return flag;
  }

  /// <summary> Сравнение двух docRow по условиям </summary>
  /// <param name="docRow1"></param>
  /// <param name="docRow2"></param>
  /// <param name="sorting_Usl_Doc"></param>
  /// <returns></returns>
  public static long Compare_DocRows(
    TableData docRow1,
    TableData docRow2,
    Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc)
  {
    if (docRow1 == null || docRow1.Nodes == null || docRow1.NodesCount == 0 || docRow2 == null || docRow2.Nodes == null || docRow2.NodesCount == 0 || sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc.Count == 0)
      return 0;
    long razdel1 = Vedomost_VB_Static.GetRazdel(docRow1);
    long razdel2 = Vedomost_VB_Static.GetRazdel(docRow2);
    long num = 0;
    if (razdel1 != razdel2)
      return razdel2 - razdel1;
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel1 = sorting_Usl_Doc._list_sorting_Usl_Doc[0];
    for (int index = 0; index < sorting_Usl_Doc._list_sorting_Usl_Doc.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel2 = sorting_Usl_Doc._list_sorting_Usl_Doc[index];
      if (sortingUslDocOneRazdel2._razdelNum == razdel1)
      {
        sortingUslDocOneRazdel1 = sortingUslDocOneRazdel2;
        break;
      }
    }
    if (sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel == null || sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel.Count == 0)
      return 0;
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sortingUslDocOneGrafa = sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel[0];
    for (int index = 0; index < sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa = sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel[index];
      TextData firstNodeByName1 = docRow1.FindFirstNodeByName(sorting_Usl_Doc_OneGrafa._grafa) as TextData;
      TextData firstNodeByName2 = docRow2.FindFirstNodeByName(sorting_Usl_Doc_OneGrafa._grafa) as TextData;
      string str = "";
      if (firstNodeByName1 != null)
        str = firstNodeByName1.Text;
      if (firstNodeByName2 != null)
      {
        string text = firstNodeByName2.Text;
      }
      num = (long) Processing_Ved_Static.StringCompareDoc_OneUsl(str, str, sorting_Usl_Doc_OneGrafa);
      if (num != 0L)
        break;
    }
    return num;
  }

  /// <summary> Сравнение двух текстов для docRow (одно условие)</summary>
  /// <param name="text1"></param>
  /// <param name="text2"></param>
  /// <param name="sorting_Usl_Doc_OneGrafa"></param>
  /// <returns></returns>
  public static int StringCompareDoc_OneUsl(
    string text1,
    string text2,
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa)
  {
    if (text1 == "" && text2 == "")
      return 0;
    if (text1 == "" && text2 != "")
      return sorting_Usl_Doc_OneGrafa._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? 1 : -1;
    if (text1 != "" && text2 == "")
      return sorting_Usl_Doc_OneGrafa._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? -1 : 1;
    int iOtcuda1 = 0;
    int iSkolko1 = 500;
    int iOtcuda2 = 0;
    int iSkolko2 = 500;
    switch (sorting_Usl_Doc_OneGrafa._beginSravn)
    {
      case Vedomost_VB.BeginSravn.S_begin:
        iOtcuda1 = 0;
        iOtcuda2 = 0;
        break;
      case Vedomost_VB.BeginSravn.S_pozicii:
        iOtcuda1 = sorting_Usl_Doc_OneGrafa._num_symb_ot;
        iOtcuda2 = sorting_Usl_Doc_OneGrafa._num_symb_ot;
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola:
        if (sorting_Usl_Doc_OneGrafa._symb_ot != null && sorting_Usl_Doc_OneGrafa._symb_ot != "")
        {
          iOtcuda1 = text1.IndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          iOtcuda2 = text2.IndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          break;
        }
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola_s_konca:
        if (sorting_Usl_Doc_OneGrafa._symb_ot != null && sorting_Usl_Doc_OneGrafa._symb_ot != "")
        {
          iOtcuda1 = text1.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          iOtcuda2 = text2.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          break;
        }
        break;
    }
    switch (sorting_Usl_Doc_OneGrafa._endSravn)
    {
      case Vedomost_VB.EndSravn.Do_end:
        iSkolko1 = 500;
        iSkolko2 = 500;
        break;
      case Vedomost_VB.EndSravn.Skolko:
        iSkolko1 = sorting_Usl_Doc_OneGrafa._num_symb_do;
        iSkolko2 = sorting_Usl_Doc_OneGrafa._num_symb_do;
        break;
      case Vedomost_VB.EndSravn.Do_symbola:
        if (!string.IsNullOrEmpty(sorting_Usl_Doc_OneGrafa._symb_do))
        {
          iSkolko1 = text1.IndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda1;
          iSkolko2 = text2.IndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda2;
          break;
        }
        break;
      case Vedomost_VB.EndSravn.Do_symbola_s_konca:
        if (!string.IsNullOrEmpty(sorting_Usl_Doc_OneGrafa._symb_do))
        {
          iSkolko1 = text1.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda1;
          iSkolko2 = text2.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda2;
          break;
        }
        break;
    }
    if (iSkolko1 < 0)
      iSkolko1 = 0;
    if (iSkolko2 < 0)
      iSkolko2 = 0;
    string text1_1 = Vedomost_VB.StrCopy(text1, iOtcuda1, iSkolko1);
    string str = Vedomost_VB.StrCopy(text2, iOtcuda2, iSkolko2);
    bool flag = sorting_Usl_Doc_OneGrafa._sravnenie == Vedomost_VB.Sravnenie.Number;
    string text2_1 = str;
    int num1 = flag ? 1 : 0;
    int num2 = Processing_Ved_Static.StringCompareDoc(text1_1, text2_1, num1 != 0, 0);
    if (sorting_Usl_Doc_OneGrafa._poriadokSortirovki == Vedomost_VB.PoriadokSortirovki.Ubyvanie)
      num2 = -num2;
    return num2;
  }

  /// <summary> Конкретное сравнение двух строк </summary>
  /// <param name="text1"></param>
  /// <param name="text2"></param>
  /// <param name="numberCompare">true это Числовое</param>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public static int StringCompareDoc(
    string text1,
    string text2,
    bool numberCompare,
    int attributeId)
  {
    if (!numberCompare)
      return string.Compare(text1, text2);
    if (string.IsNullOrEmpty(text1) && string.IsNullOrEmpty(text2))
      return 0;
    if (!string.IsNullOrEmpty(text1) && string.IsNullOrEmpty(text2))
      return 1;
    if (string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
      return -1;
    if (text1 == text2)
      return 0;
    int startIndex1 = 0;
    int startIndex2 = 0;
    int numberLength1 = 0;
    int numberLength2 = 0;
    int numberBegin1 = 0;
    int numberBegin2 = 0;
    double num1 = 0.0;
    double num2 = 0.0;
    NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
    ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.DECIMAL | ParserOptions.THOUSANDS | ParserOptions.SCIENTIFIC | ParserOptions.PERCENT | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
    ParsedNumberData number1;
    ParsedNumberData number2;
    string strA;
    string strB;
    int num3;
    int num4;
    int num5;
    while (true)
    {
      number1 = new ParsedNumberData();
      int num6 = NumberParserAdvanced.ParseNumber(text1, startIndex1, options, number1, currentInfo, out numberBegin1, out numberLength1) ? 1 : 0;
      number2 = new ParsedNumberData();
      int num7 = NumberParserAdvanced.ParseNumber(text2, startIndex2, options, number2, currentInfo, out numberBegin2, out numberLength2) ? 1 : 0;
      if ((num6 & num7) != 0)
      {
        if (numberBegin1 != 0 || numberBegin2 <= 0)
        {
          if (numberBegin2 != 0 || numberBegin1 <= 0)
          {
            int length = Math.Min(Math.Min(Math.Max(numberBegin1 - startIndex1, numberBegin2 - startIndex2), text1.Length - startIndex1), text2.Length - startIndex2);
            strA = text1.Substring(startIndex1, length);
            strB = text2.Substring(startIndex2, length);
            num3 = string.Compare(strA, strB);
            if (num3 == 0)
            {
              NumberParserAdvanced.NumberToDouble(number1, out num1);
              NumberParserAdvanced.NumberToDouble(number2, out num2);
              num4 = num1.CompareTo(num2);
              if (num4 == 0)
              {
                num5 = string.Compare(text1.Substring(startIndex1, numberBegin1 - startIndex1 + numberLength1), text2.Substring(startIndex2, numberBegin2 - startIndex2 + numberLength2));
                if (num5 == 0)
                {
                  startIndex1 = numberBegin1 + numberLength1;
                  startIndex2 = numberBegin2 + numberLength2;
                }
                else
                  goto label_24;
              }
              else
                goto label_22;
            }
            else
              goto label_17;
          }
          else
            goto label_15;
        }
        else
          break;
      }
      else
        goto label_26;
    }
    return -1;
label_15:
    return 1;
label_17:
    if (attributeId == AvsIDCache.Attr_Designation && (strA.EndsWith("-") || strB.EndsWith("-")) && strA.Length > 0 && strB.Length > 0 && !(strA.Remove(strA.Length - 1) != strB.Remove(strA.Length - 1)))
    {
      double num8 = 0.0;
      double num9 = 0.0;
      if (!strA.EndsWith("-") ? NumberParserAdvanced.NumberToDouble(number2, out num9) : NumberParserAdvanced.NumberToDouble(number1, out num8))
        return num8.CompareTo(num9);
    }
    return num3;
label_22:
    return num4;
label_24:
    return num5;
label_26:
    return string.Compare(text1.Substring(startIndex1), text2.Substring(startIndex2));
  }

  /// <summary> Создается список строк tableDatas (docRow)  </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public static List<TableData> Preparation_Before_Sorting_Doc(ImDocument document)
  {
    if (document == null || document.Nodes == null || document.NodesCount == 0)
      return (List<TableData>) null;
    List<TableData> tableDataList = new List<TableData>();
    Processing_Ved_Static.Flipping_Lines.Clean();
    Processing_Ved_Static.Flipping_Lines.document = document;
    while (!Processing_Ved_Static.Flipping_Lines._endDoc)
    {
      DocumentTreeNode nextDocRow = Processing_Ved_Static.Flipping_Lines.Get_Next_DocRow();
      if (!(Vedomost_VB_Static.GetTypeRowS(nextDocRow) == "Empty"))
      {
        TableData tableData = nextDocRow.Clone(true, true) as TableData;
        tableDataList.Add(tableData);
      }
    }
    return tableDataList;
  }

  /// <summary> Восстновление ДОКУМЕНТА из tableDatas </summary>
  /// <param name="document"></param>
  /// <param name="tableDatas"></param>
  public static void Recovery_After_Sorting_Doc(
    ImDocument document,
    List<TableData> tableDatas,
    ImDocument docTemplate)
  {
    Vedomost_VB_Static.Clean_Document(document);
    TableData tableData1 = Vedomost_VB_Static.FindFirstMainTable(document);
    PageData firstNodeByName1 = docTemplate.FindFirstNodeByName("Примечания") as PageData;
    TableData firstNodeByName2 = docTemplate.FindFirstNodeByName("Пустая строка") as TableData;
    bool flag = false;
    for (int index = 0; index < tableDatas.Count; ++index)
    {
      TableData tableData2 = tableDatas[index];
      if (index < tableDatas.Count - 1)
      {
        string attributeValue = tableData2.GetAttributeValue("TypeRow", true);
        long razdel = Vedomost_VB_Static.GetRazdel(tableData2);
        if (!string.IsNullOrEmpty(attributeValue) && attributeValue == "Remark" && !flag && firstNodeByName1 != null && razdel == 9999L)
        {
          PageData child = firstNodeByName1.Clone() as PageData;
          tableData1 = child.FindFirstNodeByName("Главная таблица") as TableData;
          tableData1.Clear(true, true);
          document.AddChildNode((DocumentTreeNode) child, false, true);
          flag = true;
        }
        tableData1.AddChildNode((DocumentTreeNode) tableData2, false, false);
        if (!string.IsNullOrEmpty(attributeValue))
        {
          switch (attributeValue)
          {
            case "Info":
              TableData child1 = firstNodeByName2.Clone() as TableData;
              tableData1.AddChildNode((DocumentTreeNode) child1, false, false);
              continue;
            case "Remark":
              if (razdel == 9999L)
                continue;
              goto case "Info";
            default:
              continue;
          }
        }
      }
      else
        tableData1.AddChildNode((DocumentTreeNode) tableData2, false, true);
    }
  }

  /// <summary> Сортировка списка строк (docRow) </summary>
  /// <param name="tableDatas"></param>
  /// <param name="sorting_Usl_Doc"></param>
  /// <returns></returns>
  public static bool Sorting_Doc(
    List<TableData> tableDatas,
    Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc)
  {
    if (sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc.Count == 0)
      return false;
    bool flag1 = false;
    bool flag2;
    do
    {
      flag2 = false;
      for (int index = 0; index < tableDatas.Count - 1; ++index)
      {
        TableData tableData1 = tableDatas[index];
        TableData tableData2 = tableDatas[index + 1];
        TableData docRow2 = tableData2;
        Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc1 = sorting_Usl_Doc;
        if (Processing_Ved_Static.Compare_DocRow(tableData1, docRow2, sorting_Usl_Doc1) < 0L)
        {
          tableDatas.RemoveAt(index + 1);
          tableDatas.Insert(index, tableData2);
          flag2 = true;
        }
      }
    }
    while (flag2);
    return flag1;
  }

  /// <summary> Сравнение двух docRow </summary>
  /// <param name="docRow1"></param>
  /// <param name="docRow2"></param>
  /// <returns></returns>
  public static long Compare_DocRow(
    TableData docRow1,
    TableData docRow2,
    Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc)
  {
    if (docRow1 == null || docRow2 == null)
      return 0;
    long num1 = 0;
    long razdel1 = Vedomost_VB_Static.GetRazdel(docRow1);
    long razdel2 = Vedomost_VB_Static.GetRazdel(docRow2);
    if (razdel1 == razdel2)
    {
      Vedomost_VB_Static.TypeRow typeRow1 = Vedomost_VB_Static.GetTypeRow(docRow1);
      Vedomost_VB_Static.TypeRow typeRow2 = Vedomost_VB_Static.GetTypeRow(docRow2);
      if (typeRow1 == typeRow2)
      {
        if (typeRow1 != Vedomost_VB_Static.TypeRow.Info)
          return 0;
        string textFromGrafa = Vedomost_VB_Static.Get_Text_From_Grafa(docRow1, "Обозначение");
        long num2 = (long) string.Compare(Vedomost_VB_Static.Get_Text_From_Grafa(docRow2, "Обозначение"), textFromGrafa, StringComparison.Ordinal);
        return -Processing_Ved_Static.Compare_DocRow_Uslov(docRow1, docRow2, sorting_Usl_Doc);
      }
      if (typeRow1 == Vedomost_VB_Static.TypeRow.Title)
        num1 = 1L;
      if (typeRow2 == Vedomost_VB_Static.TypeRow.Title)
        num1 = -1L;
      if (typeRow1 == Vedomost_VB_Static.TypeRow.Remark)
        num1 = -1L;
      if (typeRow2 == Vedomost_VB_Static.TypeRow.Remark)
        num1 = 1L;
    }
    else
      num1 = razdel1 != 0L ? (razdel2 != 0L ? razdel2 - razdel1 : -1L) : 1L;
    return num1;
  }

  public static long Compare_DocRow_Uslov(
    TableData docRow1,
    TableData docRow2,
    Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc)
  {
    long num = 0;
    long razdel = Vedomost_VB_Static.GetRazdel(docRow1);
    if (razdel == 0L)
      return 0;
    Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel1 = (Vedomost_VB.Sorting_Usl_Doc_OneRazdel) null;
    for (int index = 0; index < sorting_Usl_Doc._list_sorting_Usl_Doc.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel2 = sorting_Usl_Doc._list_sorting_Usl_Doc[index];
      if (sortingUslDocOneRazdel2._razdelNum == 0L || sortingUslDocOneRazdel2._razdelNum == razdel)
      {
        sortingUslDocOneRazdel1 = sortingUslDocOneRazdel2;
        break;
      }
    }
    if (sortingUslDocOneRazdel1 == null || sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel == null || sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel.Count == 0)
      return 0;
    for (int index = 0; index < sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa = sortingUslDocOneRazdel1._list_sorting_Usl_Doc_OneRazdel[index];
      num = Processing_Ved_Static.Compare_DocRow_OneUsl(docRow1, docRow2, sorting_Usl_Doc_OneGrafa);
      if (num != 0L)
        break;
    }
    return num;
  }

  public static long Compare_DocRow_OneUsl(
    TableData docRow1,
    TableData docRow2,
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa)
  {
    return docRow1 == null || docRow1 == null || sorting_Usl_Doc_OneGrafa == null ? 0L : Processing_Ved_Static.Compare_String_DocRow_OneUsl(Vedomost_VB_Static.Get_Text_From_Grafa(docRow1, sorting_Usl_Doc_OneGrafa._grafa), Vedomost_VB_Static.Get_Text_From_Grafa(docRow2, sorting_Usl_Doc_OneGrafa._grafa), sorting_Usl_Doc_OneGrafa);
  }

  public static long Compare_String_DocRow_OneUsl(
    string text1,
    string text2,
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa)
  {
    if (text1 == "" && text2 == "")
      return 0;
    if (text1 == "" && text2 != "")
      return sorting_Usl_Doc_OneGrafa._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? 1L : -1L;
    if (text1 != "" && text2 == "")
      return sorting_Usl_Doc_OneGrafa._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? -1L : 1L;
    int iOtcuda1 = 0;
    int iSkolko1 = 500;
    int iOtcuda2 = 0;
    int iSkolko2 = 500;
    switch (sorting_Usl_Doc_OneGrafa._beginSravn)
    {
      case Vedomost_VB.BeginSravn.S_begin:
        iOtcuda1 = 0;
        iOtcuda2 = 0;
        break;
      case Vedomost_VB.BeginSravn.S_pozicii:
        iOtcuda1 = sorting_Usl_Doc_OneGrafa._num_symb_ot;
        iOtcuda2 = sorting_Usl_Doc_OneGrafa._num_symb_ot;
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola:
        if (sorting_Usl_Doc_OneGrafa._symb_ot != null && sorting_Usl_Doc_OneGrafa._symb_ot != "")
        {
          iOtcuda1 = text1.IndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          iOtcuda2 = text2.IndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          break;
        }
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola_s_konca:
        if (sorting_Usl_Doc_OneGrafa._symb_ot != null && sorting_Usl_Doc_OneGrafa._symb_ot != "")
        {
          iOtcuda1 = text1.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          iOtcuda2 = text2.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_ot);
          break;
        }
        break;
    }
    switch (sorting_Usl_Doc_OneGrafa._endSravn)
    {
      case Vedomost_VB.EndSravn.Do_end:
        iSkolko1 = 500;
        iSkolko2 = 500;
        break;
      case Vedomost_VB.EndSravn.Skolko:
        iSkolko1 = sorting_Usl_Doc_OneGrafa._num_symb_do;
        iSkolko2 = sorting_Usl_Doc_OneGrafa._num_symb_do;
        break;
      case Vedomost_VB.EndSravn.Do_symbola:
        if (!string.IsNullOrEmpty(sorting_Usl_Doc_OneGrafa._symb_do))
        {
          iSkolko1 = text1.IndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda1;
          iSkolko2 = text2.IndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda2;
          break;
        }
        break;
      case Vedomost_VB.EndSravn.Do_symbola_s_konca:
        if (!string.IsNullOrEmpty(sorting_Usl_Doc_OneGrafa._symb_do))
        {
          iSkolko1 = text1.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda1;
          iSkolko2 = text2.LastIndexOf(sorting_Usl_Doc_OneGrafa._symb_do) - iOtcuda2;
          break;
        }
        break;
    }
    if (iSkolko1 < 0)
      iSkolko1 = 0;
    if (iSkolko2 < 0)
      iSkolko2 = 0;
    string strX = Vedomost_VB.StrCopy(text1, iOtcuda1, iSkolko1);
    string str = Vedomost_VB.StrCopy(text2, iOtcuda2, iSkolko2);
    bool flag = sorting_Usl_Doc_OneGrafa._sravnenie == Vedomost_VB.Sravnenie.Number;
    int num1 = 0;
    if (sorting_Usl_Doc_OneGrafa._grafa == "Обозначение")
      num1 = AvsIDCache.Attr_Designation;
    string strY = str;
    int num2 = flag ? 1 : 0;
    int attributeId = num1;
    long num3 = (long) Vedomost_VB.StringCompareForVed(strX, strY, num2 != 0, attributeId);
    if (sorting_Usl_Doc_OneGrafa._poriadokSortirovki == Vedomost_VB.PoriadokSortirovki.Ubyvanie)
      num3 = -num3;
    return num3;
  }

  /// <summary> /// Сортировка ДОКУМЕНТА /// </summary>
  /// <param name="document"></param>
  /// <param name="sorting_Usl_Doc"></param>
  /// <returns></returns>
  public static bool Sorting_Document(
    ImDocument document,
    Vedomost_VB.Sorting_Usl_Doc sorting_Usl_Doc,
    ImDocument docTemplate)
  {
    if (document == null || document.Nodes == null || document.NodesCount == 0 || sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc == null || sorting_Usl_Doc._list_sorting_Usl_Doc.Count == 0)
      return false;
    List<TableData> tableDatas = Processing_Ved_Static.Preparation_Before_Sorting_Doc(document);
    Processing_Ved_Static.Sorting_Doc(tableDatas, sorting_Usl_Doc);
    if (tableDatas == null || tableDatas.Count < 2)
      return false;
    Processing_Ved_Static.Recovery_After_Sorting_Doc(document, tableDatas, docTemplate);
    return true;
  }

  /// <summary> Класс листания строк (перебора всех строк) в ДОКУМЕНТЕ </summary>
  /// 
  ///              Перед новым документом рекомендуется выполнять
  ///              Flipping_Lines.Clean();
  ///             Flipping_Lines.document = document;
  public static class Flipping_Lines
  {
    public static ImDocument document = (ImDocument) null;
    public static int _i_docRow_Curr = -1;
    public static int _i_Page_Curr = 0;
    public static PageData _Page_Curr = (PageData) null;
    private static TableData _MainTable = (TableData) null;
    public static DocumentTreeNode _DocRow_Curr = (DocumentTreeNode) null;
    public static string sMessage = "";
    public static bool _endDoc = false;
    public static bool _isBegin = true;

    /// <summary> Очистка данных. Подготовка к обработке нового документа </summary>
    public static void Clean()
    {
      Processing_Ved_Static.Flipping_Lines.document = (ImDocument) null;
      Processing_Ved_Static.Flipping_Lines._i_docRow_Curr = 0;
      Processing_Ved_Static.Flipping_Lines._i_Page_Curr = 0;
      Processing_Ved_Static.Flipping_Lines._Page_Curr = (PageData) null;
      Processing_Ved_Static.Flipping_Lines._MainTable = (TableData) null;
      Processing_Ved_Static.Flipping_Lines._DocRow_Curr = (DocumentTreeNode) null;
      Processing_Ved_Static.Flipping_Lines.sMessage = "";
      Processing_Ved_Static.Flipping_Lines._endDoc = false;
      Processing_Ved_Static.Flipping_Lines._isBegin = true;
    }

    /// <summary> Получение СЛЕДУЮЩЕЙ строки </summary>
    /// <returns></returns>
    public static DocumentTreeNode Get_Next_DocRow()
    {
      if (Processing_Ved_Static.Flipping_Lines.document == null)
      {
        Processing_Ved_Static.Flipping_Lines.sMessage = "Document == null";
        return (DocumentTreeNode) null;
      }
      if (Processing_Ved_Static.Flipping_Lines.document.NodesCount == 0)
      {
        Processing_Ved_Static.Flipping_Lines.sMessage = "В документе 0 страниц";
        return (DocumentTreeNode) null;
      }
      if (Processing_Ved_Static.Flipping_Lines._isBegin)
      {
        Processing_Ved_Static.Flipping_Lines._isBegin = false;
        Processing_Ved_Static.Flipping_Lines._Page_Curr = Processing_Ved_Static.Flipping_Lines.document.Nodes[Processing_Ved_Static.Flipping_Lines._i_Page_Curr] as PageData;
        if (Processing_Ved_Static.Flipping_Lines._Page_Curr.Name == "Титульный лист")
        {
          if (Processing_Ved_Static.Flipping_Lines.document.NodesCount > 1)
          {
            Processing_Ved_Static.Flipping_Lines._i_Page_Curr = 1;
            Processing_Ved_Static.Flipping_Lines._Page_Curr = Processing_Ved_Static.Flipping_Lines.document.Nodes[Processing_Ved_Static.Flipping_Lines._i_Page_Curr] as PageData;
          }
          else
            Processing_Ved_Static.Flipping_Lines._Page_Curr = (PageData) null;
        }
        if (Processing_Ved_Static.Flipping_Lines._Page_Curr == null)
        {
          Processing_Ved_Static.Flipping_Lines._endDoc = true;
          Processing_Ved_Static.Flipping_Lines.sMessage = "Нет страницы 0";
          return (DocumentTreeNode) null;
        }
        Processing_Ved_Static.Flipping_Lines._MainTable = (TableData) Processing_Ved_Static.Flipping_Lines._Page_Curr.FindFirstNodeByName("Главная таблица");
        if (Processing_Ved_Static.Flipping_Lines._MainTable == null)
        {
          Processing_Ved_Static.Flipping_Lines.sMessage = $"На странице {(Processing_Ved_Static.Flipping_Lines._i_Page_Curr + 1).ToString()} не найдена Главная таблица";
          if (Processing_Ved_Static.Flipping_Lines._i_Page_Curr < Processing_Ved_Static.Flipping_Lines.document.NodesCount - 1)
            ++Processing_Ved_Static.Flipping_Lines._i_Page_Curr;
          return (DocumentTreeNode) null;
        }
      }
      if (Processing_Ved_Static.Flipping_Lines._i_docRow_Curr < Processing_Ved_Static.Flipping_Lines._MainTable.NodesCount)
        Processing_Ved_Static.Flipping_Lines._DocRow_Curr = Processing_Ved_Static.Flipping_Lines._MainTable.Nodes[Processing_Ved_Static.Flipping_Lines._i_docRow_Curr];
      ++Processing_Ved_Static.Flipping_Lines._i_docRow_Curr;
      if (Processing_Ved_Static.Flipping_Lines._i_docRow_Curr >= Processing_Ved_Static.Flipping_Lines._MainTable.NodesCount)
      {
        ++Processing_Ved_Static.Flipping_Lines._i_Page_Curr;
        if (Processing_Ved_Static.Flipping_Lines._i_Page_Curr >= Processing_Ved_Static.Flipping_Lines.document.NodesCount)
        {
          Processing_Ved_Static.Flipping_Lines._endDoc = true;
        }
        else
        {
          Processing_Ved_Static.Flipping_Lines._i_docRow_Curr = 0;
          Processing_Ved_Static.Flipping_Lines._Page_Curr = Processing_Ved_Static.Flipping_Lines.document.Nodes[Processing_Ved_Static.Flipping_Lines._i_Page_Curr] as PageData;
          if (Processing_Ved_Static.Flipping_Lines._Page_Curr.Name == "Следующая страница" || Processing_Ved_Static.Flipping_Lines._Page_Curr.Name == "Примечания")
          {
            Processing_Ved_Static.Flipping_Lines._MainTable = (TableData) Processing_Ved_Static.Flipping_Lines._Page_Curr.FindFirstNodeByName("Главная таблица");
            if (Processing_Ved_Static.Flipping_Lines._MainTable == null)
              Processing_Ved_Static.Flipping_Lines._endDoc = true;
          }
          else
            Processing_Ved_Static.Flipping_Lines._endDoc = true;
        }
      }
      return Processing_Ved_Static.Flipping_Lines._DocRow_Curr;
    }
  }
}
