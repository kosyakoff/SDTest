using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SDTest.ViewModels
{

    public class InputObjectComparer : IComparer<InputObject>
    {
        private List<Color> _colors = new List<Color>();
        public InputObjectComparer(List<Color> colors)
        {
            _colors = colors;
        }
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
    }

    public class InputObject
    {
        public Color InputColor { get; set; }
        public InputObject(Color color)
        {
            InputColor = color;
        }
    }
    public class MainViewModel : INotifyPropertyChanged
    {

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }
         
        private Color _selectedColor1;
        public Color SelectedColor1
        {
            get { return _selectedColor1; }
            set
            {
                _selectedColor1 = value;
                OnPropertyChanged("SelectedColor1");
            }
        }

        private Color _selectedColor2;
        public Color SelectedColor2
        {
            get { return _selectedColor2; }
            set
            {
                _selectedColor2 = value;
                OnPropertyChanged("SelectedColor2");
            }
        }

        private Color _selectedColor3;
        public Color SelectedColor3
        {
            get { return _selectedColor3; }
            set
            {
                _selectedColor3 = value;
                OnPropertyChanged("SelectedColor3");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GenerateUnsortListCommand { get; set; }
        public ICommand SortListCommand { get; set; }
        private List<Color> SelectedColorList = new List<Color>();
        public ObservableCollection<InputObject> InputObjectList { get; set; } = new ObservableCollection<InputObject>();
        public List<InputObject> OrderedList { get; set; } = new List<InputObject>();

        public MainViewModel()
        {

            GenerateUnsortListCommand = new CommandHandler(() => GeneratesUnortList(), true);
            SortListCommand = new CommandHandler(() => SortList(), true);
        }

        
        private void SortList()
        {
            OrderedList = new List<InputObject>(InputObjectList);
            OrderedList.Sort(new InputObjectComparer(SelectedColorList));
            OnPropertyChanged("OrderedList");
        }

        private void GeneratesUnortList()
        {
            SelectedColorList = new List<Color> { SelectedColor1, SelectedColor2, SelectedColor3 }.Distinct().ToList();
            var randomIndex = new Random();

            InputObjectList.Clear();
            for (int i = 0; i < 25; i++)
            {
                var randomColor = SelectedColorList.ElementAt(randomIndex.Next(SelectedColorList.Count));
                InputObjectList.Add(new InputObject(randomColor));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
