using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NoteDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private DateTime date;
        public int Id { get; set; }

        public NoteDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public Command DeleteCommand { get; }

        private async void OnDelete()
        {
            Note newItem = new Note()
            {
                ID = ItemId,
                Text = Text,
                Date = Date
            };

            await App.Database.DeleteNoteAsync(newItem);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                //INoteDataStore note = null;

                var item = await App.Database.GetNoteAsync(itemId);
                Id = item.ID;
                Text = item.Text;
                date = item.Date;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
