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
            SongManager.GetAllSongs(songs);
            //this.UpdateColorForFavoriteSons();
        }
        #region "Extra Code"
        //public  List<TChild> GetChildren<TChild>(this DependencyObject reference)
        // where TChild : class
        //{
        //    List<TChild> result = new List<TChild>();

        //    // enumerate all of the children of the supplied element searching for all the   
        //    // elements that match the supplied type  
        //    for (int x = 0; x < VisualTreeHelper.GetChildrenCount(reference); x++)
        //    {
        //        DependencyObject child = VisualTreeHelper.GetChild(reference, x);
        //        TChild tChildInstance = child as TChild;
        //        if (tChildInstance != null)
        //        {
        //            result.Add(tChildInstance);
        //        }

        //        // now repeat the process on all the children of the current child element  
        //        // by recursively calling this method  
        //        //result.AddRange(child.GetChildren<TChild>());
        //    }
        //    return result;
        //}
        private void UpdateColorForFavoriteSons()
        {
            var listofButtons = new List<Button>();
            //FindChildren(listofButtons, this);
            var t = TraverseCTFindShape<UIElement>(this, "abc");
            //var tControls = GetChildren<StackPanel>(this);
            foreach (Song s in songs)
            {
                Song existingSong = SongManager.favoriteSongs.FirstOrDefault(s1 => s1.SongName.Equals(s.SongName));
                if (existingSong != null)
                {

                }
            }
        }
        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
        where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        public static T TraverseCTFindShape<T>(DependencyObject root, String name) where T : Windows.UI.Xaml.UIElement
        {
            T control = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);

                string childTag = child.GetValue(FrameworkElement.TagProperty) as string;
                control = child as T;

                if (childTag == name)
                {
                    return control;
                }
                else
                {
                    control = TraverseCTFindShape<T>(child, name);

                    if (control != null)
                    {
                        return control;
                    }
                }
            }

            return control;
        }

        void FindTextBoxex(object uiElement, IList<Button> foundOnes)
        {
            if (uiElement is Button)
            {
                foundOnes.Add((Button)uiElement);
            }
            else if (uiElement is Panel)
            {
                var uiElementAsCollection = (Panel)uiElement;
                foreach (var element in uiElementAsCollection.Children)
                {
                    FindTextBoxex(element, foundOnes);
                }
            }
            else if (uiElement is StackPanel)
            {
                var uiElementAsUserControl = (UserControl)uiElement;
                FindTextBoxex(uiElementAsUserControl.Content, foundOnes);
            }
            else if (uiElement is ContentControl)
            {
                var uiElementAsContentControl = (ContentControl)uiElement;
                FindTextBoxex(uiElementAsContentControl.Content, foundOnes);
            }

        }
        #endregion

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            songs.Clear();
            SongManager.GetAllSongs(songs);
            SongTextBlock.Text = "All Songs";
         }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            songs.Clear();
            SongManager.GetAllSongs(songs);
            
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
            ////Windows.UI.Xaml.Media.SolidColorBrush sb = (Windows.UI.Xaml.Media.SolidColorBrush) button.Foreground;
            ////bool bAddSongToFav = false;
            ////if (sb.Color.R != 0)
            ////{
            ////    button.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
            ////}
            ////else
            ////{
            ////    button.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            ////    bAddSongToFav = true;
            ////}
            ////find out if song exists in fav list
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
    }
}
