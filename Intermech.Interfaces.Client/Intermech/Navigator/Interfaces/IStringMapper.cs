// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IStringMapper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс для отображения строк в положительные числовые идентификаторы,
/// уникальные для текущего сеанса работы программы. Отображение предназначено
/// для ускорения работы программы и уменьшения объема используемой оперативной
/// памяти.
/// </summary>
public interface IStringMapper
{
  int Register(string value);

  void Unregister(int cookie);

  string this[int cookie] { get; }

  int this[string value] { get; }
}
