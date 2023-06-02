using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using RiSC;
using System;
using System.Collections.Generic;
using RiSC.MainPageMVVM;
using Rhino.Display;
using System.IO;

namespace RhinoStableDiffuison
{
    public class RhinoStableDiffuison : Command
    {
        public RhinoStableDiffuison()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoStableDiffuison Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "RhinoStableDiffuison";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("The {0} command activate.", EnglishName);

            //getpics getpics1 = new getpics();

            //System.Drawing.Bitmap bitmap = RhinoDoc.ActiveDoc.Views.ActiveView.CaptureToBitmap();
            //RhinoApp.WriteLine("The {0} command activate.", EnglishName);
            //bitmap.Save(@"G:\riscop\aaa.png", System.Drawing.Imaging.ImageFormat.Png);

            MainWindow window = new MainWindow();
            window.Show();
            return Result.Success;
        }
    }
}
