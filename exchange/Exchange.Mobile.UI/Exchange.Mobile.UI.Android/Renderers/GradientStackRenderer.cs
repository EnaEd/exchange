using Android.Content;
using Android.Graphics;
using Exchange.Mobile.UI.Controls;
using Exchange.Mobile.UI.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientStack), typeof(GradientStackRenderer))]

namespace Exchange.Mobile.UI.Droid.Renderers
{
    public class GradientStackRenderer : VisualElementRenderer<StackLayout>
    {
        public Xamarin.Forms.Color StartColor { get; set; }
        public Xamarin.Forms.Color EndColor { get; set; }

        //'VisualElementRenderer<StackLayout>.VisualElementRenderer()' is obsolete:
        //'This constructor is obsolete as of version 2.5. Please use VisualElementRenderer(Context) instead.'  
        public GradientStackRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            //for vertical gradient use var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            var gradient =
                new LinearGradient(default, default, Width, default, StartColor.ToAndroid(), EndColor.ToAndroid(), Shader.TileMode.Mirror);

            var paint = new Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
            }
            catch (Exception ex)
            {
                //TODO EE: handle exception
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

    }
}
