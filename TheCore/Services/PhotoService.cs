using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace TheCore.Services
{
    public class PhotoService
    {
        IPhotoRepository _repo;

        public PhotoService(IPhotoRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IPhoto> GetAllPhotos()
        {
            return _repo.FindAll();
        }

        public IPhoto GetPhoto(Guid id)
        {
            return _repo.FindByPhotoId(id);
        }

        public IList<IPhoto> GetPhotosByUserAndShow(Guid userId, Guid showId)
        {
            return _repo.FindAllByUserIdAndShowId(userId, showId);
        }

        public IQueryable<IPhoto> GetPhotosByShow(Guid showId)
        {
            return _repo.FindAll().Where(x => x.ShowId == showId);
        }

        public void SaveCommit(IPhoto photo, IImageFormatSpec mediaFormat, out bool success)
        {
            using (var unitOfWork = UnitOfWork.Begin())
            {
                Save(photo, mediaFormat, out success);
                if (success)
                {
                    unitOfWork.Commit();
                }
            }
        }

        //consider changing the out parameter to a validation type object
        //public void Save(IPhoto photo, out bool success)
        //{
        //    Checks.Argument.IsNotNull(photo, "photo");

        //    success = false;

        //    if (null == _repo.FindByPhotoId(photo.PhotoId))
        //    {
        //        try
        //        {
        //            _repo.Add(photo);
        //            success = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            success = false;
        //        }
        //    }
        //}

        public void Save(IPhoto photo, IImageFormatSpec mediaFormat, out bool success)
        {
            success = false;
            Checks.Argument.IsNotNull(photo, "photo");
            Checks.Argument.IsNotNull(mediaFormat, "mediaFormat");


            //ValidateTempImageStorage(tempImageStorage, out validationState);

            //if (!validationState.IsValid)
            //{
            //    return;
            //}

            if (null == this.GetPhoto(photo.PhotoId))
            {
                try
                {

                    _repo.Add(photo);
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

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(IPhoto photo)
        {
            Checks.Argument.IsNotNull(photo, "photo");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(photo);
                u.Commit();
            }
        }

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
    }
}
