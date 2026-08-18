// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.UserWorkItem
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class UserWorkItem : IEquatable<UserWorkItem>
{
  public UserWorkItem(string text, DBObjectGraphVertex vertex)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    this.Text = text;
    this.Vertex = vertex;
  }

  public string Text { get; }

  public DBObjectGraphVertex Vertex { get; }

  public bool Equals(UserWorkItem other) => other != null && other.Text == this.Text;

  public override bool Equals(object obj)
  {
    return !(obj is UserWorkItem other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => this.Text.GetHashCode();

  public override string ToString() => this.Text;
}
