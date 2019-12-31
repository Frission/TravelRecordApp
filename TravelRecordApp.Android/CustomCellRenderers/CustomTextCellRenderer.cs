using Android.Content;
using Android.Views;
using System.ComponentModel;
using TravelRecordApp.CustomCells;
using TravelRecordApp.Droid.CustomCellRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TextCell), typeof(CustomTextCellRenderer))]
namespace TravelRecordApp.Droid.CustomCellRenderers
{
    public class CustomTextCellRenderer : TextCellRenderer
    {
        private Android.Views.View _cell;
        private bool _isSelected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cell = base.GetCellCore(item, convertView, parent, context);
            _cell.SetBackgroundColor(Android.Graphics.Color.Transparent);
            _isSelected = false;

            return _cell;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if (e.PropertyName == "IsSelected")
            {
                _isSelected = !_isSelected;

                if (_isSelected)
                {
                    var customTextCell = sender as CustomTextCell;
                    if (customTextCell == null)
                        return;
                    _cell.SetBackgroundColor(customTextCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cell.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }
    }
}