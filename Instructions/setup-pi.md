# Instructions to Setup Raspberry Pi

To be completed! This guide is a WIP.

## Steps

1. Install Docker

    curl -sSL https://get.docker.com | sh

2. Add permission to Pi User to run Docker Commands

    sudo usermod -aG docker pi

Reboot here or run the next commands with a sudo

3. Test Docker installation

    docker run hello-world

4. IMPORTANT! Install proper dependencies

    sudo apt-get install -y libffi-dev libssl-dev

    sudo apt-get install -y python3 python3-pip

    sudo apt-get remove python-configparser

5. Install Docker Compose

This step takes a little while.

    sudo pip3 -v install docker-compose

6. Enable the Docker system service to start your containers on boot

    sudo systemctl enable docker

Boom! 🔥 It's done!


## References : 

- https://pumpingco.de/blog/setup-your-raspberry-pi-for-docker-and-docker-compose/
- https://dev.to/elalemanyo/how-to-install-docker-and-docker-compose-on-raspberry-pi-1mo
- https://dev.to/rohansawant/installing-docker-and-docker-compose-on-the-raspberry-pi-in-5-simple-steps-3mgl
- https://cultiv.nl/blog/running-umbraco-9-on-your-raspberry-pi/