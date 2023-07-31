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
using Eto.Forms;
using Eto.Threading;

namespace RhinoStableDiffuison
{
    public class RhinoStableDiffuison :Rhino.Commands.Command
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
            try
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.Message);
                Thread.CurrentThread.Abort();
                return Result.Failure;                
            }
            

            return Result.Success;
        }
    }
}
