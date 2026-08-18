// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.GetShowDwgObjectDelegate
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Show;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Создает объект для визуализации из представленных данных</summary>
/// <param name="objectId">Идентификатор объекта</param>
/// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
/// <param name="fileName">Имя файла</param>
/// <param name="data">Данные</param>
/// <returns></returns>
public delegate IShowDwg GetShowDwgObjectDelegate(
  long objectId,
  int valueIndex,
  string fileName,
  byte[] data);
