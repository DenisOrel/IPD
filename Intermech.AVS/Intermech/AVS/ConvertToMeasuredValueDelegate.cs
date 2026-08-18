// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ConvertToMeasuredValueDelegate
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AVS;

/// <summary>Преобразовать значение в формат MeasuredValue</summary>
/// <param name="value">Значение</param>
/// <param name="defaultMeasure">Физическая величина по умолчанию</param>
/// <param name="exceptionIfFail">Генерировать исключение, если нельзя конвертировать</param>
/// <returns></returns>
public delegate MeasuredValue ConvertToMeasuredValueDelegate(
  object value,
  MeasureDescriptor defaultMeasure,
  bool exceptionIfFail);
