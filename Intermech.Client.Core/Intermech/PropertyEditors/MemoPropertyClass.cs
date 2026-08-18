
// Type: Intermech.PropertyEditors.MemoPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (MemoEditor), typeof (UITypeEditor))]
public class MemoPropertyClass
{
  private string memo = string.Empty;
  private bool isNull;
  private int maxMemoSize = CoreConsts.MaxMemoEditorSizeDefault;

  public string Memo
  {
    get => this.memo;
    set
    {
      this.memo = value;
      this.isNull = false;
    }
  }

  public int MaxMemoSize => this.maxMemoSize;

  public bool IsNull => this.isNull;

  public MemoPropertyClass(string aMemo)
    : this(aMemo, false, CoreConsts.MaxMemoEditorSizeDefault)
  {
  }

  public MemoPropertyClass(string aMemo, bool aIsNull)
    : this(aMemo, aIsNull, CoreConsts.MaxMemoEditorSizeDefault)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aMemo"></param>
  /// <param name="aMaxMemoSize">максимальная длина в символах</param>
  public MemoPropertyClass(string aMemo, int aMaxMemoSize)
    : this(aMemo, false, aMaxMemoSize)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aMemo"></param>
  /// <param name="aIsNull"></param>
  /// <param name="aMaxMemoSize">максимальная длина в символах</param>
  public MemoPropertyClass(string aMemo, bool aIsNull, int aMaxMemoSize)
  {
    this.memo = aMemo;
    this.isNull = aIsNull;
    this.maxMemoSize = aMaxMemoSize;
  }

  public override string ToString()
  {
    return this.isNull ? string.Empty : this.memo.ToString().Replace('\r', ' ').Replace('\n', ' ');
  }
}
