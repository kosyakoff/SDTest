using System;
using System.Windows.Media;

namespace SDTest.Common
{
    public class InputObject : IInputObject
    {
        #region Public Constructors

        public InputObject(Color color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            InputColor = color;
        }

        #endregion Public Constructors

        #region Public Properties

        public Color InputColor { get; set; }

        #endregion Public Properties
    }
}