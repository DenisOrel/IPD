
// Type: Intermech.Bars.ContainerBarDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Serializable]
    internal class ContainerBarDesigner : ToolBarDesigner
    {
      private ContainerBar _bar;

      public ContainerBarDesigner() => this._bar = (ContainerBar) null;

      [Obsolete]
      public override void OnSetComponentDefaults()
      {
        base.OnSetComponentDefaults();
        this._bar.Controls.Add((Control) ((IDesignerHost) this.GetService(typeof (IDesignerHost))).CreateComponent(typeof (ContainerBarClientPanel)));
      }

      public override void Initialize(IComponent A_0)
      {
        base.Initialize(A_0);
        this._bar = (ContainerBar) A_0;
      }

      public override ICollection AssociatedComponents
      {
        get
        {
          ArrayList associatedComponents = new ArrayList(base.AssociatedComponents);
          if (this._bar.ClientPanel != null)
            associatedComponents.Add((object) this._bar.ClientPanel);
          return (ICollection) associatedComponents;
        }
      }
    }
}
