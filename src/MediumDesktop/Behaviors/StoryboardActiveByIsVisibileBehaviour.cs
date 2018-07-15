using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace MediumDesktop.Behaviors
{
    public sealed class StoryboardActiveByIsVisibileBehaviour : IDisposable
    {
        private FrameworkElement _storyboardOwner;
        private Storyboard _storyboard;

        public StoryboardActiveByIsVisibileBehaviour(FrameworkElement storyboardOwner, string storyboardResourceKey)
        {
            if (string.IsNullOrWhiteSpace(storyboardResourceKey)) throw new ArgumentNullException(nameof(storyboardResourceKey));

            _storyboardOwner = storyboardOwner ?? throw new ArgumentNullException(nameof(storyboardOwner));
            _storyboard = storyboardOwner.Resources[storyboardResourceKey] as Storyboard;

            Debug.Assert(_storyboard != null, "_storyboard != null");

            _storyboardOwner.IsVisibleChanged += StoryboardOwnerOnIsVisibleChanged;
        }

        private void StoryboardOwnerOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var isVisible = (bool)args.NewValue;
            if (isVisible)
            {
                _storyboard.Begin();
            }
            else
            {
                _storyboard.Stop();
            }
        }

        public void Dispose()
        {
            if (_storyboardOwner != null)
            {
                _storyboardOwner.IsVisibleChanged -= StoryboardOwnerOnIsVisibleChanged;
                _storyboardOwner = null;
            }
            _storyboard = null;
        }
    }

}
