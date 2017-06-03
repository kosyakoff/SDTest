using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.Common.Tests
{
    [TestClass]
    public class InputDataManagerTests : BaseTest
    {
        #region Private Fields

        private List<Color> FilledColorList = new List<Color> { Colors.Red, Colors.Green, Colors.Blue };
        private int NumberOfResultingInputObjects = 25;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void GenerateUnsortedList_AllOutputObjects_must_be_in_selectedColorList()
        {
            var list = InputDataManager.GenerateUnsortedList(FilledColorList, NumberOfResultingInputObjects).ToList();

            Assert.IsTrue(list.All(
                outObject => FilledColorList.Contains(outObject.InputColor)
                ));
        }

        [TestMethod]
        public void GenerateUnsortedList_AllOutputObjects_must_be_not_null_and_have_color()
        {
            var unsortedObjectList = InputDataManager.GenerateUnsortedList(FilledColorList, NumberOfResultingInputObjects).ToList();

            Assert.IsTrue(unsortedObjectList.All(
                outObject => outObject != null && outObject.InputColor != default(Color))
                );
        }

        [TestMethod]
        public void GenerateUnsortedList_CorrectInputValues_ShouldPass()
        {
            var list = InputDataManager.GenerateUnsortedList(FilledColorList, NumberOfResultingInputObjects).ToList();
        }

        [TestMethod]
        public void GenerateUnsortedList_numberOfInputObjects_25_should_equal_numberOfOutputObjects_25()
        {
            var list = InputDataManager.GenerateUnsortedList(FilledColorList, NumberOfResultingInputObjects).ToList();
            var numberOfOutputObjects = NumberOfResultingInputObjects;

            Assert.AreEqual(numberOfOutputObjects, list.Count);
        }

        [TestMethod]
        public void GenerateUnsortedList_Param_NumberOfResultingObject_is_negative_throws_ArgumentOutOfRangeException()
        {
            var colorList = new List<Color> { Colors.Red };

            Assert.Throws<ArgumentOutOfRangeException>(() => InputDataManager.GenerateUnsortedList(colorList, -1).ToList());
        }

        [TestMethod]
        public void GenerateUnsortedList_Param_SelectedObjects_is_empty_throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => InputDataManager.GenerateUnsortedList(new List<Color>(), 1).ToList());
        }

        ///GenerateUnsortedList method tests
        [TestMethod]
        public void GenerateUnsortedList_Param_SelectedObjects_is_null_throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => InputDataManager.GenerateUnsortedList(null, 1).ToList());
        }

        ///SortList method tests
        ///

        [TestMethod]
        public void SortList_Param_SelectedColorList_is_null_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => InputDataManager.SortList(new List<IInputObject>(), null));
        }

        [TestMethod]
        public void SortList_Param_SelectedColorList_is_zero_should_throw_ArgumentException()
        {
            var unsortedObjectList = InputDataManager.GenerateUnsortedList(FilledColorList, NumberOfResultingInputObjects).ToList();
            Assert.Throws<ArgumentException>(() => InputDataManager.SortList(unsortedObjectList, new List<Color>()));
        }

        [TestMethod]
        public void SortList_Param_UnsortedObjectList_is_null_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => InputDataManager.SortList(null, FilledColorList));
        }

        [TestMethod]
        public void SortList_Should_return_correct_sortedList()
        {
            List<IInputObject> unsortedList = new List<IInputObject>
            {
                new InputObject(Colors.Blue),
                new InputObject(Colors.Green),
                new InputObject(Colors.Red)
            };

            List<IInputObject> sortedList = new List<IInputObject>
            {
                new InputObject(Colors.Red),
                 new InputObject(Colors.Green),
                 new InputObject(Colors.Blue)
            };

            var actualList = InputDataManager.SortList(unsortedList, FilledColorList).ToList();

            Assert.IsTrue(sortedList.Select(x => x.InputColor).SequenceEqual(actualList.Select(y => y.InputColor)));
        }

        #endregion Public Methods
    }
}