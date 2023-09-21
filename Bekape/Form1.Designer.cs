namespace Bekape
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_Usuarios = new System.Windows.Forms.ListView();
            this.button_ADDuser = new System.Windows.Forms.Button();
            this.button_RemoveUser = new System.Windows.Forms.Button();
            this.listView_Folders = new System.Windows.Forms.ListView();
            this.button_RemoveFolder = new System.Windows.Forms.Button();
            this.button_ADDfolder = new System.Windows.Forms.Button();
            this.button_Synchronize = new System.Windows.Forms.Button();
            this.textBox_consoleNEW = new System.Windows.Forms.TextBox();
            this.textBox_consoleMOD = new System.Windows.Forms.TextBox();
            this.label_NEW = new System.Windows.Forms.Label();
            this.label_MOD = new System.Windows.Forms.Label();
            this.button_console = new System.Windows.Forms.Button();
            this.linkLabel_Github = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_bar = new System.Windows.Forms.Panel();
            this.label_space = new System.Windows.Forms.Label();
            this.button_Atualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_Usuarios
            // 
            this.listView_Usuarios.HideSelection = false;
            this.listView_Usuarios.Location = new System.Drawing.Point(12, 41);
            this.listView_Usuarios.Name = "listView_Usuarios";
            this.listView_Usuarios.Size = new System.Drawing.Size(169, 158);
            this.listView_Usuarios.TabIndex = 0;
            this.listView_Usuarios.UseCompatibleStateImageBehavior = false;
            this.listView_Usuarios.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_Usuarios_ItemSelectionChanged);
            // 
            // button_ADDuser
            // 
            this.button_ADDuser.Location = new System.Drawing.Point(12, 12);
            this.button_ADDuser.Name = "button_ADDuser";
            this.button_ADDuser.Size = new System.Drawing.Size(75, 23);
            this.button_ADDuser.TabIndex = 1;
            this.button_ADDuser.Text = "Add User";
            this.button_ADDuser.UseVisualStyleBackColor = true;
            this.button_ADDuser.Click += new System.EventHandler(this.button_ADDuser_Click);
            // 
            // button_RemoveUser
            // 
            this.button_RemoveUser.Location = new System.Drawing.Point(93, 12);
            this.button_RemoveUser.Name = "button_RemoveUser";
            this.button_RemoveUser.Size = new System.Drawing.Size(88, 23);
            this.button_RemoveUser.TabIndex = 2;
            this.button_RemoveUser.Text = "Remove User";
            this.button_RemoveUser.UseVisualStyleBackColor = true;
            this.button_RemoveUser.Click += new System.EventHandler(this.button_RemoveUser_Click);
            // 
            // listView_Folders
            // 
            this.listView_Folders.HideSelection = false;
            this.listView_Folders.Location = new System.Drawing.Point(215, 42);
            this.listView_Folders.Name = "listView_Folders";
            this.listView_Folders.Size = new System.Drawing.Size(757, 186);
            this.listView_Folders.TabIndex = 3;
            this.listView_Folders.UseCompatibleStateImageBehavior = false;
            // 
            // button_RemoveFolder
            // 
            this.button_RemoveFolder.Location = new System.Drawing.Point(296, 13);
            this.button_RemoveFolder.Name = "button_RemoveFolder";
            this.button_RemoveFolder.Size = new System.Drawing.Size(90, 23);
            this.button_RemoveFolder.TabIndex = 5;
            this.button_RemoveFolder.Text = "Remove Folder";
            this.button_RemoveFolder.UseVisualStyleBackColor = true;
            this.button_RemoveFolder.Click += new System.EventHandler(this.button_RemoveFolder_Click);
            // 
            // button_ADDfolder
            // 
            this.button_ADDfolder.Location = new System.Drawing.Point(215, 12);
            this.button_ADDfolder.Name = "button_ADDfolder";
            this.button_ADDfolder.Size = new System.Drawing.Size(75, 23);
            this.button_ADDfolder.TabIndex = 4;
            this.button_ADDfolder.Text = "ADD Folder";
            this.button_ADDfolder.UseVisualStyleBackColor = true;
            this.button_ADDfolder.Click += new System.EventHandler(this.button_ADDfolder_Click);
            // 
            // button_Synchronize
            // 
            this.button_Synchronize.Location = new System.Drawing.Point(887, 12);
            this.button_Synchronize.Name = "button_Synchronize";
            this.button_Synchronize.Size = new System.Drawing.Size(85, 23);
            this.button_Synchronize.TabIndex = 6;
            this.button_Synchronize.Text = "Synchronize";
            this.button_Synchronize.UseVisualStyleBackColor = true;
            this.button_Synchronize.Click += new System.EventHandler(this.button_Synchronize_Click);
            // 
            // textBox_consoleNEW
            // 
            this.textBox_consoleNEW.Location = new System.Drawing.Point(12, 300);
            this.textBox_consoleNEW.Multiline = true;
            this.textBox_consoleNEW.Name = "textBox_consoleNEW";
            this.textBox_consoleNEW.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_consoleNEW.Size = new System.Drawing.Size(477, 375);
            this.textBox_consoleNEW.TabIndex = 7;
            // 
            // textBox_consoleMOD
            // 
            this.textBox_consoleMOD.Location = new System.Drawing.Point(493, 300);
            this.textBox_consoleMOD.Multiline = true;
            this.textBox_consoleMOD.Name = "textBox_consoleMOD";
            this.textBox_consoleMOD.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_consoleMOD.Size = new System.Drawing.Size(477, 375);
            this.textBox_consoleMOD.TabIndex = 8;
            // 
            // label_NEW
            // 
            this.label_NEW.AutoSize = true;
            this.label_NEW.Location = new System.Drawing.Point(209, 284);
            this.label_NEW.Name = "label_NEW";
            this.label_NEW.Size = new System.Drawing.Size(60, 13);
            this.label_NEW.TabIndex = 9;
            this.label_NEW.Text = "NEW files: ";
            // 
            // label_MOD
            // 
            this.label_MOD.AutoSize = true;
            this.label_MOD.Location = new System.Drawing.Point(649, 284);
            this.label_MOD.Name = "label_MOD";
            this.label_MOD.Size = new System.Drawing.Size(86, 13);
            this.label_MOD.TabIndex = 10;
            this.label_MOD.Text = "MODIFIED files: ";
            // 
            // button_console
            // 
            this.button_console.Location = new System.Drawing.Point(924, 229);
            this.button_console.Name = "button_console";
            this.button_console.Size = new System.Drawing.Size(48, 23);
            this.button_console.TabIndex = 73;
            this.button_console.Text = "Log ▼";
            this.button_console.UseVisualStyleBackColor = true;
            this.button_console.Click += new System.EventHandler(this.button_console_Click);
            // 
            // linkLabel_Github
            // 
            this.linkLabel_Github.AutoSize = true;
            this.linkLabel_Github.Location = new System.Drawing.Point(12, 234);
            this.linkLabel_Github.Name = "linkLabel_Github";
            this.linkLabel_Github.Size = new System.Drawing.Size(173, 13);
            this.linkLabel_Github.TabIndex = 74;
            this.linkLabel_Github.TabStop = true;
            this.linkLabel_Github.Text = "https://github.com/DanielSvoboda";
            this.linkLabel_Github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Github_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(213, 237);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 12);
            this.panel1.TabIndex = 75;
            // 
            // panel_bar
            // 
            this.panel_bar.BackColor = System.Drawing.Color.LimeGreen;
            this.panel_bar.Location = new System.Drawing.Point(215, 235);
            this.panel_bar.Name = "panel_bar";
            this.panel_bar.Size = new System.Drawing.Size(200, 12);
            this.panel_bar.TabIndex = 76;
            // 
            // label_space
            // 
            this.label_space.AutoSize = true;
            this.label_space.Location = new System.Drawing.Point(419, 234);
            this.label_space.Name = "label_space";
            this.label_space.Size = new System.Drawing.Size(71, 13);
            this.label_space.TabIndex = 76;
            this.label_space.Text = "info space hd";
            // 
            // button_Atualizar
            // 
            this.button_Atualizar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_Atualizar.Location = new System.Drawing.Point(12, 205);
            this.button_Atualizar.Name = "button_Atualizar";
            this.button_Atualizar.Size = new System.Drawing.Size(169, 23);
            this.button_Atualizar.TabIndex = 77;
            this.button_Atualizar.Text = "Check program update?";
            this.button_Atualizar.UseVisualStyleBackColor = true;
            this.button_Atualizar.Click += new System.EventHandler(this.button_Atualizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 260);
            this.Controls.Add(this.button_Atualizar);
            this.Controls.Add(this.panel_bar);
            this.Controls.Add(this.label_space);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabel_Github);
            this.Controls.Add(this.button_console);
            this.Controls.Add(this.label_MOD);
            this.Controls.Add(this.label_NEW);
            this.Controls.Add(this.textBox_consoleMOD);
            this.Controls.Add(this.textBox_consoleNEW);
            this.Controls.Add(this.button_Synchronize);
            this.Controls.Add(this.button_RemoveFolder);
            this.Controls.Add(this.button_ADDfolder);
            this.Controls.Add(this.listView_Folders);
            this.Controls.Add(this.button_RemoveUser);
            this.Controls.Add(this.button_ADDuser);
            this.Controls.Add(this.listView_Usuarios);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BEKAPE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_Usuarios;
        private System.Windows.Forms.Button button_ADDuser;
        private System.Windows.Forms.Button button_RemoveUser;
        private System.Windows.Forms.ListView listView_Folders;
        private System.Windows.Forms.Button button_RemoveFolder;
        private System.Windows.Forms.Button button_ADDfolder;
        private System.Windows.Forms.Button button_Synchronize;
        private System.Windows.Forms.TextBox textBox_consoleNEW;
        private System.Windows.Forms.TextBox textBox_consoleMOD;
        private System.Windows.Forms.Label label_NEW;
        private System.Windows.Forms.Label label_MOD;
        private System.Windows.Forms.Button button_console;
        private System.Windows.Forms.LinkLabel linkLabel_Github;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_bar;
        private System.Windows.Forms.Label label_space;
        private System.Windows.Forms.Button button_Atualizar;
    }
}

