// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadDocumentProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Runtime;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Win32;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта документа CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
public class CadDocumentProxy : CadObjectProxy, ICadDocumentProxy
{
  private static readonly string BackgroundPlotVarName = "BACKGROUNDPLOT";
  private static readonly int BackgroundPlotVarTestValue = 0;
  protected object rawDocument;
  protected readonly string documentName;
  protected CadProxy cadSystem;

  /// <summary>Создает объект.</summary>
  /// <param name="rawDocument">Необернутый COM-объект документа</param>
  /// <param name="documentName">Имя документа для сообщений об ошибках</param>
  /// <param name="cadSystem">Прокси-объект приложения</param>
  public CadDocumentProxy(object rawDocument, string documentName, CadProxy cadSystem)
  {
    if (rawDocument == null)
      throw new ArgumentNullException("documentObject");
    if (documentName == null)
      throw new ArgumentNullException(nameof (documentName));
    if (cadSystem == null)
      throw new ArgumentNullException("proxy");
    this.rawDocument = rawDocument;
    this.documentName = documentName;
    this.cadSystem = cadSystem;
  }

  /// <summary>
  /// Возвращает описание мастер-файла документа. Если новый документ еще не был сохранен на диске, то
  /// этот метод будет возвращать null.
  /// </summary>
  /// <returns>Мастер-файл документа</returns>
  public string GetMasterFile()
  {
    string fullName = this.RawGetFullName();
    return !this.DoTestIsNewDocument(fullName) ? fullName : (string) null;
  }

  /// <summary>Возвращает сателлитные файлы документа.</summary>
  /// <param name="selectedTypes">Выбранные типы присоединенных документов</param>
  /// <returns>Список файлов</returns>
  public List<string> GetSatelliteFiles(SatelliteFileType selectedTypes)
  {
    return this.GetSatelliteFilesNew(selectedTypes);
  }

  private List<string> GetSatelliteFilesNew(SatelliteFileType selectedTypes)
  {
    string masterFile = this.GetMasterFile();
    string directoryName = string.IsNullOrEmpty(masterFile) ? (string) null : Path.GetDirectoryName(masterFile);
    string supportPath = this.cadSystem.SupportPath;
    string[] strArray;
    if (!string.IsNullOrEmpty(supportPath))
      strArray = supportPath.Split(';');
    else
      strArray = (string[]) null;
    string[] supportPaths = strArray;
    List<string> graphicsSatelliteFiles = this.GetGraphicsSatelliteFiles(selectedTypes, directoryName, supportPaths);
    if ((selectedTypes & SatelliteFileType.Dwg) == SatelliteFileType.Dwg)
    {
      List<string> xrefSatelliteFiles = this.GetXRefSatelliteFiles(directoryName, supportPaths);
      graphicsSatelliteFiles.AddRange((IEnumerable<string>) xrefSatelliteFiles);
    }
    return graphicsSatelliteFiles;
  }

