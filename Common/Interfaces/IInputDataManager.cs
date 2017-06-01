using System.Collections.Generic;
using System.Windows.Media;

namespace SDTest.Common
{
    public interface IInputDataManager
    {
        IEnumerable<IInputObject> GenerateUnsortedList(IEnumerable<Color> selectedColorsList, int numberOfResultingInputObjects);
        IEnumerable<IInputObject> SortList(IEnumerable<IInputObject> inputObjectList, IEnumerable<Color> selectedColorList);
    }
}