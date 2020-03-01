using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Team1MusicPlayer.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + "MusicIcon.png", UriKind.RelativeOrAbsolute));
            SongManager.GetAllSongs(songs);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            songs.Clear();
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + "MusicIcon.png", UriKind.RelativeOrAbsolute));
            SongManager.GetAllSongs(songs);
            SongTextBlock.Text = "All Songs";
        }

        private void SongListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            Uri pathUri = new Uri("ms-appx:///Assets/AudioFile/" + song.AudioFile);
            SongPlayer.Source = MediaSource.CreateFromUri(pathUri);
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + song.Album.ImageFile, UriKind.RelativeOrAbsolute));

        }
        private void AlbumListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Album)
            {
                var image = (Album)e.ClickedItem;
                Uri pathUri = new Uri("ms-appx:///Assets/ImageFile/" + image.AlbumName);
                SongPlayer.Source = MediaSource.CreateFromUri(pathUri);
            }
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

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            //find out if song exists in fav list
            Song favSong = (Song)button.DataContext;
            //Song existingSong =SongManager.favoriteSongs.FirstOrDefault(s => s.AudioFile.Equals(favSong.AudioFile));

            //if (existingSong == null)
            //{
            //    SongManager.AddFavoriteSong(favSong);
            //}
            //else
            //{
            //    SongManager.RemoveFavoriteSong(favSong);                                
            //}
            SongManager.AddFavoriteSong(favSong);
            if (SongTextBlock.Text == "Favorite Songs")
                SongManager.GetFavoriteSongs(songs);
            else if (SongTextBlock.Text == "All Songs")
                SongManager.GetAllSongs(songs);


        }
        private void Album1Button_Click(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "alai");
            SongTextBlock.Text = "Album1 Songs";
        }

        private void Album2Button_Click(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "AEM");
            SongTextBlock.Text = "Album2 Songs";
        }

        private void FavoritePlaylist_Click(object sender, RoutedEventArgs e)
        {
            songs.Clear();
            SongManager.GetFavoriteSongs(songs);
            SongTextBlock.Text = "Favorite Songs";

        }
        private void RemoveFavButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Song favSong = (Song)button.DataContext;
            songs.Clear();
            SongManager.RemoveFavoriteSong(favSong);
            if (SongTextBlock.Text == "Favorite Songs")
                SongManager.GetFavoriteSongs(songs);
            else if (SongTextBlock.Text == "All Songs")
                SongManager.GetAllSongs(songs);
        }

        private async void AddSongs_Click(object sender, RoutedEventArgs e)
        {
            var userPlayList = new UserPlayList();
            await userPlayList.AddMedia(AddPlaylist, SongPlayer);
        }

        private void DeleteSongs_Click(object sender, RoutedEventArgs e)
        {
            AddPlaylist.Items.Remove(AddPlaylist.SelectedItem);
        }

        private async void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);
                MyImage.Source = image;
            }
        }

        private  void AddPlaylist_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }
    }
}
