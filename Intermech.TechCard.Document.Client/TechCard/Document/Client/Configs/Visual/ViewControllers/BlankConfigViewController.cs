// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.ViewControllers.BlankConfigViewController
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Client.Setup.Views;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.ViewControllers;

[DocumentConfigElementEditor(DocumentConfigElementType.Document)]
internal class BlankConfigViewController : SetupViewController
{
  protected override IConfigView View { get; }

  public BlankConfigViewController(IServiceProvider services)
    : base(services)
  {
    this.View = (IConfigView) new BlankConfigView((IConfigViewController) this, services);
  }
}
