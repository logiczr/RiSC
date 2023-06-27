using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using RhinoStableDiffuison;
using System.Net;

namespace RiSC.MainPageMVVM
{
    public class MainPageModel
    {
        public int steps { get; set; } = 20;

        public int Width { get; set; } = 512;

        public int Height { get; set; } = 512;

        public double CFGscale { get; set; } = 7;

        public string Prompt { get; set; } = "dog";

        public string NegativePrompt { get; set; } = "";

        public string SamplerSelected { get; set; } = "Euler a";

        public bool IsActivateUpscaler { get; set; } = false;

        public string SelectedModel { get; set; }

        public string LastTimeModel { get { return Usersetting.Default.LastTimeModel; } set { Usersetting.Default.LastTimeModel = value; Usersetting.Default.Save(); } }

        public ImageSource ResultImage { get; set; }

        public ImageSource ControlNetPageImg { get; set; }

        public ImageSource RhinoCapturePic { get; set; }

        public List<string> ModelName { get; set; } = new List<string>();

        public Dictionary<string,string> LoraList { get; set; } = new Dictionary<string, string>();

        public KeyValuePair<string, string> SelectedLora { get; set; }

        public List<string> UpscalerSampler { get; set; } = new List<string>();

        public string UpscalerSelected { get; set; }

        public int Scale { get; set; } = 2;

        public bool IsgetProcessImage { get {return Usersetting.Default.GetProcessimg; } set { Usersetting.Default.GetProcessimg = value; Usersetting.Default.Save(); } }

        public int DenosingStep { get; set; } = 10;

        public double DenoisingStrength { get; set; } = 0.2;

        public List<string> Sampler { get; set; } = new List<string>();

        public bool IsControlNetOn { get { return Usersetting.Default.IsCNOn; } set { Usersetting.Default.IsCNOn = value; Usersetting.Default.Save(); } }

        public string ControlNetModule { get; set; }

        public string ControlNetModule1 { get; set; }

        public string ControlNetModule2 { get; set; }

        public string ControlNetModule3 { get; set; }

        public string ControlNetModel { get { return Usersetting.Default.ControlNetModel; } set { Usersetting.Default.ControlNetModel = value; Usersetting.Default.Save(); } }

        public string ControlNetModel1 { get { return Usersetting.Default.ControlNetModel1; } set { Usersetting.Default.ControlNetModel1 = value; Usersetting.Default.Save(); } }

        public string ControlNetModel2 { get { return Usersetting.Default.ControlNetModel2; } set { Usersetting.Default.ControlNetModel2 = value; Usersetting.Default.Save(); } }

        public string ControlNetModel3 { get { return Usersetting.Default.ControlNetModel3; } set { Usersetting.Default.ControlNetModel3 = value; Usersetting.Default.Save(); } }

        public double ControlNetModelWeight { get; set; } = 1;

        public double ControlNetModelWeight1 { get; set; } = 1;

        public double ControlNetModelWeight2 { get; set; } = 1;

        public double ControlNetModelWeight3 { get; set; } = 1;

        public List<string> ControlNetModelList { get; set; } = new List<string>() {"No Model" };

        public List<string> ControlNetModuleList { get; set; } = new List<string>();

        public string ControlNetInputImage { get; set; }

        public string DeveloperInfo { get; set; }

        public bool IsGenerateEnable { get; set; } = true;

        public string ImageSavePath { get { return Usersetting.Default.imagepath; } set { Usersetting.Default.imagepath = value; Usersetting.Default.Save(); } }

        public bool IsAutoSave { get { return Usersetting.Default.IsAutoSave; } set { Usersetting.Default.IsAutoSave = value; Usersetting.Default.Save(); } }

        public string IPaddress { get { return Usersetting.Default.IPaddress; } set { Usersetting.Default.IPaddress = value; Usersetting.Default.Save(); } }

        public string Port { get { return Usersetting.Default.Port; } set { Usersetting.Default.Port = value; Usersetting.Default.Save(); } }

        public PayLoad LastTimePayLoad
        {
            get { return ConvertToPayLoad.Converter(IsControlNetOn,(JObject)JsonConvert.DeserializeObject(PayLoadSave)); }
            set { PayLoadSave = JsonConvert.SerializeObject(value); }
        }

        public string PayLoadSave { get { return Usersetting.Default.payloadsave; } set { Usersetting.Default.payloadsave = value;Usersetting.Default.Save(); } }
    }

    public class ConvertToPayLoad 
    {
        public static PayLoad Converter(bool IsCNOn,JObject obj) 
        {
            return new PayLoad(IsCNOn, (JObject)obj["ControlNetParam"]) 
            {
            height= (int)obj["height"],
            width= (int)obj["width"],   
            steps = (int)obj["steps"],
            prompt = (string)obj["prompt"],
            negative_prompt = (string)obj["negative_prompt"],
            hr_scale = (int)obj["hr_scale"],
            sampler_name = (string)obj["sampler_name"],
            hr_second_pass_steps = (int)obj["hr_second_pass_steps"],
            denoising_strength = (double)obj["denoising_strength"],
            enable_hr = (bool)obj["enable_hr"],
            alwayson_scripts = (JObject)obj["alwayson_scripts"]
            };
        }
    }
}
