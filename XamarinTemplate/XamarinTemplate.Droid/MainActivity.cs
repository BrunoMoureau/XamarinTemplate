using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XamarinTemplate.Droid
{
    [Activity(Label = "XamarinTemplate", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Android.Glide.Forms.Init(this);
            LoadApplication(new App());
        }
    }
}