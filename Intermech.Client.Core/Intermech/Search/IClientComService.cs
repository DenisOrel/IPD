
// Type: Intermech.Search.IClientComService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.InteropServices;


namespace Intermech.Search;

[ComVisible(true)]
[Guid("1871E541-6540-455C-A10B-598A15A6077B")]
public interface IClientComService
{
  bool IsStartupCompleted { get; }
}