  /// <summary>
  /// Пытается найти присоединенные к чертежу графические файлы (pdf, растровые изображения)
  /// </summary>
  /// <param name="selectedTypes">Выбранные типы присоединенных документов</param>
  /// <param name="masterFileDirectory">Директория исходного чертежа</param>
  /// <param name="supportPaths">Вспомогательные пути</param>
  /// <returns>Коллекция абсолютных путей к присоединенным файлам</returns>
  private List<string> GetGraphicsSatelliteFiles(
    SatelliteFileType selectedTypes,
    string masterFileDirectory,
    string[] supportPaths)
  {
    List<string> graphicsSatelliteFiles = new List<string>();
    CadSelectionSetFilterBuilder setFilterBuilder = this.cadSystem.CreateSelectionSetFilterBuilder();
    if ((selectedTypes & SatelliteFileType.RasterImage) == SatelliteFileType.RasterImage)
      setFilterBuilder.EntityTypeFilter.Add(DxfEntityType.IMAGE);
    if ((selectedTypes & SatelliteFileType.Underlay) == SatelliteFileType.Underlay)
    {
      setFilterBuilder.EntityTypeFilter.Add(DxfEntityType.PDFUNDERLAY);
      setFilterBuilder.EntityTypeFilter.Add(DxfEntityType.DWFUNDERLAY);
      setFilterBuilder.EntityTypeFilter.Add(DxfEntityType.DGNUNDERLAY);
    }
    foreach (CadEntityProxy selectAllEntity in this.SelectAllEntities(setFilterBuilder.ToFilter()))
    {
      if (selectAllEntity is IСadEntityProxyWithFile entityProxyWithFile)
      {
        string filePath = entityProxyWithFile.TryGetFilePath();
        if (filePath != null)
        {
          string satelliteFilePath = this.GetSatelliteFilePath(filePath, masterFileDirectory, supportPaths);
          if (satelliteFilePath != null)
            graphicsSatelliteFiles.Add(satelliteFilePath);
        }
      }
    }
    setFilterBuilder.Clear();
    return graphicsSatelliteFiles;
  }

  /// <summary>Пытается найти присоединенные к чертежу иные чертежи</summary>
  /// <param name="masterFileDirectory">Директория исходного чертежа</param>
  /// <param name="supportPaths">Вспомогательные пути</param>
  /// <returns>Коллекция абсолютных путей к присоединенным файлам</returns>
  private List<string> GetXRefSatelliteFiles(string masterFileDirectory, string[] supportPaths)
  {
    List<string> xrefSatelliteFiles = new List<string>();
    foreach (CadBlockProxy selectBlock in this.SelectBlocks())
    {
      string path = selectBlock.Path;
      if (path != null)
      {
        string satelliteFilePath = this.GetSatelliteFilePath(path, masterFileDirectory, supportPaths);
        if (satelliteFilePath != null)
          xrefSatelliteFiles.Add(satelliteFilePath);
      }
    }
    return xrefSatelliteFiles;
  }

  /// <summary>
  /// Пытается найти присоединенный файл по его пути, необязательно полному, директории с исходным чертежом и вспомогательным путям
  /// </summary>
  /// <param name="rawFilePath">Путь к присоединенному файлу</param>
  /// <param name="masterFileDirectory">Директория исходного чертежа; может быть равен null</param>
  /// <param name="supportPaths">Вспомогательные пути; может быть равен null</param>
  /// <returns>Абсолютный путь к присоединенному файлу либо null, если путь не найден</returns>
  private string GetSatelliteFilePath(
    string rawFilePath,
    string masterFileDirectory,
    string[] supportPaths)
  {
    if (Path.IsPathRooted(rawFilePath) && File.Exists(rawFilePath))
      return Path.GetFullPath(rawFilePath);
    if (masterFileDirectory != null)
    {
      string path1 = Path.Combine(masterFileDirectory, rawFilePath);
      if (File.Exists(path1))
        return Path.GetFullPath(path1);
      if (supportPaths != null)
      {
        string fileName = Path.GetFileName(rawFilePath);
        foreach (string supportPath in supportPaths)
        {
          string path2 = Path.Combine(supportPath, fileName);
          if (File.Exists(path2))
            return Path.GetFullPath(path2);
        }
      }
    }
    return (string) null;
  }

