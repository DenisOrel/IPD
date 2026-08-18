// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IAddressService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Сервис для перехода через тоолбар адреса</summary>
public interface IAddressService
{
  /// <summary>Текст в адресной строке</summary>
  string Text { get; set; }

  /// <summary>История значений</summary>
  string[] History { get; set; }

  /// <summary>Разрешить или запретить адресную панель</summary>
  bool Enabled { get; set; }
}
