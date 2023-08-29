using System;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using Amazon.S3;
using System.Configuration;
using Amazon.S3.Transfer;

namespace Sync_LocalMachineFolder_With_AWSS3
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)
        public Service1()
        {
            InitializeComponent();
        }

        private async void SyncS3()
        {
            try
            {
                // Retrieve Credentials, Directoryname and BUcketName from AppSetting
                string _S3AccessKey = ConfigurationManager.AppSettings["S3AccessKey"];
                string _S3SecretKey = ConfigurationManager.AppSettings["S3SecretKey"];
                string localFolderPath = ConfigurationManager.AppSettings["Folder_path"];
                string bucketName = ConfigurationManager.AppSettings["S3BuckeyName"]; 


                // Creating Credentials
                // *Note* Change Region Name based on Region Configured on AWS Account 
                AmazonS3Client s3Client = new AmazonS3Client(_S3AccessKey, _S3SecretKey, Amazon.RegionEndpoint.APSouth1);
                TransferUtility transferUtility = new TransferUtility(s3Client);

                //string s3Prefix = "optional-prefix";

                TransferUtilityUploadDirectoryRequest uploadRequest = new TransferUtilityUploadDirectoryRequest
                {
                    BucketName = bucketName,
                    Directory = localFolderPath,
                    SearchOption = SearchOption.AllDirectories,
                    SearchPattern = "*", // Upload all files
                    //KeyPrefix = s3Prefix
                };

                await transferUtility.UploadDirectoryAsync(uploadRequest);
            }
            catch (AmazonS3Exception ex)
            {
                WriteToFile(ex.Message + "  -  " + DateTime.Now);
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            // _timeinterval by default set to 30 minute // can be replace from config file
            timer.Interval = Int32.Parse(ConfigurationManager.AppSettings["_timeintervl"]) * 60000;                        
            timer.Enabled = true;

            //System.Diagnostics.Debugger.Launch();

            try
            {
                SyncS3();
            }
            catch (Exception ex )
            {
                WriteToFile(ex.Message + "  -  " + DateTime.Now);
            }
        }
        protected override void OnShutdown()
        {
            try
            {
                SyncS3();
            }
            catch (Exception ex)
            {
                WriteToFile("----------------Exception envoke while service shutdown at " + DateTime.Now + "  -  " + ex.Message.ToString());
            }
            WriteToFile("Service is stopped while Shutdown at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                SyncS3();
            }
            catch (Exception ex)
            {
                WriteToFile("----------------Exception envoke at " + DateTime.Now + "  -  " + ex.Message.ToString());
            }
            WriteToFile("Service is recall at " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
