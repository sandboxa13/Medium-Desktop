using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace MediumDesktop.Animations
{
    public static class StoryBoardHelpers
    {
        public static void SlideFromRight(this Storyboard storyboard, float sec, double offset, float decRatio = 0.9f)
        {
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(sec)),
                From = new Thickness(offset + 100, 0, -offset + 100, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.9f,
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        public static void SLideFromLeft(this Storyboard storyboard, float sec, double offset, float decRatio = 0.9f)
        {
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(sec)),
                From = new Thickness(-offset, 0, offset + 1000, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.1f,
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }
    }
}
