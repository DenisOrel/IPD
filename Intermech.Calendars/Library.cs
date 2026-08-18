using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Reflection;


namespace Intermech.Calendars
{
    public static class Library
    {
      [NotNull]
      private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

      [NotNull]
      internal static Assembly Assembly => typeof (Library).Assembly;

      /// <summary>Инициализация сервисов, кэшей и т.п. библиотеки Intermech.Project.Controls</summary>
      public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
      {
        Library._initOnce.Invoke(ref session, (Action) (() =>
        {
          MetadataLoader.Init(session);
          Intermech.Extensions.Interfaces.Library.Init(serviceProvider, session);
        }));
      }
    }
}
