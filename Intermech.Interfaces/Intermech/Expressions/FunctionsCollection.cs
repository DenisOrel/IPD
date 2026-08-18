
// Type: Intermech.Expressions.FunctionsCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions
{
    public class FunctionsCollection : IdentifiersCollection
    {
      internal event FunctionsCollection.AddEventHandler FunctionAdd;

      internal FunctionsCollection()
      {
      }

      public int Add(Function function) => this.List.Add((object) function);

      protected override void OnInsert(int index, object value)
      {
        this.RaiseFunctionAddEvent(((Function) value).Name);
      }

      private void RaiseFunctionAddEvent(string NewName)
      {
        if (this.FunctionAdd == null)
          return;
        this.FunctionAdd(NewName);
      }

      public void Remove(Function function) => this.List.Remove((object) function);

      public Function this[int index] => (Function) this.List[index];

      internal delegate void AddEventHandler(string NewName);
    }
}
