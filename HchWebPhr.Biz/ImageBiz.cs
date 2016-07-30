using HchWebPhr.Biz.Base;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Service;
using HchWebPhr.Utilities.Extensions;
using HchWebPhr.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Biz
{
    public class ImageBiz : BaseBiz
    {
        public bool GetAlbums(string ChartNo, DateTime StartDate, DateTime EndDate, out IList<PhotoAlbumInfo> Albums)
        {
#if DEBUG
            IDictionary<string, PhotoAlbumInfo> DictAlbums = null;
#else
            IDictionary<string, PhotoAlbumInfo> DictAlbums = CacheHelper.GetCache("ALBUMS_" + ChartNo) as IDictionary<string, PhotoAlbumInfo>;
#endif
            if (DictAlbums == null) { DictAlbums = new Dictionary<string, PhotoAlbumInfo>(); }
            Albums = new List<PhotoAlbumInfo>();
            HchService svc = new HchService();
            var albumList = svc.GetAlbumListByChartNoAndDateRange(ChartNo, StartDate, EndDate);
            if (albumList != null && albumList.Count > 0)
            {
                bool updDictionary = false;
                foreach (var album in albumList)
                {
                    if (album.IsImgArrive == false) { continue; }

                    PhotoAlbumInfo newAlbum = null;

                    if (DictAlbums.ContainsKey(album.ApplyNo) == false)
                    {
                        var tmpAlbum = new PhotoAlbumInfo
                        {
                            ApplyNo = album.ApplyNo,
                            ExamDate = album.ExamDate.toDateTime(),
                            ExamCode = album.ExamCode,
                            ExamName = album.ExamName,
                            ExamType = album.ExamType,
                            ExamTypeDesc = album.TypeDesc,
                            HasImage = album.IsImgArrive,
                        };
                        updDictionary = true;
                        //DictAlbums[album.ApplyNo] = tmpAlbum;
                        newAlbum = tmpAlbum;
                    } else
                    {
                        newAlbum = DictAlbums[album.ApplyNo];
                    }
                    IList<PhotoInfo> photoList = null;
                    if (this.GetPhotos(ChartNo, newAlbum.ApplyNo, out photoList))
                    {
                        if (photoList != null)
                        {
                            if (newAlbum.Photos == null || newAlbum.Photos.Count != photoList.Count)
                            {
                                newAlbum.Photos = photoList;
                                newAlbum.ThumbnailImage = string.Format("~/Content/img/albums/{0}/{1}",
                                                                        photoList.FirstOrDefault().FileFolder.Trim('/'),
                                                                        photoList.FirstOrDefault().FileName.Trim('/'));
                                updDictionary = true;
                            }
                        }
                    }
                    Albums.Add(newAlbum);
                    DictAlbums[album.ApplyNo] = newAlbum;
                }
                if (updDictionary) { CacheHelper.SetCache("ALBUMS_" + ChartNo, DictAlbums); }
                //Albums = DictAlbums.Values.OrderByDescending(x => x.ExamDate).ToList();
                Albums = Albums.OrderByDescending(x => x.ExamDate).ToList();
                return true;
            }
            else
            {
                ErrorCode = svc.ErrorCode;
                ErrorMessage = svc.ErrorMessage;
                return false;
            }
        }

        public bool GetAlbum(string ChartNo, string ApplyNo, out PhotoAlbumInfo Album)
        {
            bool updDictionary = false;
            Album = null;
            if (string.IsNullOrEmpty(ChartNo) || string.IsNullOrEmpty(ApplyNo))
            {
                ErrorCode = "500";
                ErrorMessage = "找不到相簿或者相簿編號錯誤!";
                return false;
            }
#if DEBUG
            IDictionary<string, PhotoAlbumInfo> DictAlbums = null;
#else
            IDictionary<string, PhotoAlbumInfo> DictAlbums = CacheHelper.GetCache("ALBUMS_" + ChartNo) as IDictionary<string, PhotoAlbumInfo>;
#endif
            if (DictAlbums == null)
            {
                DictAlbums = new Dictionary<string, PhotoAlbumInfo>();
            }
            if (DictAlbums.ContainsKey(ApplyNo) == false)
            {
                HchService svc = new HchService();
                var albumList = svc.GetAlbumByApplyNo(ApplyNo);
                if (albumList != null && albumList.Count > 0)
                {
                    if (albumList[0].IsImgArrive == false)
                    {
                        ErrorCode = "404";
                        ErrorMessage = "找不到相簿！";
                        return false;
                    }
                    Album = new PhotoAlbumInfo
                    {
                        ApplyNo = albumList[0].ApplyNo,
                        ExamDate = albumList[0].ExamDate.toDateTime(),
                        ExamCode = albumList[0].ExamCode,
                        ExamName = albumList[0].ExamName,
                        ExamType = albumList[0].ExamType,
                        ExamTypeDesc = albumList[0].TypeDesc,
                        HasImage = albumList[0].IsImgArrive
                    };
                    updDictionary = true;
                } else
                {
                    ErrorCode = "404";
                    ErrorMessage = "找不到相簿！";
                    return false;
                }
            } else
            {
                Album = DictAlbums[ApplyNo];
            }
            IList<PhotoInfo> photoList = null;
            if (Album.HasImage)
            {
                if (this.GetPhotos(ChartNo, ApplyNo, out photoList))
                {
                    if (Album.Photos == null || Album.Photos.Count != photoList.Count)
                    {
                        Album.Photos = photoList;
                        Album.ThumbnailImage = string.Format("~/Content/img/albums/{0}/{1}",
                                                             photoList.FirstOrDefault().FileFolder.Trim('/'),
                                                             photoList.FirstOrDefault().FileName.Trim('/'));
                        updDictionary = true;
                    }
                }
            }
            DictAlbums[ApplyNo] = Album;
            if (updDictionary) { CacheHelper.SetCache("ALBUMS_" + ChartNo, DictAlbums); }
            return true;
        }
        public bool GetPhotos(string ChartNo, string ApplyNo, out IList<PhotoInfo> Photos)
        {
#if DEBUG
            Photos = null;
#else
            Photos = CacheHelper.GetCache("PHOTOS_" + ChartNo + "_" + ApplyNo) as IList<PhotoInfo>;
#endif
            if (Photos == null)
            {
                HchService svc = new HchService();
                var photoList = svc.GetPhotosByApplyNo(ApplyNo);
                if (photoList != null && photoList.Count > 0)
                {
                    Photos = photoList.Select(x => new PhotoInfo
                    {
                        FileName = x.FileName,
                        FileFolder = x.Folder
                    }).OrderBy(x => x.FileFolder).ThenBy(x => x.FileName).ToList();

                    CacheHelper.SetCache("PHOTOS_" + ChartNo + "_" + ApplyNo, Photos);
                    return true;
                }
                else
                {
                    ErrorCode = svc.ErrorCode;
                    ErrorMessage = svc.ErrorMessage;
                    return false;
                }
            } else
            {
                return true;
            }
        }
    }
}
