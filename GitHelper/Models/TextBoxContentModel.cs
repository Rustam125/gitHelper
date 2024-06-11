using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GitHelper.Models
{
    public class TextBoxContentModel : INotifyPropertyChanged
    {
        private string _text;
        private int _linesCount;

        public TextBoxContentModel() { }

        public TextBoxContentModel(string text)
        {
            _text = text;
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    LinesCount = GetLinesCount(value);
                    OnPropertyChanged();
                }
            }
        }

        public bool HasValue => string.IsNullOrEmpty(_text) == false && string.IsNullOrWhiteSpace(_text) == false;

        public int LinesCount
        {
            get
            {
                return _linesCount;
            }
            private set
            {
                _linesCount = value;
            }
        }

        public string LinesCountText
        {
            get
            {
                return $"Кол-во строк: {LinesCount}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private int GetLinesCount(string str)
        {
            int index_r = -1, index_rn = -1;
            int count = 0;

            if (string.IsNullOrEmpty(str))
            {
                return count;
            }

            do
            {
                index_r = str.IndexOf("\r\n", index_r + 1);
                index_rn = str.IndexOf("\n", index_rn + 1);
                count++;
            }
            while (index_r != -1 ||
                   index_rn != -1);

            return count;
        }
    }
}