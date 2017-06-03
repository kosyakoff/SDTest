using SDTest.Common;
using SDTest.Utility;
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
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly int _numberOfInputElements;
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

            PropertyChanged += new PropertyChangedEventHandler(SelectedColorChangedListener);

            _numberOfInputElements = 25;

            GenerateUnsortListCommand = new RelayCommand((obj) => GenerateUnortList(), (obj) => true);
            SortListCommand = new RelayCommand((obj) => SortList(), (obj) => { return InputObjectList != null && InputObjectList.Any(); });
        }

        private void SelectedColorChangedListener(object sender, PropertyChangedEventArgs e)
        {
            List<String> colorPropNames = new List<string>() { nameof(SelectedColor1), nameof(SelectedColor2), nameof(SelectedColor3) };
            if (colorPropNames.Contains(e.PropertyName))
            {
                GetSelectedColors();
            }
            
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public ICommand GenerateUnsortListCommand { get; set; }
        public ObservableCollection<IInputObject> InputObjectList { get; set; } = new ObservableCollection<IInputObject>();
        public List<IInputObject> OrderedList { get; set; } = new List<IInputObject>();

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

        private void LogError(string message)
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

        private void GetSelectedColors()
        {
            _selectedColorList = new List<Color> { SelectedColor1, SelectedColor2, SelectedColor3 }.Distinct().ToList();
        }

        private void GenerateUnortList()
        {
            GetSelectedColors();
            InputObjectList.Clear();
            try
            {
                foreach (var inpObj in InputDataManager.GenerateUnsortedList(_selectedColorList, _numberOfInputElements))
                {
                    InputObjectList.Add(inpObj);
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                InputObjectList.Clear();
            }
        }

        private void SortList()
        {
            try
            {
                OrderedList = InputDataManager.SortList(InputObjectList, _selectedColorList).ToList();
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                if (OrderedList != null)
                    OrderedList.Clear();
            }

            OnPropertyChanged(nameof(OrderedList));
        }

        #endregion Private Methods
    }
}