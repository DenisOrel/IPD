
// Type: Intermech.Tools.Data.DBObjectErrorInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Data;

public sealed class DBObjectErrorInfo : IEquatable<DBObjectErrorInfo>
{
  public DBObjectErrorInfo(string uniqueId, string category, string text)
  {
    if (uniqueId == null)
      throw new ArgumentNullException(nameof (uniqueId));
    if (category == null)
      throw new ArgumentNullException(nameof (uniqueId));
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    this.UniqueId = uniqueId;
    this.Category = category;
    this.Text = text;
  }

  public string UniqueId { get; private set; }

  public string Category { get; private set; }

  public string Text { get; private set; }

  public bool Equals(DBObjectErrorInfo other)
  {
    return other != null && this.UniqueId == other.UniqueId && this.Category == other.Category && this.Text == other.Text;
  }

  public override bool Equals(object obj)
  {
    return !(obj is DBObjectErrorInfo other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => this.UniqueId.GetHashCode();
}
