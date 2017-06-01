using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.Common
{
    public class InputDataManager : IInputDataManager
    {
        #region Public Methods

        public IEnumerable<IInputObject> GenerateUnsortedList(IEnumerable<Color> selectedColorsList, int numberOfResultingInputObjects)
        {
            var randomIndex = new Random();
            int colorCount = selectedColorsList.Count();
            List<IInputObject> inputObjectList = new List<IInputObject>();

            for (int i = 0; i < numberOfResultingInputObjects; i++)
            {
                var randomColor = selectedColorsList.ElementAt(randomIndex.Next(colorCount));
                yield return new InputObject(randomColor);
            }
        }

        public IEnumerable<IInputObject> SortList(IEnumerable<IInputObject> inputObjectList, IEnumerable<Color> selectedColorList)
        {
            var comparer = new InputObjectComparer(selectedColorList);
            var OrderedList = new List<IInputObject>(inputObjectList);
            OrderedList.Sort(comparer);

            return OrderedList;
        }

        #endregion Public Methods
    }
}