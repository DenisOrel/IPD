
// Type: Intermech.Controls.SpellCheck.ErrorStruct
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Controls.SpellCheck;

public struct ErrorStruct
{
  private int start;
  private int end;

  /// <summary>Начало ошибки</summary>
  public int Start
  {
    get => this.start;
    set => this.start = value;
  }

  /// <summary>Конечеая позиция ошибки</summary>
  public int End
  {
    get => this.end;
    set => this.end = value;
  }
}
