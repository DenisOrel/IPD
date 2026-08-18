// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.RejectingFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Отбрасывает данные любого формата. Используется элементами пространства
/// навигации для исключения своих подиерархий из процесса обработки
/// событий обновления в дереве навигатора.
/// </summary>
public class RejectingFilter : IDataFormatFilter, ICloneable
{
  public bool Join(IDataFormatFilter filter) => filter is RejectingFilter;

  public bool Disjoin(IDataFormatFilter filter) => filter is RejectingFilter;

  public bool CanPassData(object data) => false;

  public object Clone() => (object) new RejectingFilter();
}
