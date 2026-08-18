
// Type: Intermech.Bars.ToolBarContainer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Bars
{
    [Designer(typeof (ToolBarContainerDesigner))]
    [ToolboxItem(false)]
    public class ToolBarContainer : ContainerControl
    {
      private BarManager _barManager;
      private bool _needLayout;
      private Guid _guid;

      public ToolBarContainer()
      {
        this._needLayout = false;
        this._guid = Guid.NewGuid();
        this._barManager = this.Manager;
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.SetStyle(ControlStyles.Selectable, false);
        this.Text = "BarDock";
      }

      internal void Repaint() => this.Invalidate(true);

      private void LayoutToolbars(bool vertical)
      {
        if (!this.IsHandleCreated)
          return;
        int A_1 = 0;
        if (this.Controls.Count != 0 && this._barManager != null)
        {
          int[] array = new int[this.Controls.Count];
          for (int index = 0; index < this.Controls.Count; ++index)
            array[index] = ((ToolBar) this.Controls[index]).DockLine;
          Array.Sort<int>(array);
          int[] numArray = new int[this.Controls.Count];
          int minValue = int.MinValue;
          int index1 = 0;
          for (int index2 = 0; index2 < array.Length; ++index2)
          {
            if (array[index2] != minValue)
            {
              numArray[index1] = array[index2];
              ++index1;
              minValue = array[index2];
            }
          }
          for (int index3 = 0; index3 < index1; ++index3)
            A_1 += this.a(numArray[index3], A_1, vertical);
        }
        if (vertical)
          this.Width = A_1;
        else
          this.Height = A_1;
      }

      internal int GetToolbarsCountAtDockLine(int dockLine)
      {
        int toolbarsCountAtDockLine = 0;
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          if (control.DockLine == dockLine)
            ++toolbarsCountAtDockLine;
        }
        return toolbarsCountAtDockLine;
      }

      internal void a(int A_0, int A_1)
      {
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          switch (A_1)
          {
            case -1:
              if (control.DockLine <= A_0)
              {
                --control._dockLine;
                continue;
              }
              continue;
            case 1:
              if (control.DockLine >= A_0)
              {
                ++control._dockLine;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }

      internal int a(int A_0, int A_1, bool A_2)
      {
        int num1 = 0;
        int length = 0;
        ToolBar[] sourceArray1 = new ToolBar[this.Controls.Count];
        int[] sourceArray2 = new int[this.Controls.Count];
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          if (control.DockLine == A_0 && control.Visible)
          {
            sourceArray1[length] = control;
            sourceArray2[length] = control.DockOffset;
            ++length;
          }
        }
        ToolBar[] destinationArray = new ToolBar[length];
        Array.Copy((Array) sourceArray1, (Array) destinationArray, length);
        ToolBar[] items = destinationArray;
        int[] numArray1 = new int[length];
        Array.Copy((Array) sourceArray2, (Array) numArray1, length);
        Array.Sort<int, ToolBar>(numArray1, items);
        int[] numArray2 = new int[length];
        int[] numArray3 = new int[length];
        for (int index = 0; index < items.Length; ++index)
        {
          this.Manager.Renderer.StartToolBarRender(items[index], A_2, items[index].RightToLeft == RightToLeft.Yes && items[index].AllowRightToLeft);
          this.Manager.Renderer.FinishToolBarRender();
          Size size = items[index].f();
          if (items[index].Stretch)
          {
            numArray3[index] = !A_2 ? this.ClientRectangle.Width - size.Width : this.ClientRectangle.Height - size.Height;
            if (numArray3[index] < 0)
              numArray3[index] = 0;
          }
          if (A_2)
          {
            numArray2[index] = size.Height;
            if (size.Width > num1)
              num1 = size.Width;
          }
          else
          {
            numArray2[index] = size.Width;
            if (size.Height > num1)
              num1 = size.Height;
          }
        }
        int[] numArray4 = new int[length];
        int num2 = 0;
        for (int index = 0; index < length; ++index)
        {
          int num3 = num2 + 2;
          if (items[index].DockOffset >= num3)
          {
            numArray4[index] = items[index].DockOffset - num3;
            num3 = items[index].DockOffset;
          }
          num2 = num3 + (numArray2[index] + numArray3[index]);
        }
        int num4 = !A_2 ? num2 - this.ClientRectangle.Width : num2 - this.ClientRectangle.Height;
        if (num4 > 0)
        {
          for (int index1 = length - 1; index1 >= 0; --index1)
          {
            if (numArray4[index1] > num4)
            {
              int[] numArray5;
              IntPtr index2;
              (numArray5 = numArray4)[(int) (index2 = (IntPtr) index1)] = numArray5[(int) index2] - num4;
              num4 = 0;
            }
            else
            {
              num4 -= numArray4[index1];
              numArray4[index1] = 0;
            }
            if (num4 == 0)
              break;
          }
        }
        if (num4 > 0)
        {
          for (int index3 = length - 1; index3 >= 0; --index3)
          {
            if (numArray3[index3] > num4)
            {
              int[] numArray6;
              IntPtr index4;
              (numArray6 = numArray3)[(int) (index4 = (IntPtr) index3)] = numArray6[(int) index4] - num4;
              num4 = 0;
            }
            else
            {
              num4 -= numArray3[index3];
              numArray3[index3] = 0;
            }
            if (num4 == 0)
              break;
          }
        }
        bool flag = false;
        if (num4 > 0 && !this.DesignMode)
        {
          int num5 = 0;
          for (int index = 0; index < length; ++index)
            num5 += numArray2[index];
          if (num5 > 0)
          {
            for (int index5 = 0; index5 < length; ++index5)
            {
              int[] numArray7;
              IntPtr index6;
              (numArray7 = numArray2)[(int) (index6 = (IntPtr) index5)] = numArray7[(int) index6] - (int) Math.Ceiling((double) numArray2[index5] / (double) num5 * (double) num4);
              flag = flag || items[index5].Overflow == ToolBarOverflow.Wrap;
            }
          }
        }
        if (flag)
        {
          for (int index = 0; index < length; ++index)
          {
            if (!(items[index] is ContainerBar))
            {
              Size preferredSizeWithExtent = items[index].GetPreferredSizeWithExtent(numArray2[index]);
              if (A_2 && preferredSizeWithExtent.Width > num1)
                num1 = preferredSizeWithExtent.Width;
              else if (!A_2 && preferredSizeWithExtent.Height > num1)
                num1 = preferredSizeWithExtent.Height;
            }
          }
        }
        int num6 = 0;
        for (int index = 0; index < length; ++index)
        {
          int num7 = num6 + 2;
          Size size = !A_2 ? new Size(numArray2[index] + numArray3[index], num1) : new Size(num1, numArray2[index] + numArray3[index]);
          if (size != items[index].Size || items[index]._contained)
          {
            items[index].Size = size;
            items[index].CalculateLayoutInternal(this.Manager.Renderer, A_2);
            items[index]._contained = false;
          }
          int num8 = num7 + numArray4[index];
          Point point = !A_2 ? new Point(num8, A_1) : new Point(A_1, num8);
          num6 = num8 + (numArray2[index] + numArray3[index]);
          if (point != items[index].Location)
          {
            items[index].Location = point;
            items[index].Invalidate();
          }
        }
        return num1;
      }

      internal bool GetDesignMode() => this.DesignMode;

      internal void b(int A_0, int A_1)
      {
        int num = A_0 + A_1;
        bool flag = false;
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          if (control.DockLine == num)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return;
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          switch (A_1)
          {
            case -1:
              if (control.DockLine < A_0)
              {
                --control._dockLine;
                continue;
              }
              continue;
            case 1:
              if (control.DockLine > A_0)
              {
                ++control._dockLine;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }

      internal void DoLayout()
      {
        this._needLayout = true;
        this.Invalidate(new Rectangle(0, 0, 1, 1));
      }

      protected override Control.ControlCollection CreateControlsInstance()
      {
        return (Control.ControlCollection) new ToolBarContainer.ToolBarCollection((Control) this);
      }

      internal void OnOwnerFormActivated()
      {
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
          control.OnOwnerFormActivated();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this.Manager = (BarManager) null;
        base.Dispose(disposing);
      }

      internal void ForceLayout()
      {
        this._needLayout = true;
        this.LayoutToolbars(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right);
      }

      internal void OnOwnerFormDeactivate()
      {
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
          control.OnOwnerFormDeactivated();
      }

      public int GetNextFreeDockLine()
      {
        int num = 0;
        foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
        {
          if (control.DockLine > num)
            num = control.DockLine;
        }
        return num + 1;
      }

      protected override void OnHandleCreated(EventArgs e)
      {
        base.OnHandleCreated(e);
        this.ForceLayout();
      }

      protected override void OnLayout(LayoutEventArgs levent)
      {
        if (levent.AffectedControl is ToolBar && levent.AffectedProperty == "Bounds")
          return;
        this.LayoutToolbars(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right);
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        base.OnMouseUp(e);
        if (e.Button != MouseButtons.Right || this._barManager == null)
          return;
        this._barManager.CustomizeToolbars((ToolBar) this.Controls[0], (Control) this, new Point(e.X, e.Y));
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        if (!this._needLayout)
          return;
        this.LayoutToolbars(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right);
        this._needLayout = false;
      }

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        if (this._barManager != null)
        {
          Rectangle layoutBounds = this._barManager.GetScreenBounds();
          layoutBounds = new Rectangle(this.PointToClient(new Point(layoutBounds.X, layoutBounds.Y)), layoutBounds.Size);
          if (layoutBounds.Width <= 0 || layoutBounds.Height <= 0)
            return;
          this._barManager.Renderer.DrawContainerBackground(pevent.Graphics, this.ClientRectangle, layoutBounds);
        }
        else
          base.OnPaintBackground(pevent);
      }

      [Browsable(false)]
      public override Color BackColor
      {
        get => base.BackColor;
        set => base.BackColor = value;
      }

      [Browsable(false)]
      public override Image BackgroundImage
      {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
      }

      [Browsable(false)]
      public override Color ForeColor
      {
        get => base.ForeColor;
        set => base.ForeColor = value;
      }

      [Browsable(false)]
      public Guid Guid
      {
        get => this._guid;
        set => this._guid = value;
      }

      [Browsable(false)]
      public BarManager Manager
      {
        get => this._barManager;
        set
        {
          if (this._barManager != null)
            this._barManager.RemoveContainerBar(this);
          this._barManager = value;
          if (this._barManager != null)
          {
            this._barManager.AddContainer(this);
            foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
              this._barManager.AddToolbar(control);
            if (this._barManager.OwnerForm != null)
            {
              foreach (ToolBar control in (ArrangedElementCollection) this.Controls)
              {
                if (control is MenuBar)
                  ((MenuBar) control).OwnerForm = this._barManager.OwnerForm;
              }
            }
          }
          this.Repaint();
        }
      }

      [Browsable(false)]
      [DefaultValue("BarDockContainer")]
      public override string Text
      {
        get => base.Text;
        set => base.Text = value;
      }

      private class ToolBarCollection : Control.ControlCollection
      {
        private ToolBarContainer _container;

        public ToolBarCollection(Control A_0)
          : base(A_0)
        {
          this._container = (ToolBarContainer) A_0;
        }

        public override void Add(Control A_0)
        {
          if (!(A_0 is ToolBar))
            throw new ArgumentException("Only toolbars can be added to a ToolBarContainer.");
          if (this._container.Manager != null)
            this._container.Manager.AddToolbar((ToolBar) A_0);
          base.Add(A_0);
          if (!(A_0 is MenuBar) || this._container.Manager == null || this._container.Manager.OwnerForm == null)
            return;
          ((MenuBar) A_0).OwnerForm = this._container.Manager.OwnerForm;
        }

        public override void Remove(Control A_0)
        {
          base.Remove(A_0);
          if (this._container.Manager == null || !this._container.GetDesignMode())
            return;
          this._container.Manager.RemoveToolbar((ToolBar) A_0);
        }
      }
    }
}
