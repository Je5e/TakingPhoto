using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TakingPhoto
{
    public partial class MainPage : ContentPage
    {
        private readonly IMedia Media =
            DependencyService.Get<IMedia>();

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnTakePhotoButtonClicked
             (object sender, EventArgs args)
        {
            if (!Media.IsCameraAvailable
                || !Media.IsTakePhotoSupported)
            {
                await DisplayAlert
                     ("No Camera", "No camera available.", "Ok");
                return;
            }

            MediaFile file = await Media.TakePhotoAsync
                (new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
            if (file != null)
            {
                ImageSource imageSource =
                    ImageSource.FromStream
                    (() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                ImagePhoto.Source = imageSource;
                Debug.WriteLine
                    ($"Ruta del archivo: {file.Path}");
                file.Dispose();
            }
        }
    }
}
