
// Type: Intermech.UI.CodeBehavior
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Базовый класс для объектов-поведений, которые позволяют в декларативной форме менять
    /// поведение элементов пользовательского интерфейса. Создание и подключение
    /// объектов-поведений выполняется в code-behind.
    /// </summary>
    public class CodeBehavior
    {
      private bool isAttached;

      /// <summary>Подключает текущий объект, если он еще не подключен.</summary>
      public void Attach()
      {
        if (this.isAttached)
          return;
        this.DoAttach();
        this.isAttached = true;
      }

      /// <summary>Отключает текущий объект, если он еще подключен.</summary>
      public void Detach()
      {
        if (!this.isAttached)
          return;
        this.DoDetach();
        this.isAttached = false;
      }

      /// <summary>Подключает текущий объект.</summary>
      protected virtual void DoAttach()
      {
      }

      /// <summary>Отключает текущий объект.</summary>
      protected virtual void DoDetach()
      {
      }

      /// <summary>Возвращает признак, что текущий объект подключен.</summary>
      public bool IsAttached
      {
        [DebuggerStepThrough] get => this.isAttached;
      }
    }
}
