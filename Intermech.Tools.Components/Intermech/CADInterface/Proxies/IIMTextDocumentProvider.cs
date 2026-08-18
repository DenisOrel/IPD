// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IIMTextDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies.Cadmech;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public interface IIMTextDocumentProvider
{
  /// <summary>
  /// Возвращает документ IMTEXT для текущего объекта CAD-системы.
  /// </summary>
  /// <param name="throwIfNoCadmechFound">Признак, что нужно бросать исключение, если CADMECH не установлен</param>
  /// <returns>Документ IMTEXT или null, если CADMECH не установлен</returns>
  /// <exception cref="T:System.ArgumentNullException">CADMECH не установлен и флаг throwIfNoCadmechFound = true</exception>
  IMTextDocumentProxy GetIMTextDocument(bool throwIfNoCadmechFound);
}
