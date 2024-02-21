# AbestoMediaToolKit

Utilities to alter media.

## Technologies Used

- .NET Core 8.0
- Abesto.MediaToolKit.API (ASP.NET Core 8.0)
- Abesto.MediaToolKit.Functions (Azure Functions)

## Important Packages

- SixLabors.ImageSharp
- Serilog
- Microsoft.Azure.Functions
- Refit

## Setup

1. Clone the repository.
2. Open the solution file (`AbestoMediaToolKit.sln`) in Visual Studio.
3. Set `Abesto.MediaToolKit.Functions` as the starting project.
4. Open the `local.settings.json` file and add the following configurations:

    ```json
    {
        "Values": {
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
            "AWS_Accesskey": "YOUR_ACCESS_KEY",
            "AWS_Secret_Key": "YOUR_SECRET_KEY",
            "ContainerName": "YOUR_CONTAINER_NAME",
            "ResourceLocationType": 1, // 1 for AWS
            "ImageLocalFilePath": "C:\\Abesto\\ImagePath", // YOUR FOLDER PATH FOR IMAGES
            "maxFilesPerAPI": 20 // MAXIMUM FILES PER REQUEST TO CLOUD SERVER
        }
    }
    ```

5. Run the project.

## Usage

Use Postman to execute the program.

**HTTP POST URL:**


http://localhost:7229/api/process-image


```
{
    "FileSuffix": "_thumb",
    "CanCrop": true,
    "MaintainAspectRatio": true,
    "ImageWidth": 600,
    "ImageHeight": 600
}
```

Execute the request.

## Note
You can change the output log file location from default to the same as ImageLocalFilePath 
from the Program.cs file instead of Directory.GetCurrentDirectory().
Otherwise, the logfile will be created inside "~AbestoMediaToolKit\Abesto.MediaToolKit.Functions\bin\Debug\net8.0" 
with the name logsYYYYMMDD.txt format.


There are many TODOs which are the feature or things that are skipped for future works
Also the project Abesto.MediaToolKit.Functions can be run and it has a UI with the same options of the JSON provided in the API and on submitting the request it will call the function.
Note: Make sure both the projects are running.