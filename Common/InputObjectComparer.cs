using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.Common
{
    public class InputObjectComparer : IComparer<IInputObject>
    {
        #region Private Fields

        private List<Color> _colors = new List<Color>();

        #endregion Private Fields

        #region Public Constructors

        public InputObjectComparer(IEnumerable<Color> colors)
        {
            _colors = colors.ToList();
        }

        #endregion Public Constructors

        #region Public Methods

        public int Compare(IInputObject x, IInputObject y)
        {
            int xIndex = _colors.IndexOf(x.InputColor);
            int yIndex = _colors.IndexOf(y.InputColor);

            if (xIndex == yIndex)
                return 0;
            if (xIndex < yIndex)
                return -1;

            return 1;
        }

        #endregion Public Methods
    }
}