// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.SpecialPeriodsList
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

public class SpecialPeriodsList : List<SpecialDay>, IXmlReaderSupport
{
  [NotNull]
  private CalendarBase Calendar { get; }

  public SpecialPeriodsList([NotNull] CalendarBase calendar) => this.Calendar = calendar;

  public string XmlNodeName => "SpecialPeriods";

  public void ReadFromXml([NotNull] XmlReader reader)
  {
    string localName = reader.LocalName;
    if (this.Count > 0)
      this.Clear();
    reader.ReadEnumeration(localName, (string) null, (XmlReaderExtensions.LoadEnumerationItemMethod) (() => this.Add(new SpecialDay(this.Calendar, reader))));
  }
}
