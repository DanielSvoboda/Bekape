using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Net;
using System.Diagnostics;

namespace Bekape
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Load += Form1_Load;
        }

        string versaoAtual= "1";
        string caminhoPrograma = AppDomain.CurrentDomain.BaseDirectory;
        string computerName = Environment.MachineName;
        string userName = Environment.UserName;
        string pc_user;

        string[] pastasDoConfig;

        private async void Form1_Load(object sender, EventArgs e)
        {
            await iniciar();

            // Desabilita o botão button_RemoveUser inicialmente
            //button_RemoveUser.Enabled = false;

            // Adiciona um manipulador de eventos para o evento SelectedIndexChanged do ListView
            //listView_Usuarios.SelectedIndexChanged += listView_Usuarios_SelectedIndexChanged;
        }

        private void listView_Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button_RemoveUser.Enabled = listView_Usuarios.SelectedItems.Count > 0;

        }

        private async Task iniciar()
        {
            pc_user = computerName + "_" + userName;
            listView_Usuarios.View = View.List;
            listView_Usuarios.MultiSelect = false;

            listView_Folders.View = View.Details;
            listView_Folders.Columns.Add("Directory", 650, HorizontalAlignment.Left);
            listView_Folders.Columns.Add("Status", 100, HorizontalAlignment.Left);

            try
            {
                await LerConfiguracaoAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the settings: " + ex.Message);
            }

            AtualizarEspacoLivre();
        }

        private void AtualizarEspacoLivre()
        {
            string caminhoCompleto = AppDomain.CurrentDomain.BaseDirectory;
            string nomeUnidade = Path.GetPathRoot(caminhoCompleto);
            DriveInfo driveInfo = new DriveInfo(nomeUnidade);
            string letraUnidade = driveInfo.Name;
            string rotuloVolume = driveInfo.VolumeLabel;

            // Barra estaço livre da unidade do programa
            DriveInfo drive = new DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory));
            long freeSpace = drive.TotalFreeSpace;
            long totalSpace = drive.TotalSize;
            long usedSpace = totalSpace - freeSpace;
            double percentUsed = (double)usedSpace / totalSpace * 100;

            // Atualize a barra de progresso com o valor percentual usado
            panel_bar.Size = new Size((int)percentUsed * 2, 12);
            label_space.Text = letraUnidade + $" {(totalSpace - freeSpace) / 1024 / 1024 / 1024:N0} GB used  -  {freeSpace / 1024 / 1024 / 1024:N0} GB free";
        }


        private async Task LerConfiguracaoAsync()
        {
            // Caminho completo para o arquivo user.config
            string caminhoConfig = Path.Combine(caminhoPrograma, "configs", pc_user + ".config");

            // Verifica se o arquivo .config existe
            if (File.Exists(caminhoConfig))
            {
                // Lê o conteúdo do arquivo e armazena em uma variável
                string conteudoArquivo = File.ReadAllText(caminhoConfig);

                addUser();
                // Verifica se o conteúdo do arquivo não está em branco
                if (!string.IsNullOrWhiteSpace(conteudoArquivo))
                {
                    pastasDoConfig = conteudoArquivo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    List<ListViewItem> items = new List<ListViewItem>();
                    for (int i = 0; i < pastasDoConfig.Length; i++)
                    {
                        string pastaLocal = pastasDoConfig[i];
                        string status = "Analyzing";

                        ListViewItem item = new ListViewItem(pastaLocal);
                        item.SubItems.Add(status);
                        items.Add(item);
                    }

                    listView_Folders.Invoke(new Action(() =>
                    {
                        listView_Folders.Items.AddRange(items.ToArray());
                    }));

                    //Se existir o usuario do proprio pc, seleciona ele
                    ListViewItem item2 = listView_Usuarios.FindItemWithText(pc_user);
                    if (item2 != null)
                    {
                        item2.Selected = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("To start, add your user with 'Add User'\nThen add the folders you want to back up with 'ADD Folder'.");
            }
        }


        private void button_ADDuser_Click(object sender, EventArgs e)
        {
            addUser();
        }


        private void addUser()
        {
            // Verifica se um item com o mesmo nome já existe na lista
            ListViewItem existingItem = listView_Usuarios.FindItemWithText(pc_user);
            if (existingItem == null)
            {
                listView_Usuarios.Items.Add(pc_user);

                string caminhoPasta = Path.Combine(caminhoPrograma, "ARQUIVOS", pc_user);
                string configsPath = Path.Combine(caminhoPrograma, "configs");
                string userConfigPath = Path.Combine(configsPath, pc_user + ".config");

                try
                {
                    if (!Directory.Exists(caminhoPasta))
                    {
                        Directory.CreateDirectory(caminhoPasta);
                    }

                    if (!Directory.Exists(configsPath))
                    {
                        Directory.CreateDirectory(configsPath);
                    }

                    if (!File.Exists(userConfigPath))
                    {
                        File.WriteAllText(userConfigPath, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while creating the directory or configuration file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("The user is already on the list!");
            }
        }

        private void button_RemoveUser_Click(object sender, EventArgs e)
        {
            // Verifica se um item está selecionado na lista
            if (listView_Usuarios.SelectedItems.Count > 0)
            {
                // Obtém o item selecionado
                ListViewItem selectedItem = listView_Usuarios.SelectedItems[0];

                string caminhoCompleto = AppDomain.CurrentDomain.BaseDirectory;
                string nomeUnidade = Path.GetPathRoot(caminhoCompleto);
                DriveInfo driveInfo = new DriveInfo(nomeUnidade);

                string letraUnidade = driveInfo.Name;
                string rotuloVolume = driveInfo.VolumeLabel;

                string msg = "Are you sure you want to delete the user" + selectedItem.Text + " and your settings?\nThis will also delete the backup files of: " + selectedItem.Text + "\nStored in the unit: " + letraUnidade + rotuloVolume;

                // Mensagem de Aviso
                DialogResult result = MessageBox.Show(msg, "Notice!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Remove o item selecionado da lista
                    listView_Usuarios.Items.Remove(selectedItem);

                    string caminhoArquivoConfig = Path.Combine(caminhoPrograma, "configs", selectedItem.Text + ".config");
                    string caminhoArquivoBackup = Path.Combine(caminhoPrograma, "ARQUIVOS", selectedItem.Text);
                    string caminhoArquivoHashes = Path.Combine(caminhoPrograma, "hashes", selectedItem.Text);

                    // Exclui a configuração
                    if (File.Exists(caminhoArquivoConfig))
                    {
                        File.Delete(caminhoArquivoConfig);
                    }

                    // Exclui os arquivos de backup
                    if (Directory.Exists(caminhoArquivoBackup))
                    {
                        Directory.Delete(caminhoArquivoBackup, true);
                    }

                    // Exclui os arquivos de Hashes
                    if (Directory.Exists(caminhoArquivoHashes))
                    {
                        Directory.Delete(caminhoArquivoHashes, true);
                    }

                    listView_Folders.Items.Clear();
                }
            }
        }


        private async void button_ADDfolder_Click(object sender, EventArgs e)
        {
            ListViewItem existingItem = listView_Usuarios.FindItemWithText(pc_user);
            if (existingItem == null)
            {
                listView_Usuarios.Items.Add(pc_user);
            }

                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string directory = dialog.FileName;
                string status = "Desynchronized";

                // Verifica se a pasta já está na lista
                bool pastaJaExiste = false;
                foreach (ListViewItem item in listView_Folders.Items)
                {
                    if (item.Text == directory)
                    {
                        pastaJaExiste = true;
                        break;
                    }
                }

                // Adiciona a pasta à lista somente se ela ainda não estiver lá
                if (!pastaJaExiste)
                {
                    ListViewItem item = new ListViewItem(directory);
                    item.SubItems.Add(status);
                    listView_Folders.Items.Add(item);
                    atualizarConfig();
                }
                else
                {
                    MessageBox.Show("The selected folder is already in the list.");
                }

                await sincronizar();
                MessageBox.Show("Folder adds to BEKAPE!");
            }
        }


        private void atualizarConfig()
        {
            // Todas as Pastas adicionadas
            string nomesDasPastas = string.Empty;
            foreach (ListViewItem anItem in listView_Folders.Items)
            {
                nomesDasPastas += anItem.Text;
                if (anItem.Index < listView_Folders.Items.Count - 1)
                {
                    nomesDasPastas += Environment.NewLine;
                }
            }

            if (!System.IO.Directory.Exists(caminhoPrograma + "/configs/"))
            {
                System.IO.Directory.CreateDirectory(caminhoPrograma + "/configs/");
            }
            System.IO.File.WriteAllText(caminhoPrograma + "/configs/" + pc_user + ".config", nomesDasPastas);
        }



        private void button_RemoveFolder_Click(object sender, EventArgs e)
        {
            // Verifica se um item está selecionado na lista
            if (listView_Folders.SelectedItems.Count > 0)
            {
                // Exibe uma caixa de mensagem perguntando ao usuário se ele tem certeza
                DialogResult result = MessageBox.Show("Are you sure you want to remove the backup folder?\nThis will erase the backup files.", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Obtém o item selecionado
                    ListViewItem selectedItem = listView_Folders.SelectedItems[0];

                    // Remove o item selecionado da lista
                    listView_Folders.Items.Remove(selectedItem);

                    // apagar esses arquivo/pasta
                    string nomeDaPastaOriginal = Path.GetFileName(selectedItem.Text.ToString());
                    string caminhoPastaBackup = Path.Combine(caminhoPrograma, "ARQUIVOS", pc_user, nomeDaPastaOriginal + "_" + HashMD5(selectedItem.Text.ToString()));
                    string caminhoArquivoHash = Path.Combine(caminhoPrograma, "hashes", pc_user, pc_user + "_" + HashMD5(selectedItem.Text.ToString()) + ".hash");

                    // Exclui a pasta de backup
                    if (Directory.Exists(caminhoPastaBackup))
                    {
                        Directory.Delete(caminhoPastaBackup, true);
                    }
                    else
                    {
                        MessageBox.Show("Unable to locate the caminhoPastaBackup:\n" + caminhoPastaBackup);
                    }

                    // Exclui o arquivo de hash
                    if (File.Exists(caminhoArquivoHash))
                    {
                        File.Delete(caminhoArquivoHash);
                    }
                    else
                    {
                        MessageBox.Show("Unable to locate the caminhoArquivoHash:\n" + caminhoArquivoHash);
                    }
                    atualizarConfig();
                }
            }
        }

        private async void button_Synchronize_Click(object sender, EventArgs e)
        {
            await sincronizar();
        }

        private static DateTime ultimaChamada = DateTime.MinValue;
        private async Task sincronizar()
        {
            if (DateTime.Now - ultimaChamada < TimeSpan.FromSeconds(5))
            {
                MessageBox.Show("You synced less than 5 seconds ago...");
                return;
            }
            // Atualiza a hora da última chamada
            ultimaChamada = DateTime.Now;

            string caminhoConfig = Path.Combine(caminhoPrograma, "configs", pc_user + ".config");
            string conteudoArquivo;
            if (File.Exists(caminhoConfig))
            {
                conteudoArquivo = File.ReadAllText(caminhoConfig);
                // Continue com o processamento do conteúdo do arquivo
            }
            else
            {
                MessageBox.Show("The 'user.config' file does not exist,\nadd a new user and its folders first.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(conteudoArquivo))
            {
                pastasDoConfig = conteudoArquivo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (pastasDoConfig == null)
            {
                return;
            }

            foreach (string caminhoPastaOriginal in pastasDoConfig)
            {
                ListViewItem item = listView_Folders.FindItemWithText(caminhoPastaOriginal);
                if (item != null)
                {
                    if (item.SubItems[1].Text == "Analyzing" || item.SubItems[1].Text == "Desynchronized")
                    {
                        item.SubItems[1].Text = "Synchronizing...";

                        string nomeDaPastaOriginal = Path.GetFileName(caminhoPastaOriginal);
                        string caminhoPastaBackup = Path.Combine(caminhoPrograma, "ARQUIVOS", pc_user, nomeDaPastaOriginal + "_" + HashMD5(caminhoPastaOriginal));

                        await Task.Run(() =>
                        {
                            if (!Directory.Exists(caminhoPastaBackup))
                            {
                                Directory.CreateDirectory(caminhoPastaBackup);
                                CopyAllDirectory(caminhoPastaOriginal, caminhoPastaBackup);
                                SalvarHashes(caminhoPastaBackup, caminhoPastaOriginal);
                            }
                            else
                            {
                                string caminhoHash = Path.Combine(caminhoPrograma, "hashes", pc_user, pc_user + "_" + HashMD5(caminhoPastaOriginal) + ".hash");
                                string[] lines = File.ReadAllLines(caminhoHash);
                                Dictionary<string, string> backupHashes = new Dictionary<string, string>();
                                for (int i = 0; i < lines.Length; i += 2)
                                {
                                    string filePath = lines[i];
                                    string fileHash = lines[i + 1];
                                    backupHashes[filePath] = fileHash;
                                }
                                Dictionary<string, string> originalFilesWithHash = ObterArquivosComHash(caminhoPastaOriginal);

                                foreach (KeyValuePair<string, string> originalFileWithHash in originalFilesWithHash)
                                {
                                    string originalFilePath = originalFileWithHash.Key;
                                    string originalFileHash = originalFileWithHash.Value;
                                    string backupFilePath = originalFilePath.Replace(caminhoPastaOriginal, caminhoPastaBackup);

                                    if (!backupHashes.ContainsKey(backupFilePath))
                                    {
                                        string backupFileDir = Path.GetDirectoryName(backupFilePath);
                                        if (!Directory.Exists(backupFileDir))
                                        {
                                            Directory.CreateDirectory(backupFileDir);
                                        }
                                        File.Copy(originalFilePath, backupFilePath);
                                        log(originalFilePath, "NEW");
                                    }
                                    else if (backupHashes[backupFilePath] != originalFileHash)
                                    {
                                        File.Copy(originalFilePath, backupFilePath, true);
                                        log(originalFilePath, "MOD");
                                    }
                                }
                                SalvarHashes(caminhoPastaBackup, caminhoPastaOriginal);
                            }
                        });
                        item.SubItems[1].Text = "OK";
                    }
                }
            }
            if (textBox_consoleNEW.Text.EndsWith("\n"))
            {
                textBox_consoleNEW.Text = textBox_consoleNEW.Text.Remove(textBox_consoleNEW.Text.Length - 2);
            }

            if (textBox_consoleMOD.Text.EndsWith("\n"))
            {
                textBox_consoleMOD.Text = textBox_consoleMOD.Text.Remove(textBox_consoleMOD.Text.Length - 2);
            }

            label_NEW.Text = "NEW files: " + textBox_consoleNEW.Lines.Length;
            label_MOD.Text = "MODIFIED files: " + textBox_consoleMOD.Lines.Length;

            MessageBox.Show("All files have been synced!", "Backup Complete!");
        }

        private void SalvarHashes(string caminhoPastaBackup, string caminhoPastaOriginal)
        {
            string hashDasPastas = string.Empty;
            Dictionary<string, string> arquivosComHash2 = ObterArquivosComHash(caminhoPastaBackup);
            foreach (KeyValuePair<string, string> arquivoComHash in arquivosComHash2)
            {
                hashDasPastas += arquivoComHash.Key + Environment.NewLine + arquivoComHash.Value + Environment.NewLine;
            }
            string caminhoDiretorioHashes = Path.Combine(caminhoPrograma, "hashes", pc_user);
            if (!Directory.Exists(caminhoDiretorioHashes))
            {
                Directory.CreateDirectory(caminhoDiretorioHashes);
            }
            string caminhoArquivoHash = Path.Combine(caminhoDiretorioHashes, pc_user + "_" + HashMD5(caminhoPastaOriginal) + ".hash");
            File.WriteAllText(caminhoArquivoHash, hashDasPastas);
        }

        private void log(string message, string type)
        {
            if (type == "NEW")
            {
                textBox_consoleNEW.Invoke(new Action(() =>
                {
                    textBox_consoleNEW.Text += message + Environment.NewLine;
                }));
            }

            if (type == "MOD")
            {
                textBox_consoleMOD.Invoke(new Action(() =>
                {
                    textBox_consoleMOD.Text += message + Environment.NewLine;
                }));
            }
        }

        void CopyAllDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            Parallel.ForEach(Directory.GetFiles(sourceDir), file =>
            {
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));
            });

            foreach (var directory in Directory.GetDirectories(sourceDir))
                CopyAllDirectory(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        Dictionary<string, string> ObterArquivosComHash(string caminhoPasta)
        {
            Dictionary<string, string> arquivosComHash = new Dictionary<string, string>();
            foreach (string filePath in Directory.GetFiles(caminhoPasta, "*.*", SearchOption.AllDirectories))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        byte[] hashBytes = md5.ComputeHash(stream);
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hashBytes.Length; i++)
                        {
                            sb.Append(hashBytes[i].ToString("X2"));
                        }
                        string hash = sb.ToString();
                        arquivosComHash[filePath] = hash;
                    }
                }
            }
            return arquivosComHash;
        }

        private string HashMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private async void listView_Usuarios_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //await Task.Delay(5000);

            string caminhoArquivo = System.IO.Path.Combine(caminhoPrograma + @"configs\", pc_user + ".config");

            if (listView_Usuarios.SelectedItems.Count > 0)
            {
                listView_Folders.Items.Clear();            // Limpa a lista de pastas
                ListViewItem selectedItem = listView_Usuarios.SelectedItems[0];

                if (selectedItem.Text == pc_user)
                {
                    //Lê o conteúdo do arquivo e armazena em uma variável
                    string conteudoArquivo = System.IO.File.ReadAllText(caminhoArquivo);

                    // Verifica se o conteúdo do arquivo não está em branco
                    if (!string.IsNullOrWhiteSpace(conteudoArquivo))
                    {
                        pastasDoConfig = conteudoArquivo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string linha in pastasDoConfig)
                        {
                            string status = "Analyzing";

                            ListViewItem item = new ListViewItem(linha);
                            item.SubItems.Add(status);
                            listView_Folders.Items.Add(item);

                            // sincronizar estava aqui dentro, removi ele pra baixo para executar depois que a lista de pasta estiver completa
                        }
                        await sincronizar();
                        AtualizarEspacoLivre();
                    }
                }
                else
                {
                    MessageBox.Show("You cannot change the settings of another 'user'");
                }
            }
        }

        private void button_console_Click(object sender, EventArgs e)
        {
            visualizar_console();
        }
        bool console = false;
        private void visualizar_console()
        {
            if (console == true)
            {
                console = false;
                this.Size = new Size(1000, 299);     //Altera o tamanho do form
                this.CenterToScreen();              // Centraliza o form
                button_console.Text = "Log ▼";
            }
            else
            {
                console = true;
                this.Size = new Size(1000, 714);     //Altera o tamanho do form
                this.CenterToScreen();              // Centraliza o form
                button_console.Text = "Log ▲";
            }
        }

        private void linkLabel_sobre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DanielSvoboda/Bekape");
        }

        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            var url_versao = "https://raw.githubusercontent.com/DanielSvoboda/Bekape/main/versao.txt";
            var url_exe = "https://github.com/DanielSvoboda/Bekape/raw/main/Bekape.exe";

            WebRequest solicitacao = HttpWebRequest.Create(url_versao);
            WebResponse resposta = solicitacao.GetResponse();
            StreamReader sr = new StreamReader(resposta.GetResponseStream());
            string versaoNoGit = sr.ReadToEnd();

            if (versaoNoGit != versaoAtual)
            {
                DialogResult dialogResult = MessageBox.Show("There is an update! \n\n" +
                    "Current version: " + versaoAtual + "\n" +
                     "New version: " + versaoNoGit + "\n\n" +
                    "Shall we update now?", "Update Available", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    var dir = Directory.GetCurrentDirectory();
                    var nome_antigo = Process.GetCurrentProcess().ProcessName;
                    var nome_novo = Directory.GetCurrentDirectory() + @"\" + nome_antigo + "_V" + versaoNoGit + ".exe";

                    var wClient = new WebClient();
                    wClient.DownloadFile(url_exe, nome_novo);

                    MessageBox.Show("Download complete!");

                    // Fecha o programa, espera 2 segundos, apaga o arquivo, renomeia com o nome antigo e com o numero da versão atual e abre ele     :)
                    Process.Start(new ProcessStartInfo()
                    {
                        Arguments = "/C choice /C Y /N /D Y /T 2 & Del \"" + dir + "\\" + nome_antigo + ".exe\" & start \"\" \"" + nome_novo + "\"",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        FileName = "cmd.exe"
                    }); ;

                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("There are no updates available  ;)");
            }
        }

        private void linkLabel_Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DanielSvoboda/Bekape");
        }
    }
}