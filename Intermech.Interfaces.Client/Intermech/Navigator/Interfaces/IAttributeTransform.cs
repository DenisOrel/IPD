// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IAttributeTransform
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Базовый интерфейс для преобразователя, который работает с произвольными атрибутами,
/// которому на "лету" можно указывать ID типа обрабатываемого атрибута
/// </summary>
public interface IAttributeTransform
{
  int AttrID { get; set; }
}
