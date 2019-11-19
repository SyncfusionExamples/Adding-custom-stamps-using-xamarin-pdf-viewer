using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomStamps
{
  public  class FontMappingHelper
    {
        public static string Save = Device.RuntimePlatform == Device.Android ? "\uE70a" : Device.RuntimePlatform == Device.iOS ? "\uE718" : "\uE705";
        public static string Delete = Device.RuntimePlatform == Device.Android ? "\uE71e" : Device.RuntimePlatform == Device.iOS ? "\uE714" : "\uE718";
        public static string Stamp = Device.RuntimePlatform == Device.Android ? "\uE703" : Device.RuntimePlatform == Device.iOS ? "\uE701" : "\uE705";
        public static string SearchBack = Device.RuntimePlatform == Device.Android ? "\uE713" : Device.RuntimePlatform == Device.iOS ? "\uE71b" : "\uE702";

        public static string TextPath =

      Device.RuntimePlatform == Device.Android ?
          "PdfViewer_Text_font.ttf" :

          Device.RuntimePlatform == Device.iOS ?

          "PdfViewer_Text_font" :

          "/Assets/Fonts/PdfViewer_Text_font.ttf#PdfViewer_Text_font";

        public static string FontFamily =
            Device.RuntimePlatform == Device.Android ?

            "Final_PDFViewer_Android_FontUpdate.ttf" :

            Device.RuntimePlatform == Device.iOS ?

            "Final_PDFViewer_IOS_FontUpdate" :

            "/Assets/Fonts/Final_PDFViewer_UWP_FontUpdate.ttf#Final_PDFViewer_UWP_FontUpdate";

        public static string BookmarkFont =
            Device.RuntimePlatform == Device.Android ?

            "PdfViewer_FONT.ttf" :

            Device.RuntimePlatform == Device.iOS ?

            "PdfViewer_FONT" :

            "/Assets/Fonts/PdfViewer_FONT.ttf#PdfViewer_FONT";

        public static string SignatureFont =
         Device.RuntimePlatform == Device.Android ?

         "Signature_PDFViewer_FONT.ttf" :

         Device.RuntimePlatform == Device.iOS ?

         "Signature_PDFViewer_FONT" :

         "/Assets/Fonts/Signature_PDFViewer_FONT.ttf#Signature_PDFViewer_FONT";

        public static string StampFont =
         Device.RuntimePlatform == Device.Android ?

         "Font Text edit stamp.ttf" :

         Device.RuntimePlatform == Device.iOS ?

         "Font Text edit stamp" :

         "/Assets/Fonts/Font Text edit stamp.ttf#Font Text edit stamp";
    }
}
