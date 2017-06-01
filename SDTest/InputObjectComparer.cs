using System.Collections.Generic;
using System.Windows.Media;

namespace SDTest
{
    public class InputObjectComparer : IComparer<InputObject>
    {
        #region Private Fields

        private List<Color> _colors = new List<Color>();

        #endregion Private Fields

        #region Public Constructors

        public InputObjectComparer(List<Color> colors)
        {
            _colors = colors;
        }

        #endregion Public Constructors

        #region Public Methods

        public int Compare(InputObject x, InputObject y)
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