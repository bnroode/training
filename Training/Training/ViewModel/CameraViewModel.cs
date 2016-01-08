using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using Training.View;
using Training.ViewModel;

namespace Training.ViewModel
{
    class CameraViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private IMediaPicker _mediaPicker;
        private ImageSource _imageSource;
        private Command _takePictureCommand;
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private string _status;

        public CameraViewModel()
        {
            Setup();
        }

        public string Status
        {
            get { return _status; }
            private set { SetProperty(ref _status, value); }
        }
        private Command _selectPictureCommand;
        public Command SelectPictureCommand
        {
            get
            {
                return _selectPictureCommand ?? (_selectPictureCommand = new Command(
                                                                           async () => await SelectPicture(),
                                                                           () => true));
            }
        }

        private async Task SelectPicture()
        {
            Setup();

            ImageSource = null;
            try
            {
                var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                ImageSource = ImageSource.FromStream(() => mediaFile.Source);
            }
            catch (System.Exception ex)
            {
                Status = ex.Message;
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);

            }
        }


        private void Setup()
        {
            if (_mediaPicker != null)
            {
                return;
            }

            var device = Resolver.Resolve<IDevice>();

            ////RM: hack for working on windows phone? 
            _mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
        }


        public Command TakePictureCommand
        {
            get
            {
                return _takePictureCommand ?? (_takePictureCommand = new Command(
                                                                       async () => await TakePicture(),
                                                                       () => true));
            }
        }

        private async Task<MediaFile> TakePicture()
        {
            Setup();

            ImageSource = null;

            return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
            {
                DefaultCamera = CameraDevice.Front,
                SaveMediaOnCapture = true,
                Directory = "Camera",
                Name = string.Format("Camera_{0}", DateTime.Now.ToString("yyMMddhhmmss")),
                MaxPixelDimension = 1024
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Status = t.Exception.InnerException.ToString();

                }
                else if (t.IsCanceled)
                {
                    Status = "Canceled";
                }
                else
                {
                    var mediaFile = t.Result;

                    ImageSource = ImageSource.FromStream(() => mediaFile.Source);

                    return mediaFile;
                }

                return null;
            }, _scheduler);
        }

    }


}
