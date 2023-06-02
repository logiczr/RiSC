﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;
using System.Security.Policy;
using System.Reflection.Emit;
using System.Windows.Threading;
using System.Windows.Forms;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Rhino.Display;
using RhinoStableDiffuison;
using System.Windows.Interop;

namespace RiSC.MainPageMVVM
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            ModelDescription();
            GetSampler();
            GetUpscaler();
            GetControlNetModel();
            GetControlNetModule();
        }

        public void NotifypropertyChanged(string PropName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        MainPageModel Model = new MainPageModel();

        #region  
        public int steps { get { return Model.steps; } set { Model.steps = value; NotifypropertyChanged("steps"); } }

        public int Height { get { return Model.Height; } set { Model.Height = value; NotifypropertyChanged("Height"); } }

        public int Width { get { return Model.Width; } set { Model.Width = value; NotifypropertyChanged("Width"); } }

        public double CFGscale { get { return Model.CFGscale; } set { Model.CFGscale = value; NotifypropertyChanged("CFGscale"); } }

        public string Prompt { get { return Model.Prompt; } set { Model.Prompt = value; NotifypropertyChanged("Prompt"); } }

        public string NegativePrompt { get { return Model.NegativePrompt; } set { Model.NegativePrompt = value; NotifypropertyChanged("NegativePrompt"); } }

        public string SamplerSelected { get { return Model.SamplerSelected; } set { Model.SamplerSelected = value; NotifypropertyChanged("SamplerSelected"); } }

        public ImageSource ResultImage { get { return Model.ResultImage; } set { Model.ResultImage = value; NotifypropertyChanged("ResultImage"); } }

        public List<string> ModelName { get { return Model.ModelName; } set { Model.ModelName = value; NotifypropertyChanged("ModelName"); } }

        public bool IsActivateUpscaler { get { return Model.IsActivateUpscaler; } set { Model.IsActivateUpscaler = value; NotifypropertyChanged("IsActivateUpscaler"); } }

        public int Scale { get { return Model.Scale; } set { Model.Scale = value; NotifypropertyChanged("Scale"); } }

        public int DenosingStep { get { return Model.DenosingStep; } set { Model.DenosingStep = value; NotifypropertyChanged("DenosingStep"); } }

        public double DensoingStrength { get { return Model.DenoisingStrength; } set { Model.DenoisingStrength = value; AddDeveloperInformation($"setting value to{value}"); NotifypropertyChanged("DenoisingStrength"); } }

        public string SelectedModel { get { return Model.SelectedModel; } set { Model.SelectedModel = value; NotifypropertyChanged("SelectedModel"); } }

        public List<string> SamplerName { get { return Model.Sampler; } set { Model.Sampler = value; NotifypropertyChanged("SamplerName"); } }

        public List<string> UpscalerSampler { get { return Model.UpscalerSampler; } set { Model.UpscalerSampler = value; NotifypropertyChanged("UpscalerSampler"); } }

        public string UpscalerSelected { get { return Model.UpscalerSelected; } set { Model.UpscalerSelected = value; NotifypropertyChanged("UpscalerSelected"); } }

        public string DeveloperInfo { get { return Model.DeveloperInfo; } set { Model.DeveloperInfo = value; NotifypropertyChanged("DeveloperInfo"); } }

        public PayLoad LastTimePayLoad { get { return Model.LastTimePayLoad; } set { Model.LastTimePayLoad = value; } }

        public bool IsGenerateEnable { get { return Model.IsGenerateEnable; }set { Model.IsGenerateEnable = value;NotifypropertyChanged("IsGenerateEnable"); } }
        #endregion
        //parameter

        #region
        public bool IsControlNetOn { get { return Model.IsControlNetOn; } set { Model.IsControlNetOn = value; NotifypropertyChanged("IsControlNetOn"); } }
        public string ControlNetModel { get { return Model.ControlNetModel; } set { Model.ControlNetModel = value; NotifypropertyChanged("ControlNetModel"); } }
        public string ControlNetModel1 { get { return Model.ControlNetModel1; } set { Model.ControlNetModel1 = value; NotifypropertyChanged("ControlNetModel1"); } }
        public string ControlNetModel2 { get { return Model.ControlNetModel2; } set { Model.ControlNetModel2 = value; NotifypropertyChanged("ControlNetModel2"); } }
        public string ControlNetModel3 { get { return Model.ControlNetModel3; } set { Model.ControlNetModel3 = value; NotifypropertyChanged("ControlNetModel3"); } }
        public List<string> ControlNetModelList { get { return Model.ControlNetModelList; } set { Model.ControlNetModelList = value; NotifypropertyChanged("ControlNetModelList"); } }
        public List<string> ControlNetModuleList { get { return Model.ControlNetModuleList; } set { Model.ControlNetModuleList = value; NotifypropertyChanged("ControlNetModuleList"); } }
        public string ControlNetModule { get { return Model.ControlNetModule; } set { Model.ControlNetModule = value; NotifypropertyChanged("ControlNetModule"); } }
        public string ControlNetModule1 { get { return Model.ControlNetModule1; } set { Model.ControlNetModule1 = value; NotifypropertyChanged("ControlNetModule1"); } }
        public string ControlNetModule2 { get { return Model.ControlNetModule2; } set { Model.ControlNetModule2 = value; NotifypropertyChanged("ControlNetModule2"); } }
        public string ControlNetModule3 { get { return Model.ControlNetModule3; } set { Model.ControlNetModule3 = value; NotifypropertyChanged("ControlNetModule3"); } }
        public string ControlNetInputImage { get { return Model.ControlNetInputImage; } set { Model.ControlNetInputImage = value; NotifypropertyChanged("ControlNetInputImage"); } }
        public ImageSource ControlNetPageImg { get { return Model.ControlNetPageImg; } set { Model.ControlNetPageImg = value; NotifypropertyChanged("ControlNetPageImg"); } }
        #endregion
        //ControlNetParameter

        #region
        public string ImageSavePath { get { return Model.ImageSavePath; }set { Model.ImageSavePath = value; NotifypropertyChanged("ImageSavePath"); } }

        #endregion
        //setting parameter

        public ImageSource RhinoCapturePic { get { return Model.RhinoCapturePic; } set { Model.RhinoCapturePic = value; NotifypropertyChanged("RhinoCapturePic"); } }
        public ICommand Generate { get { return new MainPageCommand((args) => { Generateimg(); }); } }

        public ICommand ChangeModel { get { return new MainPageCommand((args) => { ModelChanged(); }); } }

        public ICommand UpLoadImage { get { return new MainPageCommand((args) => { ControlNetUploadImage(); }); } }

        public ICommand DenoiseChanged { get { return new MainPageCommand((args) => { try {this.DensoingStrength = Convert.ToDouble(args); } catch(Exception e) { } }); } }

        public ICommand UseLastTimePara { get { return new MainPageCommand((args) => { UseLastTimeGenerateParameter(LastTimePayLoad); }); } }

        public ICommand SelectedImagePath { get { return new MainPageCommand((args) => { SelectedSaveImagePath(); }); } }

        public ICommand GetRhinoPics { get { return new MainPageCommand((args) => { GetRhinoPic(); }); } }

        public ICommand SaveImage { get { return new MainPageCommand((args) => { SavingImage(this.ResultImage, $@"{ImageSavePath}\{DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.png"); }); } }

        HttpClient Client = new HttpClient();
        HttpClient Client1 = new HttpClient();

        public async void ModelDescription()
        {
            Client.BaseAddress = new Uri("http://127.0.0.1:7860/sdapi/v1/sd-models");
            StringBuilder builder = new StringBuilder();
            HttpContent ApplyMessage = new StringContent("{}", encoding: Encoding.UTF8, "application/json");
            int NumberOfBytes = 0;
            byte[] bytes = new byte[1024];
            var Reciviedmsg = await (await Client.GetAsync(Client.BaseAddress)).Content.ReadAsStreamAsync();
            do
            {
                NumberOfBytes = Reciviedmsg.Read(bytes, 0, bytes.Length);
                builder.Append(Encoding.UTF8.GetString(bytes, 0, NumberOfBytes));
            }
            while (NumberOfBytes > 0);
            string ToJson = builder.ToString();
            JArray ModelList = (JArray)JsonConvert.DeserializeObject(ToJson);
            foreach (JObject item in ModelList)
            {
                string SingelModelName = item["title"].ToString();
                if (!string.IsNullOrEmpty(SingelModelName))
                {
                    this.ModelName.Add(SingelModelName);
                }
            }
            this.SelectedModel = this.ModelName.FirstOrDefault();
        }

        public async void GetSampler()
        {
            Client1.BaseAddress = new Uri("http://127.0.0.1:7860/sdapi/v1/samplers");
            StringBuilder builder = new StringBuilder();
            HttpContent ApplyMessage = new StringContent("{}", encoding: Encoding.UTF8, "application/json");
            int NumberOfBytes = 0;
            byte[] bytes = new byte[1024];
            var Reciviedmsg = await (await Client1.GetAsync(Client1.BaseAddress)).Content.ReadAsStreamAsync();
            do
            {
                NumberOfBytes = Reciviedmsg.Read(bytes, 0, bytes.Length);
                builder.Append(Encoding.UTF8.GetString(bytes, 0, NumberOfBytes));
            }
            while (NumberOfBytes > 0);
            string ToJson = builder.ToString();
            JArray SamplerList = (JArray)JsonConvert.DeserializeObject(ToJson);
            foreach (JObject item in SamplerList)
            {
                string SingelSamplerName = item["name"].ToString();
                if (!string.IsNullOrEmpty(SingelSamplerName))
                {
                    this.SamplerName.Add(SingelSamplerName);
                }
                this.SamplerSelected = this.SamplerName[0].ToString();
            }
        }

        public async void Generateimg()
        {
            IsGenerateEnable = false;
            PayLoad payLoad = new PayLoad(IsControlNetOn, SerControlNetPara
                (this.ControlNetModule, this.ControlNetModel,
                this.ControlNetModule1,this.ControlNetModel1,
                this.ControlNetModule2,this.ControlNetModel2,
                this.ControlNetModule3,this.ControlNetModel3,
                this.ControlNetInputImage))
            {
                steps = this.steps,
                height = this.Height,
                width = this.Width,
                prompt = this.Prompt,
                sampler_name = this.SamplerSelected,
                negative_prompt = this.NegativePrompt,
                enable_hr = this.IsActivateUpscaler,
                hr_upscaler = this.UpscalerSelected,
                hr_second_pass_steps = this.DenosingStep,
                denoising_strength = this.DensoingStrength,
                hr_scale = this.Scale,
            };
            //this.LastTimePayLoad= payLoad;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:7860/sdapi/v1/txt2img");
            string payload = JsonConvert.SerializeObject(payLoad);
            HttpContent data = new StringContent(payload, Encoding.UTF8, "application/json");
            var json = await (await client.PostAsync(client.BaseAddress, data)).Content.ReadAsStreamAsync();
            byte[] bytes = new byte[1024];
            StringBuilder builder = new StringBuilder();
            int numofbytes = 0;
            do
            {
                numofbytes = json.Read(bytes, 0, bytes.Length);
                builder.Append(Encoding.UTF8.GetString(bytes, 0, numofbytes));
            } while (numofbytes > 0);
            json.Close();
            IsGenerateEnable = true;
            var jpic = JObject.Parse(builder.ToString());
            var str2 = jpic["images"][0].ToString();
            if (str2 == null) { AddDeveloperInformation(jpic.ToString()); }
            else
            {                
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(str2)))
                {
                    this.ResultImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap((new Bitmap(ms).GetHbitmap()), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        public void AddDeveloperInformation(string Msg)
        {
            DeveloperInfo += Msg  +"  "+ DateTime.Now+ "\r\n";
        }

        public void ModelChanged() 
        {
            JObject SendMsg = new JObject
            {
                { "sd_model_checkpoint", this.SelectedModel.ToString() }
            };
            string URL = "http://127.0.0.1:7860/sdapi/v1/options";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            string datapost = JsonConvert.SerializeObject(SendMsg);
            //byte[] bytes = Encoding.UTF8.GetBytes(datapost);
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(datapost);
            }
            Thread thread = new Thread(() => {var httpresponse = (HttpWebResponse)request.GetResponse();});
            thread.Start();            
        }

        public void GetUpscaler() 
        {
            string URL = "http://127.0.0.1:7860/sdapi/v1/upscalers";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            Thread thread = new Thread(() => 
            { 
                var httpresponse = (HttpWebResponse)request.GetResponse(); 
                var Stream = httpresponse.GetResponseStream();
                int NumberofBytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do 
                {
                    NumberofBytes = Stream.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetString(buffer,0, NumberofBytes));    
                }
                while (NumberofBytes > 0);
                string ToJson = sb.ToString();
                JArray UpScalerList = (JArray)JsonConvert.DeserializeObject(ToJson);
                foreach (JObject item in UpScalerList)
                {
                    UpscalerSampler.Add(item["name"].ToString());
                }
            });
            thread.Start();
        }

        public void ControlNetUploadImage() 
        {
            OpenFileDialog dialog= new OpenFileDialog();
            if ((bool)dialog.ShowDialog()) 
            { 
                ControlNetPageImg = new BitmapImage(new Uri(dialog.FileName)); 
                Bitmap ReadyToEncode = new Bitmap(dialog.FileName);
                using (MemoryStream ms1 = new MemoryStream())
                {
                    ReadyToEncode.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr1 = new byte[ms1.Length];
                    ms1.Position = 0;
                    ms1.Read(arr1, 0, (int)ms1.Length);
                    ms1.Close();
                    ControlNetInputImage = Convert.ToBase64String(arr1);
                }
            }
        }

        public void GetControlNetModel()
        {
            string URL = "http://127.0.0.1:7860/controlnet/model_list";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            Thread thread = new Thread(() =>
            {
                var httpresponse = (HttpWebResponse)request.GetResponse();
                var Stream = httpresponse.GetResponseStream();
                int NumberofBytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do
                {
                    NumberofBytes = Stream.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, NumberofBytes));
                }
                while (NumberofBytes > 0);
                string ToJson = sb.ToString();
                JObject ControlNetModelList = (JObject)JsonConvert.DeserializeObject(ToJson);
                foreach (var item in ControlNetModelList["model_list"])
                {
                    this.ControlNetModelList.Add(item.ToString());
                }
            });
            thread.Start();
        }

        public void GetControlNetModule() 
        {
            string URL = "http://127.0.0.1:7860/controlnet/module_list?alias_names=false";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            Thread thread = new Thread(() =>
            {
                var httpresponse = (HttpWebResponse)request.GetResponse();
                var Stream = httpresponse.GetResponseStream();
                int NumberofBytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do
                {
                    NumberofBytes = Stream.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, NumberofBytes));
                }
                while (NumberofBytes > 0);
                string ToJson = sb.ToString();
                JObject ControlNetModuleList = (JObject)JsonConvert.DeserializeObject(ToJson);
                foreach (var item in ControlNetModuleList["module_list"])
                {
                    this.ControlNetModuleList.Add(item.ToString());
                }
            });
            thread.Start();
        }

        public void GetProgressImage() 
        {
            do
            {
                Thread.Sleep(1500);
                string URL = "http://127.0.0.1:7860/sdapi/v1/progress?skip_current_image=false";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                var httpresponse = (HttpWebResponse)request.GetResponse();
                var Stream = httpresponse.GetResponseStream();
                int NumberofBytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do
                {
                    NumberofBytes = Stream.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, NumberofBytes));
                }
                while (NumberofBytes > 0);
                string ToJson = sb.ToString();
                JObject FromStream = (JObject)JsonConvert.DeserializeObject(ToJson);
                if ((int)FromStream["state"]["job_count"] == -1 && FromStream["current_image"] == null) 
                {
                    AddDeveloperInformation("stop getting progress image");
                    break; 
                }
                else 
                {
                    var str = FromStream["current_image"].ToString();
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(str)))
                    {
                        this.ResultImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap((new Bitmap(ms).GetHbitmap()), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                }
            }while(true);

        }

        public void GetRhinoPic() 
        {
            getpics command = new getpics();
            RhinoCapturePic = Imaging.CreateBitmapSourceFromHBitmap(command.Bitmap.GetHbitmap(),IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
        }

        public void SavingImage(ImageSource source,string filepath) 
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)source));
            using (var fs = new System.IO.FileStream(filepath,FileMode.Create))
            {
            encoder.Save(fs);
            }
        }

        public void UseLastTimeGenerateParameter(PayLoad load)
        {
            this.steps = load.steps;
            this.Height= load.height;
            this.Width= load.width;
            this.Prompt= load.prompt;
            this.NegativePrompt = load.negative_prompt;
            this.IsActivateUpscaler = load.enable_hr;
            this.DensoingStrength = load.denoising_strength;
            this.DenosingStep = load.hr_second_pass_steps;
            this.UpscalerSelected = load.hr_upscaler;
            this.SamplerSelected = load.sampler_name;
            this.Scale = load.hr_scale;
        }

        public void SelectedSaveImagePath() 
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImageSavePath = openFileDialog.SelectedPath;
            }
        }

        public JObject SerControlNetPara
            (string ControlNetModule, string ControlNetModel, 
            string ControlNetModule1, string ControlNetModel1, string ControlNetModule2,
            string ControlNetModel2, string ControlNetModule3, string ControlNetModel3, 
            string ControlNetInputImage)
        {
            JArray Json2 = new JArray();
            if (ControlNetModel != string.Empty && ControlNetModel != null)
            {
                JObject JsonAdd = new JObject();
                JsonAdd.Add("enabled", true);
                JsonAdd.Add("input_image", ControlNetInputImage);
                JsonAdd.Add("module", ControlNetModule);
                JsonAdd.Add("model", ControlNetModel);               
                Json2.Add(JsonAdd);
            }
            else 
            {
                JObject Json5 = new JObject();
                Json5.Add("enabled", false);
                Json2.Add(Json5);
            }//0
            if (ControlNetModel1 != string.Empty && ControlNetModel1 != null)
            {
                JObject JsonAdd = new JObject();
                JsonAdd.Add("input_image", ControlNetInputImage);
                JsonAdd.Add("module", ControlNetModule1);
                JsonAdd.Add("model", ControlNetModel1);
                JsonAdd.Add("enabled", true);
                Json2.Add(JsonAdd);
            }
            else
            {
                JObject Json5 = new JObject();
                Json5.Add("enabled", false);
                Json2.Add(Json5);
            }//1
            if (ControlNetModel2 != string.Empty && ControlNetModel2 != null)
            {
                JObject JsonAdd = new JObject();
                JsonAdd.Add("input_image", ControlNetInputImage);
                JsonAdd.Add("module", ControlNetModule2);
                JsonAdd.Add("model", ControlNetModel2);
                JsonAdd.Add("enabled", true);
                Json2.Add(JsonAdd);
            }
            else
            {
                JObject Json5 = new JObject();
                Json5.Add("enabled", false);
                Json2.Add(Json5);
            }//2
            if (ControlNetModel3 != string.Empty && ControlNetModel3 != null)
            {
                JObject JsonAdd = new JObject();
                JsonAdd.Add("input_image", ControlNetInputImage);
                JsonAdd.Add("module", ControlNetModule3);
                JsonAdd.Add("model", ControlNetModel3);
                JsonAdd.Add("enabled", true);
                Json2.Add(JsonAdd);
            }
            else
            {
                JObject Json5 = new JObject();
                Json5.Add("enabled", false);
                Json2.Add(Json5);
            }//3
            JObject Json3 = new JObject();
            Json3.Add("args",Json2);
            JObject Json4 = new JObject();
            Json4.Add("controlnet", Json3);            
            return Json4;
        }
    }

    public class PayLoad
    {
        public int steps { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string negative_prompt { get; set; }
        public string prompt { get; set; }
        public string sampler_name { get; set; } = "Euler";
        public int hr_scale { get; set; }
        public string hr_upscaler { get; set; }
        public int hr_second_pass_steps {get; set; }
        public double denoising_strength { get; set; }
        public bool enable_hr { get; set; }
        public JObject alwayson_scripts { get; set; }

        public PayLoad(bool IsControlNetOn,JObject ControlNetParam) 
        {
            if (IsControlNetOn) { alwayson_scripts = ControlNetParam; }
            else 
            {
                alwayson_scripts= new JObject();
            }
        }
    }

}
