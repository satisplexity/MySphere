using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Main;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
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
            if (_currentIndex <= 0)
                    return;

                _currentIndex--;

                foreach (var preview in Previews)
                {
                preview.Index = preview.Index - 1;
                }

        });
    }

    public void SetPreview(BitmapSource fullSizePreview, BitmapSource smallPreview, ViewModelBase viewModel)
    {
        CurrentPreview = fullSizePreview;

        SpacePreview preview = Contain(viewModel);

        if (preview is null)
        {
            preview = new SpacePreview(smallPreview, viewModel);
            Previews.Insert(0, preview);
            RecalculateIndexes();
        }
        else
        {
            preview.Preview = smallPreview;

            Previews.Remove(preview);
            Previews.Insert(0, preview);
            RecalculateIndexes();
        }
    }

    private void RecalculateIndexes()
    {
        for(int index = 0; index < Previews.Count; index++)
        {
            Previews[index].Index = index * -1;
        }
    }

    private SpacePreview Contain(ViewModelBase viewModel)
    {
        Debug.WriteLine($"Search ViewModel {viewModel.Id}");

        foreach(var preview in Previews)
        {
            if(preview.ViewModel.Id  == viewModel.Id)
            {
                return preview;
            }
        }

        return null!;
    }

    public void AnimateTransitionIn()
    {
        PlayOpenAnimation = true;
        OnPropertyChanged(nameof(PlayOpenAnimation));

        TimerFactory.Run(() =>
        {
            PreviewVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(PreviewVisibility));
        }, 0.5);

        OnPropertyChanged(nameof(Previews));
    }

    public void AnimateTransitionOut()
    {
        CurrentPreview = Previews[_currentIndex].Preview;
        OnPropertyChanged(nameof(CurrentPreview));

        PreviewVisibility = Visibility.Visible;
        OnPropertyChanged(nameof(PreviewVisibility));

        PlayOpenAnimation = false;
        OnPropertyChanged(nameof(PlayOpenAnimation));

        ViewModelBase vm = Previews[_currentIndex].ViewModel;

        _currentIndex = 0;
        RecalculateIndexes();

        TimerFactory.Run(() =>
        {
            _serviceProvider.GetRequiredService<MainViewModel>().Navigation.ReplaceWith(vm);
        }, 0.5);
    }
}