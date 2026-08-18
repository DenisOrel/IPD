// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapGeneralNode
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapGeneralNode : MapNode, IMapNodeIconConstraint
    {
      public const int ChangedBottomLabel = 2405;
      public const int ChangedIcon = 2406;
      public const int ChangedOrientation = 2407;
      public const int ChangedTopLabel = 2404;
      public const int InsertedPort = 2401;
      private MapText myBottomLabel;
      private MapObject myIcon;
      private List<MapGeneralNodePort> myLeftPorts;
      private Orientation myOrientation;
      private List<MapGeneralNodePort> myRightPorts;
      private MapText myTopLabel;
      public const int RemovedPort = 2402;
      public const int ReplacedPort = 2403;

      public MapGeneralNode()
      {
        this.myTopLabel = (MapText) null;
        this.myBottomLabel = (MapText) null;
        this.myIcon = (MapObject) null;
        this.myLeftPorts = new List<MapGeneralNodePort>();
        this.myRightPorts = new List<MapGeneralNodePort>();
        this.myOrientation = Orientation.Horizontal;
      }

      public void AddLeftPort(MapGeneralNodePort p) => this.InsertLeftPort(this.LeftPortsCount, p);

      public void AddRightPort(MapGeneralNodePort p) => this.InsertRightPort(this.RightPortsCount, p);

      public List<MapGeneralNodePort> LeftPorts => this.myLeftPorts;

      public List<MapGeneralNodePort> RightPorts => this.myRightPorts;

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        MapGeneralNodePort oldValue = (MapGeneralNodePort) e.OldValue;
        switch (e.SubHint)
        {
          case 2401:
            int oldInt1 = e.OldInt;
            if (oldInt1 >= 0)
            {
              if (undo)
              {
                if (oldInt1 >= this.RightPortsCount)
                  break;
                this.myRightPorts.RemoveAt(oldInt1);
                break;
              }
              if (oldInt1 < this.RightPortsCount)
              {
                this.myRightPorts.Insert(oldInt1, oldValue);
                break;
              }
              this.myRightPorts.Add(oldValue);
              break;
            }
            int index1 = -oldInt1 - 1;
            if (!undo)
            {
              if (index1 < this.LeftPortsCount)
              {
                this.myLeftPorts.Insert(index1, oldValue);
                break;
              }
              this.myLeftPorts.Add(oldValue);
              break;
            }
            if (index1 >= this.LeftPortsCount)
              break;
            this.myLeftPorts.RemoveAt(index1);
            break;
          case 2402:
            int oldInt2 = e.OldInt;
            if (oldInt2 >= 0)
            {
              if (undo)
              {
                if (oldInt2 < this.RightPortsCount)
                {
                  this.myRightPorts.Insert(oldInt2, oldValue);
                  break;
                }
                this.myRightPorts.Add(oldValue);
                break;
              }
              if (oldInt2 >= this.RightPortsCount)
                break;
              this.myRightPorts.RemoveAt(oldInt2);
              break;
            }
            int index2 = -oldInt2 - 1;
            if (!undo)
            {
              if (index2 >= this.LeftPortsCount)
                break;
              this.myLeftPorts.RemoveAt(index2);
              break;
            }
            if (index2 >= this.LeftPortsCount)
            {
              this.myLeftPorts.Add(oldValue);
              break;
            }
            this.myLeftPorts.Insert(index2, oldValue);
            break;
          case 2403:
            int oldInt3 = e.OldInt;
            if (oldInt3 >= 0)
            {
              if (oldInt3 >= this.RightPortsCount)
                break;
              this.myRightPorts[oldInt3] = (MapGeneralNodePort) e.GetValue(undo);
              break;
            }
            int index3 = -oldInt3 - 1;
            if (index3 >= this.LeftPortsCount)
              break;
            this.myLeftPorts[index3] = (MapGeneralNodePort) e.GetValue(undo);
            break;
          case 2404:
            this.TopLabel = (MapText) e.GetValue(undo);
            break;
          case 2405:
            this.BottomLabel = (MapText) e.GetValue(undo);
            break;
          case 2406:
            this.Icon = (MapObject) e.GetValue(undo);
            break;
          case 2407:
            this.setOrientation((Orientation) e.GetInt(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        MapGeneralNode mapGeneralNode = (MapGeneralNode) newgroup;
        base.CopyChildren(newgroup, env);
        mapGeneralNode.myLeftPorts = new List<MapGeneralNodePort>();
        mapGeneralNode.myRightPorts = new List<MapGeneralNodePort>();
        mapGeneralNode.myIcon = (MapObject) env[(object) this.myIcon];
        mapGeneralNode.myTopLabel = (MapText) env[(object) this.myTopLabel];
        mapGeneralNode.myBottomLabel = (MapText) env[(object) this.myBottomLabel];
        for (int index = 0; index < this.myLeftPorts.Count; ++index)
        {
          MapGeneralNodePort leftPort = this.myLeftPorts[index];
          if (leftPort != null)
          {
            MapGeneralNodePort mapGeneralNodePort = (MapGeneralNodePort) env[(object) leftPort];
            if (mapGeneralNodePort != null)
            {
              mapGeneralNode.myLeftPorts.Add(mapGeneralNodePort);
              mapGeneralNodePort.SideIndex = mapGeneralNode.myLeftPorts.Count - 1;
              mapGeneralNodePort.LeftSide = true;
            }
          }
        }
        for (int index = 0; index < this.myRightPorts.Count; ++index)
        {
          MapGeneralNodePort rightPort = this.myRightPorts[index];
          if (rightPort != null)
          {
            MapGeneralNodePort mapGeneralNodePort = (MapGeneralNodePort) env[(object) rightPort];
            if (mapGeneralNodePort != null)
            {
              mapGeneralNode.myRightPorts.Add(mapGeneralNodePort);
              mapGeneralNodePort.SideIndex = mapGeneralNode.myRightPorts.Count - 1;
              mapGeneralNodePort.LeftSide = false;
            }
          }
        }
      }

      protected virtual MapObject CreateIcon(ResourceManager res, string iconname)
      {
        if (iconname != null)
        {
          MapNodeIcon icon = new MapNodeIcon();
          if (res != null)
            icon.ResourceManager = res;
          icon.Name = iconname;
          icon.MinimumIconSize = new SizeF(20f, 20f);
          icon.MaximumIconSize = new SizeF(1000f, 2000f);
          icon.Size = icon.MinimumIconSize;
          return (MapObject) icon;
        }
        MapRectangle icon1 = new MapRectangle();
        icon1.Selectable = false;
        icon1.Size = new SizeF(20f, 20f);
        return (MapObject) icon1;
      }

      protected virtual MapObject CreateIcon(ImageList imglist, int imgindex)
      {
        MapNodeIcon icon = new MapNodeIcon();
        icon.ImageList = imglist;
        icon.Index = imgindex;
        icon.MinimumIconSize = new SizeF(20f, 20f);
        icon.MaximumIconSize = new SizeF(1000f, 2000f);
        icon.Size = icon.MinimumIconSize;
        return (MapObject) icon;
      }

      protected virtual MapText CreateLabel(bool top, string text)
      {
        MapText label = (MapText) null;
        if (text != null)
        {
          label = new MapText();
          label.Text = text;
          label.Selectable = false;
          label.Alignment = this.Orientation != Orientation.Vertical ? (!top ? 32 /*0x20*/ : 128 /*0x80*/) : (!top ? 256 /*0x0100*/ : 64 /*0x40*/);
          label.Editable = true;
          this.Editable = true;
        }
        return label;
      }

      protected virtual MapGeneralNodePort CreatePort(bool input)
      {
        MapGeneralNodePort port = new MapGeneralNodePort();
        port.LeftSide = input;
        port.IsValidFrom = !input;
        port.IsValidTo = input;
        return port;
      }

      protected virtual MapGeneralNodePortLabel CreatePortLabel(bool input)
      {
        return new MapGeneralNodePortLabel();
      }

      public virtual MapGeneralNodePort GetLeftPort(int i)
      {
        return i >= 0 && i < this.myLeftPorts.Count ? this.myLeftPorts[i] : (MapGeneralNodePort) null;
      }

      public virtual MapGeneralNodePort GetRightPort(int i)
      {
        return i >= 0 && i < this.myRightPorts.Count ? this.myRightPorts[i] : (MapGeneralNodePort) null;
      }

      public virtual void Initialize(
        ResourceManager res,
        string iconname,
        string top,
        string bottom,
        int numinports,
        int numoutports)
      {
        this.Initializing = true;
        this.Icon = this.CreateIcon(res, iconname);
        this.initializeCommon(top, bottom, numinports, numoutports);
      }

      public virtual void Initialize(
        ImageList imglist,
        int imgindex,
        string top,
        string bottom,
        int numinports,
        int numoutports)
      {
        this.Initializing = true;
        this.Icon = this.CreateIcon(imglist, imgindex);
        this.initializeCommon(top, bottom, numinports, numoutports);
      }

      private void initializeCommon(string top, string bottom, int numinports, int numoutports)
      {
        this.TopLabel = this.CreateLabel(true, top);
        this.BottomLabel = this.CreateLabel(false, bottom);
        for (int index = 0; index < numinports; ++index)
          this.AddLeftPort(this.MakePort(true));
        for (int index = 0; index < numoutports; ++index)
          this.AddRightPort(this.MakePort(false));
        this.PropertiesDelegatedToSelectionObject = true;
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public bool PropertiesDelegated
      {
        get => this.PropertiesDelegatedToSelectionObject;
        set => this.PropertiesDelegatedToSelectionObject = value;
      }

      private void initializePort(MapGeneralNodePort p)
      {
        if (p == null || p.Parent != null)
          return;
        this.Add((MapObject) p);
        if (p.Label == null)
          return;
        this.Add((MapObject) p.Label);
      }

      public virtual void InsertLeftPort(int i, MapGeneralNodePort p)
      {
        if (p == null || i < 0)
          return;
        p.LeftSide = true;
        if (i < this.LeftPortsCount)
        {
          this.myLeftPorts.Insert(i, p);
          p.SideIndex = i;
        }
        else
        {
          this.myLeftPorts.Add(p);
          i = this.LeftPortsCount - 1;
          p.SideIndex = i;
        }
        this.initializePort(p);
        this.Changed(2401, -(i + 1), (object) p, MapObject.NullRect, -(i + 1), (object) p, MapObject.NullRect);
      }

      public virtual void InsertRightPort(int i, MapGeneralNodePort p)
      {
        if (p == null || i < 0)
          return;
        p.LeftSide = false;
        if (i < this.RightPortsCount)
        {
          this.myRightPorts.Insert(i, p);
          p.SideIndex = i;
        }
        else
        {
          this.myRightPorts.Add(p);
          i = this.RightPortsCount - 1;
          p.SideIndex = i;
        }
        this.initializePort(p);
        this.Changed(2401, i, (object) p, MapObject.NullRect, i, (object) p, MapObject.NullRect);
      }

      /// <summary>Расставить дочерние элементы</summary>
      /// <param name="childchanged">Измененный child (не используется)</param>
      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        this.Initializing = true;
        MapObject icon = this.Icon;
        MapObject topLabel = (MapObject) this.TopLabel;
        MapObject bottomLabel = (MapObject) this.BottomLabel;
        if (this.myOrientation == Orientation.Horizontal)
        {
          int leftPortsCount = this.LeftPortsCount;
          float num1 = 0.0f;
          float val1 = 0.0f;
          for (int i = 0; i < leftPortsCount; ++i)
          {
            MapGeneralNodePort leftPort = this.GetLeftPort(i);
            if (leftPort.Visible)
            {
              num1 += leftPort.PortAndLabelHeight;
              val1 = Math.Max(val1, leftPort.PortAndLabelWidth);
            }
          }
          if (icon != null)
          {
            SizeF minimumIconSize = this.MinimumIconSize;
            float width = Math.Max(minimumIconSize.Width, icon.Width);
            float height = Math.Max(minimumIconSize.Height, icon.Height);
            icon.Bounds = new RectangleF(icon.Left - (float) (((double) width - (double) icon.Width) / 2.0), icon.Top - (float) (((double) height - (double) icon.Height) / 2.0), width, height);
          }
          float x1 = icon != null ? icon.Left : this.Left;
          float num2 = icon != null ? icon.Top : this.Top + (topLabel != null ? topLabel.Height : 0.0f);
          if (icon != null && (double) icon.Height > (double) num1)
            num2 += (float) (((double) icon.Height - (double) num1) / 2.0);
          float num3 = 0.0f;
          for (int i = 0; i < leftPortsCount; ++i)
          {
            MapGeneralNodePort leftPort = this.GetLeftPort(i);
            if (leftPort.Visible)
            {
              float num4 = num3 + leftPort.PortAndLabelHeight / 2f;
              leftPort.SetSpotLocation(64 /*0x40*/, new PointF(x1, num2 + num4));
              leftPort.LayoutLabel();
              num3 = num4 + leftPort.PortAndLabelHeight / 2f;
            }
          }
          int rightPortsCount = this.RightPortsCount;
          float num5 = 0.0f;
          for (int i = 0; i < rightPortsCount; ++i)
          {
            MapGeneralNodePort rightPort = this.GetRightPort(i);
            if (rightPort.Visible)
              num5 += rightPort.PortAndLabelHeight;
          }
          float x2 = icon != null ? icon.Right : this.Right;
          float num6 = icon != null ? icon.Top : this.Top + (topLabel != null ? topLabel.Height : 0.0f);
          if (icon != null && (double) icon.Height > (double) num5)
            num6 += (float) (((double) icon.Height - (double) num5) / 2.0);
          float num7 = 0.0f;
          for (int i = 0; i < rightPortsCount; ++i)
          {
            MapGeneralNodePort rightPort = this.GetRightPort(i);
            if (rightPort.Visible)
            {
              float num8 = num7 + rightPort.PortAndLabelHeight / 2f;
              rightPort.SetSpotLocation(256 /*0x0100*/, new PointF(x2, num6 + num8));
              rightPort.LayoutLabel();
              num7 = num8 + rightPort.PortAndLabelHeight / 2f;
            }
          }
          if (topLabel != null)
          {
            if (icon != null)
              topLabel.SetSpotLocation(128 /*0x80*/, icon, 32 /*0x20*/);
            else
              topLabel.SetSpotLocation(32 /*0x20*/, (MapObject) this, 32 /*0x20*/);
          }
          if (bottomLabel != null)
          {
            if (icon != null)
              bottomLabel.SetSpotLocation(32 /*0x20*/, icon, 128 /*0x80*/);
            else
              bottomLabel.SetSpotLocation(128 /*0x80*/, (MapObject) this, 128 /*0x80*/);
          }
        }
        else
        {
          int leftPortsCount = this.LeftPortsCount;
          float num9 = 0.0f;
          float val1 = 0.0f;
          for (int i = 0; i < leftPortsCount; ++i)
          {
            MapGeneralNodePort leftPort = this.GetLeftPort(i);
            if (leftPort.Visible)
            {
              num9 += leftPort.PortAndLabelWidth;
              val1 = Math.Max(val1, leftPort.PortAndLabelHeight);
            }
          }
          if (icon != null)
          {
            SizeF minimumIconSize = this.MinimumIconSize;
            float width = Math.Max(minimumIconSize.Width, icon.Width);
            float height = Math.Max(minimumIconSize.Height, icon.Height);
            icon.Bounds = new RectangleF(icon.Left - (float) (((double) width - (double) icon.Width) / 2.0), icon.Top - (float) (((double) height - (double) icon.Height) / 2.0), width, height);
          }
          float num10 = icon != null ? icon.Left : this.Left + (topLabel != null ? topLabel.Width : 0.0f);
          float y1 = icon != null ? icon.Top : this.Top;
          if (icon != null && (double) icon.Width > (double) num9)
            num10 += (float) (((double) icon.Width - (double) num9) / 2.0);
          float num11 = 0.0f;
          for (int i = 0; i < leftPortsCount; ++i)
          {
            MapGeneralNodePort leftPort = this.GetLeftPort(i);
            if (leftPort.Visible)
            {
              float num12 = num11 + leftPort.PortAndLabelWidth / 2f;
              leftPort.SetSpotLocation(128 /*0x80*/, new PointF(num10 + num12, y1));
              leftPort.LayoutLabel();
              num11 = num12 + leftPort.PortAndLabelWidth / 2f;
            }
          }
          int rightPortsCount = this.RightPortsCount;
          float num13 = 0.0f;
          for (int i = 0; i < rightPortsCount; ++i)
          {
            MapGeneralNodePort rightPort = this.GetRightPort(i);
            if (rightPort.Visible)
              num13 += rightPort.PortAndLabelWidth;
          }
          float num14 = icon != null ? icon.Left : this.Left + (topLabel != null ? topLabel.Width : 0.0f);
          float y2 = icon != null ? icon.Bottom : this.Bottom;
          if (icon != null && (double) icon.Width > (double) num13)
            num14 += (float) (((double) icon.Width - (double) num13) / 2.0);
          float num15 = 0.0f;
          for (int i = 0; i < rightPortsCount; ++i)
          {
            MapGeneralNodePort rightPort = this.GetRightPort(i);
            if (rightPort.Visible)
            {
              float num16 = num15 + rightPort.PortAndLabelWidth / 2f;
              rightPort.SetSpotLocation(32 /*0x20*/, new PointF(num14 + num16, y2));
              rightPort.LayoutLabel();
              num15 = num16 + rightPort.PortAndLabelWidth / 2f;
            }
          }
          if (topLabel != null)
          {
            if (icon != null)
              topLabel.SetSpotLocation(64 /*0x40*/, icon, 256 /*0x0100*/);
            else
              topLabel.SetSpotLocation(256 /*0x0100*/, (MapObject) this, 256 /*0x0100*/);
          }
          if (bottomLabel != null)
          {
            if (icon != null)
              bottomLabel.SetSpotLocation(256 /*0x0100*/, icon, 64 /*0x40*/);
            else
              bottomLabel.SetSpotLocation(64 /*0x40*/, (MapObject) this, 64 /*0x40*/);
          }
        }
        this.Initializing = false;
      }

      public virtual MapGeneralNodePort MakePort(bool input)
      {
        MapGeneralNodePort port = this.CreatePort(input);
        if (port != null)
        {
          MapGeneralNodePortLabel portLabel = this.CreatePortLabel(input);
          port.Label = portLabel;
          if (portLabel != null)
            portLabel.Port = port;
          if (this.Orientation == Orientation.Vertical)
          {
            port.ToSpot = 32 /*0x20*/;
            port.FromSpot = 128 /*0x80*/;
          }
          else
          {
            port.ToSpot = 256 /*0x0100*/;
            port.FromSpot = 64 /*0x40*/;
          }
          port.Name = !input ? this.RightPortsCount.ToString((IFormatProvider) CultureInfo.CurrentCulture) : this.LeftPortsCount.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          PointF position;
          if (this.Icon != null)
          {
            position = this.Icon.Position;
            if (this.Orientation == Orientation.Vertical)
            {
              if (input)
                position.Y -= port.Height;
              else
                position.Y = this.Icon.Bottom;
            }
            else if (input)
              position.X -= port.Width;
            else
              position.X = this.Icon.Right;
          }
          else
            position = this.Position;
          port.Position = position;
          if (portLabel != null)
            portLabel.Position = position;
        }
        return port;
      }

      public virtual void OnOrientationChanged(Orientation old)
      {
        int leftPortsCount = this.LeftPortsCount;
        for (int i = 0; i < leftPortsCount; ++i)
        {
          MapGeneralNodePort leftPort = this.GetLeftPort(i);
          if (this.Orientation == Orientation.Vertical)
          {
            leftPort.ToSpot = 32 /*0x20*/;
            leftPort.FromSpot = 32 /*0x20*/;
          }
          else
          {
            leftPort.ToSpot = 256 /*0x0100*/;
            leftPort.FromSpot = 256 /*0x0100*/;
          }
        }
        int rightPortsCount = this.RightPortsCount;
        for (int i = 0; i < rightPortsCount; ++i)
        {
          MapGeneralNodePort rightPort = this.GetRightPort(i);
          if (this.Orientation == Orientation.Vertical)
          {
            rightPort.ToSpot = 128 /*0x80*/;
            rightPort.FromSpot = 128 /*0x80*/;
          }
          else
          {
            rightPort.ToSpot = 64 /*0x40*/;
            rightPort.FromSpot = 64 /*0x40*/;
          }
        }
        this.LayoutChildren((MapObject) null);
      }

      public override void Remove(MapObject obj)
      {
        if (obj is MapGeneralNodePort mapGeneralNodePort)
        {
          int index = this.myLeftPorts.IndexOf(mapGeneralNodePort);
          if (index >= 0)
          {
            this.myLeftPorts.RemoveAt(index);
            if (mapGeneralNodePort.Label != null)
              this.Remove((MapObject) mapGeneralNodePort.Label);
            base.Remove((MapObject) mapGeneralNodePort);
            mapGeneralNodePort.SideIndex = -1;
            this.Changed(2402, -(index + 1), (object) mapGeneralNodePort, MapObject.NullRect, -(index + 1), (object) mapGeneralNodePort, MapObject.NullRect);
            return;
          }
          int num = this.myRightPorts.IndexOf(mapGeneralNodePort);
          if (num >= 0)
          {
            this.myRightPorts.RemoveAt(num);
            if (mapGeneralNodePort.Label != null)
              this.Remove((MapObject) mapGeneralNodePort.Label);
            base.Remove((MapObject) mapGeneralNodePort);
            mapGeneralNodePort.SideIndex = -1;
            this.Changed(2402, num, (object) mapGeneralNodePort, MapObject.NullRect, num, (object) mapGeneralNodePort, MapObject.NullRect);
            return;
          }
        }
        base.Remove(obj);
        if (obj == this.myTopLabel)
          this.myTopLabel = (MapText) null;
        else if (obj == this.myBottomLabel)
        {
          this.myBottomLabel = (MapText) null;
        }
        else
        {
          if (obj != this.myIcon)
            return;
          this.myIcon = (MapObject) null;
        }
      }

      public virtual void RemoveLeftPort(int i)
      {
        if (i < 0 || i >= this.LeftPortsCount)
          return;
        this.Remove((MapObject) this.myLeftPorts[i]);
      }

      public virtual void RemoveRightPort(int i)
      {
        if (i < 0 || i >= this.RightPortsCount)
          return;
        this.Remove((MapObject) this.myRightPorts[i]);
      }

      public virtual void SetLeftPort(int i, MapGeneralNodePort p)
      {
        MapGeneralNodePort leftPort = this.GetLeftPort(i);
        if (leftPort == p)
          return;
        if (leftPort != null)
        {
          if (p != null)
            p.Bounds = leftPort.Bounds;
          if (leftPort.Label != null)
            this.Remove((MapObject) leftPort.Label);
          base.Remove((MapObject) leftPort);
          leftPort.SideIndex = -1;
        }
        this.myLeftPorts[i] = p;
        p.LeftSide = true;
        p.SideIndex = i;
        this.initializePort(p);
        this.Changed(2403, -(i + 1), (object) leftPort, MapObject.NullRect, -(i + 1), (object) p, MapObject.NullRect);
      }

      private void setOrientation(Orientation o, bool undoing)
      {
        Orientation orientation = this.myOrientation;
        if (orientation == o)
          return;
        this.myOrientation = o;
        this.Changed(2407, (int) orientation, (object) null, MapObject.NullRect, (int) o, (object) null, MapObject.NullRect);
        this.OnOrientationChanged(orientation);
      }

      public virtual void SetRightPort(int i, MapGeneralNodePort p)
      {
        MapGeneralNodePort rightPort = this.GetRightPort(i);
        if (rightPort == p)
          return;
        if (rightPort != null)
        {
          if (p != null)
            p.Bounds = rightPort.Bounds;
          if (rightPort.Label != null)
            this.Remove((MapObject) rightPort.Label);
          base.Remove((MapObject) rightPort);
          rightPort.SideIndex = -1;
        }
        this.myRightPorts[i] = p;
        p.LeftSide = false;
        p.SideIndex = i;
        this.initializePort(p);
        this.Changed(2403, i, (object) rightPort, MapObject.NullRect, i, (object) p, MapObject.NullRect);
      }

      public virtual MapText BottomLabel
      {
        get => this.myBottomLabel;
        set
        {
          MapText bottomLabel = this.myBottomLabel;
          if (bottomLabel == value)
            return;
          if (bottomLabel != null)
            this.Remove((MapObject) bottomLabel);
          this.myBottomLabel = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2405, 0, (object) bottomLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapObject Icon
      {
        get => this.myIcon;
        set
        {
          MapObject icon = this.myIcon;
          if (icon == value)
            return;
          this.CopyPropertiesFromSelectionObject(icon, value);
          if (icon != null)
            this.Remove(icon);
          this.myIcon = value;
          if (value != null)
            this.InsertBefore((MapObject) null, value);
          this.Changed(2406, 0, (object) icon, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapImage Image => this.Icon as MapImage;

      public override MapText Label
      {
        get
        {
          if (this.BottomLabel != null)
            return this.BottomLabel;
          return this.TopLabel != null ? this.TopLabel : (MapText) null;
        }
      }

      public int LeftPortsCount => this.myLeftPorts.Count;

      [Description("The maximum size for the icon")]
      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF MaximumIconSize
      {
        get => this.Icon is MapNodeIcon icon ? icon.MaximumIconSize : new SizeF(1000f, 2000f);
        set
        {
          if (!(this.Icon is MapNodeIcon icon))
            return;
          icon.MaximumIconSize = value;
        }
      }

      [Category("Appearance")]
      [Description("The minimum size for the icon")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF MinimumIconSize
      {
        get
        {
          if (this.Orientation == Orientation.Horizontal)
          {
            float width = 20f;
            float val1_1 = 20f;
            if (this.Icon is MapNodeIcon icon)
            {
              width = icon.MinimumIconSize.Width;
              val1_1 = icon.MinimumIconSize.Height;
            }
            int leftPortsCount = this.LeftPortsCount;
            float val2_1 = 0.0f;
            for (int i = 0; i < leftPortsCount; ++i)
            {
              MapGeneralNodePort leftPort = this.GetLeftPort(i);
              if (leftPort != null && leftPort.Visible)
                val2_1 += leftPort.PortAndLabelHeight;
            }
            float val1_2 = Math.Max(val1_1, val2_1);
            int rightPortsCount = this.RightPortsCount;
            float val2_2 = 0.0f;
            for (int i = 0; i < rightPortsCount; ++i)
            {
              MapGeneralNodePort rightPort = this.GetRightPort(i);
              if (rightPort != null && rightPort.Visible)
                val2_2 += rightPort.PortAndLabelHeight;
            }
            float height = Math.Max(val1_2, val2_2);
            return new SizeF(width, height);
          }
          float val1_3 = 20f;
          float height1 = 20f;
          if (this.Icon is MapNodeIcon icon1)
          {
            val1_3 = icon1.MinimumIconSize.Width;
            height1 = icon1.MinimumIconSize.Height;
          }
          int leftPortsCount1 = this.LeftPortsCount;
          float val2_3 = 0.0f;
          for (int i = 0; i < leftPortsCount1; ++i)
          {
            MapGeneralNodePort leftPort = this.GetLeftPort(i);
            if (leftPort != null && leftPort.Visible)
              val2_3 += leftPort.PortAndLabelWidth;
          }
          float val1_4 = Math.Max(val1_3, val2_3);
          int rightPortsCount1 = this.RightPortsCount;
          float val2_4 = 0.0f;
          for (int i = 0; i < rightPortsCount1; ++i)
          {
            MapGeneralNodePort rightPort = this.GetRightPort(i);
            if (rightPort != null && rightPort.Visible)
              val2_4 += rightPort.PortAndLabelWidth;
          }
          return new SizeF(Math.Max(val1_4, val2_4), height1);
        }
        set
        {
          if (!(this.Icon is MapNodeIcon icon))
            return;
          icon.MinimumIconSize = value;
        }
      }

      [Category("Appearance")]
      [Description("The general orientation of the node and how links connect to it")]
      [DefaultValue(0)]
      public Orientation Orientation
      {
        get => this.myOrientation;
        set => this.setOrientation(value, false);
      }

      public int RightPortsCount => this.myRightPorts.Count;

      public override MapObject SelectionObject => this.Icon != null ? this.Icon : (MapObject) this;

      public virtual MapText TopLabel
      {
        get => this.myTopLabel;
        set
        {
          MapText topLabel = this.myTopLabel;
          if (topLabel == value)
            return;
          if (topLabel != null)
            this.Remove((MapObject) topLabel);
          this.myTopLabel = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2404, 0, (object) topLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
