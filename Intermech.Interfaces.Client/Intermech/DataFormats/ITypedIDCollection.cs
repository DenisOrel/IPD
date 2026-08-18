// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ITypedIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Коллекция типизированных идентификаторов всевозможных объектов (не
/// только объектов БД) для передачи через clipboard, а также между
/// различными частями системы. Типизация выполняется с помощью
/// интерфейсов-форматов данных.
/// </summary>
public interface ITypedIDCollection : IEnumerator
{
  object this[int index] { get; }

  int Count { get; }
}
