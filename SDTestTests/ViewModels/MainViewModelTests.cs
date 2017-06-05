using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SDTest.ViewModels.Tests
{
    [TestClass()]
    public class MainViewModelTests : BaseTest
    {
        #region Private Fields

        private List<Color> FilledColorList = new List<Color> { Colors.Red, Colors.Green, Colors.Blue };

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        [TestCategory("UnitTests")]
        public void Constructor_Should_create_correct_instance()
        {
            var model = new MainViewModel();

            Assert.IsNotNull(model.GenerateUnsortListCommand);
            Assert.IsNotNull(model.InputObjectList);
            Assert.IsNotNull(model.OrderedList);
            Assert.IsNotNull(model.SortListCommand);
            Assert.IsNotNull(model.SourceColors);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GenerateUnsortedList_Should_throw_NullReferenceException_on_null_InputObjectList()
        {
            var model = new MainViewModel();
            model.InputObjectList = null;
            Assert.Throws<NullReferenceException>(() => model.GenerateUnsortListCommand.Execute(null));
        }

        [TestMethod]
        [TestCategory("Integration Tests")]
        public void GenerateUnsortedList_Should_work_correctly()
        {
            var model = new MainViewModel();
            int expectedNumOfInputObjectsBeforeGen = 0;

            Assert.AreEqual<int>(expectedNumOfInputObjectsBeforeGen, model.InputObjectList.Count);

            int expectedNumOfInputObjectsAfterGen = 25;
            model.GenerateUnsortListCommand.Execute(null);

            Assert.AreEqual<int>(expectedNumOfInputObjectsAfterGen, model.InputObjectList.Count);

            Assert.IsTrue(model.InputObjectList.All(
                obj => FilledColorList.Contains(obj.InputColor)));
        }

        /// <summary>
        /// Сложно сделать 100% корректный тест, т.к. входные объекты random'ные и каких то цветов во входном наборе может не быть
        /// </summary>
        [TestMethod]
        [TestCategory("Integration Tests")]
        public void SortList_Should_sort_unsorted_input_objects_correctly()
        {
            var model = new MainViewModel();

            //Получаем отсортированный список объектов
            model.GenerateUnsortListCommand.Execute(null);
            model.SortListCommand.Execute(null);

            //Берём индексы всех красных, зеленых и белых объектов
            List<int> indexesOfAllRedObjects
                = model.OrderedList.Where(x => x.InputColor.Equals(Colors.Red)).Select(x => model.OrderedList.IndexOf(x)).ToList();
            List<int> indexesOfAllGreenObjects
     = model.OrderedList.Where(x => x.InputColor.Equals(Colors.Green)).Select(x => model.OrderedList.IndexOf(x)).ToList();
            List<int> indexesOfAllBlueObjects
      = model.OrderedList.Where(x => x.InputColor.Equals(Colors.Blue)).Select(x => model.OrderedList.IndexOf(x)).ToList();

            //Проверка что индексы всех красных объектов меньше индексов зелёных
            bool allRedsAreLessThanGreen = true;

            if (indexesOfAllGreenObjects.Any() && indexesOfAllRedObjects.Any())
                allRedsAreLessThanGreen = indexesOfAllRedObjects.All(x => !indexesOfAllGreenObjects.Any(y => y <= x));

            Assert.IsTrue(allRedsAreLessThanGreen);

            //Проверка что индексы всех зелёных объектов меньше индексов синих
            bool allGreenAreLessThanBlue = true;

            if (indexesOfAllGreenObjects.Any() && indexesOfAllBlueObjects.Any())
                allGreenAreLessThanBlue = indexesOfAllGreenObjects.All(x => !indexesOfAllBlueObjects.Any(y => y <= x));

            Assert.IsTrue(allGreenAreLessThanBlue);

            //Проверка что индексы всех красных объектов меньше индексов синих
            bool allRedAreLessThanBlue = true;

            if (indexesOfAllRedObjects.Any() && indexesOfAllBlueObjects.Any())
                allRedAreLessThanBlue = indexesOfAllRedObjects.All(x => !indexesOfAllBlueObjects.Any(y => y <= x));

            Assert.IsTrue(allRedAreLessThanBlue);
        }

        #endregion Public Methods
    }
}