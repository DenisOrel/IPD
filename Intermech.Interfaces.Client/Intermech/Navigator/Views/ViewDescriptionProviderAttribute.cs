// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ViewDescriptionProviderAttribute
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Views;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ViewDescriptionProviderAttribute : Attribute
{
  public ViewDescriptionProviderAttribute(Type viewDescriptionProvider)
  {
    if (viewDescriptionProvider == (Type) null)
      throw new ArgumentNullException(nameof (viewDescriptionProvider));
    this.ViewDescriptionProvider = typeof (IViewDescriptionProvider).IsAssignableFrom(viewDescriptionProvider) ? viewDescriptionProvider : throw new ArgumentException();
  }

  public Type ViewDescriptionProvider { get; private set; }

  public ViewDescription GetViewDescription(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider)
  {
    return ((IViewDescriptionProvider) Activator.CreateInstance(this.ViewDescriptionProvider)).GetViewDescription(selectedItems, serviceProvider);
  }
}
