using LYMG.OrbitVisualization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormSample
{
    public partial class Form1 : Form
    {
        private CesiumService CesiumService;
        private CesiumViewerProxy CesiumViewerProxy;
        private Process ChromeProcess;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            CesiumService = new CesiumService("https://localhost:44389");
            CesiumViewerProxy = new CesiumViewerProxy("345", CesiumService);
            await CesiumService.StartAsync();
            btnConnect.Text = "连接成功";
        }

        private async void btnOpenWebBrowser_Click(object sender, EventArgs e)
        {
            ChromeProcess = await CesiumViewerProxy.Start(CesiumViewerProxy.Mode.App, "http://localhost:8080/#/Cesium/");
            //btnOpenWebBrowser.Text = ChromeProcess.ProcessName;
        }

        private async void btnSayHello_Click(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            ctrl.Enabled = false;
            await CesiumViewerProxy.Post("SayHello", 233);
            ctrl.Enabled = true;
        }

        private async void btnGetCesiumViewers_Click(object sender, EventArgs e)
        {
            var viewers = await CesiumService.GetCesiumViewers();
            MessageBox.Show(string.Join("\r\n", viewers));
        }

        private async void btnWaitViewer_Click(object sender, EventArgs e)
        {
            await CesiumService.WaitViewer("233");
            btnWaitViewer.Text = "Ok";
        }
    }
}
