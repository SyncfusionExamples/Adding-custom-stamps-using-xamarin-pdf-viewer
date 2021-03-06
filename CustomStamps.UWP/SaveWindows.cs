﻿#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomStamps.UWP.SaveWindows))]
namespace CustomStamps.UWP
{
    public class SaveWindows : ISave
    {
        MemoryStream stream;
        StorageFolder folder;
        StorageFile file;
        public string Save(MemoryStream documentStream)
        {
            documentStream.Position = 0;
            stream = documentStream;
            SaveAsync();
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, "SavedDocument.pdf");
        }

        private async void SaveAsync()
        {
            folder = ApplicationData.Current.LocalFolder;
            file = await folder.CreateFileAsync("SavedDocument.pdf", CreationCollisionOption.ReplaceExisting);
            if (file != null)
            {
                Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                st.Flush();
                st.Dispose();
                fileStream.Dispose();
            }
        }
    }
}
