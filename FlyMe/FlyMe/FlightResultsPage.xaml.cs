﻿using FlyMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace FlyMe
{
    public partial class FlightResultsPage : ContentPage
    {
        public FlightResultsPage()
        {
            InitializeComponent();

            Shell.SetSearchHandler(this, new FeedSearchHandler(vm));
        }
    }

    public class FeedSearchHandler : SearchHandler
    {
        FlightResultsViewModel _vm;
        public FeedSearchHandler(ViewModels.FlightResultsViewModel vm)
        {
            SearchBoxVisibility = SearchBoxVisibility.Expanded;
            IsSearchEnabled = true;
            ShowsResults = false;
            Placeholder = "Find a Flight";
            CancelButtonColor = Color.DarkBlue;
            TextColor = Color.DarkBlue;

            _vm = vm;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var results = new List<string>();
                results = _vm.Flights.Where(x => x.Price.IndexOf(newValue, StringComparison.InvariantCultureIgnoreCase) > -1).Select(x => $"{x.From} to {x.To} - {x.Price}").ToList<string>();
                ItemsSource = results;
            }
        }
    }
}
