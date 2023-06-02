using System;
using Eto.Drawing;
using Rhino;
using Rhino.Commands;
using Rhino.Display;
using System.Drawing;
using System.Data.Odbc;

namespace RhinoStableDiffuison
{
    public class getpics : Command
    {
        public getpics()
        {
            Instance = this;
            RunCommand(RhinoDoc.ActiveDoc, RunMode.Interactive);
        }

        ///<summary>The only instance of the MyCommand command.</summary>
        public static getpics Instance { get; private set; }

        public override string EnglishName => "getpics";

        public System.Drawing.Bitmap Bitmap { get; private set; }
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.
            RhinoApp.WriteLine("getpics activate");
            System.Drawing.Bitmap bitmap = RhinoDoc.ActiveDoc.Views.ActiveView.CaptureToBitmap();
            this.Bitmap = bitmap;
            RhinoApp.WriteLine("get pic!");
            return Result.Success;
        }
    }
}