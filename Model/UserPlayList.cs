using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace Team1MusicPlayer.Model
{
    public class UserPlayList
    {
        public async System.Threading.Tasks.Task AddMedia(ListView listView, MediaPlayerElement mediaPlayerElement)
        {

            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();

            filePicker.FileTypeFilter.Add(".mp3");

            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            MediaPlaybackList mediaPlaybackList = new MediaPlaybackList();

            var pickedFiles = await filePicker.PickMultipleFilesAsync();

            foreach (var file in pickedFiles)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
                mediaPlaybackList.Items.Add(mediaPlaybackItem);
                listView.Items.Add(file.DisplayName);
            }

            mediaPlayerElement.Source = mediaPlaybackList;
        }
    }
}
