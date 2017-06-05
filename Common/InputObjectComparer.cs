using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.Common
{
    public class InputObjectComparer : IComparer<IInputObject>
    {
        #region Private Fields

        private List<Color> _orderedColorList = new List<Color>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="orderedColors">Список цветов в заданном порядке, по которому будут соритроваться входные объекты</param>
        public InputObjectComparer(IEnumerable<Color> orderedColors)
        {
            if (orderedColors == null)
                throw new ArgumentNullException(nameof(orderedColors));

            if (!orderedColors.Any())
                throw new ArgumentException("Входной массив цветов не может быть пустым");

            _orderedColorList = orderedColors.ToList();
        }

        #endregion Public Constructors

        #region Public Methods

        public int Compare(IInputObject x, IInputObject y)
        {
            if (x == null)
                throw new ArgumentException(nameof(x));
            if (y == null)
                throw new ArgumentException(nameof(y));

            int xIndex = _orderedColorList.IndexOf(x.InputColor);
            int yIndex = _orderedColorList.IndexOf(y.InputColor);

            if (xIndex == yIndex)
                return 0;
            if (xIndex < yIndex)
                return -1;

            return 1;
        }

        #endregion Public Methods
    }
}