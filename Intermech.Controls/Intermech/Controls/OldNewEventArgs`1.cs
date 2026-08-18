
// Type: Intermech.Controls.OldNewEventArgs`1
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls;

public class OldNewEventArgs<T> : EventArgs
{
  private T m_oldValue;
  private T m_newValue;

  public OldNewEventArgs(T oldValue, T newValue)
  {
    this.OldValue = oldValue;
    this.NewValue = newValue;
  }

  public T OldValue
  {
    get => this.m_oldValue;
    protected set => this.m_oldValue = value;
  }

  public T NewValue
  {
    get => this.m_newValue;
    protected set => this.m_newValue = value;
  }
}
