# Umbraco on Docker on a Pi

## These are the steps to run Umbraco on a Raspberry Pi

Minimum Requirements :

- At least 1 Raspberry Pi 3b+
- Warning : this WILL be slow on a Pi 3b+

Recommended Requirements : 

- A Raspberry Pi 4 8gb 
- an SSD drive to boot from

## Setup Instructions

Full setup for instructions to follow to set-up a Raspberry Pi for Umbraco on Docker can be found [here](instructions/setup-pi.md).


## Additional Hardware

The RGL LED array is based on the Pimoroni Unicorn Hat HD.

- You can buy one at [https://shop.pimoroni.com/products/unicorn-hat-hd](https://shop.pimoroni.com/products/unicorn-hat-hd).
- There's a Git Repository associated with this project [https://github.com/pimoroni/unicorn-hat-hd](https://github.com/pimoroni/unicorn-hat-hd) with a lot of cool samples.

# Set up Umbraco

## Set up SQL Azure container

Run the following.

    docker run -d --restart always --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SQL_password123' -v mssqlsystem:/var/opt/mssql -v mssqluser:/var/opt/sqlserver -p 1433:1433 mcr.microsoft.com/azure-sql-edge

You will need to connect to your SQL server and create a database. I suggest using the excellent LinqPad tool, which can be downloaded [here](https://www.linqpad.net/).

To create a new database you just need to connect to the server and run the following SQL

    CREATE DATABASE umbracoDb;

## Set up RabbitMQ

Run the following.

    docker pull rabbitmq:3-management

This will map port 15672 for the management web app and port 5672 for the message broker.

    docker run -it -d --restart always -p 15672:15672 -p 5672:5672 rabbitmq:3-management

You can test the management web app by going to http://[Pi.IP.Add.ress]:15672 in your browser. You can also test the message broker by going to http://[Pi.IP.Add.ress]:5672 in your browser. Pi.IP.Add.ress is the IP address of your Pi.

The default username is "guest" and the default password is "guest".

## Set up the Umbraco Site

### Create solution/project
    dotnet new globaljson --sdk-version 5.0.404
    dotnet new sln --name UmbDockPi

### Ensure we have the latest Umbraco templates
    dotnet new -i Umbraco.Templates

### Add the Umbraco Project

    dotnet new umbraco -n UmbDockPi --friendly-name "Admin User" --email "admin@admin.com" --password "Pa55word!!" --connection-string "Server=[ServerIpAddress];Database=umbracoDb;User Id=sa;Password=SQL_password123;"
    dotnet sln add UmbDockPi
    dotnet add UmbDockPi package Portfolio
    dotnet add UmbDockPi package Newtonsoft.Json

### Modify csProj

Edit the csproj file to change following element:

    <!-- Force windows to use ICU. Otherwise Windows 10 2019H1+ will do it, but older windows 10 and most if not all winodws servers will run NLS -->
    <ItemGroup Condition="'$(OS)' == 'Windows_NT'">
        <PackageReference Include="Microsoft.ICU.ICU4C.Runtime" Version="68.2.0.9" />
        <RuntimeHostConfigurationOption Include="System.Globalization.AppLocalIcu" Value="68.2" />
    </ItemGroup>

Without this step, the project won't compile on Linux, but will compile in windows.

### Run the site

    dotnet run --project UmbDockPi

## RabbitMQ Tester

The console apps rmqRx and rmqSend can be used to send and receive test messages.

## References

### Raspberry Pi Setup
- https://pumpingco.de/blog/setup-your-raspberry-pi-for-docker-and-docker-compose/
- https://dev.to/elalemanyo/how-to-install-docker-and-docker-compose-on-raspberry-pi-1mo
- https://dev.to/rohansawant/installing-docker-and-docker-compose-on-the-raspberry-pi-in-5-simple-steps-3mgl
- https://cultiv.nl/blog/running-umbraco-9-on-your-raspberry-pi/

### Umbraco
- https://github.com/prjseal/Clean-Starter-Kit-for-Umbraco-v9
