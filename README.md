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

- You can buy one [here](- You can buy one [here](https://shop.pimoroni.com/products/unicorn-hat-hd).
- There's a Git Repository associated with this project [here](https://github.com/pimoroni/unicorn-hat-hd).


# Set up Umbraco

## Set up SQL Azure container

Run the following.

    docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SQL_password123' -v mssqlsystem:/var/opt/mssql -v mssqluser:/var/opt/sqlserver -p 1433:1433 mcr.microsoft.com/azure-sql-edge

You will need to connect to your SQL server and create a database. I suggest using the excellent LinqPad tool, which can be downloaded [here](https://www.linqpad.net/).

To create a new database you just need to connect to the server and run the following SQL

    CREATE DATABASE umbracoDb;

## Set up RabbitMQ

Run the following.

    docker pull rabbitmq:3-management

This will map port 15672 for the management web app and port 5672 for the message broker.

    docker run --rm -it -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management

