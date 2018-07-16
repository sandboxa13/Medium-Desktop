using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MediumDesktop.Animations
{
    public static class PageAnimationExtentions
    {
        public static async Task SlideFromRighTask(this Page page, float seconds)
        {
            var sb = new Storyboard();

            sb.SlideFromRight(seconds, page.WindowWidth);

            sb.Begin(page);

            page.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideFromLeftTask(this Page page, float seconds)
        {
            var sb = new Storyboard();

            sb.SLideFromLeft(seconds, page.WindowWidth);

            sb.Begin(page);

            page.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }
    }
}
