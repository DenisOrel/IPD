// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IParametersContainerProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Коллекция именованных значений (атрибутов)</summary>
public interface IParametersContainerProxy
{
  /// <summary>Получить список имён значений</summary>
  /// <returns>Список имён значений</returns>
  IList<string> GetParameterNames();

  /// <summary>Получить список именованных значений</summary>
  /// <returns>Список именованных значений</returns>
  List<ValueRecord> GetParameters();

  /// <summary>Получить список указанных именованных значений</summary>
  /// <param name="parameterNames">Имена значений</param>
  /// <returns>Список указанных именованных значений</returns>
  /// <exception cref="T:ArgumentNullException">parameterNames</exception>
  List<ValueRecord> GetParameters(IList<string> parameterNames);

  /// <summary>Внести в коллекцию указанные именованные значения</summary>
  /// <param name="parameters">Список именованных значений</param>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  void SetParameters(IList<ValueRecord> parameters);

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение или null</returns>
  ValueRecord TryGetParameter(string parameterName);

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение</returns>
  /// <exception cref="T:ArgumentProxyException">Не удалось найти указанный параметр</exception>
  ValueRecord GetParameter(string parameterName);

  /// <summary>Внести в коллекцию указанное именованное значение</summary>
  /// <param name="parameter">Именованное значение</param>
  /// <exception cref="T:ArgumentNullException">parameter</exception>
  void SetParameter(ValueRecord parameter);
}