  /// <summary>
  /// Данный метод устарел в связи с удалением необходимого API в CAD-системах
  /// </summary>
  private List<string> GetSatelliteFilesOld(DwgLookupMode dwgLookupMode)
  {
    List<string> satelliteFilesOld = new List<string>(32 /*0x20*/);
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Action<CallSite, CadDocumentProxy, DwgLookupMode, object, List<string>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "ScanXRefsForSatelliteFiles", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    CadDocumentProxy.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__7.\u003C\u003Ep__0, this, dwgLookupMode, this.rawDocument, satelliteFilesOld);
    return satelliteFilesOld;
  }

  private void ScanXRefsForSatelliteFiles(
    DwgLookupMode dwgLookupMode,
    object doc,
    List<string> satelliteFiles)
  {
    if (dwgLookupMode == DwgLookupMode.Skip)
      return;
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int?>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (int?), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int?> target1 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int?>> p1 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadDocumentProxy, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "TryGetFileDependenciesCount", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__0, this, doc);
      int num1 = target1((CallSite) p1, obj1) ?? 0;
      for (int index = 1; index <= num1; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Item", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target2 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p3 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FileDependencies", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__2, doc);
        int num2 = index;
        object obj3 = target2((CallSite) p3, obj2, num2);
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p6 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, object> target4 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, object>> p5 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__5;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Feature", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__4.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__4, obj3);
        object obj5 = target4((CallSite) p5, obj4, "Acad:XRef");
        if (target3((CallSite) p6, obj5))
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target5 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__8.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p8 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__8;
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FullFileName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__7, obj3);
          string path = target5((CallSite) p8, obj6);
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__13 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target6 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__13.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p13 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__13;
          bool flag = !Path.IsPathRooted(path);
          object obj7;
          if (flag)
          {
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__12 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__12 = CallSite<Func<CallSite, bool, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, bool, object, object> target7 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__12.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, bool, object, object>> p12 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__12;
            int num3 = flag ? 1 : 0;
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__11 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, object> target8 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__11.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, object>> p11 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__11;
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__10 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__10 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "IsNullOrEmpty", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, Type, object, object> target9 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__10.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, Type, object, object>> p10 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__10;
            Type type = typeof (string);
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__9 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FoundPath", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj8 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__9.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__9, obj3);
            object obj9 = target9((CallSite) p10, type, obj8);
            object obj10 = target8((CallSite) p11, obj9);
            obj7 = target7((CallSite) p12, num3 != 0, obj10);
          }
          else
            obj7 = (object) flag;
          if (target6((CallSite) p13, obj7))
          {
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__17 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string> target10 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__17.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string>> p17 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__17;
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__16 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__16 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetFullPath", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, Type, object, object> target11 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__16.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, Type, object, object>> p16 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__16;
            Type type1 = typeof (Path);
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__15 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__15 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Combine", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, Type, object, string, object> target12 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__15.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, Type, object, string, object>> p15 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__15;
            Type type2 = typeof (Path);
            // ISSUE: reference to a compiler-generated field
            if (CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__14 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FoundPath", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj11 = CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__14.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__8.\u003C\u003Ep__14, obj3);
            string str = path;
            object obj12 = target12((CallSite) p15, type2, obj11, str);
            object obj13 = target11((CallSite) p16, type1, obj12);
            path = target10((CallSite) p17, obj13);
          }
          if (Path.IsPathRooted(path) && File.Exists(path) && !satelliteFiles.Contains(path))
            satelliteFiles.Add(path);
        }
      }
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.FileDependencies", "свойство было исключено из API CAD-системы");
    }
  }

  private int? TryGetFileDependenciesCount(object doc)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (int), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> target1 = CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> p2 = CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FileDependencies", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__9.\u003C\u003Ep__0, doc);
      object obj2 = target2((CallSite) p1, obj1);
      return new int?(target1((CallSite) p2, obj2));
    }
    catch
    {
      return new int?();
    }
  }

  internal List<CadEntityProxy> SelectAllEntities(CadSelectionSetFilter filter)
  {
    List<object> objectList = filter != null ? this.RawSelectAllEntities(filter) : throw new ArgumentNullException(nameof (filter));
    List<CadEntityProxy> cadEntityProxyList1 = new List<CadEntityProxy>(objectList.Count);
    foreach (object obj1 in objectList)
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Action<CallSite, List<CadEntityProxy>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, List<CadEntityProxy>, object> target = CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, List<CadEntityProxy>, object>> p1 = CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__1;
      List<CadEntityProxy> cadEntityProxyList2 = cadEntityProxyList1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "CreateEntityProxy", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__10.\u003C\u003Ep__0, this.cadSystem, obj1, this);
      target((CallSite) p1, cadEntityProxyList2, obj2);
    }
    return cadEntityProxyList1;
  }

  private List<object> RawSelectAllEntities(CadSelectionSetFilter filter)
  {
    string str1 = $"{"TEMP"}_{Path.GetRandomFileName()}";
    object rawSelectionSet = (object) null;
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target1 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p1 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "SelectionSets", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__0, this.rawDocument);
      string str2 = str1;
      rawSelectionSet = target1((CallSite) p1, obj1, str2);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__2 = CallSite<Action<CallSite, object, int, object, object, short[], object[]>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Select", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__2, rawSelectionSet, 5, (object) null, (object) null, filter.Ids, filter.Values);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__3.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__3, rawSelectionSet);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__4 = CallSite<Func<CallSite, Type, object, List<object>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      List<object> objectList1 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__4.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__4, typeof (List<object>), obj2);
      int num = 0;
      while (true)
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target2 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p6 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__5 = CallSite<Func<CallSite, int, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThan, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__5.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__5, num, obj2);
        if (target2((CallSite) p6, obj3))
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__8 = CallSite<Action<CallSite, List<object>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<object>, object> target3 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__8.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<object>, object>> p8 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__8;
          List<object> objectList2 = objectList1;
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Item", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj4 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__7, rawSelectionSet, num);
          target3((CallSite) p8, objectList2, obj4);
          ++num;
        }
        else
          break;
      }
      return objectList1;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.CADSystem.ApplicationName, "IAcadSelectionSet.Select()");
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p10 = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__9.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__9, rawSelectionSet, (object) null);
      if (target((CallSite) p10, obj))
        SilentActionInvoker.Default.Invoke((Action) (() =>
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__11 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__11 = CallSite<Action<CallSite, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Delete", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__11.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__11.\u003C\u003Ep__11, rawSelectionSet);
        }));
    }
  }

  internal List<CadBlockProxy> SelectBlocks(bool isXRefOnly = true)
  {
    List<object> objectList = this.RawSelectBlocks(isXRefOnly);
    List<CadBlockProxy> cadBlockProxyList1 = new List<CadBlockProxy>(objectList.Count);
    foreach (object obj1 in objectList)
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Action<CallSite, List<CadBlockProxy>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, List<CadBlockProxy>, object> target = CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, List<CadBlockProxy>, object>> p1 = CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__1;
      List<CadBlockProxy> cadBlockProxyList2 = cadBlockProxyList1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "CreateBlockProxy", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__12.\u003C\u003Ep__0, this.cadSystem, obj1, this);
      target((CallSite) p1, cadBlockProxyList2, obj2);
    }
    return cadBlockProxyList1;
  }

  private List<object> RawSelectBlocks(bool isXRefOnly)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Blocks", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__0, this.rawDocument);
      List<object> objectList = new List<object>();
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      foreach (object obj2 in CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__7, obj1))
      {
        object obj3;
        if (isXRefOnly)
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, bool, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, bool, object, object> target = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__2.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, bool, object, object>> p2 = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__2;
          int num = isXRefOnly ? 1 : 0;
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "IsXRef", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj4 = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__1, obj2);
          obj3 = target((CallSite) p2, num != 0, obj4);
        }
        else
          obj3 = (object) isXRefOnly;
        object obj5 = obj3;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__5.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__5, obj5))
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__4.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p4 = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__4;
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__3.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__3, obj5, !isXRefOnly);
          if (!target((CallSite) p4, obj6))
            continue;
        }
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__6 = CallSite<Action<CallSite, List<object>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__6.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__13.\u003C\u003Ep__6, objectList, obj2);
      }
      return objectList;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Blocks");
    }
  }

  public void Activate()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (Activate), (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__14.\u003C\u003Ep__0, this.rawDocument);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Activate()");
    }
  }

  public void Save() => this.SaveInternal();

  private void SaveInternal()
  {
    if (this.DoTestIsNewDocument(this.RawGetFullName()))
    {
      this.RawSaveToNewFile();
    }
    else
    {
      if (!this.Modified)
        return;
      this.RawSave();
    }
  }

  private void RawSaveToNewFile()
  {
    IntPtr app = this.cadSystem.SwitchToApp();
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SendCommand", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__17.\u003C\u003Ep__0, this.rawDocument, "_save\n");
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.SendCommand(\"_save\\n\")");
    }
    finally
    {
      if (app != IntPtr.Zero)
        ForegroundWindowHelper.Default.TrySetWindow(app);
    }
  }

  private void RawSave()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Save", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__18.\u003C\u003Ep__0, this.rawDocument);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Save()");
    }
  }

  public void Close(bool saveChanges)
  {
    if (saveChanges)
      this.Save();
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, bool, Missing>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (Close), (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__19.\u003C\u003Ep__0, this.rawDocument, false, Missing.Value);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Close()");
    }
  }

  /// <summary>Экспортируем активный документ в PDF файл</summary>
  /// <param name="pdfFileName">полный путь pdf файла с именем и расширением</param>
  public void ExportToPDF(string pdfFileName, string fileVaultTempAreaPath, bool cadmechFinded)
  {
    if (string.IsNullOrEmpty(pdfFileName))
      throw new ArgumentException("Не задан путь к pdf-файлу.", nameof (pdfFileName));
    if (!Path.IsPathRooted(pdfFileName))
      throw new ArgumentException("Путь к pdf-файлу должен быть абсолютным.", nameof (pdfFileName));
    if (string.IsNullOrEmpty(fileVaultTempAreaPath))
      throw new ArgumentException("Не задан путь к временному хранилищу.", nameof (fileVaultTempAreaPath));
    if (!Path.IsPathRooted(fileVaultTempAreaPath))
      throw new ArgumentException("Путь к временному хранилищу должен быть абсолютным.", nameof (fileVaultTempAreaPath));
    if (!Directory.Exists(fileVaultTempAreaPath))
      throw new ApplicationProxyException($"Директория временного хранилища {fileVaultTempAreaPath} не найдена.");
    this.DoCheckPlotDeviceNames();
    this.DoSetCanonicalMediaNames();
    this.DoCheckCadmechAndExportToPdf(pdfFileName, fileVaultTempAreaPath, cadmechFinded);
  }

  protected virtual void DoCheckPlotDeviceNames()
  {
  }

  protected virtual void DoSetCanonicalMediaNames()
  {
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ActiveLayout", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__0, this.rawDocument);
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__19 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (CadDocumentProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target1 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__19.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p19 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__19;
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Layouts", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__1.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__1, this.rawDocument);
    foreach (object obj3 in target1((CallSite) p19, obj2))
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target2 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p8 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__8;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ModelType", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__2, obj3);
      object obj5 = target3((CallSite) p3, obj4);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj6;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__7, obj5))
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target4 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p6 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__6;
        object obj7 = obj5;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "IsNullOrEmpty", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, Type, object, object> target5 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, Type, object, object>> p5 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__5;
        Type type = typeof (string);
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "CanonicalMediaName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__4.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__4, obj3);
        object obj9 = target5((CallSite) p5, type, obj8);
        obj6 = target4((CallSite) p6, obj7, obj9);
      }
      else
        obj6 = obj5;
      if (target2((CallSite) p8, obj6))
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "ActiveLayout", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__9.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__9, this.rawDocument, obj3);
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target6 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p12 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target7 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p11 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__11;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "PlotType", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__10.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__10, obj3);
        object obj12 = target7((CallSite) p11, obj11, 5);
        if (target6((CallSite) p12, obj12))
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__13 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "PlotType", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj13 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__13.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__13, obj3, 5);
        }
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__14 = CallSite<Func<CallSite, CadDocumentProxy, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetCanonicalMediaName", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj14 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__14.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__14, this, obj3);
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target8 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__16.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p16 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__16;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__15.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__15, obj14, (object) null);
        if (target8((CallSite) p16, obj15))
        {
          // ISSUE: reference to a compiler-generated field
          if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__17 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          throw new ApplicationProxyException($"Не удалось экспортировать документ: для листа {CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__17.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__17, obj3)} не задан формат.");
        }
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "CanonicalMediaName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__18.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__18, obj3, obj14);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__20 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "ActiveLayout", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj17 = CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__20.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__22.\u003C\u003Ep__20, this.rawDocument, obj1);
  }

  /// <summary>Пытается получить любой доступный формат листа</summary>
  /// <param name="activeLayout">Активное пространство, с помощью которого осуществляется попытка получения форматов</param>
  /// <returns>Первое полученное имя допустимого формата</returns>
  /// <exception cref="T:Intermech.Runtime.ComInterop.Proxies.ApplicationProxyException">Вызывается в случае, когда не был найден ни один допустимый формат</exception>
  protected string GetCanonicalMediaName(object activeLayout)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RefreshPlotDeviceInfo", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__0, activeLayout);
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetCanonicalMediaNames", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__1.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__1, activeLayout);
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p4 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int, object> target2 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int, object>> p3 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Length", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__2, obj1);
    object obj3 = target2((CallSite) p3, obj2, 0);
    if (target1((CallSite) p4, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target3 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p6 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__5.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__5, obj1, 0);
      return target3((CallSite) p6, obj4);
    }
    string str1 = $"{"TEMP"}_{Path.GetRandomFileName()}";
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Add", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target4 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__8.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p8 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__8;
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "PlotConfigurations", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__7, this.rawDocument);
    string str2 = str1;
    object obj6 = target4((CallSite) p8, obj5, str2);
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetCanonicalMediaNames", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__9.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__9, obj6);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target5 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__12.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p12 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__12;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target6 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__11.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p11 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__11;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Length", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__10.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__10, obj7);
      object obj9 = target6((CallSite) p11, obj8, 0);
      if (target5((CallSite) p12, obj9))
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> target7 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> p14 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__13.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__13, obj7, 0);
        return target7((CallSite) p14, obj10);
      }
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__15 = CallSite<Action<CallSite, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Delete", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__15.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__23.\u003C\u003Ep__15, obj6);
    }
    return (string) null;
  }

  private void DoCheckCadmechAndExportToPdf(
    string pdfFileName,
    string fileVaultTempAreaPath,
    bool cadmechFinded)
  {
    if (File.Exists(pdfFileName))
      File.Delete(pdfFileName);
    string directoryName = Path.GetDirectoryName(pdfFileName);
    if (!Directory.Exists(directoryName))
      Directory.CreateDirectory(directoryName);
    if (cadmechFinded)
      this.DoExportToPdfCadmech(pdfFileName, fileVaultTempAreaPath);
    else
      this.DoExportToPdf(pdfFileName, fileVaultTempAreaPath);
  }

  private void DoExportToPdfCadmech(string pdfFileName, string fileVaultTempAreaPath)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    string str = $"{CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__0, this.rawDocument)}.pdf";
    if (File.Exists(str))
      File.Delete(str);
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Action<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SendCommand", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__1, this.rawDocument, "(load \"lisp/dwgtopdf\")\n");
    // ISSUE: reference to a compiler-generated field
    if (CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__2 = CallSite<Action<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SendCommand", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__25.\u003C\u003Ep__2, this.rawDocument, "DWGTOPDF\n");
    if (File.Exists(str))
    {
      if (!(str.ToUpper() != pdfFileName.ToUpper()))
        return;
      try
      {
        File.Copy(str, pdfFileName, true);
      }
      finally
      {
        if (!File.Exists(pdfFileName))
          throw new ApplicationProxyException($"{this.cadSystem.ApplicationName} не удалось сформировать аутентичный файл. Подробности смотрите в окне {this.cadSystem.ApplicationName}.");
      }
    }
    else
      this.DoExportToPdf(pdfFileName, fileVaultTempAreaPath);
  }

  protected virtual void DoExportToPdf(string pdfFileName, string fileVaultTempAreaPath)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__0, this.rawDocument);
      string str1 = target1((CallSite) p1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target2 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__2.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__2, this.rawDocument);
      string str2 = target2((CallSite) p3, obj2);
      string str3 = "FILEDIA";
      List<PublishFileInfo> source = new List<PublishFileInfo>();
      List<string> entries = new List<string>();
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Layouts", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__4.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__4, this.rawDocument);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      foreach (object obj4 in CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__9.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__9, obj3))
      {
        PublishFileInfo publishFileInfo1 = new PublishFileInfo();
        publishFileInfo1.DwgPath = str2;
        publishFileInfo1.DwgName = str1;
        PublishFileInfo publishFileInfo2 = publishFileInfo1;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> target3 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> p6 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__5.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__5, obj4);
        string str4 = target3((CallSite) p6, obj5);
        publishFileInfo2.LayoutName = str4;
        publishFileInfo1.Setup = string.Empty;
        PublishFileInfo publishFileInfo3 = publishFileInfo1;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (int), typeof (CadDocumentProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> target4 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__8.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> p8 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__8;
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "TabOrder", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__7.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__7, obj4);
        int num = target4((CallSite) p8, obj6);
        publishFileInfo3.TabOrder = num;
        PublishFileInfo publishFileInfo4 = publishFileInfo1;
        source.Add(publishFileInfo4);
      }
      foreach (PublishFileInfo publishFileInfo in source.OrderBy<PublishFileInfo, int>((Func<PublishFileInfo, int>) (x => x.TabOrder)).ToList<PublishFileInfo>())
        entries.Add(publishFileInfo.CreatePublishInfo());
      string path2 = Path.GetFileName(pdfFileName) + ".dsd";
      string str5 = Path.Combine(fileVaultTempAreaPath, path2);
      using (FileStream fileStream = new FileStream(str5, FileMode.Create))
      {
        using (StreamWriter dsdWriter = new StreamWriter((Stream) fileStream, Encoding.Unicode))
          this.DoCreateDsdFile(dsdWriter, pdfFileName, entries);
      }
      FileInfo fileInfo = new FileInfo(str5);
      if (fileInfo.Length <= 0L)
        return;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__10.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__10, this.rawDocument, str3);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__11 = CallSite<Action<CallSite, object, string, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__11.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__11, this.rawDocument, str3, 0);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__12.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__12, this.rawDocument, CadDocumentProxy.BackgroundPlotVarName);
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__13 = CallSite<Action<CallSite, object, string, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__13.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__13, this.rawDocument, CadDocumentProxy.BackgroundPlotVarName, CadDocumentProxy.BackgroundPlotVarTestValue);
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__14 = CallSite<Action<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SendCommand", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__14.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__14, this.rawDocument, $"-PUBLISH {fileInfo.FullName}\n");
      }
      finally
      {
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__15 = CallSite<Action<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__15.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__15, this.rawDocument, str3, obj7);
        // ISSUE: reference to a compiler-generated field
        if (CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__16 = CallSite<Action<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetVariable", (IEnumerable<Type>) null, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__16.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__26.\u003C\u003Ep__16, this.rawDocument, CadDocumentProxy.BackgroundPlotVarName, obj8);
        if (!File.Exists(pdfFileName))
          throw new ApplicationProxyException($"{this.cadSystem.ApplicationName} не удалось сформировать аутентичный файл. Подробности смотрите в окне {this.cadSystem.ApplicationName}.");
      }
    }
    catch (COMException ex)
    {
      throw new ApplicationProxyException($"При экспорте документа {this.documentName} в PDF произошла внутренняя ошибка приложения {this.CADSystem.ApplicationName}: {ex.Message}.", (Exception) ex);
    }
  }

  /// <summary>Создает .dsd-файл для экспорта чертежа в pdf-документ</summary>
  /// <param name="pdfFileName">Полный путь с именем и расширением, по которому будет сохранен экспортированный pdf-докмуент</param>
  protected virtual void DoCreateDsdFile(
    StreamWriter dsdWriter,
    string pdfFileName,
    List<string> entries)
  {
    dsdWriter.WriteLine("[DWF6Version]");
    dsdWriter.WriteLine("Ver=1");
    foreach (string entry in entries)
      dsdWriter.WriteLine(entry);
    dsdWriter.WriteLine("[Target]");
    dsdWriter.WriteLine("Type=6");
    dsdWriter.WriteLine("DWF=" + pdfFileName);
    dsdWriter.WriteLine("OUT=" + Path.GetDirectoryName(pdfFileName));
    dsdWriter.WriteLine("PWD=");
  }

  /// <summary>Возвращает прокси-объект CAD-системы.</summary>
  public ICadProxy CADSystem
  {
    [DebuggerStepThrough] get => (ICadProxy) this.cadSystem;
  }

  /// <summary>
  /// Возвращает исходный необернутый COM-объект документа CAD-системы.
  /// Это свойство должно использоваться только в тех случаях, когда
  /// COM-объект требуется передать в другое приложение.
  /// Внутри IPS должен использоваться только прокси-объект.
  /// </summary>
  public object RawObject
  {
    [DebuggerStepThrough] get => this.rawDocument;
  }

  public string Name => this.RawGetName();

  public bool IsReadOnly
  {
    get
    {
      try
      {
        if (CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__1 == null)
          CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (CadDocumentProxy)));
        Func<CallSite, object, bool> target = CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, bool>> p1 = CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__1;
        if (CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__0 == null)
          CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ReadOnly", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__35.\u003C\u003Ep__0, this.rawDocument);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.ReadOnly");
      }
    }
  }

  public bool IsNew => this.DoTestIsNewDocument(this.RawGetFullName());

  /// <summary>Возвращает признак активного документа CAD-системы.</summary>
  public bool IsActive
  {
    get
    {
      try
      {
        if (CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__1 == null)
          CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (CadDocumentProxy)));
        Func<CallSite, object, bool> target = CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, bool>> p1 = CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__1;
        if (CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__0 == null)
          CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Active", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__39.\u003C\u003Ep__0, this.rawDocument);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Active");
      }
    }
  }

  public bool Modified
  {
    get
    {
      try
      {
        if (CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__2 == null)
          CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (CadDocumentProxy)));
        Func<CallSite, object, bool> target1 = CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__2.Target;
        CallSite<Func<CallSite, object, bool>> p2 = CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__2;
        if (CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__1 == null)
          CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        Func<CallSite, object, object> target2 = CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, object>> p1 = CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__1;
        if (CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__0 == null)
          CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj1 = CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__41.\u003C\u003Ep__0, this.rawDocument);
        object obj2 = target2((CallSite) p1, obj1);
        return target1((CallSite) p2, obj2);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Saved");
      }
    }
  }

  /// <summary>
  /// Проверяет, является ли текущий документ CAD-системы новым и еще не сохраненным на диск.
  /// Для этого используется значение свойства IAcadDocument.FullName
  /// </summary>
  /// <param name="fullName">Значение свойства IAcadDocument.FullName</param>
  /// <returns>true - новый документ без файла на диске; false - существующий документ, открытый из файла на диске</returns>
  protected virtual bool DoTestIsNewDocument(string fullName) => string.IsNullOrEmpty(fullName);

  protected virtual string RawGetName()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__43.\u003C\u003Ep__0, this.rawDocument);
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.Name");
    }
  }

  protected virtual string RawGetFullName()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadDocumentProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (CadDocumentProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__0.Target((CallSite) CadDocumentProxy.\u003C\u003Eo__44.\u003C\u003Ep__0, this.rawDocument);
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADSystem.ApplicationName, "IAcadDocument.FullName");
    }
  }
}
