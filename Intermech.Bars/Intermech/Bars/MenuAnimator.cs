
// Type: Intermech.Bars.MenuAnimator
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;


namespace Intermech.Bars
{
    internal class MenuAnimator
    {
      private static void Fade(PopupMenu A_0)
      {
        Win32.ANIMATE_FLAGS A_2 = Win32.ANIMATE_FLAGS.AW_BLEND;
        Win32.AnimateWindow(A_0.Handle, 200, A_2);
      }

      private static void Slide(PopupMenu A_0, bool A_1)
      {
        Win32.ANIMATE_FLAGS animateFlags = Win32.ANIMATE_FLAGS.AW_SLIDE;
        Win32.ANIMATE_FLAGS A_2;
        switch (A_0.MenuItem.MenuDirection)
        {
          case MenuOffset.Top:
            A_2 = animateFlags | Win32.ANIMATE_FLAGS.AW_VER_NEGATIVE;
            if (A_1)
            {
              A_2 |= A_0.Host.RightToLeft ? Win32.ANIMATE_FLAGS.AW_HOR_NEGATIVE : Win32.ANIMATE_FLAGS.AW_HOR_POSITIVE;
              break;
            }
            break;
          case MenuOffset.Left:
            A_2 = animateFlags | Win32.ANIMATE_FLAGS.AW_HOR_NEGATIVE;
            if (A_1)
            {
              A_2 |= Win32.ANIMATE_FLAGS.AW_VER_POSITIVE;
              break;
            }
            break;
          case MenuOffset.Right:
            A_2 = animateFlags | Win32.ANIMATE_FLAGS.AW_HOR_POSITIVE;
            if (A_1)
            {
              A_2 |= Win32.ANIMATE_FLAGS.AW_VER_POSITIVE;
              break;
            }
            break;
          default:
            A_2 = animateFlags | Win32.ANIMATE_FLAGS.AW_VER_POSITIVE;
            if (A_1)
            {
              A_2 |= A_0.Host.RightToLeft ? Win32.ANIMATE_FLAGS.AW_HOR_NEGATIVE : Win32.ANIMATE_FLAGS.AW_HOR_POSITIVE;
              break;
            }
            break;
        }
        Win32.AnimateWindow(A_0.Handle, 200, A_2);
      }

      public static void Animate(PopupMenu menu, MenuAnimation A_1)
      {
        menu._animating = true;
        switch (A_1)
        {
          case MenuAnimation.Fade:
            MenuAnimator.Fade(menu);
            break;
          case MenuAnimation.Slide:
          case MenuAnimation.Unfold:
            MenuAnimator.Slide(menu, A_1 == MenuAnimation.Unfold);
            break;
        }
        menu._animating = false;
      }

      public static MenuAnimation SystemAnimation(MenuAnimation animation, bool skip)
      {
        if (skip)
          return MenuAnimation.None;
        if (animation != MenuAnimation.System)
          return animation;
        int A_2_1 = 0;
        Win32.SystemParametersInfo(4098, 0, ref A_2_1, 0);
        if (A_2_1 == 0)
          return MenuAnimation.None;
        int A_2_2 = 0;
        Win32.SystemParametersInfo(4114, 0, ref A_2_2, 0);
        return A_2_2 != 1 ? MenuAnimation.Slide : MenuAnimation.Fade;
      }
    }
}
