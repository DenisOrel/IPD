
// Type: Intermech.Interfaces.Contexts.CurrentEditingContextHeaderSerializer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Сериализатор объектов типа <see cref="T:Intermech.Interfaces.Contexts.CurrentEditingContext" /> в строковое представление,
    /// пригодное для передачи в виде custom заголовка протокола HTTP.
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    internal sealed class CurrentEditingContextHeaderSerializer
    {
      private const string DummyKeyword = "Dummy";
      private const string EmptyKeyword = "Empty";

      public string Serialize(CurrentEditingContext editingContext)
      {
        if (editingContext.IsDummy)
          return "Dummy";
        if (editingContext.IsEmpty)
          return "Empty";
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(128 /*0x80*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(editingContext.ContextID.ToString());
          stringBuilder.Append(';');
          stringBuilder.Append(editingContext.ModificationID.ToString());
          stringBuilder.Append(';');
          stringBuilder.Append(editingContext.ContextMode.ToString());
          return stringBuilder.ToString();
        }
      }

      public CurrentEditingContext Deserialize(string serializedData)
      {
        switch (serializedData)
        {
          case "Dummy":
            return CurrentEditingContext.Dummy;
          case "Empty":
            return CurrentEditingContext.Empty;
          default:
            string[] strArray = serializedData.Split(';');
            long result1;
            long result2;
            EditingContextMode result3;
            if (strArray.Length == 3 && long.TryParse(strArray[0], out result1) && long.TryParse(strArray[1], out result2) && Enum.TryParse<EditingContextMode>(strArray[2], out result3))
              return new CurrentEditingContext(result1, result2, result3);
            throw new FormatException($"Unable to deserialize the string value '{serializedData} to {"CurrentEditingContext"} object.");
        }
      }
    }
}
