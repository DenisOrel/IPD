// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.MetadataLoader
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Загрузчик метаданных IPS.Calendars</summary>
internal abstract class MetadataLoader : Intermech.Metadata.MetadataLoader
{
  internal new static void Init([NotNull] IUserSession session) => Intermech.Metadata.MetadataLoader.Init(session);
}
