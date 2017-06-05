using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SDTest.Common.Tests
{
    [TestClass]
    public class InputObjectComparerTests : BaseTest
    {
        #region Private Fields

        private List<Color> FilledColorList = new List<Color> { Colors.Red, Colors.Green, Colors.Blue };

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void Compare_If_inputObject_is_not_in_selectedColors_when_it_less_than_color_in_list()
        {
            var comparer = new InputObjectComparer(FilledColorList);
            var objectWithColorInList = new InputObject(Colors.Red);
            var objectWithColorNotInList = new InputObject(Colors.Black);

            var firstElementIsLess = -1;

            int result = comparer.Compare(objectWithColorNotInList, objectWithColorInList);

            Assert.AreEqual(result, firstElementIsLess);
        }

        [TestMethod]
        public void Compare_Input_objects_with_same_color_should_be_equal()
        {
            var comparer = new InputObjectComparer(FilledColorList);
            var objectRed1 = new InputObject(Colors.Red);
            var objectRed2 = new InputObject(Colors.Red);

            var objectBlue1 = new InputObject(Colors.Blue);
            var objectBlue2 = new InputObject(Colors.Blue);

            var colorsAreEqual = 0;

            int result = comparer.Compare(objectRed1, objectRed2);

            Assert.AreEqual(result, colorsAreEqual);

            result = comparer.Compare(objectBlue1, objectBlue2);

            Assert.AreEqual(result, colorsAreEqual);
        }

        [TestMethod]
        public void Compare_Param_X_or_y_is_null_should_throw_ArgumentException()
        {
            var comparer = new InputObjectComparer(FilledColorList);
            var correctObject = new InputObject(Colors.Red);

            Assert.Throws<ArgumentException>(() => comparer.Compare(correctObject, null));
            Assert.Throws<ArgumentException>(() => comparer.Compare(null, correctObject));
        }

        /// <summary>
        /// Можно было бы использовать MSTest DataSource, но в данном случае текста бы вышло столько же
        /// </summary>
        [TestMethod]
        public void Compare_Should_return_correct_result()
        {
            var comparer = new InputObjectComparer(FilledColorList);
            var objectRed = new InputObject(Colors.Red);
            var objectBlue = new InputObject(Colors.Blue);
            var objectGreen = new InputObject(Colors.Green);

            var firstIsLess = -1;
            var firstIsGreater = 1;

            //Берётся условие что первый элемент будет меньше
            int result = comparer.Compare(objectRed, objectBlue);
            Assert.AreEqual(result, firstIsLess);

            result = comparer.Compare(objectRed, objectGreen);
            Assert.AreEqual(result, firstIsLess);

            result = comparer.Compare(objectGreen, objectBlue);
            Assert.AreEqual(result, firstIsLess);

            //Берётся условие что первый элемент будет больше
            result = comparer.Compare(objectBlue, objectRed);
            Assert.AreEqual(result, firstIsGreater);

            result = comparer.Compare(objectBlue, objectGreen);
            Assert.AreEqual(result, firstIsGreater);
        }

        [TestMethod]
        public void Constructor_Param_OrderedColors_is_empty_should_throw_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new InputObjectComparer(new List<Color>()));
        }

        [TestMethod]
        public void Constructor_Param_OrderedColors_is_null_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new InputObjectComparer(null));
        }

        #endregion Public Methods
    }
}