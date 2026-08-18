
// Type: Intermech.ControlFlow.CompositeAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow
{
    public class CompositeAction : IAction
    {
      private readonly IEnumerable<IAction> actions;

      public CompositeAction(params IAction[] actions)
      {
        this.actions = actions != null ? (IEnumerable<IAction>) actions : throw new ArgumentNullException(nameof (actions));
      }

      public CompositeAction(IEnumerable<IAction> actions)
      {
        this.actions = actions != null ? actions : throw new ArgumentNullException(nameof (actions));
      }

      public void Perform()
      {
        foreach (IAction action in this.actions)
          action.Perform();
      }
    }
}
