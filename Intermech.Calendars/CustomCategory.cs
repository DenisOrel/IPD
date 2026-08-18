// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CustomCategory
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;

#nullable disable
namespace Intermech.Calendars;

internal class CustomCategory([NotNull] string category) : CustomCategoryBase(Localization.Resources, category)
{
}
