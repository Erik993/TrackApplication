using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using TrackApplicationData.Models;

namespace TrackApplication
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            //MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}


//private readonly IServiceProvider _services;

//added IServiceProvider serviceProvider + serviceProvider.GetRequiredService<MainPage>()
/*
public App(IServiceProvider serviceProvider)
{
    //InitializeComponent();
    _services = serviceProvider;
    MainPage = new NavigationPage(serviceProvider.GetRequiredService<MainPage>());



}*/