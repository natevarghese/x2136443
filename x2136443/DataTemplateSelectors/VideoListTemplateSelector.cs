using System;
using x2136443.DataModels;
using Xamarin.Forms;

namespace x2136443.DataTemplateSelectors
{
    public class VideoListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SectionTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case Section _:
                    return SectionTemplate;
                case Video _:
                    return VideoTemplate;
            }

            return SectionTemplate;
        }
    }
}