# InsuranceCompany
Insurance Company Net Core

API for an insurance company

As an insurance company we've been asked to develop an application that manages some information about our insurance policies and company clients

I made a Rest API Backend with five endpoints 
- Login
- GetClientById
- GetClientByUserName
- GetPoliciesByUserName
- GetClientByPolicyNumber

This project was made using
* .Net Core 3.1
* JwtBearer Package for authentication
* IdentityModel Tokens and IdentityModel.Tokens.Jwt for generating the Auth token
* DependencyInjection 
* Newtonsoft.Json


## Download the Project
`git clone https://github.com/lauraborgato/InsuranceCompany.git`

### If you are on Windows 
- Install visual studio 
- Open the project on visual studio
- Restore NugetPackages
- Run the project

### If you are on Linux
- Install .Net Core Sdk from the oficial page https://dotnet.microsoft.com/download
- Open a terminal
- `cd InsuranceCompany`
- `dotnet restore`
- `dotnet build && dotnetrun`

### APIs

Using PostMan or a similar program 

#### Login with Email method: POST url: http://localhost:5000/auth/login body: { [email:srtring] }

`Expected Response "token"`

#### Get user data filtered by user id method: GET url: http://localhost:5000/client/{id} header: Authorization Bearer Token

#### Get user data filtered by user name method: GET url: http://localhost:5000/client/name/{userName} header: Authorization Bearer Token

#### Get the list of policies linked to a user name method: GET url: http://localhost:5000/policy/{userName} header: Authorization Bearer Token

#### Get the user linked to a policy number method: GET url: http://localhost:5000/policy/client/{policyNumber} header: Authorization Bearer Token

### Testing
In the folder InsuranceCompany.Test run the command `dotnet test` or if you are using visual studio go to "execute tests"
