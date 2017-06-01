using System.Windows.Media;

namespace SDTest
{
    public class InputObject
    {
        #region Public Constructors

        public InputObject(Color color)
        {
            InputColor = color;
        }

        #endregion Public Constructors

        #region Public Properties

        public Color InputColor { get; set; }

        #endregion Public Properties
    }
}