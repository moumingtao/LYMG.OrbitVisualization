namespace WindowsFormSample
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenWebBrowser = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSayHello = new System.Windows.Forms.Button();
            this.btnGetCesiumViewers = new System.Windows.Forms.Button();
            this.btnWaitViewer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenWebBrowser
            // 
            this.btnOpenWebBrowser.Location = new System.Drawing.Point(13, 42);
            this.btnOpenWebBrowser.Name = "btnOpenWebBrowser";
            this.btnOpenWebBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnOpenWebBrowser.TabIndex = 0;
            this.btnOpenWebBrowser.Text = "打开浏览器";
            this.btnOpenWebBrowser.UseVisualStyleBackColor = true;
            this.btnOpenWebBrowser.Click += new System.EventHandler(this.btnOpenWebBrowser_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(13, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "连接服务";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSayHello
            // 
            this.btnSayHello.Location = new System.Drawing.Point(13, 72);
            this.btnSayHello.Name = "btnSayHello";
            this.btnSayHello.Size = new System.Drawing.Size(75, 23);
            this.btnSayHello.TabIndex = 2;
            this.btnSayHello.Text = "Say Hello";
            this.btnSayHello.UseVisualStyleBackColor = true;
            this.btnSayHello.Click += new System.EventHandler(this.btnSayHello_Click);
            // 
            // btnGetCesiumViewers
            // 
            this.btnGetCesiumViewers.Location = new System.Drawing.Point(13, 102);
            this.btnGetCesiumViewers.Name = "btnGetCesiumViewers";
            this.btnGetCesiumViewers.Size = new System.Drawing.Size(105, 23);
            this.btnGetCesiumViewers.TabIndex = 3;
            this.btnGetCesiumViewers.Text = "获取所有客户端";
            this.btnGetCesiumViewers.UseVisualStyleBackColor = true;
            this.btnGetCesiumViewers.Click += new System.EventHandler(this.btnGetCesiumViewers_Click);
            // 
            // btnWaitViewer
            // 
            this.btnWaitViewer.Location = new System.Drawing.Point(13, 132);
            this.btnWaitViewer.Name = "btnWaitViewer";
            this.btnWaitViewer.Size = new System.Drawing.Size(112, 23);
            this.btnWaitViewer.TabIndex = 4;
            this.btnWaitViewer.Text = "等到已有的客户端";
            this.btnWaitViewer.UseVisualStyleBackColor = true;
            this.btnWaitViewer.Click += new System.EventHandler(this.btnWaitViewer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 370);
            this.Controls.Add(this.btnWaitViewer);
            this.Controls.Add(this.btnGetCesiumViewers);
            this.Controls.Add(this.btnSayHello);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnOpenWebBrowser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenWebBrowser;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSayHello;
        private System.Windows.Forms.Button btnGetCesiumViewers;
        private System.Windows.Forms.Button btnWaitViewer;
    }
}

