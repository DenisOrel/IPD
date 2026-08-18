// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ItemChangeEventArgs`1
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project;

public class ItemChangeEventArgs<T> : ItemEventArgs<T> where T : Entity
{
  public ItemChangeEventArgs([NotNull] T item, [NotNull] string propertyName)
    : base(item)
  {
    this.PropertyName = propertyName;
  }

  [NotNull]
  public string PropertyName { get; }
}
