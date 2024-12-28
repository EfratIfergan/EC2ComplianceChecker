# EC2 Compliance Checker 🔍🛡️

## Project Description  
This project is designed to collect data from AWS resources, check the compliance status of Amazon EC2 instances managed by Systems Manager (SSM), and evaluate whether they meet the recommendation:  
**"[SSM.3] Amazon EC2 instances managed by Systems Manager should have an association compliance status of COMPLIANT."**  

## What Does the Project Include?  
1. **Data Collection**: Uses AWS SDK to gather the required information.  
2. **Data Analysis**: Evaluates whether the resources comply with the recommendation based on the retrieved compliance data.  
3. **Status Report**: Generates a list of resources that do not meet the recommendation.  

## How to Run the Project  
1. **Prepare the Environment**:  
   - Ensure that .NET Framework or .NET Core is installed on your development environment.  
   - Set up AWS credentials using the AWS CLI or an appropriate configuration file.  

2. **Run the Application**:  
   - Open the project file (`.sln`) in Visual Studio.  
   - Set the main application file as the "Startup Project".  
   - Click `Run` or press `F5` to launch the application window.  

## Author  
This project was developed as part of a task to analyze AWS resource compliance.  
Good luck! 🤗
