// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsChangedEventHandler
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Делегат для событий от редактора "весов" типов объектов-документов
/// </summary>
/// <param name="sender">Отправитель</param>
/// <param name="e">Аргументы события</param>
public delegate void DocumentTypesWeightsChangedEventHandler(
  object sender,
  DocumentTypesWeightsEventArgs e);
