// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.OrderReportTest
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

internal class OrderReportTest : CustomBackgroundTask
{
  private long _orderID;
  private long _templateID;
  /// <summary>Детали</summary>
  private int _objtypePartID;
  /// <summary>
  /// 
  /// </summary>
  private int _objtypeAssemblyUnit;
  /// <summary>Состав изделий</summary>
  private int _relTypeSPID;
  /// <summary>Технологический состав</summary>
  private int _relTypeTechCompositionID;
  /// <summary>Прочие изделия</summary>
  private int _otherProductsID;
  /// <summary>Стандартные изделия</summary>
  private int _standardProductID;
  /// <summary>Материал</summary>
  private int _materialID;
  private string operTemplate = "Тшт=(?<q1>[\\d\\,]+)  Тпз=(?<q2>[\\d\\,]+)";

  public OrderReportTest()
  {
    this._objtypePartID = MetaDataHelper.GetObjectTypeID("cad00250-306c-11d8-b4e9-00304f19f545");
    this._objtypeAssemblyUnit = MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545");
    this._relTypeSPID = MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
    this._relTypeTechCompositionID = MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545");
    this._otherProductsID = MetaDataHelper.GetObjectTypeID("cad0038d-306c-11d8-b4e9-00304f19f545");
    this._standardProductID = MetaDataHelper.GetObjectTypeID("cad00252-306c-11d8-b4e9-00304f19f545");
    this._materialID = MetaDataHelper.GetObjectTypeID("cad00172-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="orderID">Ид. заказа</param>
  /// <param name="templateID">Шаблон ведомости</param>
  internal static void Generate(long orderID, long templateID)
  {
    OrderReportTest task = new OrderReportTest();
    task._orderID = orderID;
    task._templateID = templateID;
    (ServicesManager.GetService(typeof (IBackgroundTaskView)) as IBackgroundTaskView).AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.GenerateOrderReport))
    {
      Name = "GenerateOrderReport_TestThread",
      IsBackground = true
    }.Start();
  }

