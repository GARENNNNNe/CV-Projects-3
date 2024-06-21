using Microsoft.Maui.Controls;
using System;

namespace GolfScorecard
{
    public partial class MainPage : ContentPage
    {
        private readonly ScorecardViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new ScorecardViewModel();
            BindingContext = _viewModel;
        }

        private void OnCalculateTotalClicked(object sender, EventArgs e)
        {
            // Force update the TotalScore by notifying property changed
            _viewModel.UpdateTotalScore();
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            _viewModel.ResetScores();
        }
    }
}
