using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Main;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.SpaceSwitcher;

public sealed class SpaceSwitcherViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<SpacePreview> Previews { get; private set; } = new();

    public RelayCommand NextCommand { get; }
    public RelayCommand PreviousCommand { get; }

    private BitmapSource _currentPreview;
    public BitmapSource CurrentPreview
    {
        get => _currentPreview;
        set
        {
            _currentPreview = value;
            PlayOpenAnimation = true;
            OnPropertyChanged(nameof(PlayOpenAnimation));

            TimerFactory.Run(() =>
            {
                PreviewVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(PreviewVisibility));
            }, 0.5);
        }
    }

    public Visibility PreviewVisibility { get; private set; } = Visibility.Visible;
    public bool PlayOpenAnimation { get; set; }
    private int _currentIndex = 0;
    public SpaceSwitcherViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        NextCommand = new(_ =>
        {
            Debug.WriteLine($"NextCommand executed. CurrentIndex: {_currentIndex}, Previews count: {Previews.Count}");

            if (_currentIndex >= Previews.Count - 1)
                    return;

                _currentIndex++;
                
                foreach (var preview in Previews)
                {
                preview.Index = preview.Index + 1;
                }
        });

        PreviousCommand = new(_ =>
        {
            Debug.WriteLine($"PreviousCommand executed. CurrentIndex: {_currentIndex}, Previews count: {Previews.Count}");

            if (_currentIndex <= 0)
                    return;

                _currentIndex--;

                foreach (var preview in Previews)
                {
                preview.Index = preview.Index - 1;
                }

        });
    }

    public void SetPreview(BitmapSource preview, ViewModelBase viewModel)
    {
        CurrentPreview = preview;
    }

    public void AnimateTransitionIn(BitmapSource preview, ViewModelBase viewModel)
    {
        CurrentPreview = preview;

        Previews.Add(new SpacePreview(preview, viewModel, Previews.Count * -1));

        OnPropertyChanged(nameof(Previews));


    }

    public void AnimateTransitionOut()
    {
        PreviewVisibility = Visibility.Visible;
        OnPropertyChanged(nameof(PreviewVisibility));

        PlayOpenAnimation = false;
        OnPropertyChanged(nameof(PlayOpenAnimation));

        ViewModelBase vm = Previews[_currentIndex].ViewModel;

        TimerFactory.Run(() =>
        {
            _serviceProvider.GetRequiredService<MainViewModel>().Navigation.ReplaceWith(vm);
        }, 0.5);
    }
}