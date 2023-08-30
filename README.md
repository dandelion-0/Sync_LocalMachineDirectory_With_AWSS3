# Sync_LocalMachineDirectory_With_AWSS3

Sync Local Machine Directory with AWS S3 is a Windows service application that allows you to synchronize a local directory with an S3 bucket at regular intervals. This project provides a convenient way to automate the process of backing up your local files to the cloud.

## Usage

1. After installing the Windows service, it will run in the background automatically.
2. The synchronization process will be automatically triggered based on the configured interval.
3. Logs of the synchronization process will be stored in the base directory in `*.txt` file format, named with the current date and time.

## Installation

1. Clone the repository: `git clone https://github.com/dandelion-0/Sync_LocalMachineDirectory_With_AWSS3.git`
2. Open the solution file in Visual Studio.
3. Build the solution to generate the executable file.

### Configuration

To configure the application, you need to modify the `app.config` file in the project.

### AWS Credentials

You need to provide your AWS access key and secret key in the configuration file. Locate the following lines and replace the placeholders with your actual keys:

xml
<add key="S3AccessKey" value="YOUR_AWS_ACCESS_KEY" />
<add key="S3SecretKey" value="YOUR_AWS_SECRET_KEY" />

### S3 Bucket

Specify the S3 bucket where you want to sync your local directory. Update the following line with your bucket name:

xml
<add key="S3BuckeyName" value="YOUR_S3_BUCKET_NAME" />

### Sync Interval

Set the interval at which the synchronization process should run. The interval is specified in minutes. Update the following line with your desired interval:

```xml
<add key="_timeinterval" value="Time_in_Minute_example_30" />

### Local Directory

Specify the local directory that you want to sync with the S3 bucket. Update the following line with the path to your local directory:

```xml
<add key="Folder_path" value="PATH_TO_LOCAL_DIRECTORY" />

## Application Installation
1. Navigate to the appropriate .NET framework directory for your machine. For 64-bit machines, use the following command:

cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319

2. To install the Windows service, follow these steps:
   - Open a command prompt with administrative privileges.
   - Navigate to the location of the built executable file.
   - Run the following command: `installutil.exe Sync_LocalMachineDirectory_With_AWSS3.exe

3. To Verify service is installed
   - Open the Services Manager and locate the service named "Sync_LocalMachineDirectory_With_AWSS3".
   - Status Type = Automatic
   

## Application Uninstallation
1. Navigate to the appropriate .NET framework directory for your machine. For 64-bit machines, use the following command:

cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319

2. To uninstall the Windows service, follow these steps:
   - Open a command prompt with administrative privileges.
   - Navigate to the location of the built executable file.
   - Run the following command: `installutil.exe -u Sync_LocalMachineDirectory_With_AWSS3.exe`## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

This project is based on the [AWS SDK for .NET](https://aws.amazon.com/sdk-for-net/), which provides the necessary functionality to interact with AWS services.

## Contact

For any questions or inquiries, please contact the project maintainer: (dandelio00@tutanota.com)

Thank you for using Sync Local Machine Directory with AWS S3!