  private void GenerateOrderReport()
  {
    try
    {
      this._state = BackgroundTaskState.Running;
      this.OnChanged(BackgroundTaskChangedType.State);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService customService = (ICompositionLoadService) sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService));
        IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00580-306c-11d8-b4e9-00304f19f545"));
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
        List<long> printedProductIDs = new List<long>();
        ColumnDescriptor[] collection1 = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
        };
        DateTime now1 = DateTime.Now;
        this._name = "Подсчет количества изделий конструкторского состава...";
        this.OnChanged(BackgroundTaskChangedType.Text);
        DataTable tableQuantities = customService.LoadComposition((object) sessionKeeper.Session.SessionGUID, this._orderID, objectTypeId, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
        {
          this._relTypeSPID
        }), (IEnumerable<int>) childrenIdRecursive, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) collection1), true, true, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, service.Filtration.OwnerID, (HybridDictionary) null, -1);
        TimeSpan timeSpan1 = DateTime.Now - now1;
        DateTime now2 = DateTime.Now;
        this._name = "Получение конструкторского и технологического составов...";
        this.OnChanged(BackgroundTaskChangedType.Text);
        ColumnDescriptor[] collection2 = new ColumnDescriptor[14]
        {
          new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 15248, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 1224, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 1184, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 1181, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 1222, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 1223, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) 15263, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
        DataTable composition = customService.LoadComposition((object) sessionKeeper.Session.SessionGUID, this._orderID, objectTypeId, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[2]
        {
          this._relTypeSPID,
          this._relTypeTechCompositionID
        }), (IEnumerable<int>) null, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) collection2), true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, service.Filtration.OwnerID, new HybridDictionary(1)
        {
          [(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"] = (object) true
        }, -1);
        composition.PrimaryKey = new DataColumn[3]
        {
          composition.Columns[0],
          composition.Columns[1],
          composition.Columns[2]
        };
        TimeSpan timeSpan2 = DateTime.Now - now2;
        DateTime now3 = DateTime.Now;
        this._name = "Формирование документа...";
        this.OnChanged(BackgroundTaskChangedType.Text);
        int countarticles = 0;
        ImDocument document = new ImDocument(DocumentEditorPlugin.LoadDocumentFromDBObject(sessionKeeper.Session.GetObjectActualCopy(this._templateID, true), -1, false, true, false), true, true);
        string str = $"{composition.Rows[0][5]} {composition.Rows[0][4]}";
        if (document.FindNode("15") is TextData node1)
          node1.AssignText(str, false, false, false);
        DocumentTreeNode node2 = document.FindNode("Рабочая область");
        this.PrintNode(document, node2, tableQuantities, composition, composition.Rows[0], printedProductIDs, composition.Columns.Count - 1, ref countarticles);
        document.UpdateLayout(0, true, false);
        long num1 = -1524884;
        sessionKeeper.Session.GetObject(num1);
        MemoryStream aSourceStream = new MemoryStream();
        try
        {
          document.SaveToXml((Stream) aSourceStream);
          aSourceStream.Position = 0L;
          BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, "report.imdx", ArcMethods.ZLibPacked, string.Empty);
          new BlobProcWriter(num1, AttributableElements.Object, DocIDCache.Attr_File, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        }
        finally
        {
          aSourceStream.Close();
        }
        TimeSpan timeSpan3 = DateTime.Now - now3;
        this._state = BackgroundTaskState.Terminated;
        this.OnChanged(BackgroundTaskChangedType.State);
        int num2 = (int) MessageBox.Show(string.Format("Конструкторский состав с группировкой: {4} Весь состав без группировки: {5} Изделий в отчете: {0}; Состав с подсчетом количества: {1}; Состав: {2}; Формирование документа: {3}", (object) countarticles, (object) timeSpan1, (object) timeSpan2, (object) timeSpan3, (object) tableQuantities.Rows.Count, (object) composition.Rows.Count));
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this.OnChanged(BackgroundTaskChangedType.Dispose);
    }
  }

  /// <summary>Распарсить строку с количеством</summary>
  /// <param name="str">Исходная строка</param>
  /// <param name="quantity">Количество</param>
  /// <param name="measure">Краткое наименование едюизмерения</param>
  private void ParseQuantity(string str, out int quantity, out string measure)
  {
    if (str == string.Empty)
    {
      quantity = 1;
      measure = "шт";
    }
    else
    {
      string[] strArray = str.Split(' ');
      quantity = Convert.ToInt32(strArray[0]);
      measure = strArray[1];
    }
  }

  /// <summary>Распарсить строку с количеством</summary>
  /// <param name="str">Исходная строка</param>
  /// <param name="quantity">Количество</param>
  /// <param name="measure">Краткое наименование едюизмерения</param>
  private void ParseDoubleQuantity(string str, out double quantity, out string measure)
  {
    if (str == string.Empty)
    {
      quantity = 1.0;
      measure = "шт";
    }
    else
    {
      try
      {
        string[] strArray = str.Split(' ');
        quantity = Convert.ToDouble(strArray[0], (IFormatProvider) CultureInfo.CurrentCulture);
        measure = strArray[1];
      }
      catch
      {
        quantity = 1.0;
        measure = "шт";
      }
    }
  }

  private void PrintNode(
    ImDocument document,
    DocumentTreeNode table,
    DataTable tableQuantities,
    DataTable composition,
    DataRow nodeRow,
    List<long> printedProductIDs,
    int partObjectIDIdx,
    ref int countarticles)
  {
    int int32 = Convert.ToInt32(nodeRow[2]);
    long int64_1 = Convert.ToInt64(nodeRow[partObjectIDIdx]);
    int quantity1 = 1;
    string measure1 = "шт";
    this.ParseQuantity(Convert.ToString(nodeRow[6]), out quantity1, out measure1);
    DataRow[] dataRowArray1 = tableQuantities.Select($"F_PART_OBJ_ID={int64_1}");
    int quantity2 = 0;
    string measure2 = string.Empty;
    if (dataRowArray1 != null && dataRowArray1.Length != 0 && Convert.ToString(dataRowArray1[0][0]) != string.Empty)
      this.ParseQuantity(Convert.ToString(dataRowArray1[0][0]), out quantity2, out measure2);
    ++countarticles;
    if (printedProductIDs.IndexOf(int64_1) >= 0)
      return;
    printedProductIDs.Add(int64_1);
    Dictionary<int, List<DataRow>> dictionary;
    switch (int32)
    {
      case 1052:
        DocumentTreeNode child1 = document.Template.FindNode("1268").CloneFromTemplate(true, true);
        table.AddChildNode(child1, false, false);
        (child1.FindFirstNodeFromTemplate_Recursive("Zak") as TextData).AssignText("0001", false, false, false);
        (child1.FindFirstNodeFromTemplate_Recursive("RANm") as TextData).AssignText(Convert.ToString(nodeRow[4]), false, false, false);
        (child1.FindFirstNodeFromTemplate_Recursive("RADn #5") as TextData).AssignText(Convert.ToString(nodeRow[5]), false, false, false);
        (child1.FindFirstNodeFromTemplate_Recursive("RACn #2") as TextData).AssignText(Convert.ToString(quantity1), false, false, false);
        (child1.FindFirstNodeFromTemplate_Recursive("RAFC") as TextData).AssignText(Convert.ToString(quantity2), false, false, false);
        DataRow[] dataRowArray2 = composition.Select($"[0]={int64_1} AND [2]={1037}");
        if (dataRowArray2 != null && dataRowArray2.Length != 0)
        {
          long int64_2 = Convert.ToInt64(dataRowArray2[0][partObjectIDIdx]);
          DataRow[] compositionRows = composition.Select($"[0]={int64_2}");
          dictionary = new Dictionary<int, List<DataRow>>(dataRowArray2.Length);
          if (compositionRows != null && compositionRows.Length != 0)
          {
            DocumentTreeNode child2 = document.Template.FindNode("1267").CloneFromTemplate(true, true);
            table.AddChildNode(child2, false, false);
            Dictionary<int, List<DataRow>> typedRows = this.GetTypedRows(compositionRows);
            try
            {
              List<DataRow> dataRowList1;
              if (typedRows.TryGetValue(1090, out dataRowList1))
              {
                DocumentTreeNode child3 = document.Template.FindNode("1024").CloneFromTemplate(true, true);
                table.AddChildNode(child3, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("Gпп #2") as TextData).AssignText(Convert.ToString(dataRowList1[0][7]), false, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("76") as TextData).AssignText(Convert.ToString(dataRowList1[0][11]), false, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("1026") as TextData).AssignText(Convert.ToString(dataRowList1[0][10]), false, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("НР") as TextData).AssignText(Convert.ToString(dataRowList1[0][8], (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
                double quantity3 = 1.0;
                string measure3 = "шт";
                this.ParseDoubleQuantity(Convert.ToString(dataRowList1[0][12]), out quantity3, out measure3);
                (child3.FindFirstNodeFromTemplate_Recursive("29") as TextData).AssignText(measure3, false, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("30") as TextData).AssignText(Convert.ToString(Math.Round(quantity3), (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
                (child3.FindFirstNodeFromTemplate_Recursive("31") as TextData).AssignText(Convert.ToString(Math.Round(quantity3 * (double) quantity1, 3), (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
                string str = Convert.ToString(dataRowList1[0][13]);
                if (str != string.Empty)
                {
                  double quantity4 = 1.0;
                  string measure4 = "шт";
                  this.ParseDoubleQuantity(str, out quantity4, out measure4);
                  (child3.FindFirstNodeFromTemplate_Recursive("32") as TextData).AssignText(Convert.ToString(Math.Round(quantity4), (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
                  (child3.FindFirstNodeFromTemplate_Recursive("33") as TextData).AssignText(Convert.ToString(Math.Round(quantity4 * (double) quantity1, 3), (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
                }
                List<DataRow> dataRowList2;
                if (typedRows.TryGetValue(1237, out dataRowList2))
                {
                  if (dataRowList2 != null)
                  {
                    if (dataRowList2.Count > 0)
                    {
                      for (int index = 0; index < dataRowList2.Count; ++index)
                        this.PrintTecnology(document, table, Convert.ToInt64(dataRowList2[index][partObjectIDIdx]), composition, quantity1, quantity2, partObjectIDIdx);
                    }
                  }
                }
              }
            }
            finally
            {
              dictionary = (Dictionary<int, List<DataRow>>) null;
            }
          }
        }
        DocumentTreeNode child4 = document.Template.FindNode("24").CloneFromTemplate(true, true);
        table.AddChildNode(child4, false, false);
        break;
      case 1074:
        DocumentTreeNode child5 = document.Template.FindNode("44").CloneFromTemplate(true, true);
        table.AddChildNode(child5, false, false);
        (child5.FindFirstNodeFromTemplate_Recursive("Zak #2") as TextData).AssignText("0001", false, false, false);
        (child5.FindFirstNodeFromTemplate_Recursive("RANm #2") as TextData).AssignText(Convert.ToString(nodeRow[4]), false, false, false);
        (child5.FindFirstNodeFromTemplate_Recursive("RADn #2") as TextData).AssignText(Convert.ToString(nodeRow[5]), false, false, false);
        (child5.FindFirstNodeFromTemplate_Recursive("RACn #4") as TextData).AssignText(Convert.ToString(quantity1), false, false, false);
        (child5.FindFirstNodeFromTemplate_Recursive("RAFC #2") as TextData).AssignText(Convert.ToString(quantity2), false, false, false);
        DataRow[] compositionRows1 = composition.Select($"[0]={int64_1}");
        if (compositionRows1 == null || compositionRows1.Length == 0)
          break;
        Dictionary<int, List<DataRow>> typedRows1 = this.GetTypedRows(compositionRows1);
        try
        {
          List<long> longList = new List<long>();
          List<DataRow> dataRowList3 = (List<DataRow>) null;
          if (typedRows1.TryGetValue(1037, out dataRowList3))
          {
            long int64_3 = Convert.ToInt64(dataRowList3[0][partObjectIDIdx]);
            DataRow[] dataRowArray3 = composition.Select($"[0]={int64_3}");
            if (dataRowArray3 != null && dataRowArray3.Length != 0)
            {
              for (int index1 = 0; index1 < dataRowArray3.Length; ++index1)
              {
                switch (Convert.ToInt32(dataRowArray3[index1][2]))
                {
                  case 1237:
                  case 1255:
                  case 1270:
                    longList.Add(Convert.ToInt64(dataRowArray3[index1][partObjectIDIdx]));
                    break;
                  case 1581:
                    DataRow[] dataRowArray4 = composition.Select($"[0]={Convert.ToInt64(dataRowArray3[index1][partObjectIDIdx])}");
                    if (dataRowArray4 != null && dataRowArray4.Length != 0)
                    {
                      DocumentTreeNode child6 = document.Template.FindNode("1033").CloneFromTemplate(true, true);
                      table.AddChildNode(child6, false, false);
                      for (int index2 = 0; index2 < dataRowArray4.Length; ++index2)
                      {
                        DocumentTreeNode child7 = document.Template.FindNode("1275").CloneFromTemplate(true, true);
                        table.AddChildNode(child7, false, false);
                        (child7.FindFirstNodeFromTemplate_Recursive("Gпп #3") as TextData).AssignText(Convert.ToString(dataRowArray4[index2][7]), false, false, false);
                        (child7.FindFirstNodeFromTemplate_Recursive("Овсм") as TextData).AssignText(Convert.ToString(dataRowArray4[index2][4]), false, false, false);
                        string str = Convert.ToString(dataRowArray4[index2][6]);
                        if (str != string.Empty)
                        {
                          string[] strArray = str.Split(' ');
                          (child7.FindFirstNodeFromTemplate_Recursive("41") as TextData).AssignText(strArray[1], false, false, false);
                          (child7.FindFirstNodeFromTemplate_Recursive("42") as TextData).AssignText(strArray[0], false, false, false);
                        }
                      }
                      break;
                    }
                    break;
                }
              }
            }
          }
          if (typedRows1.TryGetValue(this._standardProductID, out dataRowList3))
          {
            DocumentTreeNode child8 = document.Template.FindNode("14").CloneFromTemplate(true, true);
            table.AddChildNode(child8, false, false);
            for (int index = 0; index < dataRowList3.Count; ++index)
            {
              DocumentTreeNode child9 = document.Template.FindNode("17").CloneFromTemplate(true, true);
              table.AddChildNode(child9, false, false);
              (child9.FindFirstNodeFromTemplate_Recursive("Gпп #6") as TextData).AssignText(Convert.ToString(dataRowList3[index][7]), false, false, false);
              (child9.FindFirstNodeFromTemplate_Recursive("Нрвм #2") as TextData).AssignText(Convert.ToString(dataRowList3[index][4]), false, false, false);
              int quantity5 = 1;
              string measure5 = "шт";
              this.ParseQuantity(Convert.ToString(dataRowList3[index][6]), out quantity5, out measure5);
              (child9.FindFirstNodeFromTemplate_Recursive("1ЕиВ #2") as TextData).AssignText(measure5, false, false, false);
              (child9.FindFirstNodeFromTemplate_Recursive("54") as TextData).AssignText(Convert.ToString(quantity5), false, false, false);
              DataRow[] dataRowArray5 = tableQuantities.Select($"F_PART_OBJ_ID={Convert.ToInt64(dataRowList3[index][partObjectIDIdx])}");
              int quantity6 = 1;
              string measure6 = "шт";
              if (dataRowArray5 != null && dataRowArray5.Length != 0 && Convert.ToString(dataRowArray5[0][0]) != string.Empty)
              {
                this.ParseQuantity(Convert.ToString(dataRowArray5[0][0]), out quantity6, out measure6);
                (child9.FindFirstNodeFromTemplate_Recursive("55") as TextData).AssignText(Convert.ToString(quantity6), false, false, false);
              }
            }
          }
          if (typedRows1.TryGetValue(this._otherProductsID, out dataRowList3))
          {
            for (int index = 0; index < dataRowList3.Count; ++index)
            {
              DocumentTreeNode child10 = document.Template.FindNode("17").CloneFromTemplate(true, true);
              table.AddChildNode(child10, false, false);
              (child10.FindFirstNodeFromTemplate_Recursive("Gпп #6") as TextData).AssignText(Convert.ToString(dataRowList3[index][7]), false, false, false);
              (child10.FindFirstNodeFromTemplate_Recursive("Нрвм #2") as TextData).AssignText(Convert.ToString(dataRowList3[index][4]), false, false, false);
              int quantity7 = 1;
              string measure7 = "шт";
              this.ParseQuantity(Convert.ToString(dataRowList3[index][6]), out quantity7, out measure7);
              (child10.FindFirstNodeFromTemplate_Recursive("1ЕиВ #2") as TextData).AssignText(measure7, false, false, false);
              (child10.FindFirstNodeFromTemplate_Recursive("54") as TextData).AssignText(Convert.ToString(quantity7), false, false, false);
              DataRow[] dataRowArray6 = tableQuantities.Select($"F_PART_OBJ_ID={Convert.ToInt64(dataRowList3[index][partObjectIDIdx])}");
              int quantity8 = 1;
              string measure8 = "шт";
              if (dataRowArray6 != null && dataRowArray6.Length != 0 && Convert.ToString(dataRowArray6[0][0]) != string.Empty)
              {
                this.ParseQuantity(Convert.ToString(dataRowArray6[0][0]), out quantity8, out measure8);
                (child10.FindFirstNodeFromTemplate_Recursive("55") as TextData).AssignText(Convert.ToString(quantity8), false, false, false);
              }
            }
          }
          if (typedRows1.TryGetValue(this._materialID, out dataRowList3))
          {
            DocumentTreeNode child11 = document.Template.FindNode("57").CloneFromTemplate(true, true);
            table.AddChildNode(child11, false, false);
            for (int index = 0; index < dataRowList3.Count; ++index)
            {
              DocumentTreeNode child12 = document.Template.FindNode("59").CloneFromTemplate(true, true);
              table.AddChildNode(child12, false, false);
              (child12.FindFirstNodeFromTemplate_Recursive("Gпп #7") as TextData).AssignText(Convert.ToString(dataRowList3[index][7]), false, false, false);
              (child12.FindFirstNodeFromTemplate_Recursive("Нрвм #3") as TextData).AssignText(Convert.ToString(dataRowList3[index][4]), false, false, false);
              double quantity9 = 1.0;
              string measure9 = "шт";
              this.ParseDoubleQuantity(Convert.ToString(dataRowList3[index][6]), out quantity9, out measure9);
              (child12.FindFirstNodeFromTemplate_Recursive("1ЕиВ #3") as TextData).AssignText(measure9, false, false, false);
              (child12.FindFirstNodeFromTemplate_Recursive("60") as TextData).AssignText(Convert.ToString(quantity9, (IFormatProvider) CultureInfo.CurrentCulture), false, false, false);
            }
          }
          if (longList.Count > 0)
          {
            for (int index = 0; index < longList.Count; ++index)
              this.PrintTecnology(document, table, longList[index], composition, quantity1, quantity2, partObjectIDIdx);
          }
          DocumentTreeNode child13 = document.Template.FindNode("24").CloneFromTemplate(true, true);
          table.AddChildNode(child13, false, false);
          List<DataRow> dataRowList4;
          if (typedRows1.TryGetValue(this._objtypePartID, out dataRowList4))
          {
            for (int index = 0; index < dataRowList4.Count; ++index)
              this.PrintNode(document, table, tableQuantities, composition, dataRowList4[index], printedProductIDs, partObjectIDIdx, ref countarticles);
          }
          if (!typedRows1.TryGetValue(this._objtypeAssemblyUnit, out dataRowList4))
            break;
          for (int index = 0; index < dataRowList4.Count; ++index)
            this.PrintNode(document, table, tableQuantities, composition, dataRowList4[index], printedProductIDs, partObjectIDIdx, ref countarticles);
          break;
        }
        finally
        {
          dictionary = (Dictionary<int, List<DataRow>>) null;
        }
    }
  }

  private Dictionary<int, List<DataRow>> GetTypedRows(DataRow[] compositionRows)
  {
    Dictionary<int, List<DataRow>> typedRows = new Dictionary<int, List<DataRow>>(compositionRows.Length);
    for (int index = 0; index < compositionRows.Length; ++index)
    {
      int int32 = Convert.ToInt32(compositionRows[index][2]);
      List<DataRow> dataRowList;
      if (!typedRows.TryGetValue(int32, out dataRowList))
      {
        dataRowList = new List<DataRow>();
        typedRows.Add(int32, dataRowList);
      }
      dataRowList.Add(compositionRows[index]);
    }
    return typedRows;
  }

  private void PrintTecnology(
    ImDocument document,
    DocumentTreeNode table,
    long processID,
    DataTable composition,
    int quantity,
    int quantity_sum,
    int partObjectIDIdx)
  {
    DataRow[] dataRowArray1 = composition.Select($"[0]={processID} AND [2]={1110}");
    if (dataRowArray1 == null || dataRowArray1.Length == 0)
      return;
    for (int index1 = 0; index1 < dataRowArray1.Length; ++index1)
    {
      DataRow[] dataRowArray2 = composition.Select($"[0]={Convert.ToInt64(dataRowArray1[index1][partObjectIDIdx])} AND [2]={1075}");
      if (dataRowArray2 != null && dataRowArray2.Length != 0)
      {
        DocumentTreeNode child1 = document.Template.FindNode("1035").CloneFromTemplate(true, true);
        table.AddChildNode(child1, false, false);
        for (int index2 = 0; index2 < dataRowArray2.Length; ++index2)
        {
          string[] strArray = Convert.ToString(dataRowArray2[index2][5]).Split(' ');
          DocumentTreeNode child2 = document.Template.FindNode("1266").CloneFromTemplate(true, true);
          table.AddChildNode(child2, false, false);
          (child2.FindFirstNodeFromTemplate_Recursive("N_ОП") as TextData).AssignText(strArray[0], false, false, false);
          (child2.FindFirstNodeFromTemplate_Recursive("Р") as TextData).AssignText(Convert.ToString(dataRowArray2[index2][9]), false, false, false);
          (child2.FindFirstNodeFromTemplate_Recursive("ОПЕР") as TextData).AssignText(Convert.ToString(dataRowArray2[index2][4]), false, false, false);
          DataRow[] dataRowArray3 = composition.Select($"[0]={Convert.ToInt64(dataRowArray2[index2][partObjectIDIdx])} AND [2]={1118}");
          if (dataRowArray3 != null && dataRowArray3.Length != 0)
          {
            DocumentTreeNode templateRecursive = child2.FindFirstNodeFromTemplate_Recursive("1287");
            DocumentTreeNode documentTreeNode = document.Template.FindNode("1288").CloneFromTemplate(true, true);
            DocumentTreeNode child3 = documentTreeNode;
            templateRecursive.AddChildNode(child3, false, false);
            (documentTreeNode.FindFirstNodeFromTemplate_Recursive("МОД") as TextData).AssignText(Convert.ToString(dataRowArray3[0][4]), false, false, false);
          }
          DataRow[] dataRowArray4 = composition.Select($"[0]={Convert.ToInt64(dataRowArray2[index2][partObjectIDIdx])} AND [2]={1212}");
          if (dataRowArray4 != null && dataRowArray4.Length != 0)
          {
            Match match = new Regex(this.operTemplate).Match(Convert.ToString(dataRowArray4[0][4]));
            try
            {
              double num1 = Convert.ToDouble(match.Groups["q1"].Value);
              double num2 = Convert.ToDouble(match.Groups["q2"].Value);
              string str1 = Convert.ToString(Math.Round((num1 + num2 / (double) quantity) / 60.0, 3));
              string str2 = Convert.ToString(Math.Round((num1 + num2 / (double) quantity) / 60.0 * (double) quantity_sum, 3));
              DocumentTreeNode templateRecursive = child2.FindFirstNodeFromTemplate_Recursive("Тпз");
              DocumentTreeNode documentTreeNode = document.Template.FindNode("8").CloneFromTemplate(true, true);
              DocumentTreeNode child4 = documentTreeNode;
              templateRecursive.AddChildNode(child4, false, false);
              (documentTreeNode.FindFirstNodeFromTemplate_Recursive("9") as TextData).AssignText(match.Groups["q1"].Value, false, false, false);
              (documentTreeNode.FindFirstNodeFromTemplate_Recursive("10") as TextData).AssignText(match.Groups["q2"].Value, false, false, false);
              (documentTreeNode.FindFirstNodeFromTemplate_Recursive("11") as TextData).AssignText(str1, false, false, false);
              (documentTreeNode.FindFirstNodeFromTemplate_Recursive("12") as TextData).AssignText(str2, false, false, false);
            }
            catch
            {
            }
          }
        }
      }
    }
  }
}
