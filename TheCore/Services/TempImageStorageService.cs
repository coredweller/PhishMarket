using System;
using TheCore.Helpers;
using TheCore.Infrastructure;
using TheCore.Interfaces;
using TheCore.Repository;
using System.IO;
using System.Runtime.InteropServices;

namespace TheCore.Services
{
    public class TempImageStorageService // : ITempImageStorageService
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(TempImageStorageService));

        //private readonly IDomainObjectFactory _domainObjectFactory;
        private readonly IPhotoRepository _tempImageStorageRepository;
        //private readonly IImageResizerService _imageResizerService;
        //private readonly IImageDiskService _imageDiskService;
        //private readonly IValidationService _validator;

        public TempImageStorageService(IPhotoRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");

            _tempImageStorageRepository = repo;
        }


        //public TempImageStorageService(IDomainObjectFactory domainObjectFactory, ITempImageStorageRepository tempImageStorageRepository, IImageResizerService imageResizerService, IImageDiskService imageDiskService, IValidationService validator)
        //{
        //    Checks.Argument.IsNotNull(domainObjectFactory, "domainObjectFactory");
        //    Checks.Argument.IsNotNull(tempImageStorageRepository, "tempImageStorageRepository");
        //    Checks.Argument.IsNotNull(imageResizerService, "imageResizerService");
        //    Checks.Argument.IsNotNull(imageDiskService, "imageDiskService");
        //    Checks.Argument.IsNotNull(validator, "validator");
        //    _domainObjectFactory = domainObjectFactory;
        //    _tempImageStorageRepository = tempImageStorageRepository;
        //    _imageResizerService = imageResizerService;
        //    _imageDiskService = imageDiskService;
        //    _validator = validator;
        //}

        public ITempImageStorage GetImage(Guid id)
        {
            return (ITempImageStorage)_tempImageStorageRepository.FindByPhotoId(id);
        }


        //NEED THIS ONE TO BE IMPLEMENTED
        //public IList<ITempImageStorage> GetImageByUserId(Guid userId)
        //{
        //    return _tempImageStorageRepository.FindByUserName(userName);
        //}


        //public IList<ITempImageStorage> GetOlderThan(DateTime date)
        //{
        //    return _tempImageStorageRepository.FindByOlderThan(date);
        //}

        #region ITempImageStorageService Members



        public void SaveCommit(ITempImageStorage tempImageStorage, IImageFormatSpec mediaFormat, out bool success)
        {
            using (var unitOfWork = UnitOfWork.Begin())
            {
                Save(tempImageStorage, mediaFormat, out success);
                if (success)
                {
                    unitOfWork.Commit();
                }
            }
        }

        public void Save(IPhoto tempImageStorage, IImageFormatSpec mediaFormat, out bool success)
        {
            success = false;
            Checks.Argument.IsNotNull(tempImageStorage, "tempImageStorage");
            Checks.Argument.IsNotNull(mediaFormat, "mediaFormat");
            Checks.Argument.IsNotNull(_tempImageStorageRepository, "_tempImageStorageRepository");


            //ValidateTempImageStorage(tempImageStorage, out validationState);

            //if (!validationState.IsValid)
            //{
            //    return;
            //}

            if (null == this.GetImage(tempImageStorage.PhotoId))
            {
                try
                {

                    _tempImageStorageRepository.Add(tempImageStorage);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                    //var valState = new ValidationState();
                    //valState.Errors.Add(new ValidationError("Id.AlreadyExists", tempImageStorage, ex.Message));
                    //validationState.Append(typeof(ITempImageStorage), valState);
                }
            }

        }



        public void DeleteCommit(ITempImageStorage tempImageStorage)
        {
            Checks.Argument.IsNotNull(tempImageStorage, "tempImageStorage");
            using (var unitOfWork = UnitOfWork.Begin())
            {
                _tempImageStorageRepository.Remove(tempImageStorage);

                unitOfWork.Commit();
            }

        }


        public void Delete(ITempImageStorage tempImageStorage)
        {

            //if not found, create a new entity for the thumbnail image
            if (tempImageStorage != null)
            {
                _tempImageStorageRepository.Remove(tempImageStorage);
            }
        }



        //public void ValidateTempImageStorage(ITempImageStorage tempImageStorage, out ValidationStateDictionary validationState)
        //{
        //    validationState = new ValidationStateDictionary();
        //    validationState.Add(typeof(ITempImageStorage), _validator.Validate(tempImageStorage));
        //}

        //public void ValidateTempImageStorage( ITempImageStorage tempImageStorage, out ValidationStateDictionary validationState ) {
        //    validationState = new ValidationStateDictionary();
        //    validationState.Add( typeof( ITempImageStorage ), _validator.Validate( tempImageStorage ) );
        //}

        public bool SaveAs(IPhoto tempImage, string filename)
        {
            Checks.Argument.IsNotEmpty(filename, "filename");
            var retVal = false;
            if (tempImage != null && tempImage.Image != null)
            {
                retVal = SaveAsMore(tempImage, filename);
            }
            return retVal;
        }

        public bool SaveAsMore(IPhoto imageBuffer, string filename)
        {

            var retVal = false;

            if (imageBuffer.Image == null) { return retVal; }

            using (var ms = new MemoryStream(imageBuffer.Image))
            {

                using (System.Drawing.Image FullsizeImage = System.Drawing.Image.FromStream(ms))
                {

                    try
                    {
                        FullsizeImage.Save(filename);
                        retVal = true;
                    }
                    catch (ExternalException ex)
                    {

                        //ERROR MESSAGE NEEDED
                        //log.Error("The was in interop error while trying to save an IImageStorage image to the following file name {0}.".FormatWith(filename), ex);

                    }
                    catch (Exception ex)
                    {
                        //ERROR MESSAGE NEEDED
                        //log.Error("The was in error while trying to save an IImageStorage image to the following file name {0}.".FormatWith(filename), ex);
                    }

                }

            }
            return retVal;
        }

        #endregion
    }
}
