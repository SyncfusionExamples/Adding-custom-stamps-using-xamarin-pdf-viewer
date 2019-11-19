using System.IO;
using Xamarin.Forms;

namespace CustomStamps
{
    public interface CustomImage
    {
        void ConvertToImage(Grid view);
        Stream GetStream();
    }
}