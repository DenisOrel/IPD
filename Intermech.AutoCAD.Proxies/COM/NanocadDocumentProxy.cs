// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.NanocadDocumentProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Runtime.ComInterop.Proxies;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>Прокси-объект для COM-объекта документа nanoCAD.</summary>
/// <summary>Создает объект.</summary>
/// <param name="rawDocument">Необернутый COM-объект документа</param>
/// <param name="documentName">Имя документа для сообщений об ошибках</param>
/// <param name="cadSystem">Прокси-объект приложения</param>
public sealed class NanocadDocumentProxy(
  object rawDocument,
  string documentName,
  CadProxy cadSystem) : CadDocumentProxy(rawDocument, documentName, cadSystem)
{
  private static readonly string DefaultPdfPrinterName = "Встроенный PDF-принтер";
  private static readonly string ExistingFileOptionName = "Существующий файл";
  private static readonly string ExistingFileOptionValue = "Присоединить страницу к существующему файлу";

  protected override void DoCheckPlotDeviceNames()
  {
    // ISSUE: reference to a compiler-generated field
    if (NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveLayout", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__0, this.rawDocument);
    // ISSUE: reference to a compiler-generated field
    if (NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RefreshPlotDeviceInfo", (IEnumerable<Type>) null, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1);
    // ISSUE: reference to a compiler-generated field
    if (NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, IEnumerable<object>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable<object>), typeof (NanocadDocumentProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable<object>> target = NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable<object>>> p3 = NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetPlotDeviceNames", (IEnumerable<Type>) null, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__2.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__1.\u003C\u003Ep__2, obj1);
    if (!target((CallSite) p3, obj2).Contains<object>((object) NanocadDocumentProxy.DefaultPdfPrinterName))
      throw new ApplicationProxyException($"Не удалось найти встроенный принтер с именем \"{NanocadDocumentProxy.DefaultPdfPrinterName}\". Проверьте настройки своей CAD-системы.");
  }

  protected override void DoExportToPdf(string pdfFileName, string fileVaultTempAreaPath)
  {
    try
    {
      if (File.Exists(pdfFileName))
        File.Delete(pdfFileName);
      string directoryName = Path.GetDirectoryName(pdfFileName);
      if (!Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      // ISSUE: reference to a compiler-generated field
      if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target1 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Layouts", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, this.rawDocument);
      object obj2 = target1((CallSite) p1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertArrayIndex, typeof (int), typeof (NanocadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      string[] strArray1 = new string[NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__2, obj2)];
      int num1 = 0;
      while (true)
      {
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target2 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p4 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThan, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__3, num1, obj2);
        if (target2((CallSite) p4, obj3))
        {
          string[] strArray2 = strArray1;
          int index = num1;
          // ISSUE: reference to a compiler-generated field
          if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (NanocadDocumentProxy)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target3 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__8.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p8 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__8;
          // ISSUE: reference to a compiler-generated field
          if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target4 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p7 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, int, object> target5 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, int, object>> p6 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__6;
          // ISSUE: reference to a compiler-generated field
          if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Layouts", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj4 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__5.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__5, this.rawDocument);
          int num2 = num1;
          object obj5 = target5((CallSite) p6, obj4, num2);
          object obj6 = target4((CallSite) p7, obj5);
          string str = target3((CallSite) p8, obj6);
          strArray2[index] = str;
          ++num1;
        }
        else
          break;
      }
      // ISSUE: reference to a compiler-generated field
      if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__10 = CallSite<Action<CallSite, object, string[]>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetLayoutsToPlot", (IEnumerable<Type>) null, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, object, string[]> target6 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, object, string[]>> p10 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Plot", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__9.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__9, this.rawDocument);
      string[] strArray3 = strArray1;
      target6((CallSite) p10, obj7, strArray3);
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, string, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "PlotToFile", (IEnumerable<Type>) null, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, string, object> target7 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, string, object>> p12 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Plot", typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__11.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__11, this.rawDocument);
        string str = pdfFileName;
        string defaultPdfPrinterName = NanocadDocumentProxy.DefaultPdfPrinterName;
        object obj9 = target7((CallSite) p12, obj8, str, defaultPdfPrinterName);
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target8 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p14 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (NanocadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__13.Target((CallSite) NanocadDocumentProxy.\u003C\u003Eo__2.\u003C\u003Ep__13, obj9);
        if (target8((CallSite) p14, obj10))
          throw new ApplicationProxyException($"{this.cadSystem.ApplicationName} не удалось сформировать аутентичный файл. Подробности смотрите в окне {this.cadSystem.ApplicationName}.");
      }
      finally
      {
        if (!File.Exists(pdfFileName))
          throw new ApplicationProxyException($"Не удалось осуществить экспорт чертежа в pdf-документ. Проверьте настройки печати принтера \"{NanocadDocumentProxy.DefaultPdfPrinterName}\": для поля \"{NanocadDocumentProxy.ExistingFileOptionName}\" должна быть выбрана настройка \"{NanocadDocumentProxy.ExistingFileOptionValue}\".");
      }
    }
    catch (COMException ex)
    {
      throw new ApplicationProxyException($"При экспорте документа {this.documentName} в PDF произошла внутренняя ошибка приложения: {ex.Message}.");
    }
  }

  /// <summary>
  /// Проверяет, является ли текущий документ CAD-системы новым и еще не сохраненным на диск.
  /// Для этого используется значение свойства IAcadDocument.FullName
  /// </summary>
  /// <param name="fullName">Значение свойства IAcadDocument.FullName</param>
  /// <returns>true - новый документ без файла на диске; false - существующий документ, открытый из файла на диске</returns>
  protected override bool DoTestIsNewDocument(string fullName) => !Path.IsPathRooted(fullName);
}
