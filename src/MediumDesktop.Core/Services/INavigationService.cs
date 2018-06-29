﻿using System;
using System.Threading.Tasks;

namespace MediumDesktop.Core.Services
{   
    public interface INavigationService
    {   
        void NavigateAsync<TViewModel>() where TViewModel : class;

        IObservable<Type> Navigated { get; }
    }
}
