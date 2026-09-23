using System.Windows.Threading;

namespace MySphere.Presentation.Utilities;

public class TimerFactory
{
    public static void Run(Action action, double seconds)
    {
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(seconds)
        };

        timer.Tick += new((_, _) =>
        {
            timer.Stop();

            action.Invoke();
        });

        timer.Start();
    }
}