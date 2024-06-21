using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GolfScorecard
{
    public class GolfHole : INotifyPropertyChanged
    {
        private int _holeNumber;
        private int _par;
        private int _score;
        private int _yards;
        private int _strokeIndex;

        public int HoleNumber
        {
            get => _holeNumber;
            set
            {
                if (_holeNumber != value)
                {
                    _holeNumber = value;
                    OnPropertyChanged(nameof(HoleNumber));
                }
            }
        }

        public int Par
        {
            get => _par;
            set
            {
                if (_par != value)
                {
                    _par = value;
                    OnPropertyChanged(nameof(Par));
                }
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        public int Yards
        {
            get => _yards;
            set
            {
                if (_yards != value)
                {
                    _yards = value;
                    OnPropertyChanged(nameof(Yards));
                }
            }
        }

        public int StrokeIndex
        {
            get => _strokeIndex;
            set
            {
                if (_strokeIndex != value)
                {
                    _strokeIndex = value;
                    OnPropertyChanged(nameof(StrokeIndex));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class ScorecardViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GolfHole> _golfHoles;
        public ObservableCollection<GolfHole> GolfHoles
        {
            get => _golfHoles;
            set
            {
                if (_golfHoles != value)
                {
                    _golfHoles = value;
                    OnPropertyChanged(nameof(GolfHoles));
                    OnPropertyChanged(nameof(TotalScore));
                    OnPropertyChanged(nameof(FrontNineScore));
                    OnPropertyChanged(nameof(BackNineScore));
                }
            }
        }

        public int TotalScore => GolfHoles?.Sum(hole => hole.Score) ?? 0;

        public int FrontNineScore => GolfHoles?.Where(hole => hole.HoleNumber <= 9).Sum(hole => hole.Score) ?? 0;

        public int BackNineScore => GolfHoles?.Where(hole => hole.HoleNumber > 9).Sum(hole => hole.Score) ?? 0;

        public ScorecardViewModel()
        {
            // Initialize with sample data
            GolfHoles = new ObservableCollection<GolfHole>
            {
                new GolfHole { HoleNumber = 1, Par = 3, Yards = 188, StrokeIndex = 10, Score = 0 },
                new GolfHole { HoleNumber = 2, Par = 4, Yards = 313, StrokeIndex = 18, Score = 0 },
                new GolfHole { HoleNumber = 3, Par = 5, Yards = 466, StrokeIndex = 12, Score = 0 },
                new GolfHole { HoleNumber = 4, Par = 4, Yards = 402, StrokeIndex = 8, Score = 0 },
                new GolfHole { HoleNumber = 5, Par = 5, Yards = 530, StrokeIndex = 2, Score = 0 },
                new GolfHole { HoleNumber = 6, Par = 4, Yards = 337, StrokeIndex = 16, Score = 0 },
                new GolfHole { HoleNumber = 7, Par = 4, Yards = 370, StrokeIndex = 14, Score = 0 },
                new GolfHole { HoleNumber = 8, Par = 4, Yards = 403, StrokeIndex = 6, Score = 0 },
                new GolfHole { HoleNumber = 9, Par = 4, Yards = 399, StrokeIndex = 4, Score = 0 },
                new GolfHole { HoleNumber = 10, Par = 4, Yards = 434, StrokeIndex = 5, Score = 0 },
                new GolfHole { HoleNumber = 11, Par = 3, Yards = 207, StrokeIndex = 11, Score = 0 },
                new GolfHole { HoleNumber = 12, Par = 4, Yards = 423, StrokeIndex = 1, Score = 0 },
                new GolfHole { HoleNumber = 13, Par = 4, Yards = 442, StrokeIndex = 3, Score = 0 },
                new GolfHole { HoleNumber = 14, Par = 3, Yards = 150, StrokeIndex = 15, Score = 0 },
                new GolfHole { HoleNumber = 15, Par = 4, Yards = 369, StrokeIndex = 7, Score = 0 },
                new GolfHole { HoleNumber = 16, Par = 3, Yards = 192, StrokeIndex = 13, Score = 0 },
                new GolfHole { HoleNumber = 17, Par = 5, Yards = 485, StrokeIndex = 17, Score = 0 },
                new GolfHole { HoleNumber = 18, Par = 5, Yards = 521, StrokeIndex = 9, Score = 0 },
            };
        }

        public void UpdateTotalScore()
        {
            OnPropertyChanged(nameof(TotalScore));
            OnPropertyChanged(nameof(FrontNineScore));
            OnPropertyChanged(nameof(BackNineScore));
        }
        public void ResetScores()
        {
            foreach (var hole in GolfHoles)
            {
                hole.Score = 0;
            }
            UpdateTotalScore();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
