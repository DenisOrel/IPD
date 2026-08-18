
// Type: Intermech.Extensions.MemberInfoExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions
{
    public static class MemberInfoExtensions
    {
      [NotNull]
      [ItemNotNull]
      [Pure]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<TAttribute> GetAttributes<TAttribute>(
        [NotNull] this MemberInfo memberInfo,
        bool inherit = true)
        where TAttribute : Attribute
      {
        return memberInfo.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>();
      }

      [NotNull]
      [Pure]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static TAttribute GetAttribute<TAttribute>([NotNull] this MemberInfo memberInfo, bool inherit = true) where TAttribute : Attribute
      {
        return memberInfo.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>().First();
      }

      [Pure]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetAttribute<TAttribute>(
        [NotNull] this MemberInfo memberInfo,
        [CanBeNull] out TAttribute attribute,
        bool inherit)
        where TAttribute : Attribute
      {
        foreach (object customAttribute in memberInfo.GetCustomAttributes(typeof (TAttribute), inherit))
        {
          if (customAttribute is TAttribute attribute1)
          {
            attribute = attribute1;
            return true;
          }
        }
        attribute = default (TAttribute);
        return false;
      }

      [Pure]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetAttribute<TAttribute>(
        [NotNull] this MemberInfo memberInfo,
        [CanBeNull] out TAttribute attribute)
        where TAttribute : Attribute
      {
        foreach (Attribute customAttribute in memberInfo.GetCustomAttributes(typeof (TAttribute)))
        {
          if (customAttribute is TAttribute attribute1)
          {
            attribute = attribute1;
            return true;
          }
        }
        attribute = default (TAttribute);
        return false;
      }

      [Pure]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool HasAttribute<TAttribute>([NotNull] this MemberInfo memberInfo) where TAttribute : Attribute
      {
        return memberInfo.GetCustomAttributes(typeof (TAttribute)).Cast<TAttribute>().Any();
      }
    }
}
