using System.IO;
using System.Drawing.Imaging;

namespace SVG_from_Script
{
    public class ImageFileTypes
    {
        public struct ImageFileType
        {
            public string Name;
            public string FileExtension;
            public bool SupportsTransparentBackground;
            public bool IsVectorFormat;
            public ImageFormat ImgFormat;

            public string FileDialogFilter
            {
                get
                {
                    string fileext = FileExtension;

                    if (!fileext.StartsWith("*."))
                        fileext = "*." + fileext;

                    return Name + "|" + fileext;
                }
            }

            public ImageFileType(string Name, string FileExtension, ImageFormat ImgFormat)
            {
                this.Name = Name;
                this.FileExtension = FileExtension;
                this.ImgFormat = ImgFormat;
                SupportsTransparentBackground = false;
                IsVectorFormat = false;
            }

            public ImageFileType(string Name, string FileExtension, ImageFormat ImgFormat, bool SupportsTransparentBackground)
            {
                this.Name = Name;
                this.FileExtension = FileExtension;
                this.ImgFormat = ImgFormat;
                this.SupportsTransparentBackground = SupportsTransparentBackground;
                IsVectorFormat = false;
            }

            public ImageFileType(string Name, string FileExtension, ImageFormat ImgFormat, bool SupportsTransparentBackground, bool IsVectorFormat)
            {
                this.Name = Name;
                this.FileExtension = FileExtension;
                this.ImgFormat = ImgFormat;
                this.SupportsTransparentBackground = SupportsTransparentBackground;
                this.IsVectorFormat = IsVectorFormat;
            }
        }

        public static ImageFileType Bmp
        {
            get { return new ImageFileType("Bitmap", "BMP", ImageFormat.Bmp); }
        }

        public static ImageFileType Emf
        {
            get { return new ImageFileType("Enhanced Metafile", "EMF", ImageFormat.Emf); }
        }

        public static ImageFileType Gif
        {
            get { return new ImageFileType("Graphics Interchange Format", "Gif", ImageFormat.Gif, true); }
        }

        public static ImageFileType Jpeg
        {
            get { return new ImageFileType("Joint Photographic Expert Group", "JPG", ImageFormat.Jpeg); }
        }

        public static ImageFileType Png
        {
            get { return new ImageFileType("Portable Network Graphics", "PNG", ImageFormat.Png, true); }
        }

        public static ImageFileType Svg
        {
            get { return new ImageFileType("Scalable Vector Graphics", "SVG", null, true, true); }
        }

        public static ImageFileType Tiff
        {
            get { return new ImageFileType("Tagged Image File Format", "TIF", ImageFormat.Tiff); }
        }

        public static ImageFileType Wmf
        {
            get { return new ImageFileType("Windows Metafile", "WMF", ImageFormat.Wmf); }
        }

        public static string FileDialogFilterAllImages
        {
            get
            {
                ImageFileType[] imgtypes = ImageTypes;
                string retval = "";

                foreach (ImageFileType imgTyp in imgtypes)
                {
                    if (retval.Length > 0)
                        retval += "|";

                    retval += imgTyp.FileDialogFilter;
                }

                return retval;
            }
        }

        public static string FileDialogFilterRasterImages
        {
            get
            {
                ImageFileType[] imgtypes = ImageTypes;
                string retval = "";

                foreach (ImageFileType imgTyp in imgtypes)
                {
                    if (imgTyp.IsVectorFormat)
                        continue;

                    if (retval.Length > 0)
                        retval += "|";

                    retval += imgTyp.FileDialogFilter;
                }

                return retval;
            }
        }

        public static string FileDialogFilterVectorImages
        {
            get
            {
                ImageFileType[] imgtypes = ImageTypes;
                string retval = "";

                foreach (ImageFileType imgTyp in imgtypes)
                {
                    if (!imgTyp.IsVectorFormat)
                        continue;

                    if (retval.Length > 0)
                        retval += "|";

                    retval += imgTyp.FileDialogFilter;
                }

                return retval;
            }
        }

        public static ImageFileType[] ImageTypes
        {
            get
            {
                return new ImageFileType[] { Bmp, Emf, Gif, Jpeg, Png, Svg, Tiff, Wmf };
            }
        }

        public static ImageFileType GetImageFileTypeFromFileName(string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                return new ImageFileType();

            string fileExtension = Path.GetExtension(FileName);

            switch (fileExtension.ToLower())
            {
                case ".bmp":
                    return Bmp;
                case ".emf":
                    return Emf;
                case ".gif":
                    return Gif;
                case ".jpg":
                case ".jpeg":
                    return Jpeg;
                case ".png":
                    return Png;
                case ".svg":
                    return Svg;
                case ".tif":
                case ".tiff":
                    return Tiff;
                case ".wmf":
                    return Wmf;
                default:
                    return new ImageFileType();
            }
        }
    }
}
