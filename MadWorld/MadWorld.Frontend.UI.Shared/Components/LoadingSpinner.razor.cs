using System.Timers;
using Timer = System.Timers.Timer;

namespace MadWorld.Frontend.UI.Shared.Components;

public partial class LoadingSpinner
{
    private readonly Timer _timer = new();
    private bool ShowSlowLoading { get; set; }
    protected override void OnInitialized()
    {
        _timer.Elapsed += EnableSlowLoadingMessage!;
        _timer.Interval = 500;
        _timer.Enabled = true;
        
        base.OnInitialized();
    }

    private void EnableSlowLoadingMessage(object source, ElapsedEventArgs e)
    {
        ShowSlowLoading = true;
        _timer.Enabled = false;
        StateHasChanged();
    }
}