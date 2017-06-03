using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.Common
{
    public static class InputDataManager
    {
        #region Public Methods

        public static IEnumerable<IInputObject> GenerateUnsortedList(IEnumerable<Color> selectedColorsList, int numberOfResultingInputObjects)
        {
            if (selectedColorsList == null)
                throw new ArgumentNullException(nameof(selectedColorsList));

            if (!selectedColorsList.Any())
                throw new ArgumentException("Массив выбранных цветов пуст");

            if (numberOfResultingInputObjects <= 0)
                throw new ArgumentOutOfRangeException("Количество генерируемых входных объектов должно быть большим чем ноль");

            var randomIndex = new Random();
            int colorCount = selectedColorsList.Count();
            List<IInputObject> inputObjectList = new List<IInputObject>();

            for (int i = 0; i < numberOfResultingInputObjects; i++)
            {
                var randomColor = selectedColorsList.ElementAt(randomIndex.Next(colorCount));
                yield return new InputObject(randomColor);
            }
        }

        public static IEnumerable<IInputObject> SortList(IEnumerable<IInputObject> unsortedObjectList, IEnumerable<Color> selectedColorList)
        {
            if (unsortedObjectList == null)
                throw new ArgumentNullException(nameof(unsortedObjectList));
            if (selectedColorList == null)
                throw new ArgumentNullException(nameof(selectedColorList));

            if (!selectedColorList.Any())
                throw new ArgumentException("Количество выбранных цветов не должно быть меньшим или равным нули");

            if (unsortedObjectList.Any(x => x == null))
                throw new ArgumentException("Один или несколько входных объектов равны Null");

            var comparer = new InputObjectComparer(selectedColorList);
            var orderedList = new List<IInputObject>(unsortedObjectList);
            orderedList.Sort(comparer);

            return orderedList;
        }

        #endregion Public Methods
    }
}