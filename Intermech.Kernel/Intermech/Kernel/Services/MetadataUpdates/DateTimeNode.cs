// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.DateTimeNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class DateTimeNode : XMLPropertyNode<DateTime>
{
  private readonly DateTime _defaultDate;

  public DateTimeNode(IUserSession session, XmlNode node, string nodeID, DateTime defaultDate)
    : base(session, node, nodeID)
  {
    this._defaultDate = defaultDate;
  }

  protected override DateTime GetValue(IUserSession session, string nodeAttributeValue)
  {
    DateTime dateTime = Convert.ToDateTime(nodeAttributeValue, (IFormatProvider) CultureInfo.InvariantCulture);
    return dateTime != DateTime.MinValue ? dateTime : this._defaultDate;
  }
}
