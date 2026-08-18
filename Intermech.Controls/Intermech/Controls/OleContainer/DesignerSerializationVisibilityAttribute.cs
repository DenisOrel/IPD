
// Type: Intermech.Controls.OleContainer.DesignerSerializationVisibilityAttribute
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
public sealed class DesignerSerializationVisibilityAttribute : Attribute
{
  public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);
  public static readonly DesignerSerializationVisibilityAttribute Default;
  public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);
  private DesignerSerializationVisibility visibility;
  public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

  static DesignerSerializationVisibilityAttribute()
  {
    DesignerSerializationVisibilityAttribute.Default = DesignerSerializationVisibilityAttribute.Visible;
  }

  public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
  {
    this.visibility = visibility;
  }

  public override bool Equals(object obj)
  {
    if (obj == this)
      return true;
    return obj is DesignerSerializationVisibilityAttribute visibilityAttribute && visibilityAttribute.Visibility == this.visibility;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override bool IsDefaultAttribute()
  {
    return this.Equals((object) DesignerSerializationVisibilityAttribute.Default);
  }

  public DesignerSerializationVisibility Visibility => this.visibility;
}
