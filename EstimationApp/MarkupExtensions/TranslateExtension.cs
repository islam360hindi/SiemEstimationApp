using System;
using EstimationApp.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstimationApp.MarkupExtensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (Text == null)
                    return string.Empty;
                var translation = AppResources.ResourceManager.GetString(Text);
                if (translation == null)
                {
                    translation = Text;
                }
                return translation;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
