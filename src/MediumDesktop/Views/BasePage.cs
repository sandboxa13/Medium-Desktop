using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MediumDesktop.Animations;

namespace MediumDesktop.Views
{
    public class BasePage : Page
    {
        #region Public Properties

        public AnimationsEnum PageLoad { get; set; } = AnimationsEnum.SlideFromRightToLeft;

        public AnimationsEnum PageUnload { get; set; } = AnimationsEnum.SlideFromleftToRight;

        public float Seconds { get; set; } = 0.5f;


        #endregion

        #region Constructor

        public BasePage()
        {
            if (PageLoad != AnimationsEnum.None)
                Visibility = Visibility.Collapsed;

            Loaded += BasePage_Loaded;
        }

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateFromLeft();
        }

        #endregion

        public async Task AnimateFromRight()
        {
            if (PageLoad == AnimationsEnum.None)
                return;
            switch (PageLoad)
            {
                case AnimationsEnum.SlideFromRightToLeft:

                    await this.SlideFromRighTask(Seconds);
                    break;
            }
        }

        public async Task AnimateFromLeft()
        {
            if (PageUnload == AnimationsEnum.None)
                return;
            switch (PageUnload)
            {
                case AnimationsEnum.SlideFromleftToRight:

                    await this.SlideFromLeftTask(Seconds);
                    break;

            }
        }
    }
}
