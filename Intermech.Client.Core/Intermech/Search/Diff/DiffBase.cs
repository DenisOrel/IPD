
// Type: Intermech.Search.Diff.DiffBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.Diff;

public abstract class DiffBase : IDiff
{
  public DiffBase(DiffOperand firstOperand, DiffOperand secondOperand)
  {
    this.FirstOperand = firstOperand;
    this.SecondOperand = secondOperand;
  }

  public DiffOperand FirstOperand { get; private set; }

  public DiffOperand SecondOperand { get; private set; }

  public DiffResult GetResult()
  {
    if (this.FirstOperand == null)
      return DiffResult.NotExist;
    if (this.SecondOperand == null)
      return DiffResult.NotExistOnOther;
    return object.Equals(this.FirstOperand.Value, this.SecondOperand.Value) ? DiffResult.ValuesEquals : DiffResult.ValuesNotEquals;
  }
}
