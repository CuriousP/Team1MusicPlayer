﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Team1MusicPlayer.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shell32;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Team1MusicPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> songs;

        public MainPage()
        {
            this.InitializeComponent();
            songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(songs);

           
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SongManager.GetAllSongs(songs);
        }
        private void SongListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;

            Uri pathUri = new Uri("ms-appx:///Assets/AudioFile/" + song.AudioFile);
            SongPlayer.Source = MediaSource.CreateFromUri(pathUri);
            


        }
        private void AlbumListView_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }
        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            SongManager.SearchSongByName(songs, mySearchBox.QueryText);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            SongManager.SearchSongByName(songs, mySearchBox.QueryText);
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "alai");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "AEM");
        }
    }
}
