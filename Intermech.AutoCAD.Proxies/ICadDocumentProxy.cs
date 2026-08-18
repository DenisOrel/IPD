// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.ICadDocumentProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoCAD.Proxies;

public interface ICadDocumentProxy
{
  string GetMasterFile();

  List<string> GetSatelliteFiles(SatelliteFileType selectedTypes);

  void Activate();

  void Save();

  void Close(bool saveChanges);

  void ExportToPDF(string pdfFileName, string fileVaultTempAreaPath, bool cadmechFinded);

  /// <summary>Возвращает прокси-объект CAD-системы.</summary>
  ICadProxy CADSystem { get; }

  /// <summary>
  /// Возвращает исходный необернутый COM-объект документа CAD-системы.
  /// Это свойство должно использоваться только в тех случаях, когда
  /// COM-объект требуется передать в другое приложение.
  /// Внутри IPS должен использоваться только прокси-объект.
  /// </summary>
  object RawObject { get; }

  string Name { get; }

  bool IsReadOnly { get; }

  bool IsNew { get; }

  /// <summary>Возвращает признак активного документа CAD-системы.</summary>
  bool IsActive { get; }

  bool Modified { get; }
}
