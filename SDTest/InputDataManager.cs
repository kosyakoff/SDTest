using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace SDTest
{
    public class InputDataManager
    {
        #region Internal Methods

        internal IEnumerable<InputObject> GenerateUnsortedList(List<Color> selectedColorsList, int numberOfResultingInputObjects)
        {
            var randomIndex = new Random();
            int colorCount = selectedColorsList.Count;
            List<InputObject> inputObjectList = new List<InputObject>();

            for (int i = 0; i < numberOfResultingInputObjects; i++)
            {
                var randomColor = selectedColorsList.ElementAt(randomIndex.Next(colorCount));
                yield return new InputObject(randomColor);
            }
        }

        internal List<InputObject> SortList(ObservableCollection<InputObject> inputObjectList, List<Color> selectedColorList)
        {
            var comparer = new InputObjectComparer(selectedColorList);
            var OrderedList = new List<InputObject>(inputObjectList);
            OrderedList.Sort(comparer);

            return OrderedList;
        }

        #endregion Internal Methods
    }
}