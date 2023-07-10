using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NewNoteViewModel : BaseViewModel
    {
        private int id;
        private string text;
        private DateTime date;

        public NewNoteViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            //this.PropertyChanged +=
            //    (_, __) => SaveCommand.ChangeCanExecute();
        }

        /*
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
        */
        public int ID
        {
            get => id;
            set => SetProperty(ref id, value);
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Note newItem = new Note()
            {
                ID = 0,
                Text = Text,
                Date = Date
            };
            //NoteDataStore ds = null;
            //await ds.SaveNoteAsync(newItem);
            await App.Database.SaveNoteAsync(newItem);
            //NoteDataStore.SaveNoteAsync(newItem);

            //var d = App.Database.GetItemsAsync(false);
            //foreach (var note in d)
            //{
            //    Debug.WriteLine(note.Text);
            //}
            // This will pop the current page off the navigation stack
            
            await Shell.Current.GoToAsync("..");
        }
    }
}
