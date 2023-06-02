using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhinoStableDiffuison;

namespace RiSC.MainPageMVVM
{
    public class MainPageModel
    {
        public int steps { get; set; } = 20;

        public int Width { get; set; } = 512;

        public int Height { get; set; } = 512;

        public double CFGscale { get; set; } = 7.5;

        public string Prompt { get; set; } = "dog";

        public string NegativePrompt { get; set; } = null;

        public string SamplerSelected { get; set; } = "Euler a";

        public bool IsActivateUpscaler { get; set; } = false;

        public string SelectedModel { get; set; }

        public ImageSource ResultImage { get; set; } = new BitmapImage(new Uri(@"G:\\NOIMAGE.png",UriKind.Absolute));

        public ImageSource ControlNetPageImg { get; set; }

        public ImageSource RhinoCapturePic { get; set; } = new BitmapImage(new Uri(@"G:\\NOIMAGE.png", UriKind.Absolute));

        public List<string> ModelName { get; set; } = new List<string>();

        public List<string> UpscalerSampler { get; set; } = new List<string>();

        public string UpscalerSelected { get; set; }

        public int Scale { get; set; } = 2;

        public int DenosingStep { get; set; } = 10;

        public double DenoisingStrength { get; set; } = 0.2;

        public List<string> Sampler { get; set; } = new List<string>();

        public bool IsControlNetOn { get { return Usersetting.Default.IsCNOn; } set { Usersetting.Default.IsCNOn = value; Usersetting.Default.Save(); } }

        public string ControlNetModule { get; set; }

        public string ControlNetModule1 { get; set; }

        public string ControlNetModule2 { get; set; }

        public string ControlNetModule3 { get; set; }

        public string ControlNetModel { get; set; }

        public string ControlNetModel1 { get; set; }

        public string ControlNetModel2 { get; set; }

        public string ControlNetModel3 { get; set; }

        public List<string> ControlNetModelList { get; set; } = new List<string>();

        public List<string> ControlNetModuleList { get; set; } = new List<string>();

        public string ControlNetInputImage { get; set; }

        public string DeveloperInfo { get; set; }

        public bool IsGenerateEnable { get; set; } = true;

        public string ImageSavePath { get { return Usersetting.Default.imagepath; } set { Usersetting.Default.imagepath = value; Usersetting.Default.Save(); } }

        
        public PayLoad LastTimePayLoad
        {
            get { return new PayLoad(false,new JObject()); }
            set { LastTimePayLoad = value; }
        }
    }
}
