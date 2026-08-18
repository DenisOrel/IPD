
// Type: Intermech.Bars.ToolBarMeasure
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.Drawing;


namespace Intermech.Bars
{
    internal class ToolBarMeasure
    {
      public static int MaxItemSize(
        ToolBar toolBar,
        Graphics g,
        IToolBarRenderer renderer,
        bool vertical)
      {
        int num = 0;
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) toolBar.Items)
        {
          if (toolbarItemBase is ButtonItemBase)
          {
            Size size = ToolBarMeasure.MeasureButton(toolBar, g, toolbarItemBase, vertical, renderer);
            if (vertical)
            {
              if (size.Height > num)
                num = size.Height;
            }
            else if (toolbarItemBase is DropDownMenuItem)
            {
              if (size.Width - 11 > num)
                num = size.Width - 11;
            }
            else if (size.Width > num)
              num = size.Width;
          }
        }
        return num;
      }

      private static void ApplyLayout(
        ToolBar toolBar,
        Graphics g,
        ToolbarItemBase[] items,
        bool A_3,
        bool A_4)
      {
        foreach (ToolbarItemBase toolbarItemBase in items)
        {
          if (!toolbarItemBase.Visible || toolbarItemBase._underChevron)
            toolbarItemBase.ApplyLayout(Rectangle.Empty, g, A_3, A_4);
          else
            toolbarItemBase.ApplyLayout(toolbarItemBase._measuredBounds, g, A_3, A_4);
          if (toolbarItemBase is ControlContainerItem)
            ((ControlContainerItem) toolbarItemBase).ContainedControl.Visible = !A_3 && !toolBar.FriendDesignMode && toolbarItemBase.Visible && !toolbarItemBase._underChevron;
        }
      }

      public static Size MeasureButton(
        ToolBar toolbar,
        Graphics g,
        ToolbarItemBase item,
        bool vertical,
        IToolBarRenderer renderer)
      {
        SizeF sizeF = SizeF.Empty;
        Size size1 = Size.Empty;
        Size empty = Size.Empty;
        Size size2 = Size.Empty;
        if (item.Text.Length != 0 && item._showText)
        {
          StringFormat genericTypographic = StringFormat.GenericTypographic;
          StringFormat format = toolbar.TextAlign != ToolBarTextAlign.Underneath ? renderer.LeftStringFormat : renderer.CenterStringFormat;
          sizeF = g.MeasureString(item.Text, item.Font, 1000, format);
          if (item is MenuBarItem)
            sizeF.Width += 4f;
        }
        if (item is ButtonItemBase)
        {
          ButtonItemBase buttonItemBase = (ButtonItemBase) item;
          if (buttonItemBase.Icon != null)
            size1 = buttonItemBase.IconSize;
          else if (buttonItemBase.Image != null)
            size1 = buttonItemBase.Image.Size;
          else if (buttonItemBase.ImageList != null && buttonItemBase.ImageIndex >= 0 && buttonItemBase.ImageIndex <= buttonItemBase.ImageList.Images.Count - 1)
            size1 = buttonItemBase.ImageList.ImageSize;
          size2.Width = Convert.ToInt32(Math.Ceiling((double) sizeF.Width));
          size2.Height = Convert.ToInt32(Math.Ceiling((double) sizeF.Height));
          if (size1.Width != 0)
          {
            if (vertical)
            {
              if ((double) sizeF.Width != 0.0)
              {
                if (toolbar.TextAlign == ToolBarTextAlign.Underneath)
                {
                  if (size1.Height > size2.Height)
                    size2.Height = size1.Height;
                  size2.Width += size1.Width;
                }
                else
                {
                  if (size1.Width > size2.Width)
                    size2.Width = size1.Width;
                  size2.Height += size1.Height + 2;
                }
              }
              else
                size2 = size1;
            }
            else if ((double) sizeF.Width != 0.0)
            {
              if (toolbar.TextAlign == ToolBarTextAlign.Underneath)
              {
                if (size1.Width > size2.Width)
                  size2.Width = size1.Width;
                size2.Height += size1.Height;
              }
              else
              {
                if (size1.Height > size2.Height)
                  size2.Height = size1.Height;
                size2.Width += size1.Width + 2;
              }
            }
            else
              size2 = size1;
          }
        }
        else if (item is ControlContainerItem)
        {
          ControlContainerItem controlContainerItem = (ControlContainerItem) item;
          size2 = new Size(controlContainerItem.MinimumControlWidth, controlContainerItem.ContainedControl.Height);
          if ((double) sizeF.Width != 0.0)
          {
            size2.Width += Convert.ToInt32(Math.Ceiling((double) sizeF.Width)) + 3;
            if ((double) sizeF.Height > (double) size2.Height)
              size2.Height = Convert.ToInt32(Math.Ceiling((double) sizeF.Height));
          }
        }
        else if (item is LabelItem)
        {
          size2.Width = Convert.ToInt32(Math.Ceiling((double) sizeF.Width));
          size2.Height = Convert.ToInt32(Math.Ceiling((double) sizeF.Height));
        }
        if (size2.Width < 16 /*0x10*/)
          size2.Width = 16 /*0x10*/;
        if (size2.Height < 12)
          size2.Height = 12;
        if (item.MinimumSize > 0)
        {
          if (vertical && size2.Height < item.MinimumSize)
            size2.Height = item.MinimumSize;
          else if (!vertical && size2.Width < item.MinimumSize)
            size2.Width = item.MinimumSize;
        }
        if (item is DropDownMenuItem)
          size2.Width += 11;
        return size2;
      }

      public static Size GetPreferredSizeWithExtent(
        ToolBar toolBar,
        Graphics g,
        IToolBarRenderer renderer,
        bool vertical,
        int extent,
        out bool wrapped)
      {
        wrapped = toolBar._wrapped;
        if (toolBar.an != Size.Empty && toolBar.ao == vertical && toolBar.ap == extent && toolBar.aq == toolBar.Situation)
          return toolBar.an;
        int width;
        int height;
        ToolBarMeasure.a(toolBar, g, renderer, vertical, extent, out width, out height, out wrapped);
        Size preferredSizeWithExtent = !vertical ? new Size(width, height) : new Size(height, width);
        toolBar.an = preferredSizeWithExtent;
        toolBar.ao = vertical;
        toolBar.ap = extent;
        toolBar.aq = toolBar.Situation;
        toolBar._wrapped = wrapped;
        return preferredSizeWithExtent;
      }

      public static void a(
        ToolBar toolBar,
        Graphics g,
        Rectangle A_2,
        IToolBarRenderer renderer,
        bool vertical,
        bool rightToLeft,
        bool flipLastItem)
      {
        int width;
        ToolbarItemBase[] toolbarItemBaseArray = ToolBarMeasure.a(toolBar, g, renderer, vertical, vertical ? A_2.Height : A_2.Width, out width, out int _, out bool _);
        ToolBarMeasure.a(toolBar, A_2, vertical, rightToLeft, flipLastItem, toolbarItemBaseArray, width);
        ToolBarMeasure.ApplyLayout(toolBar, g, toolbarItemBaseArray, vertical, rightToLeft);
      }

      private static void a(
        ToolBar A_0,
        Rectangle A_1,
        bool A_2,
        bool A_3,
        bool A_4,
        ToolbarItemBase[] A_5,
        int A_6)
      {
        int num1 = A_0.Items.Count - 1;
        for (int index = 0; index <= num1; ++index)
        {
          ToolbarItemBase toolbarItemBase = A_5[index];
          toolbarItemBase._underChevron = false;
          if (toolbarItemBase is ControlContainerItem & A_2)
          {
            toolbarItemBase._underChevron = true;
            Size l = toolbarItemBase.l;
            A_6 -= A_2 ? l.Height : l.Width;
            if (toolbarItemBase.BeginGroup)
              A_6 -= 7;
          }
        }
        if (A_6 > (A_2 ? A_1.Height : A_1.Width) && A_0.Overflow != ToolBarOverflow.Wrap)
        {
          for (int index1 = 0; index1 <= 4; ++index1)
          {
            for (int index2 = num1; index2 >= 0; --index2)
            {
              if (A_5[index2].Importance == (ToolBarItemImportance) index1)
              {
                A_5[index2]._underChevron = true;
                if (A_5[index2].BeginGroup)
                  A_6 -= 7;
                Size l = A_5[index2].l;
                A_6 -= A_2 ? l.Height : l.Width;
                --A_6;
                if (A_6 <= (A_2 ? A_1.Height : A_1.Width))
                  goto label_17;
              }
            }
          }
        }
    label_17:
        int numRef2 = 0;
        int numRef1 = 0;
        while (numRef1 < A_5.Length)
          ToolBarMeasure.a(A_0, A_1, ref numRef1, ref numRef2, A_2, A_3, A_4, A_5);
        int num2 = numRef2 - 1;
        int num3 = A_2 ? A_1.Width : A_1.Height;
        if (num3 <= num2 + 1)
          return;
        int num4 = (num3 - num2) / 2;
        foreach (ToolbarItemBase toolbarItemBase in A_5)
        {
          Rectangle measuredBounds = toolbarItemBase._measuredBounds;
          if (A_2)
            measuredBounds.X += num4;
          else
            measuredBounds.Y += num4;
          toolbarItemBase._measuredBounds = measuredBounds;
        }
      }

      private static ToolbarItemBase[] a(
        ToolBar toolBar,
        Graphics g,
        IToolBarRenderer renderer,
        bool vertical,
        int extent,
        out int width,
        out int height,
        out bool wrapped)
      {
        int num1 = 0;
        wrapped = false;
        ToolbarItemBase[] toolbarItemBaseArray = new ToolbarItemBase[toolBar.Items.Count];
        if (toolBar.TextAlign == ToolBarTextAlign.Underneath)
          num1 = ToolBarMeasure.MaxItemSize(toolBar, g, renderer, vertical);
        int num2 = 0;
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) toolBar.Items)
        {
          Size size = ToolBarMeasure.MeasureButton(toolBar, g, toolbarItemBase, vertical, renderer);
          if (num1 != 0 && toolbarItemBase is ButtonItemBase)
          {
            if (vertical)
              size.Height = num1;
            else
              size.Width = !(toolbarItemBase is DropDownMenuItem) ? num1 : num1 + 11;
          }
          toolbarItemBase.k = size;
          toolbarItemBaseArray[num2++] = toolbarItemBase;
        }
        foreach (ToolbarItemBase toolbarItemBase in toolbarItemBaseArray)
        {
          Size k = toolbarItemBase.k;
          if (vertical)
          {
            k.Width += toolbarItemBase.Padding.Top + toolbarItemBase.Padding.Bottom;
            k.Height += toolbarItemBase.Padding.Left + toolbarItemBase.Padding.Right;
          }
          else
          {
            k.Width += toolbarItemBase.Padding.Left + toolbarItemBase.Padding.Right;
            k.Height += toolbarItemBase.Padding.Top + toolbarItemBase.Padding.Bottom;
          }
          toolbarItemBase.l = k;
        }
        int num3 = 0;
        int num4 = 0;
        width = 0;
        int num5 = 0;
        bool flag = true;
        foreach (ToolbarItemBase toolbarItemBase in toolbarItemBaseArray)
        {
          Size l = toolbarItemBase.l;
          int num6 = vertical ? l.Height : l.Width;
          int num7 = vertical ? l.Width : l.Height;
          if (toolbarItemBase.Visible && (!(toolbarItemBase is ControlContainerItem) || !vertical))
          {
            int num8 = 0;
            if (toolbarItemBase.BeginGroup && !flag)
              num8 += 7;
            flag = false;
            int num9 = num8 + num6;
            if (toolBar.Overflow == ToolBarOverflow.Wrap && num3 + num9 > extent)
            {
              num3 = 0;
              num4 += num5 + 1;
              num5 = 0;
              wrapped = true;
            }
            if (toolBar.Overflow != ToolBarOverflow.Wrap || num3 + num9 <= extent)
              num3 += num9 + 1;
            else
              num7 = 0;
            if (num3 > width)
              width = num3;
          }
          if ((!(toolbarItemBase is ControlContainerItem) || !vertical) && num7 > num5)
            num5 = num7;
        }
        height = num4 + num5 + 1;
        return toolbarItemBaseArray;
      }

      private static void a(
        ToolBar bar1,
        Rectangle rectangle1,
        ref int numRef1,
        ref int numRef2,
        bool flag2,
        bool flag4,
        bool flag3,
        ToolbarItemBase[] baseArray2)
      {
        int num1 = 0;
        int index1 = -1;
        ToolbarItemBase[] toolbarItemBaseArray = new ToolbarItemBase[baseArray2.Length];
        int num2 = 0;
        bool flag1 = true;
        bool flag5 = false;
        int num3 = 0;
        int num4 = 0;
        for (int index2 = numRef1; index2 < baseArray2.Length; ++index2)
        {
          ToolbarItemBase toolbarItemBase = baseArray2[index2];
          if (toolbarItemBase.Visible && !toolbarItemBase._underChevron)
          {
            int num5 = 0;
            toolbarItemBase._drawSeparator = toolbarItemBase.BeginGroup | flag5 && !flag1;
            flag5 = false;
            if (toolbarItemBase._drawSeparator)
              num5 += 7;
            flag1 = false;
            Size l = toolbarItemBase.l;
            int num6 = num5 + (flag2 ? l.Height : l.Width);
            if (bar1.Overflow != ToolBarOverflow.Wrap || num1 + num6 <= (flag2 ? rectangle1.Height : rectangle1.Width))
            {
              toolbarItemBase.n = num1;
              if (toolbarItemBase._drawSeparator)
                toolbarItemBase.n += 7;
              num1 += num6 + 1;
              if (flag2 && l.Width > num3)
                num3 = l.Width;
              else if (!flag2 && l.Height > num3)
                num3 = l.Height;
              index1 = index2;
              toolbarItemBaseArray[num2++] = toolbarItemBase;
              if (toolbarItemBase.Stretch)
                ++num4;
            }
            else
              break;
          }
          else
          {
            index1 = index2;
            if (!flag5)
              flag5 = toolbarItemBase.BeginGroup;
          }
        }
        if (num4 != 0 && index1 != -1)
        {
          int num7 = flag2 ? rectangle1.Height - num1 : rectangle1.Width - num1;
          int num8 = num4;
          int num9 = num7 / num8;
          for (int index3 = numRef1; index3 <= index1; ++index3)
          {
            if (baseArray2[index3].Stretch)
            {
              int num10 = num8 == 1 ? num7 : num9;
              Size l = baseArray2[index3].l;
              Size k = baseArray2[index3].k;
              if (flag2)
              {
                l.Height += num10;
                k.Height += num10;
              }
              else
              {
                l.Width += num10;
                k.Width += num10;
              }
              baseArray2[index3].l = l;
              baseArray2[index3].k = k;
              num7 -= num10;
              --num8;
              if (index3 < index1)
              {
                for (int index4 = index3 + 1; index4 <= index1; ++index4)
                  baseArray2[index4].n += num10;
              }
            }
          }
        }
        if (index1 == -1)
        {
          index1 = numRef1;
          baseArray2[index1]._underChevron = true;
        }
        if (flag3 && index1 == baseArray2.Length - 1 && !baseArray2[index1]._underChevron && baseArray2[index1].Visible)
        {
          Size l = baseArray2[index1].l;
          baseArray2[index1].n = !flag2 ? rectangle1.Width - l.Width : rectangle1.Height - l.Height;
        }
        float num11 = (float) num3 / 2f;
        for (int index5 = numRef1; index5 <= index1; ++index5)
        {
          ToolbarItemBase toolbarItemBase = baseArray2[index5];
          if (toolbarItemBase.Visible && !toolbarItemBase._underChevron)
          {
            Size l = toolbarItemBase.l;
            int num12 = flag2 ? l.Width : l.Height;
            toolbarItemBase.o = (int) ((double) num11 - (double) num12 / 2.0);
          }
        }
        for (int index6 = numRef1; index6 <= index1; ++index6)
        {
          ToolbarItemBase toolbarItemBase = baseArray2[index6];
          if (toolbarItemBase.Visible && !toolbarItemBase._underChevron)
          {
            Size l = toolbarItemBase.l;
            Size k = toolbarItemBase.k;
            toolbarItemBase._measuredBounds = !flag2 ? (!flag4 ? new Rectangle(rectangle1.X + toolbarItemBase.n, rectangle1.Y + numRef2 + toolbarItemBase.o, l.Width, l.Height) : new Rectangle(rectangle1.Right - toolbarItemBase.n - l.Width, rectangle1.Y + numRef2 + toolbarItemBase.o, l.Width, l.Height)) : (!flag4 ? new Rectangle(rectangle1.X + numRef2 + toolbarItemBase.o, rectangle1.Y + toolbarItemBase.n, l.Width, l.Height) : new Rectangle(rectangle1.X + numRef2 + toolbarItemBase.o, rectangle1.Bottom - toolbarItemBase.n - l.Height, l.Width, l.Height));
          }
        }
        numRef2 += num3 + 1;
        numRef1 = index1 + 1;
      }
    }
}
