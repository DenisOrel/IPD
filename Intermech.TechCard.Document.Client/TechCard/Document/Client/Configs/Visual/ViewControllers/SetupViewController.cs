// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.ViewControllers.SetupViewController
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.ViewControllers;

internal abstract class SetupViewController : IConfigViewController
{
  protected readonly System.IServiceProvider Services;

  [NotNull]
  protected abstract IConfigView View { get; }

  public SetupViewController(System.IServiceProvider services) => this.Services = services;

  public void Show(Control parent, [NotNull] IConfigViewSettings settings)
  {
    this.View.SetupView(settings);
    if (!(this.View is Control view))
      return;
    parent.SuspendLayout();
    bool flag = false;
    foreach (object control1 in (ArrangedElementCollection) parent.Controls)
    {
      if (control1 != view && ((Control) control1).Visible && control1 is Control control2)
        control2.Hide();
      if (control1 == view)
        flag = true;
    }
    if (!flag)
    {
      view.Dock = DockStyle.Fill;
      parent.Controls.Add(view);
    }
    if (!view.Visible)
      view.Show();
    parent.ResumeLayout();
  }

  public bool ApplyChanges(out IDocumentConfigElement config) => this.View.ApplyChanges(out config);

  public void CancelChanges() => this.View.CancelChanges();
}
