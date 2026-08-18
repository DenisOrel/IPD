
// Type: Intermech.Interfaces.Compositions.SeriesDatesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Вспомогательный класс, позволяющий загружать информацию о сериях/датах
    /// для объектов в матрицы
    /// </summary>
    public static class SeriesDatesHelper
    {
      /// <summary>Удельный "вес" версии после подбора</summary>
      public static int GetWeight(ObjectFiltrationState state)
      {
        switch (state)
        {
          case ObjectFiltrationState.fsNotRequired:
            return 2;
          case ObjectFiltrationState.fsMainArticleNotFound:
            return 4;
          case ObjectFiltrationState.fsVersionByDate:
            return 1;
          case ObjectFiltrationState.fsVersionBySeries:
            return 0;
          case ObjectFiltrationState.fsVarianceSeriesDate:
            return 3;
          default:
            return 5;
        }
      }
    }
}
