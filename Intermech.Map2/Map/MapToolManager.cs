// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolManager
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolManager : MapTool
    {
      [NonSerialized]
      private bool myStarted;

      public MapToolManager(MapView v)
        : base(v)
      {
        this.myStarted = false;
      }

      /// <summary>действия когда клавиша клавиатуры нажата</summary>
      public override void DoKeyDown()
      {
        MapInputEventArgs lastInput = this.LastInput;
        bool control = lastInput.Control;
        Keys key = lastInput.Key;
        if (key == Keys.Delete)
          this.View.EditDelete();
        else if (control && key == Keys.A)
          this.View.SelectAll();
        else if (control && key == Keys.C)
          this.View.EditCopy();
        else if (control && key == Keys.X)
          this.View.EditCut();
        else if (control && key == Keys.V)
        {
          this.View.EditPaste();
        }
        else
        {
          switch (key)
          {
            case Keys.Prior:
              if (lastInput.Shift)
              {
                this.View.ScrollPage(-1f, 0.0f);
                break;
              }
              this.View.ScrollPage(0.0f, -1f);
              break;
            case Keys.Next:
              if (lastInput.Shift)
              {
                this.View.ScrollPage(1f, 0.0f);
                break;
              }
              this.View.ScrollPage(0.0f, 1f);
              break;
            case Keys.End:
              RectangleF documentBounds1 = this.View.ComputeDocumentBounds();
              SizeF docExtentSize = this.View.DocExtentSize;
              PointF pointF = !control ? new PointF(documentBounds1.X + documentBounds1.Width - docExtentSize.Width, this.View.DocPosition.Y) : new PointF(documentBounds1.X + documentBounds1.Width - docExtentSize.Width, documentBounds1.Y + documentBounds1.Height - docExtentSize.Height);
              this.View.DocPosition = new PointF(Math.Max(0.0f, pointF.X), Math.Max(0.0f, pointF.Y));
              break;
            case Keys.Home:
              RectangleF documentBounds2 = this.View.ComputeDocumentBounds();
              if (control)
              {
                this.View.DocPosition = new PointF(documentBounds2.X, documentBounds2.Y);
                break;
              }
              this.View.DocPosition = new PointF(documentBounds2.X, this.View.DocPosition.Y);
              break;
            case Keys.F2:
              this.View.EditEdit();
              break;
            default:
              if (control && key == Keys.Z)
              {
                this.View.Undo();
                break;
              }
              if (control && key == Keys.Y)
              {
                this.View.Redo();
                break;
              }
              if (key == Keys.Escape)
              {
                if (this.View.CanSelectObjects())
                  this.Selection.Clear();
                base.DoKeyDown();
                break;
              }
              bool flag = false;
              if (!control && !lastInput.Alt && this.View.SelectsByFirstChar)
              {
                string str = TypeDescriptor.GetConverter(typeof (Keys)).ConvertToString((ITypeDescriptorContext) null, CultureInfo.CurrentCulture, (object) lastInput.Key);
                char minValue = char.MinValue;
                if (str.Length == 1)
                  minValue = str[0];
                else if (str.Length == 2 && str[0] == 'D')
                  minValue = str[1];
                if (char.IsLetterOrDigit(minValue))
                  flag = this.View.SelectNextNode(minValue);
              }
              if (flag)
                break;
              base.DoKeyDown();
              break;
          }
        }
      }

      /// <summary>действия когда клавиша мыши нажата</summary>
      public override void DoMouseDown()
      {
        foreach (IMapTool mouseDownTool in (IEnumerable) this.View.MouseDownTools)
        {
          if (mouseDownTool != null && mouseDownTool.CanStart())
          {
            this.View.Tool = mouseDownTool;
            return;
          }
        }
        this.Started = true;
      }

      public override void DoMouseHover() => this.View.DoHover(this.LastInput);

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        if (this.Started)
        {
          foreach (IMapTool mouseMoveTool in (IEnumerable) this.View.MouseMoveTools)
          {
            if (mouseMoveTool != null && mouseMoveTool.CanStart())
            {
              this.View.Tool = mouseMoveTool;
              return;
            }
          }
        }
        this.View.DoMouseOver(this.LastInput);
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        if (!this.Started)
          return;
        foreach (IMapTool mouseUpTool in (IEnumerable) this.View.MouseUpTools)
        {
          if (mouseUpTool != null && mouseUpTool.CanStart())
          {
            this.View.Tool = mouseUpTool;
            break;
          }
        }
      }

      public override void DoMouseWheel() => this.View.DoWheel(this.LastInput);

      public override void Stop() => this.Started = false;

      public bool Started
      {
        get => this.myStarted;
        set => this.myStarted = value;
      }
    }
}
