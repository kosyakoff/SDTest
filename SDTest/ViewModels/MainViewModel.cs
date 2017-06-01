using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SDTest.ViewModels
{
    public class InputDataManager
    {
        #region Public Methods

        internal IEnumerable<InputObject> GenerateUnsortedList(List<Color> selectedColorsList, int numberOfResultingInputObjects)
        {
            var randomIndex = new Random();
            int colorCount = selectedColorsList.Count;
            List<InputObject> inputObjectList = new List<InputObject>();

            for (int i = 0; i < numberOfResultingInputObjects; i++)
            {
                var randomColor = selectedColorsList.ElementAt(randomIndex.Next(colorCount));
                yield return new InputObject(randomColor);
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal List<InputObject> SortList(ObservableCollection<InputObject> inputObjectList, List<Color> selectedColorList)
        {
            var comparer = new InputObjectComparer(selectedColorList);
            var OrderedList = new List<InputObject>(inputObjectList);
            OrderedList.Sort(comparer);

            return OrderedList;
        }

        #endregion Internal Methods
    }

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

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly int _numberOfInputElements;
        private InputDataManager _inputDataManager;
        private Color _selectedColor1;

        private Color _selectedColor2;

        private Color _selectedColor3;
        private List<Color> _selectedColorList = new List<Color>();

        #endregion Private Fields

        #region Public Constructors

        public MainViewModel()
        {
            SourceColors.Add(SelectedColor1 = Colors.Red);
            SourceColors.Add(SelectedColor2 = Colors.Green);
            SourceColors.Add(SelectedColor3 = Colors.Blue);

            _numberOfInputElements = 25;
            _inputDataManager = new InputDataManager();

            GenerateUnsortListCommand = new RelayCommand((obj) => GeneratesUnortList(), (obj) => true);
            SortListCommand = new RelayCommand((obj) => SortList(), (obj) => { return InputObjectList != null && InputObjectList.Any(); });
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public ICommand GenerateUnsortListCommand { get; set; }
        public ObservableCollection<InputObject> InputObjectList { get; set; } = new ObservableCollection<InputObject>();
        public List<InputObject> OrderedList { get; set; } = new List<InputObject>();

        public Color SelectedColor1
        {
            get { return _selectedColor1; }
            set
            {
                _selectedColor1 = value;
                OnPropertyChanged(nameof(SelectedColor1));
            }
        }

        public Color SelectedColor2
        {
            get { return _selectedColor2; }
            set
            {
                _selectedColor2 = value;
                OnPropertyChanged(nameof(SelectedColor2));
            }
        }

        public Color SelectedColor3
        {
            get { return _selectedColor3; }
            set
            {
                _selectedColor3 = value;
                OnPropertyChanged(nameof(SelectedColor3));
            }
        }

        public ICommand SortListCommand { get; set; }
        public ObservableCollection<Color> SourceColors { get; set; } = new ObservableCollection<Color>();

        #endregion Public Properties

        #region Internal Methods

        internal void LogError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void GeneratesUnortList()
        {
            _selectedColorList = new List<Color> { SelectedColor1, SelectedColor2, SelectedColor3 }.Distinct().ToList();
            InputObjectList.Clear();
            try
            {
                foreach (var inpObj in _inputDataManager.GenerateUnsortedList(_selectedColorList, _numberOfInputElements))
                {
                    InputObjectList.Add(inpObj);
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        private void SortList()
        {
            try
            {
                OrderedList = _inputDataManager.SortList(InputObjectList, _selectedColorList);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }

            OnPropertyChanged(nameof(OrderedList));
        }

        #endregion Private Methods

        #region Public Classes

        public class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }

        #endregion Public Classes
    }
}