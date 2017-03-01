# PayFast [![Build status](https://ci.appveyor.com/api/projects/status/0m01brynop7cxjj1/branch/master?svg=true)](https://ci.appveyor.com/project/e3PTeam/payfast/branch/master)

This is a .Net library facilitates working with the PayFast api.
It provides classes for both the request and notify urls.
The classes make it simple to perform both creating the url for the comfirm page,
also known as the GET string. The other class is for the return variables for the itn process.
It supports using the passphrase system. It also handles generating the signature for requests.

# Supported Request Types

* Once Off Payment
* Recurring Billing
* AdHoc Agreements

# Support Post Type

* ITN Callback post from PayFast

# Packages

## Nuget Feed

[Nuget Link](https://www.nuget.org/packages/PayFast/)

> Install-Package PayFast -Pre

The library is currently undergoing a quality control process, once it passes this process
the pre-release flag will be removed.

## MyGet Feed

A public MyGet feed exists where the latest builds can be downloaded.
In order to use this, you will need to add this feed to Visual Studio.

Package Name: 

> PayFast

### Visual Studio 2015 url

> https://www.myget.org/F/payfast/api/v3/index.json

### Visual Studio 2012+ url

> https://www.myget.org/F/payfast/api/v2

### Adding Custom Feeds

For a how to guide click [This Link](https://docs.microsoft.com/en-us/nuget/hosting-packages/overview)

# Samples

Samples can be found in the samples directory on disk, or in the samples solution folder in VS.

## PayFast.Web.Core

This is a ASP.NET Core application. In order to run the sample, use something like ngrok.
With a command like ngrok http 42817. To instruct IISExpress to accept the connection, you will 
need to mody the applicationhost.config file. you are looking for a value that looks like this:

> ```<binding protocol="http" bindingInformation="*:42817:localhost" />```

You would then change it to look like this:

> ```<binding protocol="http" bindingInformation="*:42817:*" />```

Sometimes visual studio has problems starting when the above change is made, try keeping it to the first one,
start the project, then close stop debugging and then make the change and then try again.

Make sure to look at appsettings.json, that is where you will configure all your PayFast related settings,
which will allow the sample to work correctly.
